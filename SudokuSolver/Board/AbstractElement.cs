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

    public AbstractElement(int index, IValidator<T> validator, IEnumerable<T> values)
    {
      Index=index;
      Validator=validator;
      _values=values.ToArray();
    }

    public IEnumerable<T> GetValues() => _values;

    public bool Validate() => Validator.Validate(_values);  

  }
}
