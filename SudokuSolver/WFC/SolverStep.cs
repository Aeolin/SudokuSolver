using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC
{
  public class SolverStep<T>
  {
    public int X { get; init; }
    public int Y { get; init; }
    public T Value { get; init; }
    public string Description { get; init; }


    public SolverStep(int x, int y, T value, string description)
    {
      X=x;
      Y=y;
      Value=value;
      Description=description;
    }
  }
}
