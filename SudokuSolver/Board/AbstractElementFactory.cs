using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Board
{
  public abstract class AbstractElementFactory<T, R> where T : IRestoreable<R>
  {
    public abstract IEnumerable<AbstractElement<T>> Elements(AbstractBoard<T, R> board);
    public abstract IEnumerable<AbstractElement<T>> Elements(AbstractBoard<T, R> board, int x, int y);
  }
}
