using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Board
{
  public abstract class AbstractBoard<T>
  {
    public int Width { get; init; }
    public int Height { get; init; }
    private AbstractElementFactory<T> _factory;
    private readonly T[] _values;


    public AbstractBoard(IEnumerable<T> values, AbstractElementFactory<T> factory, int width, int height)
    {
      this.Width = width;
      this.Height = height;
      this._values = values.ToArray();

      if (_values.Length != Width*Height)
        throw new ArgumentException($"Passed array length {_values.Length} mistmatches board size {Width}x{Height} = {Width*Height}");
      _factory=factory;
    }

    public AbstractBoard(Func<int, int, T> initializer, AbstractElementFactory<T> factory, int width, int height) : this(Enumerable.Range(0, width*height).Select(pos => initializer(pos % width, pos / width)), factory, width, height)
    {

    }

    public T[] Cells => _values;

    public T Get(int x, int y) => _values[y*Width+x];
    
    public void Set(int x, int y, T value) => _values[y*Width+x] = value;
    
    public void Iterate(Action<int, int, T> function)
    {
      for (int y = 0; y < Height; y++)
        for (int x = 0; x < Width; x++)
          function(x, y, Get(x, y));
    }

    public void Replace(Func<int, int, T, T> replaceFunction) => Iterate((x, y, v) => Set(x, y, replaceFunction(x, y, v)));

    public IEnumerable<AbstractElement<T>> Elements() => _factory.Elements(this);

    public IEnumerable<AbstractElement<T>> ElementsForCell(int x, int y) => _factory.Elements(this, x, y); 
  }
}
