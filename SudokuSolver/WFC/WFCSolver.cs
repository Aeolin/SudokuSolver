using SudokuSolver.Board;
using SudokuSolver.WFC.SolverRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.WFC
{
	public class WFCSolver : AbstractBoard<WFCState, WFCStateBackup>
	{
		private readonly int[] ALL_VALUES;
		private readonly List<ISolverRule<WFCState, int?>> solverRules = new List<ISolverRule<WFCState, int?>>();
		private string Alphabet { get; set; } = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩαβγδεζηθικλμνξοπρστυφχψωБГДЁЖЗИЙЛМНОПРУФХЦЧШЩЪЫЬЭЮЯ";
		public int RectanglesPerSide { get; init; }
		public int RectangleCellCount => (Width / RectanglesPerSide)*(Height/RectanglesPerSide);

		public WFCSolver(IEnumerable<WFCState> values, IValidator<WFCState> validator, int width, int height, int rectsPerSide) : base(values, new SudokuElementFactory<WFCState, WFCStateBackup>(rectsPerSide, validator), width, height)
		{
			ALL_VALUES = Enumerable.Range(1, width*height).ToArray();
			RectanglesPerSide = rectsPerSide;
		}

		public WFCSolver(Func<int, int, WFCState> initializer, IValidator<WFCState> validator, int width, int height, int rectsPerSide) : base(initializer, new SudokuElementFactory<WFCState, WFCStateBackup>(rectsPerSide, validator), width, height)
		{
			ALL_VALUES = Enumerable.Range(1, width*height).ToArray();
			RectanglesPerSide = rectsPerSide;
		}

		public static WFCSolver FromList(IEnumerable<int> board, int width, int height, int rectsPerSide)
		{
			var arr = board.ToArray();
			var values = Enumerable.Range(1, rectsPerSide*rectsPerSide).ToArray();
			var solver = new WFCSolver((x, y) => new WFCState(x, y, new HashSet<int>(values)), new WFCValidator(), width, height, rectsPerSide);
			solver.WithSolverRule(new OnlyLeftInElementRule(solver)).WithSolverRule(new QuasiCollapsedRule()).WithSolverRule(new SameRemainingInElementRule(solver));
			var countExpected = width*height;
			if (arr.Length != countExpected)
				throw new ArgumentException($"board is expected to have {countExpected} elements");

			for (int i = 0; i < arr.Length; i++)
				if (arr[i] > 0)
					solver.Collapse(i % width, i / width, arr[i]);

			return solver;
		}

		public bool TrySolveStep(out SolverStep<int?> step)
		{
			var elements = Elements().ToArray();
			foreach (var rule in solverRules.OrderBy(x => x.Cost))
				if (rule.TrySolve(elements, out step))
					return true;

			step = null;
			return false;
		}

		public bool Solve(out int steps, bool muteOutput = false)
		{
			steps = 0;
			while (Cells.Any(x => x.HasRemaining) || PopState())
			{
				while (TrySolveStep(out SolverStep<int?> step) && (IsSolved() == false))
				{
					if (step.Value.HasValue)
						Collapse(step.X, step.Y, step.Value.Value);

					steps++;
					if (muteOutput == false)
					{
						Console.WriteLine($"Step #{steps}: {step.Description}");
						Console.WriteLine(Print());
					}

					if (Validate() == false)
					{
						if (PopState())
						{
							continue;
						}
						else
						{
							return false;
						}
					}
				}

				// Backtrack
				var nextState = Cells.Where(x => x.HasRemaining).OrderBy(x => x.RemainingValues.Count).FirstOrDefault();
				if (nextState != null)
				{
					var value = nextState.RemainingValues.First();
					Console.WriteLine($"Collapsed to {value} for {nextState}");
					nextState.RemovePossibleValue(value);
					PushState();
					Collapse(nextState.X, nextState.Y, value);
				}
			}

			return IsSolved();
		}

		public WFCSolver WithSolverRule(ISolverRule<WFCState, int?> rule)
		{
			solverRules.Add(rule);
			return this;
		}

		public void Reset()
		{
			this.Replace((x, y, v) => new WFCState(x, y, new HashSet<int>(ALL_VALUES)));
		}

		public void Collapse(int x, int y, int value)
		{
			var state = Get(x, y);
			state.Collapse(value);
			foreach (var eState in ElementsForCell(x, y).SelectMany(x => x.GetValues()).Where(x => x.IsCollapsable && x != state))
				eState.RemovePossibleValue(value);
		}

		public void RemovePossibleValue(int x, int y, int value) => Get(x, y).RemovePossibleValue(value);

		public bool Validate() => Elements().All(x => x.Validate());

		public bool IsSolved() => Cells.All(x => x.IsCollapsed);

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

		public string PrintWithStates()
		{
			var builder = new StringBuilder();
			var rectHeight = Height / RectanglesPerSide;
			var rectWidth = Width / RectanglesPerSide;
			var divider = new string('-', (rectWidth*2+1)*Width+Width+1);
			var halfSpacer = new string(' ', rectWidth);
			var fullSpacer = new string(' ', rectWidth*2+1);
			builder.AppendLine(divider);

			for (int y = 0; y < Height; y++)
			{
				for (int line = 0; line < rectHeight; line++)
				{
					builder.Append('|');
					for (int x = 0; x < Width; x++)
					{
						var state = Get(x, y);
						if (state.IsCollapsed)
						{
							if (line == rectHeight/2)
								builder.Append($"{halfSpacer}{Alphabet[state.GetValueOrDefault(0)]}{halfSpacer}|");
							else
								builder.Append($"{fullSpacer}|");
						}
						else
						{
							builder.Append(' ');
							for (int i = line*rectWidth; i < (line + 1)*rectWidth; i++)
								builder.Append(state.IsRemaining(i) ? Alphabet[i+1] + " " : "  ");
							builder.Append('|');
						}
					}

					builder.AppendLine();
				}

				builder.AppendLine(divider);
			}

			return builder.ToString();
		}

	}
}