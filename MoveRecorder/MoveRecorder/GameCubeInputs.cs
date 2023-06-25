using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.DualShock4;

namespace MoveRecorder
{
	public class GameCubeInputs
	{
		private readonly IDualShock4Controller _controller;
		private const int _delay = 300;

		public GameCubeInputs(IDualShock4Controller controller)
		{
			_controller = controller;
		}

		public void Press(DualShock4Button button, bool frameAdvance = true)
		{
			_controller.SetButtonState(button, true);
			Thread.Sleep(_delay);
			if (frameAdvance)
			{
				FrameAdvance();
			}
			_controller.SetButtonState(button, false);
		}

		public void FastPress(DualShock4Button button, bool frameAdvance = true)
		{
			_controller.SetButtonState(button, true);
			Thread.Sleep(20);
			if (frameAdvance)
			{
				FrameAdvance();
			}
			_controller.SetButtonState(button, false);
		}

		public void FrameAdvance()
		{
			Press(GameCubeButton.Z, false);
		}

		public void Press(DualShock4DPadDirection dpadDirection, bool frameAdvance = true)
		{
			_controller.SetDPadDirection(dpadDirection);
			if (frameAdvance)
			{
				_controller.SetButtonState(GameCubeButton.Z, true);

			}
			Thread.Sleep(_delay);
			if (frameAdvance)
			{
				_controller.SetButtonState(GameCubeButton.Z, false);

			}
			_controller.SetDPadDirection(GameCubeButton.DpadNeutral);
		}

		public void FastPress(DualShock4DPadDirection dpadDirection)
		{
			_controller.SetDPadDirection(dpadDirection);
			Thread.Sleep(100);
			_controller.SetDPadDirection(GameCubeButton.DpadNeutral);
		}

		public void Hold(DualShock4Button button)
		{
			_controller.SetButtonState(button, true);
		}
		public void Hold(DualShock4DPadDirection dpadDirection)
		{
			_controller.SetDPadDirection(dpadDirection);
		}

		public void Release(DualShock4Button button)
		{
			_controller.SetButtonState(button, false);
		}

		public void ReleaseDPad()
		{
			_controller.SetDPadDirection(GameCubeButton.DpadNeutral);
		}

		public void Move(int stickIndex, short value)
		{
			_controller.SetAxisValue(stickIndex, value);
			_controller.SetButtonState(GameCubeButton.Z, true);
			Thread.Sleep(_delay);
			_controller.SetAxisValue(stickIndex, 0);
		}

		public void Hold(int stickIndex, short value)
		{
			_controller.SetAxisValue(stickIndex, value);
		}

		public void TiltUp(int stickIndex)
		{
			_controller.SetAxisValue(stickIndex, short.MaxValue / 4);
		}

		public void TiltDown(int stickIndex)
		{
			_controller.SetAxisValue(stickIndex, short.MinValue / 4);
		}

		public void Release(int stickIndex)
		{
			_controller.SetAxisValue(stickIndex, 0);
		}

		public void Reset()
		{
			_controller.ResetReport();
		}
	}
}
