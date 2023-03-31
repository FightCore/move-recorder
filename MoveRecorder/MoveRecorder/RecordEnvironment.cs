using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.DualShock4;

namespace MoveRecorder
{
	// Mapping the PS4 controller to the GC controller
	// I tried to keep things semi understandable.
	public class RecordEnvironment
	{
		private const int _delay = 500;
		private readonly GameCubeInputs _controller;

		public RecordEnvironment(GameCubeInputs controller)
		{
			_controller = controller;
		}

		public void Setup()
		{
			// Enable development/debug mode
			_controller.Press(GameCubeButton.Start);
			_controller.Press(GameCubeButton.DpadRight);
			Thread.Sleep(_delay);
			_controller.Press(GameCubeButton.DpadRight);
			Thread.Sleep(_delay);

			// Enable hitboxes
			_controller.Hold(GameCubeButton.R);
			_controller.Press(GameCubeButton.DpadRight);
			Thread.Sleep(_delay);
			_controller.Release(GameCubeButton.R);
			Thread.Sleep(_delay);

			// Unpause the game
			_controller.Hold(GameCubeButton.X);
			_controller.Press(GameCubeButton.DpadUp);
			_controller.Release(GameCubeButton.X);
		}
	}
}
