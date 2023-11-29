using SudokuSolver.Board;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.BruteForce
{
	public class BruteForceState : IRestoreable<int?>
	{
		public int X { get; init; }
		public int Y { get; init; }
		public int? Value { get; set; }
		public bool Fixed { get; set; }
		public bool AllTried => Value == 9;


		public BruteForceState(int x, int y)
		{
			X=x;
			Y=y;
		}

		public int? Backup() => Value;

		public void Restore(int? value)
		{
			Value = value;
		}

		public int GetValueOrDefault(int @default) => Value.HasValue ? Value.Value : @default;
	}
}
