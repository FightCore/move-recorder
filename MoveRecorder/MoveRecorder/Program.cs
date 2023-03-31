// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Drawing.Imaging;
using MoveRecorder;
using Point = System.Drawing.Point;

Console.WriteLine("Hello, World!");

var baseFolder = "C:/tmp/";
var screens = Screen.AllScreens;
var i = 0;
foreach (var screen in screens)
{
	Console.WriteLine($"{i}: {screen.DeviceName} {screen.Bounds} {screen.Primary}");
	i++;
}
Console.WriteLine("Choose an device");
var decision = Console.ReadLine();
var screenToUse = screens[Convert.ToInt32(decision)];

Console.WriteLine("What is the normalized name of the character to process?");
var character = Console.ReadLine();



Console.WriteLine("What is the normalized name of the move to record");
var move = Console.ReadLine();

var frameCounter = 0;

var folder = $"{baseFolder}{character}/{move}/";

Directory.CreateDirectory(folder);

var frames = new List<Bitmap>();
while (true)
{
	var line = Console.ReadLine();
	if (!string.IsNullOrWhiteSpace(line))
	{
		break;
	}

	var fileName = $"{folder}{frameCounter:D3}.png";
	var bitmap = new Bitmap(screenToUse.Bounds.Width, screenToUse.Bounds.Height);
	using (var g = Graphics.FromImage(bitmap))
	{
		g.CopyFromScreen(new Point(screenToUse.Bounds.Left, screenToUse.Bounds.Top), Point.Empty, screenToUse.Bounds.Size);
	}
	bitmap.Save(fileName, ImageFormat.Png);

	frames.Add(bitmap);
	Console.WriteLine($"Taken screenshot of frame {frameCounter}");
	frameCounter++;
}

Console.WriteLine("Saving as GIF");
new GifCreator().Create($"{folder}.gif", frames, screenToUse);
Console.WriteLine("Saved");