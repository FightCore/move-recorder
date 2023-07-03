using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveRecorder
{
	public class CharacterSelectMover
	{
		private readonly GameCubeInputs _controller;

		public CharacterSelectMover(GameCubeInputs controller)
		{
			_controller = controller;
		}

		public void Execute(List<CharacterSelectMovement> movements)
		{
			foreach (var operation in movements)
			{
				switch (operation)
				{
					case CharacterSelectMovement.Down:
						Move(GameCubeButton.Down);
						break;
					case CharacterSelectMovement.Right:
						Move(GameCubeButton.Right, 80);
						break;
					case CharacterSelectMovement.Left:
						Move(GameCubeButton.Left);
						break;
					case CharacterSelectMovement.Up:
						Move(GameCubeButton.Up);
						break;
					case CharacterSelectMovement.Z:
						_controller.Hold(GameCubeButton.Z);
						Thread.Sleep(20);
						_controller.Release(GameCubeButton.Z);
						break;
				}
			}
		}

		private void Move((int Index, short Value) stick, int time = 100)
		{
			_controller.Hold(stick.Index, stick.Value);
			Thread.Sleep(time);
			_controller.Release(stick.Index);
		}
	}
}
