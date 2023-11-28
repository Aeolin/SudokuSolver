using SudokuSolver.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC
{
  public class WFCValidator : IValidator<WFCState>
  {
    public bool Validate(IEnumerable<WFCState> values)
    {
      var collapsed = values.Where(x => x.IsCollapsed).Select(x => x.GetValueOrDefault(0)).ToList();
      return collapsed.Count() == collapsed.Distinct().Count();
    }
  }
}
