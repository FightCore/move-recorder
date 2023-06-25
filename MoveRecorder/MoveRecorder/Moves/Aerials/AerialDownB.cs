using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Aerials
{
	public class AerialDownB : AirMove
	{
		public AerialDownB(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "adownb";
		public override void Execute()
		{
			Controller.Hold(GameCubeButton.Down.Index, GameCubeButton.Down.Value);
			Controller.Press(GameCubeButton.B);
			Controller.Release(GameCubeButton.Down.Index);
		}
	}
}
