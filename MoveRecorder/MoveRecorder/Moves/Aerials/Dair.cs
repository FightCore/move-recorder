using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Aerials
{
	public class Dair : AirMove
	{
		public Dair(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "dair";

		public override void Execute()
		{
			Controller.Hold(GameCubeButton.CStickDown.Index, GameCubeButton.CStickDown.Value);
			Controller.FrameAdvance();
		}

		public override void Cleanup()
		{
			Controller.Release(GameCubeButton.CStickDown.Index);
		}
	}
}
