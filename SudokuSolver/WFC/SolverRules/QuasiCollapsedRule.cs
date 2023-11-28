using SudokuSolver.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC.SolverRules
{
  public class QuasiCollapsedRule : ISolverRule<WFCState, int?>
  {
    public object Clone()
    {
      throw new NotImplementedException();
    }

    public int Cost => 0;

    public bool TrySolve(IEnumerable<AbstractElement<WFCState>> elements, out SolverStep<int?> step)
    {
      step = null;
      var quasiCollapsed = elements.Where(x => x is RowElement<WFCState>).SelectMany(x => x.GetValues()).FirstOrDefault(x => x.IsQuasiCollapsed);
      if (quasiCollapsed == null)
        return false;

      var value = quasiCollapsed.RemainingValues.First();
      step = new SolverStep<int?>(quasiCollapsed.X, quasiCollapsed.Y, value, $"Collapse State ({quasiCollapsed.X}/{quasiCollapsed.Y}) to {value} because it was the only value left for this cell");
      return true;
    }
  }
}
