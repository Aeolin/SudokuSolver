using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Board
{
	public class AbstractElementGroup<T>
	{
		private static int IndexCounter;
		public int Index { get; init; }
		public HashSet<T> Members { get; init; }

		public AbstractElementGroup(HashSet<T> members)
		{
			Index = Interlocked.Increment(ref IndexCounter);
			Members=members;
		}
	}
}
