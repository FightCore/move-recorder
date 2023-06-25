using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Ground
{
	public class Dashattack : Move
	{
		public Dashattack(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "dattack";

		public override void Setup(ISetupInformation setupInformation)
		{
			// 1 frame of right input to initiate a dash.
			Controller.Hold(GameCubeButton.Right.Index, GameCubeButton.Right.Value);
			for (var i = 0; i < 4; i++)
			{
				Controller.FrameAdvance();
				Thread.Sleep(500);
			}
		}

		public override void Execute()
		{
			Controller.Press(GameCubeButton.A);
		}
	}
}
