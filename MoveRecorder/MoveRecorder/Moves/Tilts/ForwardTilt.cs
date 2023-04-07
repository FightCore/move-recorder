using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Tilts
{
	internal class ForwardTilt : Move
	{
		public ForwardTilt(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "ftilt";
		public override void Execute()
		{
			Controller.TiltUp(GameCubeButton.Right.Index);
			Controller.Press(GameCubeButton.Z, false);
			Controller.Press(GameCubeButton.A);
			Controller.Release(GameCubeButton.Right.Index);
		}
	}
}
