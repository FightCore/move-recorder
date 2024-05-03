namespace MoveRecorder
{
	public static class RecordEnvironment
	{
		public static void Setup(GameCubeInputs controller)
		{
			controller.FastPress(GameCubeButton.Start, false);

			// Enable hitboxes
			controller.Hold(GameCubeButton.R);
			for (var hudIterator = 0; hudIterator < 2; hudIterator++)
			{
				controller.FastPress(GameCubeButton.DpadRight);
				Thread.Sleep(50);
			}

			controller.Release(GameCubeButton.R);
			controller.Hold(GameCubeButton.X);

			// Disable hud and background
			for (var hudIterator = 0; hudIterator < 3; hudIterator++)
			{
				controller.FastPress(GameCubeButton.DpadDown);
				Thread.Sleep(50);
			}

			// Camera select
			for (var hudIterator = 0; hudIterator < 2; hudIterator++)
			{
				controller.FastPress(GameCubeButton.DpadLeft);
				Thread.Sleep(50);
			}

			controller.Release(GameCubeButton.X);

			// Setup camera with zoom.
			controller.Hold(GameCubeButton.B);
			controller.Hold(GameCubeButton.DpadLeft);
			controller.Hold(GameCubeButton.CStickDown.Index, GameCubeButton.CStickDown.Value);
			Thread.Sleep(150);
			controller.Release(GameCubeButton.CStickDown.Index);
			controller.ReleaseDPad();
			controller.Release(GameCubeButton.B);

			// Set up action display
			controller.Hold(GameCubeButton.Y);
			controller.FastPress(GameCubeButton.DpadDown);
			controller.Release(GameCubeButton.Y);

			// Unpause the game.
			controller.FastPress(GameCubeButton.Start, false);
			Thread.Sleep(50);

			// Airdodge down to land on the ground (counters the 0-Grav mode)
			controller.Hold(GameCubeButton.Down.Index, GameCubeButton.Down.Value);
			controller.Press(GameCubeButton.R, false);
			controller.Release(GameCubeButton.Down.Index);
			controller.Reset();
			// Sleep for a long time to allow the character to land.
			Thread.Sleep(10000);

			// Pause the game again to setup frame advance
			controller.Press(GameCubeButton.Start, false);
			Thread.Sleep(100);

			// Save to the second savestate slot.
			controller.Press(GameCubeButton.SaveSaveState2, false);
			Thread.Sleep(100);
		}
	}
}
