using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Board
{
  public interface IValidator<T>
  {
    public bool Validate(IEnumerable<T> value);
  }
}
