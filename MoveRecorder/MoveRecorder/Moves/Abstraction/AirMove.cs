namespace MoveRecorder.Moves.Abstraction
{
	public abstract class AirMove : Move
	{
		protected AirMove(GameCubeInputs controller) : base(controller)
		{
		}

		public override void Setup(ISetupInformation setupInformation)
		{
			Controller.Press(GameCubeButton.X);

			for (var i = 0; i < 15; i++)
			{
				Controller.FrameAdvance();
				Thread.Sleep(500);
			}
		}
	}
}
