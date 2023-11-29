using SudokuSolver.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.BruteForce
{
	public class BruteForceValidator : IValidator<BruteForceState>
	{
		public bool Validate(IEnumerable<BruteForceState> values)
		{
			var setItems = values.Where(x => x.Value.HasValue).Select(x => x.Value.Value).ToList();
			return setItems.Count() == setItems.Distinct().Count();
		}
	}
}
