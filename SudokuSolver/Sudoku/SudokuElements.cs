using SudokuSolver.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Sudoku
{
    public class RowElement<T> : AbstractElement<T>
    {
        public RowElement(int index, IValidator<T> validator, IEnumerable<T> values) : base(index, true, validator, values)
        {
        }

        public override string ToString() => $"Row {Index + 1}";
    }

    public class ColumnElement<T> : AbstractElement<T>
    {
        public ColumnElement(int index, IValidator<T> validator, IEnumerable<T> values) : base(index, true, validator, values)
        {
        }

        public override string ToString() => $"Column {Index + 1}";
    }

    public class RectangleElement<T> : AbstractElement<T>
    {
        public RectangleElement(int index, IValidator<T> validator, IEnumerable<T> values) : base(index, true, validator, values)
        {
        }

        public override string ToString() => $"Rectangle {Index + 1}";
    }

    public class GroupElement<T> : AbstractElement<T>
    {
        public GroupElement(int index, bool isFullGroup, IValidator<T> validator, IEnumerable<T> values) : base(index, isFullGroup, validator, values)
        {
        }

        public override string ToString() => $"Group {Index + 1}";
    }

}
