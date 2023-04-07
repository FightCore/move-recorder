using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Uniques
{
	internal class Idle : Move
	{
		public Idle(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "idle";

		public override void Execute()
		{
			// Nothing to put here, just go idle animation
		}
	}
}
