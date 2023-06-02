using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Ledge
{
	public class EdgeGetup : Move
	{
		public EdgeGetup(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "EdgeGetup";
		public override void Execute()
		{
			Controller.Move(GameCubeButton.Right.Index, GameCubeButton.Right.Value);
		}
	}
}
