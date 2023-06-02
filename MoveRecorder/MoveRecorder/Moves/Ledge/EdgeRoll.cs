using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Ledge
{
	public class EdgeRoll : Move
	{
		public EdgeRoll(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "edgeroll";

		public override void Execute()
		{
			Controller.Press(GameCubeButton.R);
		}
	}
}
