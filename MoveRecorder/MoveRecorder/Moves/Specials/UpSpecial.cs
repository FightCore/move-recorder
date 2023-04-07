using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Specials
{
	internal class UpSpecial : Move
	{
		public UpSpecial(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "upb";

		public override void Execute()
		{
			Controller.Hold(GameCubeButton.Up.Index, GameCubeButton.Up.Value);
			Controller.Press(GameCubeButton.B);
			Controller.Release(GameCubeButton.Up.Index);
		}
	}
}
