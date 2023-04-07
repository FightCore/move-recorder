using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Specials
{
	internal class DownSpecial : Move
	{
		public DownSpecial(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "downb";

		public override void Execute()
		{
			Controller.Hold(GameCubeButton.Down.Index, GameCubeButton.Down.Value);
			Controller.Press(GameCubeButton.B);
			Controller.Release(GameCubeButton.Down.Index);
		}
	}
}
