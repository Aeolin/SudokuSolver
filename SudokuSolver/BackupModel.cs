using SudokuSolver.WFC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
	public class BackupModel
	{
		public int Number { get; set; }
		public string Name { get; set; }
		public WFCStateBackup[] States { get; set; }
	}
}
