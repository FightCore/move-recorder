using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IronOcr.OcrResult;

namespace MoveRecorder
{
	public class CharacterSelectMover
	{
		private readonly GameCubeInputs _controller;

		public CharacterSelectMover(GameCubeInputs controller)
		{
			_controller = controller;
		}

		public void Reset()
		{
			_controller.Press(GameCubeButton.LoadSaveState1, false);
			Thread.Sleep(200);
			_controller.Hold(GameCubeButton.B);
			Thread.Sleep(20);
			_controller.Release(GameCubeButton.B);
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

		public void GoToStageSelect()
		{
			_controller.Hold(GameCubeButton.A);
			Thread.Sleep(20);
			_controller.Release(GameCubeButton.A);

			_controller.Hold(GameCubeButton.Start);
			Thread.Sleep(2000);
			_controller.Release(GameCubeButton.Start);
			Thread.Sleep(2000);
			// Was previously used to toggle 20xx pages, now this is done when creating the savestate
			//inputs.FastPress(GameCubeButton.DpadDown);
			//Thread.Sleep(500);
		}

		public void SelectStage()
		{
			// Select the stage using preset moves.
			// Selects the Pokemon Kalos League stage on 20XX, has no background and decent spawn.
			Execute([
				CharacterSelectMovement.Up, CharacterSelectMovement.Up, CharacterSelectMovement.Up,
				CharacterSelectMovement.Left, CharacterSelectMovement.Left
			]);

			_controller.FastPress(GameCubeButton.A, false);
		}

		private void Move((int Index, short Value) stick, int time = 100)
		{
			_controller.Hold(stick.Index, stick.Value);
			Thread.Sleep(time);
			_controller.Release(stick.Index);
		}
	}
}
