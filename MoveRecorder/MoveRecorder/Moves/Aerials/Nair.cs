using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Aerials
{
	public class Nair : AirMove
	{
		public Nair(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "nair";

		public override void Execute()
		{
			Controller.Press(GameCubeButton.A);
		}
	}
}
