using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC
{
	public struct WFCStateBackup
	{
		public HashSet<int> States { get; init; }
		public int? Collapsed { get; init; }
	}
}
