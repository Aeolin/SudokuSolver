using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Board
{
  public abstract class AbstractElement<T>
  {
    public int Index { get; init; }
    public IValidator<T> Validator { get; init; }
    private T[] _values;
    public bool IsFullElement { get; init; }

    public AbstractElement(int index, bool isFullElement, IValidator<T> validator, IEnumerable<T> values)
    {
      Index=index;
      Validator=validator;
      _values=values.ToArray();
      IsFullElement = isFullElement;
    }

    public IEnumerable<T> GetValues() => _values;

    public bool Validate() => Validator.Validate(_values);  

  }
}
