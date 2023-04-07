using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Ground
{
	internal class DashGrab : Move
	{
		public DashGrab(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "dashgrab";

		public override void Setup(ISetupInformation setupInformation)
		{
			// 1 frame of right input to initiate a dash.
			Controller.Hold(GameCubeButton.Right.Index, GameCubeButton.Right.Value);
			Controller.FrameAdvance();
		}

		public override void Execute()
		{
			Controller.Hold(GameCubeButton.L);
			Controller.Press(GameCubeButton.A);
			Controller.Release(GameCubeButton.L);
			Controller.Release(GameCubeButton.Right.Index);
		}
	}
}
