using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC
{
  public class WFCState
  {
    public int X { get; init; }
    public int Y { get; init; }
    private int? _collapsed;
    public HashSet<int> RemainingValues { get; init; }

    public bool IsCollapsed => _collapsed != null;
    public bool IsCollapsable => _collapsed == null;
    public bool IsQuasiCollapsed => _collapsed == null && RemainingValues.Count == 1;

    public override string ToString()
    {
      return $"State ({X}/{Y}): {_collapsed?.ToString() ?? string.Join(", ", RemainingValues)}";
    }

    public WFCState(int x, int y, HashSet<int> initialValues)
    {
      this.X = x;
      this.Y = y;
      this.RemainingValues = initialValues;
      _collapsed = null;
    }

    public void RemovePossibleValue(int value) => RemainingValues.Remove(value);

    public void RemovePossibleValues(IEnumerable<int> values)
    {
      foreach(var value in values)
        RemainingValues.Remove(value);
    }

    public bool IsRemaining(int value) => RemainingValues.Contains(value);


    public void Collapse(int value) => _collapsed = value;

    public int GetValueOrDefault(int @default) => _collapsed ?? @default;
  }
}
