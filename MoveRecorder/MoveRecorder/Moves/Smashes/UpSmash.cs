using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Smashes
{
	internal class UpSmash : Move
	{
		public UpSmash(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "usmash";

		public override void Execute()
		{
			Controller.Move(GameCubeButton.CStickUp.Index, GameCubeButton.CStickUp.Value);
		}
	}
}
