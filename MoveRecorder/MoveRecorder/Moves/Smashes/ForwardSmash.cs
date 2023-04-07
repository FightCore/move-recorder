using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Smashes
{
	internal class ForwardSmash : Move
	{
		public ForwardSmash(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "fsmash";

		public override void Execute()
		{
			Controller.Move(GameCubeButton.CStickRight.Index, GameCubeButton.CStickRight.Value);
		}
	}
}
