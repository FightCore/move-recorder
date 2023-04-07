namespace MoveRecorder.Moves.Abstraction;

public abstract class Move : IMove
{
	protected GameCubeInputs Controller;
	protected Move(GameCubeInputs controller)
	{
		Controller = controller;
	}

	public abstract string Name { get; }

	public abstract void Execute();

	public virtual void Setup(ISetupInformation setupInformation)
	{
	}
}