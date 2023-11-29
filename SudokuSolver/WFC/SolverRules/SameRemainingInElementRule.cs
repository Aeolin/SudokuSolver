using SudokuSolver.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC.SolverRules
{
  public class SameRemainingInElementRule : ISolverRule<WFCState, int?>
  {
    public int Cost => 2;
    private WFCSolver _solver;

    public SameRemainingInElementRule(WFCSolver solver)
    {
      _solver = solver;
    }

    public object Clone()
    {
      return this;
    }

    public bool TrySolve(IEnumerable<AbstractElement<WFCState>> elements, out SolverStep<int?> step)
    {
      foreach (var element in elements.Where(x => x.IsFullElement))
      {
        var collapsables = element.GetValues()
          .Where(x => x.IsCollapsable)
          .GroupBy(x => x.RemainingValues);

        var sameElements = collapsables.FirstOrDefault(x => x.Key.Count > 1 && x.Key.Count == x.Count());
        if (sameElements != null)
        {
          foreach (var state in element.GetValues().Where(s => sameElements.Contains(s) == false))
            state.RemovePossibleValues(sameElements.Key);

          step = new SolverStep<int?>(0, 0, null, $"Removed values [{string.Join(", ", sameElements.Key)} from {element} because the rule of n's]");
          return true;
        }
      }

      step = null;
      return false;
    }
  }
}
