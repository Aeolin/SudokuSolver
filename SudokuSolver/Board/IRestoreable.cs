using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Board
{
	public interface IRestoreable<T>
	{
		public T Backup();
		public void Restore(T value);
	}
}
