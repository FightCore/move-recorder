using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveRecorder
{
	public class Character
	{
		public string Name { get; set; }

		public List<CharacterSelectMovement> SelectMovement { get; set; }

		public Character(string name, int down, int right)
		{
			Name = name;
			SelectMovement = new List<CharacterSelectMovement>();

			for (var i = 0; i < down; i++)
			{
				SelectMovement.Add(CharacterSelectMovement.Down);
			}

			for (var i = 0; i < right; i++)
			{
				SelectMovement.Add(CharacterSelectMovement.Right);
			}
		}
	}
}
