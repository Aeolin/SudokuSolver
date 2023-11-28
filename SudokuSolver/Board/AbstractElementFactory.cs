using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Board
{
  public abstract class AbstractElementFactory<T>
  {
    public abstract IEnumerable<AbstractElement<T>> Elements(AbstractBoard<T> board);
    public abstract IEnumerable<AbstractElement<T>> Elements(AbstractBoard<T> board, int x, int y);
  }
}
