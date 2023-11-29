using SudokuSolver.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC.CollapseRules
{
	public class CollapseInElements : ICollapseRule<WFCState, WFCStateBackup>
	{
		public void Collapse(int x, int y, int value, AbstractBoard<WFCState, WFCStateBackup> board)
		{
			var state = board[x, y];
			foreach (var element in board.ElementsForCell(x, y).SelectMany(x => x.GetValues()).Where(x => x.Equals(state) == false && x.IsCollapsable).Distinct())
				element.RemovePossibleValue(value);
		}
	}
}
