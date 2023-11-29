using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace SudokuSolver.Cli
{
	public partial class SudokuCli
	{
		private static readonly ConsoleColor[] GROUP_COLORS = {
			ConsoleColor.Green,
			ConsoleColor.Red,
			ConsoleColor.Blue,
			ConsoleColor.Magenta,
			ConsoleColor.DarkGreen,
			ConsoleColor.DarkYellow,
			ConsoleColor.DarkCyan,
			ConsoleColor.DarkMagenta
		};

		private void Render()
		{
			Console.Clear();
			RenderBackups(FieldHeight, BACKUP_LIST_WIDTH, 0, 0);
			//DrawVLine(BACKUP_LIST_WIDTH, 0, FieldHeight);
			RenderField(BACKUP_LIST_WIDTH+1, 0);
			DrawHLine(0, FieldHeight, FieldWidth+BACKUP_LIST_WIDTH);
			RenderLog(0, FieldHeight+1, LOG_HEIGHT, FieldWidth+BACKUP_LIST_WIDTH);
			Console.SetCursorPosition(0, FieldHeight+LOG_HEIGHT+1);
			Console.Write("> ");
		}

		private void RenderLog(int x, int y, int height, int width)
		{
			var foreground = Console.ForegroundColor;
			var lastLogs = _logs.Skip(Math.Max(0, _logs.Count - height));
			foreach (var (message, color) in lastLogs)
			{
				Console.SetCursorPosition(x, y++);
				Console.ForegroundColor = color;
				Console.Write(message.Limit(width));
			}
			Console.ForegroundColor = foreground;
		}

		private void DrawHLine(int x, int y, int length, char @char = '-')
		{
			Console.SetCursorPosition(x, y);
			Console.Write(new string(@char, length));
		}

		private void DrawVLine(int x, int y, int length, char @char = '|')
		{
			for (int i = 0; i < length; i++)
			{
				Console.SetCursorPosition(x, y+i);
				Console.Write(@char);
			}
		}

		private void RenderBackups(int height, int width, int x, int y)
		{
			var backups = _solver.Backups.ToArray();
			var lines = Math.Min(height, backups.Length);
			for (int i = 0; i < lines; i++)
			{
				var backup = backups[backups.Length - i - 1];
				Console.SetCursorPosition(x, y + i);
				Console.Write(backup.Name.Limit(width));
			}
		}

		private void RenderField(int offsetX, int offsetY)
		{
			var builder = new StringBuilder();
			var rectHeight = _solver.Height / _solver.RectanglesPerSide;
			var rectWidth = _solver.Width / _solver.RectanglesPerSide;
			var divider = new string('-', (rectWidth*2+1)*_solver.Width+_solver.Width+1);
			var halfSpacer = new string(' ', rectWidth);
			var fullSpacer = new string(' ', rectWidth*2+1);
			builder.AppendLine(divider);

			Console.SetCursorPosition(offsetX, offsetY);

			for (int y = 0; y < _solver.Height; y++)
			{
				for (int line = 0; line < rectHeight; line++)
				{
					Console.BackgroundColor = ConsoleColor.Black;
					Console.Write('|');

					for (int x = 0; x < _solver.Width; x++)
					{
						var index = _solver.Index(x, y).Value;
						if (_solver.Groups.TryGetValue(index, out var group))
						{
							Console.ForegroundColor = ConsoleColor.White;
							Console.BackgroundColor = GROUP_COLORS[group.Index % GROUP_COLORS.Length];
						}
						else
						{
							Console.BackgroundColor = ConsoleColor.Black;
						}


						var state = _solver[x, y];
						if (state.IsCollapsed)
						{
							if (line == rectHeight/2)
								Console.Write($"{halfSpacer}{ALPHABET[state.GetValueOrDefault(0)]}{halfSpacer}");
							else
								Console.Write($"{fullSpacer}");

							var bg = Console.BackgroundColor;
							Console.BackgroundColor = ConsoleColor.Black;
							Console.Write('|');
							Console.BackgroundColor = bg;
						}
						else
						{
							Console.Write(' ');
							for (int i = line*rectWidth; i < (line + 1)*rectWidth; i++)
								Console.Write(state.IsRemaining(i+1) ? ALPHABET[i+1] + " " : "  ");

							var bg = Console.BackgroundColor;
							Console.BackgroundColor = ConsoleColor.Black;
							Console.Write('|');
							Console.BackgroundColor = bg;
						}
					}

					Console.WriteLine();
					Console.SetCursorPosition(offsetX, ++offsetY);
				}

				Console.BackgroundColor = ConsoleColor.Black;
				Console.WriteLine(divider);
				Console.SetCursorPosition(offsetX, ++offsetY);
			}

			Console.BackgroundColor = ConsoleColor.Black;

		}
	}
}
