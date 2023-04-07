using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Specials
{
	internal class ForwardSpecial : Move
	{
		public ForwardSpecial(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "sideb";

		public override void Execute()
		{
			Controller.Hold(GameCubeButton.Right.Index, GameCubeButton.Right.Value);
			Controller.Press(GameCubeButton.B);
			Controller.Release(GameCubeButton.Right.Index);
		}
	}
}
