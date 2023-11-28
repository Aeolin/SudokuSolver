using SudokuSolver.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC
{
  public class RowElement<T> : AbstractElement<T>
  {
    public RowElement(int index, IValidator<T> validator, IEnumerable<T> values) : base(index, validator, values)
    {
    }

    public override string ToString() => $"Row {Index+1}";
  }

  public class ColumnElement<T> : AbstractElement<T>
  {
    public ColumnElement(int index, IValidator<T> validator, IEnumerable<T> values) : base(index, validator, values)
    {
    }

    public override string ToString() => $"Column {Index+1}";
  }

  public class RectangleElement<T> : AbstractElement<T>
  {
    public RectangleElement(int index, IValidator<T> validator, IEnumerable<T> values) : base(index, validator, values)
    {
    }

    public override string ToString() => $"Rectangle {Index+1}";
  }

}
