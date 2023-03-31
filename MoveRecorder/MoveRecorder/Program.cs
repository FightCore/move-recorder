using System.Drawing.Imaging;
using MoveRecorder;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets.DualShock4;
using Point = System.Drawing.Point;



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

Console.WriteLine("What is the normalized name of the move to record?");
var move = Console.ReadLine();

var frameCounter = 0;

Console.WriteLine("How many frames is this move?");

var totalFrames = Convert.ToInt32(Console.ReadLine());

var folder = $"{baseFolder}{character}/{move}/";

Directory.CreateDirectory(folder);

Console.WriteLine("Connecting controller");

var client = new ViGEmClient();
var ds4 = client.CreateDualShock4Controller();
ds4.Connect();

var inputs = new GameCubeInputs(ds4);

Console.WriteLine("Please setup the move within Dolphin, ensure the first frame of the move is visible and the game is paused using debug mode.");
Console.WriteLine("Please configure the controller in Dolphin");
Console.ReadLine();
new RecordEnvironment(inputs).Setup();
Console.WriteLine("Done setting up record environment");

inputs.Press(GameCubeButton.Start);
new MoveExecutor(inputs).Execute(move);

var frames = new List<Bitmap>();
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

	inputs.Press(GameCubeButton.Z);
	Thread.Sleep(200);

	if (frameCounter > totalFrames)
	{
		break;
	}
}

Console.WriteLine("Saving as GIF");
new GifCreator().Create($"{folder}/{move}.gif", frames);
Console.WriteLine("Saved");