using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder.Moves.Tilts
{
	internal class UpTilt : Move
	{
		public UpTilt(GameCubeInputs controller) : base(controller)
		{
		}

		public override string Name => "utilt";

		public override void Execute()
		{
			Controller.TiltUp(GameCubeButton.Down.Index);
			Controller.Press(GameCubeButton.Z, false);
			Controller.Press(GameCubeButton.A);
			Controller.Release(GameCubeButton.Down.Index);
		}
	}
}
