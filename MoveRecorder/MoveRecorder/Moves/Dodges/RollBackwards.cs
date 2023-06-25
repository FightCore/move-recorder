using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Dodges
{
	public class RollBackwards : Move
	{
		public RollBackwards(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "rollbackwards";

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
			Controller.Move(GameCubeButton.Left.Index, GameCubeButton.Left.Value);
		}

		public override void Cleanup()
		{
			Controller.Release(GameCubeButton.Left.Index);
			Controller.Release(GameCubeButton.L);
		}
	}
}
