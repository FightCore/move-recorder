using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Aerials
{
	public class Uair : AirMove
	{
		public Uair(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "uair";

		public override void Execute()
		{
			Controller.Hold(GameCubeButton.CStickUp.Index, GameCubeButton.CStickUp.Value);
			Controller.FrameAdvance();
		}

		public override void Cleanup()
		{
			Controller.Release(GameCubeButton.CStickUp.Index);
		}
	}
}
