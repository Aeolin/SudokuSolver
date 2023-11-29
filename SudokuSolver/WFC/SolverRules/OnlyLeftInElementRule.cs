using SudokuSolver.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC.SolverRules
{
  public class OnlyLeftInElementRule : ISolverRule<WFCState, int?>
  {
    private WFCSolver _solver;

    public OnlyLeftInElementRule(WFCSolver solver)
    {
      _solver = solver;
    }

    public int Cost => 1;


    public object Clone()
    {
      return this;
    }

    public bool TrySolve(IEnumerable<AbstractElement<WFCState>> elements, out SolverStep<int?> step)
    {
      foreach(var element in elements.Where(x => x.IsFullElement))
      {
        var values = element.GetValues().Where(x => x.IsCollapsable);
        var onlyPossible = Enumerable.Range(1, _solver.RectangleCellCount)
          .ToDictionary(k => k, v => values.Where(x => x.IsRemaining(v)).ToArray())
          .FirstOrDefault(v => v.Value.Length == 1);

        if(onlyPossible.Value != null)
        {
          var state = onlyPossible.Value.First();
          step = new SolverStep<int?>(state.X, state.Y, onlyPossible.Key, $"Collapsed State ({state.X}/{state.Y}) to {onlyPossible.Key} because it was the only possible element left in {element}");
          return true;
        }

      }

      step = null;
      return false;
    }
  }
}
