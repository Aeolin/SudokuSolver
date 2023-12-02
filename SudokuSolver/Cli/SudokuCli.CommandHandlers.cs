using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SudokuSolver.Cli
{
	public partial class SudokuCli
	{
		private void HandlePushCommand(Match match)
		{
			var name = match.Groups[1].Value;
			_solver.PushState(name);
			AppendLog($"Pushed state {name}", ConsoleColor.Yellow);
		}

		private void HandlePopCommand(Match match)
		{
			var name = match.Groups[1].Value;
			var next = _solver.Backups.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
			if (next == null)
			{
				AppendLog($"No state {name} found", ConsoleColor.Red);
			}
			else
			{
				_solver.Restore(next);
				AppendLog($"Restored state {name}", ConsoleColor.Green);
			}
		}

		private void HandleSaveCommand(Match match)
		{
			var fileName = match.Groups[1].Success ? match.Groups[1].Value : null;
			if (fileName != null)
				if (Path.IsPathRooted(fileName) == false)
					fileName = $"./saves/{fileName}";

			fileName ??= $"./saves/{_puzzle.Name.Replace(' ', '-')}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.json";
			if (fileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase) == false)
				fileName += ".json";

			var parent = Directory.GetParent(fileName);
			Directory.CreateDirectory(parent.FullName);
			var model = _puzzle.Clone();
			model.Backups = _solver.Backups.Select(x => new BackupModel
			{
				Number = x.Number,
				Name = x.Name,
				States = x.Backup.ToArray()
			}).ToArray();
			model.Puzzle = _solver.Cells.Select(x => x.GetValueOrDefault(0)).ToArray();
			if (File.Exists(fileName))
			{
				AppendLog($"File with name {fileName} already exists", ConsoleColor.Red);
				return;
			}

			File.WriteAllText(fileName, JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true }));
			AppendLog($"Saved as {Path.GetFileName(fileName)}", ConsoleColor.Yellow);
		}

		private void HandleUndoCommand(Match match)
		{
			if (_solver.Backups.Count() == 0)
			{
				AppendLog("Nothing left to undo", ConsoleColor.Red);
				return;
			}

			var count = match.Groups[1].Success ? int.Parse(match.Groups[1].Value) : 1;
			var min = Math.Min(count, _solver.Backups.Count());
			for (int i = 0; i < min; i++)
				_solver.PopState();

			AppendLog($"Undid {min} states", ConsoleColor.Yellow);

		}

		private void HandleSolveCommand(Match match)
		{
			_solver.PushState();
			var count = match.Groups[1].Success ? int.Parse(match.Groups[1].Value) : -1;
			int solved = 0;
			while ((count > 0 || count == -1) && _solver.TrySolveStep(out var step))
			{
				if (step.Value.HasValue)
				{
					solved++;
					_solver.Collapse(step.X, step.Y, step.Value.Value);
					AppendLog(step.Description, ConsoleColor.Gray);
				}
				if (count > 0)
					count--;
			}

			AppendLog($"Solved {solved} steps", solved == 0 ? ConsoleColor.Yellow : ConsoleColor.Gray);
		}

		private void HandleEliminateCommand(Match match)
		{
			var from = match.Groups[1].Value.ToUpper();
			if (int.TryParse(match.Groups[2].Value, out var value) == false)
				AppendLog("Invalid number", ConsoleColor.Red);

			_solver.PushState();
			// row + col
			if(from.Length == 2)
			{
				var row = _solver.Width - 1 - ROWS.IndexOf(from[0]);
				var col = int.Parse(from[1].ToString()) - 1;
				_solver[col, row].RemovePossibleValue(value);
				AppendLog($"Removed {value} from cell ({from[0]},{from[1]})");
			}
			else if (char.IsDigit(from[0]))
			{
				var col = int.Parse(from) - 1;
				for (int i = 0; i < _solver.Width; i++)
					_solver[col, i].RemovePossibleValue(value);
				AppendLog($"Removed {value} from column {from}");

			}
			else if (char.IsLetter(from[0]))
			{
				var row = _solver.Width - 1 - ROWS.IndexOf(from[0]);
				for(int i= 0; i < _solver.Height; i++)
					_solver[i, row].RemovePossibleValue(value);
				AppendLog($"Removed {value} from row {row}");
			}
		}

		private void HandleSetCommand(Match match)
		{
			var y = _solver.Width - 1 - ROWS.IndexOf(match.Groups[1].Value.ToUpper());
			var x = int.Parse(match.Groups[2].Value) - 1;
			var value = int.Parse(match.Groups[3].Value);
			var force = match.Groups[4].Value == "!";
			if (y < 0 || y >= _solver.Height)
			{
				AppendLog($"Invalid row {match.Groups[1].Value.ToUpper()}", ConsoleColor.Red);
				return;
			}

			if (x < 0 || x >= _solver.Width)
			{
				AppendLog($"Invalid column {x+1}", ConsoleColor.Red);
				return;
			}

			if (value < 0 || value > _solver.Width)
			{
				AppendLog($"Invalid value {value}", ConsoleColor.Red);
				return;
			}

			if (_solver[x, y].IsCollapsed && force == false)
			{
				AppendLog("Cell is already collapsed", ConsoleColor.Red);
				return;
			}

			if (_solver[x, y].RemainingValues.Contains(value) == false && force == false)
			{
				AppendLog("Value is not possible", ConsoleColor.Red);
				return;
			}

			_solver.PushState();
			_solver.Collapse(x, y, value);
			AppendLog(match.Value, ConsoleColor.Gray);
		}

	}
}
