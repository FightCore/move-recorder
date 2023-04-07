using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Smashes
{
	internal class DownSmash : Move
	{
		public DownSmash(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "dsmash";

		public override void Execute()
		{
			Controller.Move(GameCubeButton.CStickDown.Index, GameCubeButton.CStickDown.Value);
		}
	}
}
