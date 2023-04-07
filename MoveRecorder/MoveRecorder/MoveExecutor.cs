using System.Reflection;
using MoveRecorder.Moves.Abstraction;

namespace MoveRecorder
{
	public class MoveExecutor
	{
		private readonly List<IMove> _moves;
		public readonly IReadOnlyList<string> AvailableMoves;

		public MoveExecutor(GameCubeInputs controller)
		{
			_moves = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
				!type.IsAbstract && !type.IsInterface && type.IsAssignableTo(typeof(IMove)))
				.Select(type => (IMove)Activator.CreateInstance(type, controller)).ToList();
			AvailableMoves = _moves.ConvertAll(move => move.Name).AsReadOnly();
		}

		public void Execute(string moveName)
		{
			_moves.First(move => move.Name == moveName).Execute();
		}
	}
}
