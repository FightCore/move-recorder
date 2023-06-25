using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Aerials
{
	public class AerialUpB : AirMove
	{
		public AerialUpB(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "aupb";
		public override void Execute()
		{
			Controller.Hold(GameCubeButton.Up.Index, GameCubeButton.Up.Value);
			Controller.Press(GameCubeButton.B);
			Controller.Release(GameCubeButton.Up.Index);
		}
	}
}
