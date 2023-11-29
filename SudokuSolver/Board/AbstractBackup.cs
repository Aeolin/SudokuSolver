using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Board
{
	public class AbstractBackup<R>
	{
		public DateTime TimeStamp { get; init; }
		public R[] Backup { get; init; }
		public int Number { get; init; }

		public string Name { get; init; }

		public AbstractBackup(R[] backup, int number, string name = null)
		{
			TimeStamp=DateTime.Now;
			Backup=backup;
			Number=number;
			Name = name ?? $"Backup #{number}";
		}

	}
}
