using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Aerials
{
	public class Bair : AirMove
	{
		public Bair(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "bair";

		public override void Execute()
		{
			Controller.Hold(GameCubeButton.CStickLeft.Index, GameCubeButton.CStickLeft.Value);
			Controller.FrameAdvance();
		}

		public override void Cleanup()
		{
			Controller.Release(GameCubeButton.CStickLeft.Index);
		}
	}
}
