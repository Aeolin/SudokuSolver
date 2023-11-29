using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC
{
	public class ElementGroup
	{
		private static int IndexCounter;
		public int Index { get; init; }
		public HashSet<int> Members { get; init; }

		public ElementGroup(HashSet<int> members)
		{
			Index = Interlocked.Increment(ref IndexCounter);
			Members=members;
		}
	}
}
