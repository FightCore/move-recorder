using System.Drawing.Imaging;
using MoveRecorder;
using MoveRecorder.Data;
using Nefarius.ViGEm.Client;
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



Console.WriteLine("Connecting controller");

var client = new ViGEmClient();
var ds4 = client.CreateDualShock4Controller();
ds4.Connect();

var inputs = new GameCubeInputs(ds4);

Console.WriteLine("Please setup the move within Dolphin, ensure the first frame of the move is visible and the game is paused using debug mode.");
Console.WriteLine("Please configure the controller in Dolphin");
Console.ReadLine();
inputs.Reset();


foreach (var character in Characters.AllCharacters)
{
	inputs.Press(GameCubeButton.LoadSaveState1, false);
	Thread.Sleep(200);
	inputs.Hold(GameCubeButton.B);
	Thread.Sleep(20);
	inputs.Release(GameCubeButton.B);

	foreach (var operation in character.SelectMovement)
	{
		switch (operation)
		{
			case CharacterSelectMovement.Down:
				inputs.Hold(GameCubeButton.Down.Index, GameCubeButton.Down.Value);
				Thread.Sleep(100);
				inputs.Release(GameCubeButton.Down.Index);
				break;
			case CharacterSelectMovement.Right:
				inputs.Hold(GameCubeButton.Right.Index, GameCubeButton.Right.Value);
				Thread.Sleep(90);
				inputs.Release(GameCubeButton.Right.Index);
				break;
			case CharacterSelectMovement.Left:
				inputs.Hold(GameCubeButton.Left.Index, GameCubeButton.Left.Value);
				Thread.Sleep(90);
				inputs.Release(GameCubeButton.Left.Index);
				break;

		}
	}
	inputs.Hold(GameCubeButton.A);
	Thread.Sleep(20);
	inputs.Release(GameCubeButton.A);
	Console.WriteLine(character.Name);
	inputs.Hold(GameCubeButton.Start);
	Thread.Sleep(2000);
	inputs.Release(GameCubeButton.Start);
	Thread.Sleep(2000);

	inputs.FastPress(GameCubeButton.DpadDown);
	Thread.Sleep(1000);

	foreach (var operation in new List<CharacterSelectMovement>()
	         {
		CharacterSelectMovement.Up, CharacterSelectMovement.Up, CharacterSelectMovement.Up,
		CharacterSelectMovement.Left, CharacterSelectMovement.Left
			 })
	{
		switch (operation)
		{
			case CharacterSelectMovement.Up:
				inputs.Hold(GameCubeButton.Up.Index, GameCubeButton.Up.Value);
				Thread.Sleep(100);
				inputs.Release(GameCubeButton.Up.Index);
				break;
			case CharacterSelectMovement.Left:
				inputs.Hold(GameCubeButton.Left.Index, GameCubeButton.Left.Value);
				Thread.Sleep(100);
				inputs.Release(GameCubeButton.Left.Index);
				break;

		}
	}

	inputs.FastPress(GameCubeButton.A, false);
	Thread.Sleep(5000);
	inputs.FastPress(GameCubeButton.Start, false);
	// Enable hitboxes
	inputs.Hold(GameCubeButton.R);
	for (var hudIterator = 0; hudIterator < 2; hudIterator++)
	{
		inputs.FastPress(GameCubeButton.DpadRight);
		Thread.Sleep(50);

	}
	inputs.Release(GameCubeButton.R);
	inputs.Hold(GameCubeButton.X);
	// Disable hud and background
	for (var hudIterator = 0; hudIterator < 7; hudIterator++)
	{
		inputs.FastPress(GameCubeButton.DpadDown);
		Thread.Sleep(50);

	}
	// Camera select
	for (var hudIterator = 0; hudIterator < 2; hudIterator++)
	{
		inputs.FastPress(GameCubeButton.DpadLeft);
		Thread.Sleep(50);

	}
	inputs.Release(GameCubeButton.X);
	inputs.Hold(GameCubeButton.B);
	inputs.Hold(GameCubeButton.DpadLeft);
	inputs.Hold(GameCubeButton.CStickDown.Index, GameCubeButton.CStickDown.Value);
	Thread.Sleep(300);
	inputs.Release(GameCubeButton.CStickDown.Index);
	inputs.ReleaseDPad();
	inputs.Release(GameCubeButton.B);
	inputs.FastPress(GameCubeButton.Start, false);
	Thread.Sleep(50);
	inputs.Hold(GameCubeButton.Down.Index, GameCubeButton.Down.Value);
	inputs.Press(GameCubeButton.R, false);
	inputs.Release(GameCubeButton.Down.Index);
	inputs.Reset();
	Thread.Sleep(10000);
	inputs.Press(GameCubeButton.Start, false);
	Thread.Sleep(100);
	inputs.Press(GameCubeButton.SaveSaveState2, false);
	Thread.Sleep(100);


	Console.WriteLine("Done with setup");
	var moves = new CsvLoader().Load($@"C://tmp/exports/{character.Name}/moves.csv");

	var moveExecutor = new MoveExecutor(inputs);
	foreach (var move in moveExecutor.AvailableMoves)
	{
		inputs.Reset();
		var moveData = moves.FirstOrDefault(storedMove => storedMove.NormalizedName == move);
		if (moveData == null)
		{
			Console.WriteLine($"Could not find move {move}");
			continue;
		}

		if (moveData.TotalFrames == null || moveData.TotalFrames == 0)
		{
			Console.WriteLine($"Frames unknown for move {move}");
			continue;
		}

		Console.WriteLine($"Recording {move}");


		var folder = $"{baseFolder}{character.Name}/{move}/";

		Directory.CreateDirectory(folder);

		if (Directory.GetFiles(folder).Any())
		{
			Console.WriteLine("Skipping move as its already there");
			continue;
		}

		Console.WriteLine("Done setting up record environment");

		Console.WriteLine("Loading save state");
		inputs.Press(GameCubeButton.SaveSaveState2);
		Thread.Sleep(2000);
		Console.WriteLine("Executing move");
		moveExecutor.Execute(move);

		var frames = new List<Bitmap>();

		// Frame count starts at 1 to respect human counting.
		var frameCounter = 1;
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

			if (frameCounter >= moveData.TotalFrames)
			{
				break;
			}
		}

		Console.WriteLine("Saving as GIF");
		new GifCreator().Create($"{folder}/{move}.gif", frames);
		foreach (var frame in frames)
		{
			frame.Dispose();
		}
		Console.WriteLine("Saved");
	}
}


