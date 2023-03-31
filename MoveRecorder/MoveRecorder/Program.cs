// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Drawing.Imaging;
using MoveRecorder;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets.DualShock4;
using Point = System.Drawing.Point;

var client = new ViGEmClient();
var ds4 = client.CreateDualShock4Controller();
ds4.Connect();

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

Console.WriteLine("Connecting controller");


Console.WriteLine("Please select the controller");
Console.ReadLine();
var frames = new List<Bitmap>();
Thread.Sleep(1000);
while (true)
{

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

	ds4.SetButtonState(DualShock4Button.Circle, true);
	Thread.Sleep(200);
	ds4.SetButtonState(DualShock4Button.Circle, false);
	Thread.Sleep(200);
	if (frameCounter > 15)
	{
		break;
	}
}

Console.WriteLine("Saving as GIF");
new GifCreator().Create($"{folder}/{move}.gif", frames);
Console.WriteLine("Saved");