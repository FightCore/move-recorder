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

		public void Press(DualShock4Button button)
		{
			_controller.SetButtonState(button, true);
			Thread.Sleep(_delay);
			_controller.SetButtonState(button, false);
		}

		public void Press(DualShock4DPadDirection dpadDirection)
		{
			_controller.SetDPadDirection(dpadDirection);
			Thread.Sleep(_delay);
			_controller.SetDPadDirection(GameCubeButton.DpadNeutral);
		}

		public void Hold(DualShock4Button button)
		{
			_controller.SetButtonState(button, true);
		}

		public void Release(DualShock4Button button)
		{
			_controller.SetButtonState(button, false);
		}

		public void Move(int stickIndex, short value)
		{
			_controller.SetAxisValue(stickIndex, value);
			Press(GameCubeButton.Z);
			Thread.Sleep(_delay);
			_controller.SetAxisValue(stickIndex, 0);
		}
	}
}
