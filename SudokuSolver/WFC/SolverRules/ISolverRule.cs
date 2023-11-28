using SudokuSolver.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC.SolverRules
{
  public interface ISolverRule<T, V> : ICloneable
  {
    public bool TrySolve(IEnumerable<AbstractElement<T>> elements, out SolverStep<V> step);
    public int Cost { get; }
  }
}
