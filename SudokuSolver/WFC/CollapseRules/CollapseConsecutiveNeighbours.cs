using SudokuSolver.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC.CollapseRules
{
	public class CollapseConsecutiveNeighbours : ICollapseRule<WFCState, WFCStateBackup>
	{
		public void Collapse(int x, int y, int value, AbstractBoard<WFCState, WFCStateBackup> board)
		{
			var smaller = value - 1;
			var bigger = value + 1;
			var left = board.Index(x-1, y);
			if (left.HasValue)
				board.Cells[left.Value].RemovePossibleValues(smaller, bigger);

			var top = board.Index(x, y-1);
			if (top.HasValue)
				board.Cells[top.Value].RemovePossibleValues(smaller, bigger);

			var right = board.Index(x+1, y);
			if (right.HasValue)
				board.Cells[right.Value].RemovePossibleValues(smaller, bigger);

			var bottom = board.Index(x, y+1);
			if (bottom.HasValue)
				board.Cells[bottom.Value].RemovePossibleValues(smaller, bigger);

		}
	}
}
