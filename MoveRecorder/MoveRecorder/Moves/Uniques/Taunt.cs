using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Uniques
{
	internal class Taunt : Move
	{
		public Taunt(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "taunt";

		public override void Execute()
		{
			Controller.Press(GameCubeButton.DpadUp);
		}
	}
}
