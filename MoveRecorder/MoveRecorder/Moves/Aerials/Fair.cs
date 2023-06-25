using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Aerials
{
	public class Fair : AirMove
	{
		public Fair(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "fair";

		public override void Execute()
		{
			Controller.Hold(GameCubeButton.CStickRight.Index, GameCubeButton.CStickRight.Value);
			Controller.FrameAdvance();
		}

		public override void Cleanup()
		{
			Controller.Release(GameCubeButton.CStickRight.Index);
		}
	}
}
