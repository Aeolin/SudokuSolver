using SudokuSolver.WFC;
using SudokuSolver.WFC.CollapseRules;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SudokuSolver.Cli
{
	public partial class SudokuCli
	{
		private const string ALPHABET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩαβγδεζηθικλμνξοπρστυφχψωБГДЁЖЗИЙЛМНОПРУФХЦЧШЩЪЫЬЭЮЯ";
		private const string ROWS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";


		private static readonly Regex MATCH_SET_COMMAND = new Regex(@$"^([{ROWS}])(\d{{1,2}})=(\d)(!?)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private static readonly Regex MATCH_UNDO_COMMAND = new Regex(@"^undo(?:\s+(\d+))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private static readonly Regex MATCH_SOLVE_COMMAND = new Regex(@"^solve(?:\s+(\d+))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private static readonly Regex MATCH_PUSH_COMMAND = new Regex(@"^push(?:\s+(\S+))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private static readonly Regex MATCH_POP_COMMAND = new Regex(@"^pop(?:\s+(\S+))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private static readonly Regex MATCH_SAVE_COMMAND = new Regex(@"^save(?:\s+(.*))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private static readonly Regex MATCH_ELIMINATE_COMMAND = new Regex(@"^-([a-z,1-9]{1,2})=([0-9])", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		private WFCSolver _solver;
		private SudokuPuzzle _puzzle;
		private List<(string message, ConsoleColor color)> _logs = new List<(string message, ConsoleColor color)>();
		private const int BACKUP_LIST_WIDTH = 15;
		private const int LOG_HEIGHT = 15;
		private int FieldHeight { get; set; }
		private int FieldWidth { get; set; }
		private readonly Dictionary<Regex, Action<Match>> _handlers;

		public SudokuCli(SudokuPuzzle puzzle)
		{
			var wfc = WFCSolver.FromList(puzzle.Puzzle, puzzle.Width, puzzle.Height, puzzle.RectsPerSquare, solver =>
			{
				foreach (var group in puzzle.Groups)
				{
					solver.WithGroup(group);
				}

				foreach(var backup in puzzle.Backups.OrderBy(x => x.Number))
				{
					solver.AddState(backup.Name, backup.Number, backup.States);
				}

				if (puzzle.ExtraRules.Contains("NonConsecutiveNeighbours"))
					solver.WithCollapseRule(new CollapseConsecutiveNeighbours());

			});

			_puzzle = puzzle;
			_solver = wfc;
			_handlers = new Dictionary<Regex, Action<Match>>
			{
				{ MATCH_SET_COMMAND, HandleSetCommand },
				{ MATCH_UNDO_COMMAND, HandleUndoCommand },
				{ MATCH_SOLVE_COMMAND, HandleSolveCommand },
				{ MATCH_PUSH_COMMAND, HandlePushCommand },
				{ MATCH_POP_COMMAND, HandlePopCommand },
				{ MATCH_SAVE_COMMAND, HandleSaveCommand },
				{ MATCH_ELIMINATE_COMMAND, HandleEliminateCommand }
			};
		}

		public static SudokuCli Create(string path = null)
		{
			if (path == null)
			{
				Console.Write("Path: ");
				path = Console.ReadLine();
			}

			var puzzle = JsonSerializer.Deserialize<SudokuPuzzle>(File.ReadAllText(path));
			return new SudokuCli(puzzle);
		}

		private void Init()
		{
			var rectWidth = _solver.Width / _solver.RectanglesPerSide;
			var rectHeight = _solver.Height / _solver.RectanglesPerSide;
			var width = 1 + ((rectWidth*2+1)*_solver.Width+_solver.Width+1);
			var height = 1 + _solver.Height*(rectHeight+1);
			FieldHeight = height-2;
			FieldWidth = width;
			Console.SetWindowSize(FieldWidth+1+BACKUP_LIST_WIDTH, FieldHeight+2+LOG_HEIGHT);
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				Console.SetBufferSize(FieldWidth+1+BACKUP_LIST_WIDTH, FieldHeight+2+LOG_HEIGHT);
		}

		public void Run()
		{
			Init();
			while (true)
			{
				Render();
				var command = Console.ReadLine();
				if (command.Equals("exit", StringComparison.OrdinalIgnoreCase))
					return;

				bool handled = false;
				foreach (var regex in _handlers.Keys)
				{
					var match = regex.Match(command);
					if (match.Success)
					{
						handled = true;
						_handlers[regex](match);
						break;
					}
				}

				if (handled == false)
					AppendLog($"Unknown Command: {command}", ConsoleColor.Red);
			}
		}


		private void AppendLog(string message, ConsoleColor color = ConsoleColor.White)
		{
			_logs.Add((message, color));
		}



	}
}
