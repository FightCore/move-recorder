using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Specials
{
	internal class NeutralSpecial : Move
	{
		public NeutralSpecial(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "neutralb";

		public override void Execute()
		{
			Controller.Press(GameCubeButton.B);
		}
	}
}
