namespace MoveRecorder
{
	public class MoveExecutor
	{
		private readonly GameCubeInputs _controller;


		public MoveExecutor(GameCubeInputs controller)
		{
			_controller = controller;
		}

		public void Execute(string move)
		{
			switch (move)
			{
				case "usmash":
					_controller.Move(GameCubeButton.CStickUp.Index, GameCubeButton.CStickUp.Value);
					break;
				default:
					Console.Write("Issue");
					break;
			}
		}
	}
}
