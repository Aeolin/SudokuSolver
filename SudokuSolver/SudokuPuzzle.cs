using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
	public class SudokuPuzzle
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int RectsPerSquare { get; set; }
		public int[] Puzzle { get; set; }
		public int[][] Groups { get; set; }
		public string[] ExtraRules { get; set; }

		public BackupModel[] Backups { get; set; }

		public SudokuPuzzle Clone()
		{
			return new SudokuPuzzle
			{
				Name = Name,
				Description = Description,
				Width = Width,
				Height = Height,
				RectsPerSquare = RectsPerSquare,
				Puzzle = Puzzle.ToArray(),
				Groups = Groups.Select(g => g.ToArray()).ToArray(),
				ExtraRules = ExtraRules.ToArray()
			};
		}
	}
}
