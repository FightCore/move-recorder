namespace MoveRecorder.Moves.Abstraction;

public interface IMove
{
	string Name { get; }

	void Execute();

	void Setup(ISetupInformation setupInformation);
}