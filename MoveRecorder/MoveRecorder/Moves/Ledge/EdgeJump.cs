using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Ledge
{
	public class EdgeJump : Move
	{
		public EdgeJump(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "edgejump";
		public override void Execute()
		{
			Controller.Press(GameCubeButton.Y);
		}
	}
}
