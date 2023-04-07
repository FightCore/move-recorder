using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Tilts
{
	internal class DownTilt : Move
	{
		public DownTilt(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "dtilt";
		public override void Execute()
		{
			Controller.TiltDown(GameCubeButton.Down.Index);
			Controller.Press(GameCubeButton.Z, false);
			Controller.Press(GameCubeButton.A);
			Controller.Release(GameCubeButton.Down.Index);
		}
	}
}
