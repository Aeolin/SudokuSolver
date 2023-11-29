using SudokuSolver;
using SudokuSolver.BruteForce;
using SudokuSolver.Cli;
using SudokuSolver.WFC;
using SudokuSolver.WFC.CollapseRules;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

internal class Program
{
	[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(SudokuPuzzle))]
	private static void Main(string[] args)
	{
		string file = null;
		if (args.Length == 1)
		{
			file = args[0];
			if (file.StartsWith('"') && file.EndsWith('"'))
				file = file[1..^1];
		}


		var cli = SudokuCli.Create(file);
		cli.Run();
	}
}