using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Board
{
	public abstract class AbstractBoard<T, R> where T : IRestoreable<R>
	{
		public int Width { get; init; }
		public int Height { get; init; }
		private AbstractElementFactory<T, R> _factory;
		private readonly T[] _values;
		private readonly Dictionary<int, AbstractElementGroup<T>> _groups = new Dictionary<int, AbstractElementGroup<T>>();
		public IReadOnlyDictionary<int, AbstractElementGroup<T>> Groups => _groups;
		private readonly Stack<AbstractBackup<R>> _restoreFrames;
		public IEnumerable<AbstractBackup<R>> Backups => _restoreFrames;

		public void PushState(string name = null)
		{
			var backup = new R[_values.Length];
			for (int i = 0; i < backup.Length; i++)
				backup[i] = _values[i].Backup();

			_restoreFrames.Push(new AbstractBackup<R>(backup, _restoreFrames.Count+1, name));
			Console.WriteLine($"Pushed Backtracking State #{_restoreFrames.Count}");
		}

		public bool PopState()
		{
			if (_restoreFrames.Count == 0)
				return false;

			Console.WriteLine($"Popped Backtracking State #{_restoreFrames.Count}");
			var backup = _restoreFrames.Pop();
			for (int i = 0; i <  _values.Length; i++)
				_values[i].Restore(backup.Backup[i]);

			return true;
		}

		public void Restore(AbstractBackup<R> backup)
		{
			while (_restoreFrames.Pop() != backup) ;
			_restoreFrames.Pop();
			for (int i = 0; i < _values.Length; i++)
				_values[i].Restore(backup.Backup[i]);	
		}

		public AbstractBoard(IEnumerable<T> values, AbstractElementFactory<T, R> factory, int width, int height)
		{
			this.Width = width;
			this.Height = height;
			this._values = values.ToArray();
			this._restoreFrames = new Stack<AbstractBackup<R>>();

			if (_values.Length != Width*Height)
				throw new ArgumentException($"Passed array length {_values.Length} mistmatches board size {Width}x{Height} = {Width*Height}");
			_factory=factory;

		}

		public AbstractBoard(Func<int, int, T> initializer, AbstractElementFactory<T, R> factory, int width, int height) : this(Enumerable.Range(0, width*height).Select(pos => initializer(pos % width, pos / width)), factory, width, height)
		{

		}

		public T[] Cells => _values;

		public T this[int x, int y] => (x >= 0 && x < Width && y >= 0 && y < Height) ? this[Index(x, y).Value] : default;
		public T this[int index] => index >= 0 && index < _values.Length ? Cells[index] : default;

		public T Get(int x, int y) => _values[Index(x, y).Value];

		public int? Index(int x, int y) => (x >= 0 && x < Width && y >= 0 && y < Height) ? y*Width+x : null;

		public void Set(int x, int y, T value) => _values[y*Width+x] = value;

		public void Iterate(Action<int, int, T> function)
		{
			for (int y = 0; y < Height; y++)
				for (int x = 0; x < Width; x++)
					function(x, y, Get(x, y));
		}

		public virtual AbstractBoard<T, R> WithGroup(HashSet<int> group)
		{
			var elements = group.Select(x => Cells[x]).ToHashSet();
			var egroup = new AbstractElementGroup<T>(elements);
			foreach (var i in group)
				_groups.Add(i, egroup);

			return this;
		}

		public virtual AbstractBoard<T, R> WithGroup(HashSet<(int x, int y)> group)
		{
			var elements = group.Select(x => this[x.x, x.y]).ToHashSet();
			var egroup = new AbstractElementGroup<T>(elements);
			foreach (var i in group)
				_groups.Add(Index(i.x, i.y).Value, egroup);

			return this;
		}

		public void Replace(Func<int, int, T, T> replaceFunction) => Iterate((x, y, v) => Set(x, y, replaceFunction(x, y, v)));

		public IEnumerable<AbstractElement<T>> Elements() => _factory.Elements(this);

		public IEnumerable<AbstractElement<T>> ElementsForCell(int x, int y) => _factory.Elements(this, x, y);
	}
}
