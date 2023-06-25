using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Aerials
{
	public class Airdodge : AirMove
	{
		public Airdodge(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "airdodge";
		public override void Execute()
		{
			Controller.Press(GameCubeButton.L);
		}
	}
}
