using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Dodges
{
	public class RollForward : Move
	{
		public RollForward(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "rollforward";

		public override void Setup(ISetupInformation setupInformation)
		{
			Controller.Hold(GameCubeButton.L);
			for (var i = 0; i < 10; i++)
			{
				Controller.FrameAdvance();
				Thread.Sleep(500);
			}
		}

		public override void Execute()
		{
			Controller.Move(GameCubeButton.Right.Index, GameCubeButton.Right.Value);
		}

		public override void Cleanup()
		{
			Controller.Release(GameCubeButton.Right.Index);
			Controller.Release(GameCubeButton.L);
		}
	}
}
