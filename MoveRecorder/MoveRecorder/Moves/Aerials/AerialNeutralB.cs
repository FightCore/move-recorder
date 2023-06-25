using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Aerials
{
	public class AerialNeutralB : AirMove
	{
		public AerialNeutralB(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "aneutralb";
		public override void Execute()
		{
			Controller.Press(GameCubeButton.B);
		}
	}
}
