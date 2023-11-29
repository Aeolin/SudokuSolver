using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
	public static class Extensions
	{
		public static string Limit(this string @string, int length, char? paddingChar = ' ', bool leftPad = false)
		{
			if (@string.Length < length)
				if (paddingChar.HasValue)
					return leftPad ? @string.PadLeft(length, paddingChar.Value) : @string.PadRight(length, paddingChar.Value);

			if (@string.Length > length)
				return @string.Substring(0, length);

			return @string;
		}
	}
}
