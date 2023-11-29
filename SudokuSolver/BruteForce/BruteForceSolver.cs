using SudokuSolver.Board;
using SudokuSolver.Sudoku;
using SudokuSolver.WFC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.BruteForce
{
	public class BruteForceSolver : AbstractBoard<BruteForceState, int?>
	{
		public int[] ALL_VALUES { get; init; }
		public int RectanglesPerSide { get; init; }
		private const string Alphabet = " 123456789";

		public BruteForceSolver(IEnumerable<BruteForceState> values, AbstractElementFactory<BruteForceState, int?> factory, int width, int height, int rectsPerSide) : base(values, factory, width, height)
		{
			ALL_VALUES = Enumerable.Range(1, width*height).ToArray();
			RectanglesPerSide = rectsPerSide;
		}

		public BruteForceSolver(Func<int, int, BruteForceState> initializer, IValidator<BruteForceState> validator, int width, int height, int rectsPerSide) : base(initializer, new SudokuElementFactory<BruteForceState, int?>(rectsPerSide, validator), width, height)
		{
			ALL_VALUES = Enumerable.Range(1, width*height).ToArray();
			RectanglesPerSide = rectsPerSide;
		}

		IEnumerable<BruteForceState> GetNeighbours(int x, int y)
		{
			var left = Index(x-1, y);
			if (left.HasValue)
				yield return Cells[left.Value];

			var top = Index(x, y-1);
			if (top.HasValue)
				yield return Cells[top.Value];

			var right = Index(x+1, y);
			if (right.HasValue)
				yield return Cells[right.Value];

			var bottom = Index(x, y+1);
			if (bottom.HasValue)
				yield return Cells[bottom.Value];
		}

		public bool ValidateConsecutive()
		{
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					if (this[x, y].Value.HasValue)
					{
						var smaller = this[x, y].Value.Value - 1;
						var bigger = this[x, y].Value.Value + 1;
						if (GetNeighbours(x, y).Any(x => x.Value == smaller || x.Value == bigger))
							return false;
					}
				}
			}

			return true;
		}

		public bool Validate() => ValidateConsecutive() && Elements().All(x => x.Validate());

		public bool IsSolved() => Cells.All(x => x.Value.HasValue);


		public static BruteForceSolver FromList(IEnumerable<int> board, int width, int height, int rectsPerSide, Action<BruteForceSolver> configure = null)
		{
			var arr = board.ToArray();
			var solver = new BruteForceSolver((x, y) => new BruteForceState(x, y), new BruteForceValidator(), width, height, rectsPerSide);
			configure?.Invoke(solver);
			var countExpected = width*height;
			if (arr.Length != countExpected)
				throw new ArgumentException($"board is expected to have {countExpected} elements");

			for (int i = 0; i < arr.Length; i++)
			{
				if (arr[i] > 0)
				{
					solver[i].Value = arr[i];
					solver[i].Fixed = true;
				}
			}

			return solver;
		}

		public override BruteForceSolver WithGroup(HashSet<int> group)
		{
			base.WithGroup(group);
			return this;
		}

		public override BruteForceSolver WithGroup(HashSet<(int x, int y)> group)
		{
			base.WithGroup(group);
			return this;
		}

		public BruteForceSolver WithGroup(params int[] group) => WithGroup(new HashSet<int>(group));
		public BruteForceSolver WithGroup(params (int x, int y)[] group) => WithGroup(new HashSet<(int, int)>(group));

		public bool Solve(out long steps)
		{
			steps = 0;
			var backtracking = new Stack<int>();
			for (int i = 0; i < Width*Height; i++)
			{
				steps++;
				if (this[i].Fixed)
					continue;

				if (this[i].Value.HasValue == false)
				{
					this[i].Value = 1;
					backtracking.Push(i);
					PushState();
				}
				else if (this[i].AllTried == false)
				{
					this[i].Value++;
					backtracking.Push(i);
					PushState();
				}

				if (Validate() == false)
				{
					if (PopState() == false)
						return false;

					i = backtracking.Pop() - 1;
				}
				else if (IsSolved())
				{
					return true;
				}

				Console.WriteLine($"Step #{steps}");
				//Console.WriteLine(Print());
			}

			return Validate();
		}

		public string Print()
		{
			var builder = new StringBuilder();
			var rectHeight = Height / RectanglesPerSide;
			var rectWidth = Width / RectanglesPerSide;
			var divider = "+" + string.Join('+', Enumerable.Range(0, RectanglesPerSide).Select(x => new string('-', rectWidth*2+1))) + "+";
			for (int y = 0; y < Height; y++)
			{
				if (y % rectHeight == 0)
					builder.AppendLine(divider);

				for (int x = 0; x < Width; x++)
				{
					if (x % rectWidth == 0)
						builder.Append("| ");

					builder.Append(Alphabet[Get(x, y).GetValueOrDefault(0)]+" ");
				}

				builder.AppendLine("|");
			}

			builder.AppendLine(divider);
			return builder.ToString();
		}
	}
}
