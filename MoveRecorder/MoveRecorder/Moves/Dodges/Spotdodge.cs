using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Dodges
{
	public class Spotdodge : Move
	{
		public Spotdodge(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "spotdodge";
		public override void Execute()
		{
			Controller.Hold(GameCubeButton.L);
			Controller.Move(GameCubeButton.Down.Index, GameCubeButton.Down.Value);
			Controller.Release(GameCubeButton.L);
			Controller.Release(GameCubeButton.Down.Index);
		}
	}
}
