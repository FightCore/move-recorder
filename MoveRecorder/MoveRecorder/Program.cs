using System.Drawing.Imaging;
using MoveRecorder;
using MoveRecorder.Data;
using Nefarius.ViGEm.Client;
using Serilog;
using Point = System.Drawing.Point;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

var baseFolder = "C:/tmp/recorder/";
var screens = Screen.AllScreens;
var i = 0;
foreach (var screen in screens)
{
	Log.Information($"{i}: {screen.DeviceName} {screen.Bounds} {screen.Primary}");
	i++;
}
Log.Information("Choose an device");
var decision = Console.ReadLine();
var screenToUse = screens[Convert.ToInt32(decision)];

Log.Information("Connecting controller");

var client = new ViGEmClient();
var ds4 = client.CreateDualShock4Controller();
ds4.Connect();
var inputs = new GameCubeInputs(ds4);

var characterSelectMover = new CharacterSelectMover(inputs);
inputs.Reset();
Log.Information("Please setup the move within Dolphin, ensure the first frame of the move is visible and the game is paused using debug mode.");
Log.Information("Please configure the controller in Dolphin");
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

		return Directory.GetFiles(folder).Length == 0;
	}).ToList();

	if (movesToExecute.Count == 0)
	{
		Log.Information("no moves to perform, skipping");
		continue;
	}

	characterSelectMover.Reset();
	characterSelectMover.Execute(character.SelectMovement);
	characterSelectMover.GoToStageSelect();
	characterSelectMover.SelectStage();
	// Wait for a while to ensure the stage and everything else is loaded.
	Thread.Sleep(3000);

	// Sets up the recording environment, including a save to SaveState slot 2
	RecordEnvironment.Setup(inputs);

	Log.Information("Done setting up record environment");

	foreach (var move in movesToExecute)
	{
		inputs.Reset();
		var moveData = moves.FirstOrDefault(storedMove => storedMove.NormalizedName == move);
		if (moveData == null)
		{
			Log.Information($"Could not find move {move}");
			continue;
		}

		if (moveData.TotalFrames == null || moveData.TotalFrames == 0)
		{
			Log.Information($"Frames unknown for move {move}");
			continue;
		}

		Log.Information($"Recording {move}");

		var folder = $"{baseFolder}{character.Name}/{move}/";

		Directory.CreateDirectory(folder);

		if (Directory.GetFiles(folder).Any())
		{
			Log.Information("Skipping move as its already there");
			continue;
		}

		// Reload second save state slot to the clean recording environment
		Log.Information("Loading save state");
		inputs.Press(GameCubeButton.LoadSaveState2);
		Thread.Sleep(1000);
		Log.Information("Executing move");
		moveExecutor.Execute(move);

		// Frame count starts at 1 to respect human counting.
		var frameCounter = 1;
		var frame = new Frame(screenToUse.Bounds.Width, screenToUse.Bounds.Height, screenToUse.Bounds.Left, screenToUse.Bounds.Top,
		screenToUse.Bounds.Size);

		while (frameCounter < moveData.TotalFrames * 2)
		{
			for (var j = 0; j < 2; j++)
			{
				var fileName = $"{folder}{frameCounter:D3}.png";
				frame.Save(fileName);

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
				frameCounter++;
			}

			Thread.Sleep(20);
		}
	}
}