using SudokuSolver.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC
{
  public class SudokuElementFactory<T> : AbstractElementFactory<T>
  {
    public int RectanglesPerSide { get; init; }
    private IValidator<T> _validator;

    public SudokuElementFactory(int rectsPerSide, IValidator<T> validator)
    {
      this.RectanglesPerSide = rectsPerSide;
      _validator=validator;
    }

    public override IEnumerable<AbstractElement<T>> Elements(AbstractBoard<T> board)
    {
      for (int i = 0; i < board.Height; i++)
        yield return Row(board, i);

      for (int i = 0; i < board.Width; i++)
        yield return Column(board, i);

      for(int i = 0; i < RectanglesPerSide*RectanglesPerSide; i++)
        yield return Rectangle(board, i);
    }

    public override IEnumerable<AbstractElement<T>> Elements(AbstractBoard<T> board, int x, int y)
    {
      yield return Row(board, y);
      yield return Column(board, x);
      yield return Rectangle(board, (y / RectanglesPerSide) * RectanglesPerSide + (x / RectanglesPerSide));
    }

    public RowElement<T> Row(AbstractBoard<T> board, int index) =>
      new RowElement<T>(index, _validator,
        Enumerable.Range(0, board.Width)
        .Select(x => board.Get(x, index))
      );

    public ColumnElement<T> Column(AbstractBoard<T> board, int index) =>
      new ColumnElement<T>(index, _validator,
        Enumerable.Range(0, board.Height)
        .Select(y => board.Get(index, y))
      );

    public RectangleElement<T> Rectangle(AbstractBoard<T> board, int index) => new RectangleElement<T>(index, _validator, rectParts(index, board));

    private IEnumerable<T> rectParts(int index, AbstractBoard<T> board)
    {
      var rectHeight = board.Height / this.RectanglesPerSide;
      var rectWidth = board.Width / this.RectanglesPerSide;
      var yOffset = index / this.RectanglesPerSide;
      var xOffset = index % this.RectanglesPerSide;

      for (int y = yOffset*rectHeight; y < (1+yOffset)*rectHeight; y++)
        for (int x = xOffset*rectWidth; x < (1+xOffset)*rectWidth; x++)
          yield return board.Get(x, y);
    }
  }
}
