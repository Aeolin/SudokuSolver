using SudokuSolver.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC.CollapseRules
{
	public interface ICollapseRule<T, R> where T : IRestoreable<R>
	{
		public void Collapse(int x, int y, int value, AbstractBoard<T, R> board);
	}
}
