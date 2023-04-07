using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Ground
{
	internal class Grab : Move
	{
		public Grab(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "grab";

		public override void Execute()
		{
			Controller.Hold(GameCubeButton.L);
			Controller.Press(GameCubeButton.A);
			Controller.Release(GameCubeButton.L);
		}
	}
}
