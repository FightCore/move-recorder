using System.Drawing.Imaging;
using System.Security.Cryptography.Pkcs;
using IronOcr;
using MoveRecorder;
using MoveRecorder.Data;
using MoveRecorder.Moves.Abstraction;
using Nefarius.ViGEm.Client;
using Point = System.Drawing.Point;

var baseFolder = "C:/tmp/recorder/";
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

var characterSelectMover = new CharacterSelectMover(inputs);
inputs.Reset();
Console.WriteLine("Please setup the move within Dolphin, ensure the first frame of the move is visible and the game is paused using debug mode.");
Console.WriteLine("Please configure the controller in Dolphin");
Console.ReadLine();
inputs.Reset();

//while (true)
//{
//	var key = Console.ReadLine();
//	switch (key)
//	{
//		case "a":
//			inputs.Move(GameCubeButton.Left.Index, GameCubeButton.Left.Value);
//			break;
//		case "w":
//			inputs.Move(GameCubeButton.Up.Index, GameCubeButton.Up.Value);
//			break;
//		case "d":
//			inputs.Move(GameCubeButton.Right.Index, GameCubeButton.Right.Value);
//			break;
//		case "s":
//			inputs.Move(GameCubeButton.Down.Index, GameCubeButton.Down.Value);
//			break;
//	}
//	Thread.Sleep(100);
//}


foreach (var character in Characters.AllCharacters)
{
	var moves = new CsvLoader().Load($"C://tmp/exports/{character.Name}/moves.csv");
	var moveExecutor = new MoveExecutor(inputs);

	var movesToExecute = moveExecutor.AvailableMoves.ToList();
	movesToExecute = movesToExecute.Where(moveToExecute =>
	{
		var moveData = moves.Find(move => move.NormalizedName == moveToExecute);
		if (moveData?.TotalFrames is null or 0)
		{
			return false;
		}

		var folder = $"{baseFolder}{character.Name}/{moveToExecute}/";

		if (!Directory.Exists(folder))
		{
			return true;
		}

		return !Directory.GetFiles(folder).Any();
	}).ToList();

	if (movesToExecute.Count == 0)
	{
		Console.WriteLine("no moves to perform, skipping");
		continue;
	}

	inputs.Press(GameCubeButton.LoadSaveState1, false);
	Thread.Sleep(200);
	inputs.Hold(GameCubeButton.B);
	Thread.Sleep(20);
	inputs.Release(GameCubeButton.B);

	characterSelectMover.Execute(character.SelectMovement);

	inputs.Hold(GameCubeButton.A);
	Thread.Sleep(20);
	inputs.Release(GameCubeButton.A);
	Console.WriteLine(character.Name);
	inputs.Hold(GameCubeButton.Start);
	Thread.Sleep(2000);
	inputs.Release(GameCubeButton.Start);
	Thread.Sleep(2000);

	// Was previously used to toggle 20xx pages, now this is done when creating the savestate
	//inputs.FastPress(GameCubeButton.DpadDown);
	//Thread.Sleep(500);

	// Select the stage using preset moves.
	characterSelectMover.Execute(new List<CharacterSelectMovement>()
	{
		CharacterSelectMovement.Up, CharacterSelectMovement.Up, CharacterSelectMovement.Up,
		CharacterSelectMovement.Left, CharacterSelectMovement.Left
	});

	inputs.FastPress(GameCubeButton.A, false);
	Thread.Sleep(3000);

	RecordEnvironment.Setup(inputs);

	Console.WriteLine("Done with setup");

	foreach (var move in movesToExecute)
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
		inputs.Press(GameCubeButton.LoadSaveState2);
		Thread.Sleep(1000);
		Console.WriteLine("Executing move");
		moveExecutor.Execute(move);

		var frames = new List<Bitmap>();

		// Frame count starts at 1 to respect human counting.
		var frameCounter = 1;
		await using (var textFile = new StreamWriter(folder + "text.csv"))
		{
			await textFile.WriteLineAsync("frame;text");

			while (true)
			{
				for (var j = 0; j < 2; j++)
				{
					var fileName = $"{folder}{frameCounter:D3}-{j}.png";
					var bitmap = new Bitmap(screenToUse.Bounds.Width, screenToUse.Bounds.Height);
					using (var g = Graphics.FromImage(bitmap))
					{
						g.CopyFromScreen(new Point(screenToUse.Bounds.Left, screenToUse.Bounds.Top), Point.Empty,
							screenToUse.Bounds.Size);
					}

					var ocr = new IronTesseract();

					using (var ocrInput = new OcrInput())
					{
						ocrInput.Add(bitmap);
						var result = await ocr.ReadAsync(ocrInput);
						await textFile.WriteLineAsync(
							$"{frameCounter};{string.Join(',', result.Words.Select(word => word.Text))}");
					}

					bitmap.Save(fileName, ImageFormat.Png);

					frames.Add(bitmap);
					Console.WriteLine($"Taken screenshot of frame {frameCounter} {j}");
					inputs.FrameAdvance();
					inputs.Hold(GameCubeButton.X);
					if (j == 0)
					{
						// Disable hud and background
						for (var hudIterator = 0; hudIterator < 3; hudIterator++)
						{
							inputs.FastPress(GameCubeButton.DpadDown);
							Thread.Sleep(50);
						}
					}
					else
					{
						// Disable hud and background
						for (var hudIterator = 0; hudIterator < 1; hudIterator++)
						{
							inputs.FastPress(GameCubeButton.DpadDown);
							Thread.Sleep(50);
						}
					}
					inputs.Release(GameCubeButton.X);
				}

				frameCounter++;
				Thread.Sleep(20);

				if (frameCounter >= moveData.TotalFrames)
				{
					break;
				}
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