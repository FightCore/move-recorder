using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Aerials
{
	public class AerialSideB : AirMove
	{
		public AerialSideB(GameCubeInputs controller) : base(controller)
		{
		}

		public override void Setup(ISetupInformation setupInformation)
		{
			Controller.Press(GameCubeButton.X);

			// Override as we need to get higher
			for (var i = 0; i < 50; i++)
			{
				Controller.FrameAdvance();
				Thread.Sleep(500);
			}
		}

		public override string Name => "asideb";

		public override void Execute()
		{
			Controller.Hold(GameCubeButton.Right.Index, GameCubeButton.Right.Value);
			Controller.Press(GameCubeButton.B);
			Controller.Release(GameCubeButton.Right.Index);
		}
	}
}
