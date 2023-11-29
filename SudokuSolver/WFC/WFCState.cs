using SudokuSolver.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC
{
	public class WFCState : IRestoreable<WFCStateBackup>
  {
    public int X { get; init; }
    public int Y { get; init; }
    public string Id { get; init; }
    private int? _collapsed;
    public HashSet<int> RemainingValues { get; private set; }

    public bool HasRemaining => RemainingValues.Count > 1 && IsCollapsed == false;

    public bool IsCollapsed => _collapsed != null;
    public bool IsCollapsable => _collapsed == null;
    public bool IsQuasiCollapsed => _collapsed == null && RemainingValues.Count == 1;

		public override bool Equals(object obj)
		{
      if (obj is WFCState other)
        return other.X == X && other.Y == Y;

			return base.Equals(obj);
		}



		public override string ToString()
    {
      return $"State ({Id}): {_collapsed?.ToString() ?? string.Join(", ", RemainingValues)}";
    }

    public WFCState(int x, int y, HashSet<int> initialValues)
    {
      this.Id = $"{x}/{y}";
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

		public void RemovePossibleValues(params int[] values)
		{
			foreach (var value in values)
				RemainingValues.Remove(value);
		}

		public bool IsRemaining(int value) => RemainingValues.Contains(value);


    public void Collapse(int value) => _collapsed = value;

    public int GetValueOrDefault(int @default) => _collapsed ?? @default;

    public WFCStateBackup Backup() => new WFCStateBackup { States = new HashSet<int>(RemainingValues), Collapsed = _collapsed };

		public void Restore(WFCStateBackup backup)
		{
      RemainingValues = backup.States;
      _collapsed = backup.Collapsed;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y);
		}
	}
}
