using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Ground
{
	internal class Jab : Move
	{
		public Jab(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "jab1";

		public override void Execute()
		{
			Controller.Press(GameCubeButton.A);
		}
	}
}
