﻿using SudokuSolver;
using SudokuSolver.BruteForce;
using SudokuSolver.Cli;
using SudokuSolver.WFC;
using SudokuSolver.WFC.CollapseRules;
using System.Diagnostics;

var sudoku16_killer_1 = new[]
{
		5, 13, 0, 0, 6, 10, 0, 9, 1, 11, 0, 0, 0, 0, 8, 15,
		0, 14, 0, 0, 1, 11, 0, 0, 13, 0, 0, 0, 0, 10, 0, 0,
		0, 10, 9, 0, 0, 0, 0, 5, 0, 8, 15, 0, 0, 11, 0, 0,
		0, 11, 4, 0, 14, 8, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0,
		3, 2, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 14, 8,
		0, 0, 0, 15, 0, 0, 4, 0, 2, 13, 0, 5, 0, 6, 10, 9,
		0, 0, 0, 12, 0, 0, 5, 3, 7, 0, 0, 15, 0, 0, 0, 0,
		0, 0, 0, 16, 0, 0, 15, 0, 0, 0, 9, 12, 2, 13, 3, 5,
		0, 0, 2, 3, 9, 0, 10, 0, 4, 0, 0, 0, 0, 0, 0, 14,
		11, 0, 0, 0, 0, 0, 0, 14, 0, 6, 10, 9, 5, 0, 13, 3,
		0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 4, 16, 1, 0,
		14, 0, 0, 8, 4, 0, 0, 1, 5, 0, 0, 3, 9, 0, 0, 0,
		0, 0, 0, 0, 10, 9, 0, 0, 11, 4, 0, 1, 0, 0, 0, 0,
		0, 8, 0, 0, 11, 4, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0,
		0, 9, 0, 0, 0, 5, 0, 0, 8, 0, 0, 0, 11, 4, 0, 0,
		0, 4, 0, 0, 8, 15, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0
};

var sudoku16_killer_1_missing = new[]
{
		5, 13, 0, 0, 6, 10, 0, 9, 1, 11, 0, 0, 0, 0, 8, 15,
		0, 14, 0, 0, 1, 11, 0, 0, 13, 0, 0, 0, 0, 10, 0, 0,
		0, 10, 9, 0, 0, 0, 0, 5, 0, 8, 15, 0, 0, 11, 0, 0,
		0, 11, 4, 0, 14, 8, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0,
		3, 2, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 14, 8,
		0, 0, 0, 15, 0, 0, 4, 0, 2, 13, 0, 5, 0, 6, 10, 9,
		0, 0, 0, 12, 0, 0, 5, 3, 7, 0, 0, 15, 0, 0, 0, 0,
		0, 0, 0, 16, 0, 0, 15, 0, 0, 0, 9, 12, 2, 13, 3, 5,
		0, 0, 2, 3, 9, 0, 10, 0, 4, 0, 0, 0, 0, 0, 0, 14,
		11, 0, 0, 0, 0, 0, 0, 14, 0, 6, 10, 9, 5, 0, 13, 3,
		0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 4, 16, 1, 0,
		14, 0, 0, 8, 4, 0, 0, 1, 5, 0, 0, 3, 9, 0, 0, 0,
		0, 0, 0, 0, 10, 9, 0, 0, 11, 4, 0, 1, 0, 0, 0, 0,
		0, 8, 0, 0, 11, 4, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0,
		0, 9, 0, 0, 0, 5, 0, 0, 8, 0, 0, 0, 11, 4, 0, 0,
		0, 4, 0, 0, 8, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
};

var sudoku36 = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 33, 0, 6, 0, 35, 0, 0, 23, 0, 25, 0, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 35, 0, 19, 25, 16, 5, 1, 22, 2, 18, 36, 31, 27, 10, 8, 30, 12, 24, 29, 34, 0, 11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 27, 23, 34, 8, 0, 6, 9, 0, 5, 0, 31, 32, 19, 12, 4, 21, 13, 3, 20, 7, 0, 18, 0, 15, 26, 0, 30, 35, 36, 10, 0, 0, 0, 0, 0, 0, 30, 15, 35, 10, 28, 12, 31, 0, 13, 20, 26, 0, 34, 25, 24, 9, 17, 32, 0, 1, 22, 2, 0, 8, 6, 27, 7, 19, 29, 3, 0, 0, 0, 0, 0, 0, 28, 7, 19, 22, 18, 24, 0, 32, 11, 0, 16, 30, 21, 0, 10, 15, 0, 29, 14, 4, 0, 35, 1, 0, 31, 36, 20, 26, 2, 33, 0, 0, 0, 0, 0, 0, 33, 19, 13, 0, 0, 30, 0, 6, 0, 0, 0, 0, 0, 0, 34, 10, 0, 0, 0, 0, 0, 0, 26, 0, 2, 0, 0, 16, 28, 32, 0, 0, 0, 0, 0, 23, 0, 35, 5, 0, 0, 0, 4, 31, 14, 0,
						10, 19, 22, 0, 28, 6, 0, 33, 18, 29, 0, 32, 21, 7, 0, 0, 0, 11, 20, 0, 12, 0, 0, 0, 0, 0, 6, 31, 32, 28, 0, 18, 0, 24, 0, 0, 7, 25, 35, 27, 0, 0, 36, 23, 17, 5, 0, 0, 20, 0, 14, 0, 4, 29, 26, 19, 0, 0, 0, 0, 0, 2, 29, 34, 0, 0, 32, 0, 7, 16, 3, 13, 36, 9, 33, 17, 0, 0, 20, 11, 35, 14, 28, 6, 15, 24, 0, 18, 0, 0, 30, 21, 5, 0, 0, 0, 0, 20, 0, 0, 30, 29, 22, 13, 23, 0, 34, 18, 6, 26, 0, 15, 0, 0, 7, 0, 3, 12, 2, 28, 0, 36, 25, 16, 35, 14, 0, 0, 33, 0, 0, 0, 0, 14, 1, 18, 17, 0, 11, 0, 25, 10, 33, 8, 0, 2, 0, 30, 32, 16, 26, 0, 4, 0, 27, 23, 9, 31, 0, 29, 0, 3, 7, 35, 6, 0, 0, 0, 32, 27, 0, 16, 0, 0, 0, 0, 12, 22, 24, 0, 0, 0, 3, 0, 7, 14, 0, 28, 0, 0, 0, 20, 35, 18, 0, 0, 0, 0, 17, 0, 13, 26, 0, 0, 0, 13, 8, 17, 23, 0, 29, 3, 14, 18, 0, 0, 0, 0, 28, 33, 25, 24, 21, 4, 0, 0, 0, 0, 27, 22, 1, 26, 0, 20, 19, 15, 36, 0, 0, 0, 36, 24, 18, 0, 10, 0, 17, 19, 15, 1, 4, 0, 0, 21, 0, 13,
						9, 26, 6, 0, 27, 0, 0, 7, 28, 29, 8, 33, 0, 23, 0, 16, 14, 32, 0, 0, 0, 3, 20, 9, 6, 0, 26, 33, 36, 0, 0, 27, 19, 0, 14, 10, 22, 11, 25, 15, 0, 18, 35, 0, 0, 2, 16, 31, 0, 21, 24, 12, 28, 0, 0, 0, 26, 19, 15, 4, 0, 0, 0, 27, 20, 21, 28, 0, 18, 1, 31, 36, 6, 13, 9, 17, 12, 23, 0, 14, 24, 11, 30, 0, 0, 0, 35, 8, 29, 2, 0, 0, 0, 12, 2, 29, 28, 7, 10, 0, 0, 0, 35, 24, 4, 17, 32, 5, 26, 19, 1, 30, 22, 16, 20, 34, 0, 0, 0, 3, 21, 9, 18, 6, 11, 0, 0, 0, 0, 7, 35, 6, 14, 9, 3, 0, 0, 0, 22, 11, 15, 4, 10, 16, 1, 27, 24, 20, 19, 13, 25, 17, 0, 0, 0, 28, 2, 31, 21, 36, 18, 0, 0, 0, 27, 31, 11, 30, 0, 0, 0, 29, 32, 14, 21, 0, 22, 28, 24, 9, 2, 3, 15, 18, 1, 10, 0, 8, 16, 26, 35, 0, 0, 0, 33, 13, 34, 23, 0, 0, 0, 1, 22, 10, 26, 0, 19, 28, 16, 0, 0, 31, 35, 0, 8, 34, 14, 2, 12, 21, 0, 33, 4, 0, 0, 27, 24, 7, 0, 6, 5, 29, 20, 0, 0, 0, 16, 4, 9, 0, 18, 0, 23, 25, 33, 15, 10, 0, 0, 7, 0, 26,
						13, 35, 11, 0, 5, 0, 0, 1, 6, 21, 22, 34, 0, 2, 0, 28, 24, 19, 0, 0, 0, 28, 3, 21, 36, 0, 13, 8, 26, 35, 0, 0, 0, 0, 12, 23, 29, 17, 16, 6, 0, 0, 0, 0, 25, 4, 20, 14, 0, 32, 11, 1, 9, 0, 0, 0, 23, 33, 0, 24, 0, 0, 0, 0, 2, 4, 27, 0, 0, 0, 25, 0, 5, 28, 0, 8, 0, 0, 0, 30, 31, 10, 0, 0, 0, 0, 3, 0, 35, 16, 0, 0, 0, 25, 21, 33, 3, 0, 4, 0, 18, 7, 36, 22, 0, 10, 0, 19, 12, 5, 28, 0, 9, 0, 23, 29, 2, 30, 0, 24, 0, 15, 1, 17, 16, 0, 0, 0, 0, 6, 0, 0, 2, 23, 31, 1, 28, 0, 9, 26, 5, 13, 0, 29, 0, 0, 22, 0, 10, 27, 18, 16, 0, 25, 34, 4, 15, 24, 0, 0, 21, 0, 0, 0, 0, 35, 17, 27, 0, 0, 24, 0, 13, 33, 2, 4, 9, 36, 11, 31, 0, 0, 32, 16, 26, 6, 3, 22, 7, 1, 0, 10, 0, 0, 25, 34, 23, 0, 0, 0, 0, 0, 16, 5, 29, 27, 0, 10, 0, 11, 0, 0, 2, 34, 18, 14, 0, 0, 33, 24, 15, 31, 0, 0, 32, 0, 36, 0, 26, 4, 9, 30, 0, 0, 0, 0, 0, 34, 0, 22, 9, 0, 0, 0, 30, 3, 29, 0, 25, 27, 23, 0, 35, 1, 0, 13, 2, 20, 0,
						19, 5, 28, 0, 0, 0, 10, 6, 0, 26, 0, 0, 0, 0, 0, 14, 36, 4, 0, 0, 35, 0, 5, 0, 0, 0, 0, 0, 0, 16, 8, 0, 0, 0, 0, 0, 0, 3, 0, 13, 0, 0, 18, 27, 2, 0, 0, 0, 0, 0, 0, 23, 13, 8, 4, 30, 7, 0, 34, 25, 0, 17, 35, 26, 0, 31, 29, 0, 2, 6, 32, 0, 36, 18, 0, 27, 15, 11, 12, 14, 20, 0, 0, 0, 0, 0, 0, 12, 26, 15, 17, 21, 14, 27, 0, 32, 10, 11, 0, 13, 20, 18, 30, 35, 9, 0, 25, 36, 33, 0, 3, 7, 2, 24, 8, 16, 22, 0, 0, 0, 0, 0, 0, 31, 32, 11, 3, 0, 16, 6, 0, 1, 0, 14, 33, 4, 7, 27, 23, 10, 22, 13, 21, 0, 25, 0, 35, 19, 0, 29, 36, 34, 24, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 11, 19, 26, 25, 23, 24, 36, 28, 8, 20, 3, 12, 16, 15, 17, 31, 22, 9, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 32, 0, 5, 0, 22, 0, 0, 14, 0, 34, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


var sudoku81 = new[]
		{ 0, 0, 0, 0, 62, 28, 25, 0, 20, 0, 65, 63, 74, 52, 72, 69, 73, 14, 29, 38, 0, 79, 0, 59, 22, 3, 16, 0, 27, 44, 7, 5, 60, 33, 40, 81, 49, 48, 57, 19, 37, 21, 58, 36, 50, 9, 64, 35, 66, 47, 68, 8, 75, 0, 53, 67, 6, 51, 0, 18, 0, 42, 77, 2, 43, 17, 56, 10, 15, 26, 23, 0, 11, 0, 39, 78, 24, 0, 0, 0, 0,
		 0, 0, 0, 0, 0, 76, 42, 0, 40, 64, 51, 23, 0, 39, 20, 17, 79, 81, 50, 73, 0, 26, 14, 63, 53, 55, 0, 49, 43, 0, 31, 41, 48, 61, 57, 11, 45, 80, 5, 34, 0, 33, 70, 4, 10, 22, 58, 18, 15, 56, 6, 0, 1, 21, 0, 28, 59, 9, 7, 44, 0, 13, 30, 37, 69, 25, 77, 16, 0, 71, 36, 68, 72, 0, 8, 29, 0, 0, 0, 0, 0,
		 0, 0, 0, 0, 45, 0, 0, 9, 0, 68, 56, 0, 35, 57, 26, 24, 15, 2, 69, 0, 30, 60, 52, 76, 8, 20, 48, 0, 0, 16, 4, 79, 70, 19, 59, 13, 0, 32, 77, 39, 1, 40, 71, 27, 0, 14, 3, 72, 54, 53, 50, 41, 0, 0, 64, 55, 43, 66, 65, 5, 62, 0, 73, 47, 49, 75, 31, 6, 67, 0, 63, 81, 0, 34, 0, 0, 42, 0, 0, 0, 0,
		 3, 0, 49, 0, 0, 0, 55, 24, 57, 0, 76, 0, 75, 0, 4, 70, 34, 21, 58, 43, 0, 56, 77, 0, 9, 64, 40, 20, 0, 25, 36, 47, 8, 65, 28, 71, 62, 59, 35, 79, 15, 51, 7, 42, 81, 69, 29, 13, 32, 80, 23, 30, 0, 74, 31, 39, 19, 0, 16, 61, 0, 38, 1, 72, 54, 22, 27, 0, 48, 0, 11, 0, 46, 67, 52, 0, 0, 0, 26, 0, 2,
		 77, 47, 26, 0, 0, 0, 0, 34, 0, 0, 0, 0, 0, 67, 29, 66, 45, 25, 42, 0, 27, 0, 80, 31, 49, 81, 5, 0, 6, 55, 24, 73, 51, 2, 74, 32, 65, 12, 11, 46, 0, 9, 28, 22, 17, 70, 20, 16, 7, 62, 43, 76, 63, 0, 57, 71, 23, 58, 72, 0, 48, 0, 78, 33, 61, 19, 35, 38, 0, 0, 0, 0, 0, 40, 0, 0, 0, 0, 36, 1, 10,
		 0, 8, 0, 0, 0, 0, 0, 0, 81, 0, 0, 0, 71, 12, 18, 55, 47, 77, 0, 0, 46, 0, 4, 66, 0, 37, 35, 23, 72, 52, 53, 76, 9, 54, 62, 38, 0, 13, 20, 73, 30, 68, 16, 56, 0, 5, 42, 57, 39, 67, 11, 25, 26, 31, 14, 10, 0, 3, 49, 0, 75, 0, 0, 51, 64, 60, 32, 28, 58, 0, 0, 0, 80, 0, 0, 0, 0, 0, 0, 21, 0,
		 64, 51, 72, 21, 0, 27, 0, 0, 43, 0, 0, 0, 0, 0, 22, 8, 50, 11, 0, 67, 70, 0, 2, 17, 0, 7, 54, 0, 66, 0, 80, 58, 15, 30, 75, 39, 0, 31, 44, 24, 0, 26, 60, 76, 0, 71, 49, 45, 40, 10, 77, 0, 48, 0, 33, 34, 0, 4, 79, 0, 25, 37, 0, 1, 78, 53, 14, 0, 0, 0, 0, 0, 9, 0, 0, 18, 0, 13, 28, 63, 5,
		 23, 30, 61, 74, 48, 16, 0, 0, 0, 0, 0, 0, 80, 28, 40, 46, 58, 0, 0, 0, 0, 39, 62, 15, 10, 71, 68, 0, 0, 0, 1, 0, 26, 56, 67, 64, 63, 0, 0, 25, 69, 75, 0, 0, 8, 79, 33, 4, 12, 0, 17, 0, 0, 0, 41, 29, 45, 76, 81, 52, 0, 0, 0, 0, 18, 5, 7, 21, 3, 0, 0, 0, 0, 0, 0, 54, 77, 47, 32, 27, 43,
		 67, 1, 78, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 33, 32, 49, 48, 0, 0, 0, 23, 24, 65, 28, 18, 11, 0, 0, 77, 0, 45, 63, 3, 17, 12, 0, 53, 0, 2, 0, 47, 0, 64, 0, 34, 61, 73, 60, 81, 0, 44, 0, 0, 56, 35, 50, 21, 26, 46, 0, 0, 0, 8, 30, 59, 57, 0, 0, 40, 0, 0, 0, 0, 0, 0, 0, 0, 22, 79, 62,
		 71, 16, 47, 0, 0, 0, 0, 0, 0, 0, 0, 0, 21, 0, 55, 0, 38, 80, 30, 68, 49, 1, 44, 0, 0, 39, 53, 24, 15, 81, 13, 26, 32, 50, 79, 18, 59, 42, 51, 52, 60, 64, 12, 73, 31, 29, 75, 19, 69, 70, 25, 54, 20, 23, 45, 3, 0, 0, 41, 7, 66, 34, 72, 61, 11, 0, 76, 0, 33, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 2, 57,
		 0, 0, 0, 0, 35, 0, 18, 0, 0, 81, 0, 0, 0, 4, 0, 51, 63, 0, 52, 5, 10, 0, 78, 72, 0, 9, 36, 0, 73, 29, 60, 44, 55, 23, 54, 0, 1, 74, 37, 13, 7, 17, 68, 11, 20, 0, 59, 21, 47, 6, 31, 79, 50, 0, 16, 46, 0, 19, 14, 0, 38, 56, 48, 0, 70, 34, 0, 41, 0, 0, 0, 8, 0, 0, 69, 0, 62, 0, 0, 0, 0,
		 39, 0, 23, 0, 65, 20, 0, 0, 0, 0, 0, 0, 0, 0, 60, 19, 5, 0, 0, 0, 75, 8, 0, 81, 15, 59, 24, 6, 74, 0, 40, 63, 80, 62, 45, 41, 61, 58, 22, 0, 0, 0, 35, 66, 57, 68, 7, 42, 44, 34, 3, 0, 49, 38, 52, 32, 1, 26, 0, 12, 31, 0, 0, 0, 25, 36, 17, 0, 0, 0, 0, 0, 0, 0, 0, 71, 48, 0, 18, 0, 29,
		 60, 0, 21, 51, 0, 42, 19, 1, 0, 0, 58, 0, 0, 0, 0, 0, 44, 15, 0, 63, 76, 55, 70, 29, 18, 0, 74, 0, 14, 57, 0, 59, 34, 12, 49, 66, 71, 0, 50, 54, 62, 78, 43, 0, 4, 32, 67, 53, 26, 8, 0, 5, 56, 0, 69, 0, 25, 61, 73, 47, 28, 2, 0, 3, 23, 0, 0, 0, 0, 0, 7, 0, 0, 41, 9, 40, 0, 33, 20, 0, 11,
		 57, 50, 0, 7, 9, 14, 44, 0, 26, 11, 70, 0, 0, 0, 0, 0, 0, 0, 0, 17, 0, 0, 45, 23, 33, 51, 32, 2, 42, 58, 0, 0, 47, 16, 20, 22, 24, 38, 72, 53, 0, 8, 46, 67, 76, 60, 80, 30, 81, 0, 0, 36, 77, 10, 75, 43, 63, 71, 18, 0, 0, 68, 0, 0, 0, 0, 0, 0, 0, 0, 62, 55, 27, 0, 1, 13, 3, 31, 0, 52, 66,
		 63, 11, 31, 6, 33, 0, 73, 37, 32, 24, 0, 71, 45, 68, 0, 0, 0, 0, 0, 25, 79, 69, 0, 58, 34, 57, 3, 0, 0, 7, 0, 38, 4, 0, 10, 0, 55, 0, 0, 26, 23, 80, 0, 0, 65, 0, 28, 0, 52, 2, 0, 78, 0, 0, 20, 27, 62, 22, 0, 51, 40, 35, 0, 0, 0, 0, 0, 60, 81, 16, 0, 75, 74, 42, 36, 0, 39, 53, 61, 12, 14,
		 80, 27, 5, 64, 59, 10, 0, 3, 69, 75, 18, 67, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 7, 0, 0, 66, 61, 0, 51, 8, 25, 0, 31, 0, 11, 78, 0, 45, 19, 41, 0, 30, 77, 33, 0, 35, 55, 0, 62, 0, 12, 73, 37, 0, 58, 65, 0, 0, 17, 74, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 43, 48, 47, 49, 0, 81, 60, 32, 21, 28, 16,
		 76, 2, 58, 34, 13, 72, 30, 0, 41, 16, 3, 56, 59, 53, 0, 0, 0, 0, 0, 42, 0, 0, 0, 0, 77, 0, 27, 0, 0, 19, 0, 52, 0, 28, 61, 1, 79, 70, 0, 29, 0, 63, 0, 40, 32, 45, 15, 66, 0, 9, 0, 71, 0, 0, 81, 0, 80, 0, 0, 0, 0, 49, 0, 0, 0, 0, 0, 39, 65, 24, 6, 57, 50, 0, 68, 8, 73, 46, 43, 54, 38,
		 75, 68, 29, 55, 8, 0, 52, 0, 74, 30, 42, 32, 2, 76, 0, 0, 78, 0, 0, 0, 0, 31, 12, 21, 41, 19, 73, 0, 0, 64, 0, 0, 5, 67, 46, 0, 0, 0, 3, 69, 0, 18, 81, 0, 0, 0, 22, 51, 17, 0, 0, 57, 0, 0, 54, 4, 33, 39, 60, 24, 0, 0, 0, 0, 80, 0, 0, 53, 66, 45, 58, 44, 23, 0, 79, 0, 56, 70, 34, 59, 26,
		 73, 49, 0, 76, 0, 77, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 72, 0, 0, 42, 29, 34, 12, 52, 0, 79, 40, 1, 58, 30, 75, 0, 15, 17, 61, 3, 65, 64, 48, 0, 51, 18, 25, 45, 78, 20, 0, 27, 60, 35, 23, 46, 0, 0, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 24, 0, 50, 0, 74, 55,
		 42, 0, 32, 0, 0, 0, 22, 0, 29, 61, 9, 47, 0, 0, 0, 0, 0, 0, 33, 0, 0, 0, 0, 28, 79, 76, 81, 66, 0, 4, 71, 0, 43, 46, 68, 0, 56, 50, 49, 0, 24, 0, 53, 26, 41, 0, 11, 58, 73, 0, 80, 70, 0, 19, 1, 44, 64, 37, 0, 0, 0, 0, 16, 0, 0, 0, 0, 0, 0, 18, 10, 74, 67, 0, 34, 0, 0, 0, 7, 0, 3,
		 0, 57, 62, 13, 4, 31, 0, 5, 70, 0, 44, 0, 77, 40, 0, 0, 46, 0, 0, 78, 0, 0, 0, 0, 50, 21, 0, 0, 36, 23, 26, 0, 0, 51, 72, 3, 68, 79, 0, 81, 75, 2, 0, 47, 11, 24, 10, 69, 0, 0, 29, 1, 34, 0, 0, 53, 60, 0, 0, 0, 0, 43, 0, 0, 76, 0, 0, 56, 41, 0, 55, 0, 19, 59, 0, 61, 16, 54, 66, 30, 0,
		 26, 72, 75, 12, 60, 79, 9, 0, 0, 34, 41, 52, 24, 0, 65, 2, 0, 10, 17, 0, 0, 0, 0, 0, 0, 0, 4, 0, 64, 0, 5, 16, 29, 69, 31, 77, 73, 0, 78, 37, 0, 19, 76, 0, 54, 39, 81, 15, 3, 48, 57, 0, 6, 0, 11, 0, 0, 0, 0, 0, 0, 0, 8, 80, 0, 30, 61, 0, 23, 50, 25, 43, 0, 0, 28, 27, 49, 20, 63, 18, 51,
		 2, 44, 6, 37, 38, 0, 0, 7, 0, 50, 0, 31, 73, 62, 0, 1, 0, 0, 0, 40, 41, 0, 0, 0, 0, 0, 0, 0, 0, 0, 55, 0, 76, 34, 0, 15, 8, 0, 23, 63, 9, 22, 30, 0, 74, 36, 0, 47, 64, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 71, 0, 0, 0, 49, 0, 75, 26, 70, 0, 77, 0, 5, 0, 0, 14, 52, 81, 56, 35,
		 46, 15, 69, 71, 47, 0, 21, 0, 68, 79, 75, 49, 8, 0, 6, 56, 0, 16, 26, 0, 0, 0, 53, 0, 0, 0, 0, 0, 0, 0, 0, 0, 57, 24, 44, 59, 7, 0, 60, 72, 0, 55, 39, 0, 14, 37, 77, 40, 33, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 13, 11, 0, 38, 3, 0, 64, 51, 81, 73, 70, 0, 42, 0, 80, 41, 58, 62, 78,
		 56, 28, 45, 40, 67, 35, 10, 23, 52, 18, 0, 29, 30, 43, 63, 11, 0, 42, 77, 0, 61, 71, 0, 0, 0, 0, 0, 0, 0, 0, 49, 0, 27, 20, 53, 0, 66, 0, 0, 0, 38, 0, 0, 0, 16, 0, 72, 26, 13, 0, 46, 0, 0, 0, 0, 0, 0, 0, 0, 81, 9, 0, 3, 12, 0, 24, 1, 7, 34, 47, 0, 17, 22, 65, 73, 48, 37, 36, 39, 32, 68,
		 17, 34, 55, 16, 0, 74, 24, 81, 0, 48, 23, 68, 3, 72, 71, 57, 76, 28, 73, 32, 0, 63, 20, 0, 0, 0, 0, 0, 0, 0, 0, 8, 18, 60, 39, 0, 10, 0, 67, 0, 13, 0, 27, 0, 69, 0, 41, 52, 50, 22, 0, 0, 0, 0, 0, 0, 0, 0, 5, 66, 0, 40, 75, 58, 6, 9, 65, 33, 78, 62, 37, 36, 0, 79, 77, 31, 0, 38, 4, 53, 46,
		 54, 41, 43, 27, 11, 80, 39, 36, 3, 22, 4, 0, 32, 38, 64, 58, 51, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 35, 37, 73, 0, 47, 33, 0, 40, 0, 5, 0, 62, 0, 70, 63, 0, 49, 67, 28, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, 18, 0, 0, 52, 42, 60, 44, 53, 0, 16, 46, 13, 76, 72, 2, 17, 26, 12, 10, 8,
		 44, 0, 28, 8, 49, 46, 0, 0, 0, 0, 35, 78, 0, 41, 0, 0, 0, 0, 71, 47, 15, 80, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 17, 68, 0, 67, 0, 0, 56, 50, 0, 43, 42, 0, 0, 57, 0, 70, 11, 31, 0, 0, 0, 0, 0, 0, 0, 0, 0, 16, 74, 55, 53, 0, 0, 0, 0, 81, 0, 76, 5, 0, 0, 0, 0, 30, 27, 24, 37, 0, 7,
		 24, 52, 40, 0, 74, 69, 77, 0, 79, 39, 15, 0, 72, 0, 56, 0, 0, 6, 53, 0, 37, 20, 0, 0, 0, 17, 0, 19, 0, 0, 0, 0, 0, 71, 48, 28, 38, 7, 59, 0, 0, 0, 47, 45, 5, 78, 27, 29, 0, 0, 0, 0, 0, 25, 0, 80, 0, 0, 0, 75, 67, 0, 12, 73, 0, 0, 64, 0, 46, 0, 42, 34, 10, 0, 23, 68, 66, 0, 8, 70, 13,
		 13, 39, 59, 3, 56, 0, 34, 29, 0, 63, 62, 53, 0, 54, 58, 38, 69, 0, 49, 21, 74, 0, 35, 64, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 66, 45, 72, 36, 0, 40, 11, 71, 0, 52, 15, 4, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 14, 77, 24, 0, 46, 51, 70, 0, 7, 12, 50, 55, 0, 65, 28, 80, 0, 33, 31, 0, 19, 44, 67, 25, 61,
		 16, 70, 66, 23, 68, 0, 32, 20, 0, 40, 0, 37, 1, 29, 76, 50, 17, 0, 0, 61, 26, 58, 0, 0, 0, 0, 0, 0, 0, 65, 0, 0, 0, 0, 0, 72, 0, 14, 0, 80, 0, 54, 0, 81, 0, 6, 0, 0, 0, 0, 0, 22, 0, 0, 0, 0, 0, 0, 0, 41, 8, 45, 0, 0, 74, 78, 11, 25, 24, 3, 0, 60, 0, 71, 75, 0, 59, 55, 62, 49, 39,
		 27, 36, 0, 65, 76, 38, 4, 0, 12, 55, 48, 19, 31, 9, 44, 33, 0, 61, 0, 18, 0, 40, 22, 0, 70, 42, 0, 30, 34, 0, 0, 0, 0, 0, 0, 0, 0, 0, 58, 0, 0, 0, 26, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 2, 0, 5, 13, 0, 69, 1, 0, 79, 0, 20, 0, 21, 51, 23, 39, 43, 71, 67, 81, 0, 47, 16, 57, 78, 0, 14, 54,
		 21, 7, 60, 61, 71, 57, 0, 72, 0, 8, 81, 42, 12, 77, 2, 49, 0, 47, 51, 62, 54, 30, 66, 48, 78, 0, 79, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 41, 38, 0, 20, 43, 6, 63, 4, 26, 40, 35, 0, 68, 13, 70, 17, 59, 27, 19, 0, 32, 0, 80, 58, 74, 52, 73, 28,
		 31, 25, 41, 30, 50, 67, 64, 18, 33, 74, 0, 14, 46, 32, 0, 73, 70, 22, 72, 39, 29, 19, 0, 12, 44, 23, 57, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 79, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 36, 81, 11, 65, 0, 56, 17, 7, 66, 6, 4, 15, 0, 49, 75, 52, 0, 1, 42, 45, 2, 34, 9, 43, 3, 5, 21,
		 43, 75, 19, 2, 6, 22, 0, 17, 58, 36, 60, 65, 11, 79, 57, 0, 80, 5, 81, 0, 24, 34, 27, 10, 13, 67, 0, 0, 23, 40, 74, 49, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 55, 1, 56, 45, 0, 0, 25, 71, 44, 48, 78, 61, 0, 32, 54, 38, 0, 62, 14, 47, 9, 33, 66, 63, 12, 0, 20, 18, 72, 29, 77, 64,
		 51, 10, 53, 26, 0, 55, 14, 0, 0, 66, 24, 59, 43, 7, 30, 67, 71, 27, 9, 3, 16, 75, 63, 25, 36, 0, 1, 81, 78, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 18, 13, 34, 23, 0, 73, 54, 33, 68, 52, 58, 57, 48, 41, 56, 45, 77, 61, 72, 79, 32, 0, 0, 65, 46, 0, 40, 38, 11, 15,
		 0, 63, 77, 56, 26, 9, 35, 44, 37, 70, 66, 80, 51, 0, 43, 23, 0, 0, 5, 31, 18, 22, 0, 0, 0, 0, 0, 62, 13, 47, 52, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 34, 48, 67, 54, 0, 0, 0, 0, 0, 17, 69, 64, 74, 0, 0, 39, 33, 0, 16, 1, 72, 28, 61, 27, 7, 12, 46, 81, 73, 20, 0,
		 29, 60, 73, 62, 75, 66, 80, 68, 0, 26, 39, 0, 16, 36, 52, 5, 0, 12, 2, 81, 20, 74, 55, 46, 69, 0, 0, 70, 54, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 21, 28, 0, 0, 72, 57, 61, 48, 18, 77, 24, 4, 0, 40, 44, 67, 37, 0, 8, 51, 0, 30, 35, 22, 34, 56, 41, 58, 31,
		 32, 38, 54, 72, 52, 3, 6, 0, 59, 58, 27, 4, 76, 18, 0, 0, 74, 56, 40, 0, 1, 0, 42, 39, 0, 41, 44, 0, 71, 66, 0, 0, 0, 0, 0, 34, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 0, 0, 0, 0, 0, 37, 11, 0, 5, 62, 0, 35, 2, 0, 55, 0, 31, 25, 45, 0, 0, 65, 36, 21, 68, 53, 43, 0, 17, 28, 13, 77, 24, 64, 50,
		 10, 58, 33, 79, 39, 0, 0, 42, 27, 41, 78, 60, 49, 45, 75, 47, 72, 0, 34, 57, 48, 29, 38, 77, 0, 12, 28, 0, 44, 24, 67, 37, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 73, 64, 3, 69, 0, 32, 76, 0, 7, 22, 4, 30, 52, 63, 0, 55, 11, 6, 61, 13, 46, 9, 56, 54, 26, 0, 0, 2, 66, 19, 8, 18,
		 69, 21, 81, 0, 0, 34, 36, 0, 67, 25, 7, 11, 0, 0, 62, 44, 54, 71, 0, 66, 64, 14, 75, 0, 76, 32, 0, 1, 46, 79, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 52, 24, 0, 16, 53, 0, 13, 50, 78, 19, 0, 18, 10, 23, 30, 0, 0, 48, 31, 20, 39, 0, 59, 38, 0, 0, 74, 57, 63,
		 20, 55, 18, 49, 43, 0, 0, 46, 24, 35, 73, 22, 61, 2, 67, 6, 57, 0, 19, 52, 23, 36, 21, 51, 0, 78, 63, 0, 29, 12, 8, 69, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 42, 10, 38, 58, 0, 25, 59, 0, 28, 45, 70, 56, 44, 79, 0, 17, 62, 66, 71, 80, 64, 75, 27, 48, 47, 0, 0, 1, 4, 14, 68, 65,
		 22, 76, 51, 19, 1, 4, 41, 0, 71, 69, 17, 3, 55, 63, 0, 0, 53, 9, 11, 0, 65, 0, 47, 27, 0, 68, 49, 0, 60, 26, 0, 0, 0, 0, 0, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 23, 0, 0, 0, 0, 0, 33, 59, 0, 39, 66, 0, 81, 37, 0, 43, 0, 20, 34, 12, 0, 0, 78, 7, 58, 14, 70, 75, 0, 25, 5, 6, 10, 45, 36, 67,
		 11, 65, 50, 5, 2, 25, 53, 78, 0, 15, 8, 0, 81, 42, 19, 64, 0, 31, 43, 16, 7, 35, 10, 56, 62, 0, 0, 58, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 13, 0, 0, 67, 1, 80, 49, 41, 12, 14, 63, 0, 74, 59, 24, 77, 0, 57, 38, 0, 9, 71, 3, 44, 79, 70, 29, 23,
		 0, 64, 16, 17, 28, 45, 40, 8, 30, 20, 79, 13, 14, 0, 24, 59, 0, 0, 6, 4, 58, 37, 0, 0, 0, 0, 0, 31, 57, 36, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 41, 63, 22, 5, 0, 0, 0, 0, 0, 23, 60, 3, 51, 0, 0, 76, 47, 0, 32, 35, 29, 50, 78, 52, 55, 21, 11, 49, 53, 69, 0,
		 9, 14, 48, 4, 0, 18, 51, 0, 0, 46, 63, 76, 19, 61, 8, 21, 31, 45, 37, 36, 53, 7, 49, 78, 25, 0, 60, 26, 47, 33, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 24, 12, 70, 10, 0, 77, 40, 38, 43, 81, 66, 15, 17, 44, 65, 67, 57, 28, 13, 3, 58, 0, 0, 80, 72, 0, 39, 50, 71, 30,
		 55, 45, 65, 68, 40, 21, 0, 63, 16, 57, 49, 73, 38, 26, 14, 0, 81, 37, 66, 0, 17, 77, 9, 70, 47, 48, 0, 0, 19, 54, 76, 75, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 71, 22, 64, 10, 0, 0, 51, 74, 32, 39, 8, 23, 0, 27, 5, 46, 0, 79, 36, 25, 11, 69, 18, 1, 4, 0, 53, 52, 2, 13, 44, 20,
		 34, 79, 7, 41, 25, 61, 20, 76, 38, 62, 0, 2, 18, 70, 0, 60, 22, 58, 54, 56, 13, 44, 0, 4, 12, 15, 26, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 78, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 73, 31, 35, 67, 0, 36, 64, 24, 46, 53, 16, 80, 0, 30, 9, 6, 0, 72, 3, 48, 74, 43, 5, 23, 77, 45, 19,
		 1, 19, 70, 22, 77, 56, 0, 10, 0, 4, 55, 69, 68, 23, 15, 65, 0, 66, 79, 33, 34, 52, 28, 45, 40, 0, 21, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 44, 37, 0, 41, 80, 20, 76, 72, 17, 62, 74, 0, 73, 49, 32, 51, 31, 12, 7, 0, 8, 0, 36, 78, 67, 54, 24, 25,
		 49, 35, 0, 15, 36, 43, 13, 0, 72, 54, 71, 79, 78, 44, 41, 27, 0, 3, 0, 23, 0, 32, 76, 0, 20, 11, 0, 52, 65, 0, 0, 0, 0, 0, 0, 0, 0, 0, 46, 0, 0, 0, 31, 0, 0, 0, 0, 0, 0, 0, 0, 0, 47, 17, 0, 1, 7, 0, 55, 29, 0, 5, 0, 66, 0, 8, 37, 2, 45, 10, 24, 14, 59, 0, 21, 6, 22, 42, 0, 16, 81,
		 8, 6, 2, 67, 30, 0, 66, 32, 0, 7, 0, 17, 20, 47, 80, 16, 10, 0, 0, 69, 5, 62, 0, 0, 0, 0, 0, 0, 0, 73, 0, 0, 0, 0, 0, 58, 0, 24, 0, 48, 0, 44, 0, 15, 0, 53, 0, 0, 0, 0, 0, 65, 0, 0, 0, 0, 0, 0, 0, 26, 14, 59, 0, 0, 21, 33, 23, 63, 4, 38, 0, 54, 0, 11, 29, 0, 79, 57, 46, 35, 70,
		 37, 31, 64, 59, 46, 0, 62, 58, 0, 13, 74, 5, 0, 6, 51, 72, 25, 0, 10, 80, 3, 0, 16, 68, 67, 0, 0, 0, 0, 0, 0, 0, 0, 0, 27, 35, 20, 11, 0, 36, 14, 39, 0, 75, 79, 30, 54, 0, 0, 0, 0, 0, 0, 0, 0, 0, 69, 48, 9, 0, 63, 57, 50, 0, 34, 29, 19, 40, 0, 61, 22, 41, 0, 17, 26, 0, 65, 76, 60, 66, 73,
		 78, 17, 71, 0, 80, 24, 54, 0, 5, 67, 50, 0, 53, 0, 35, 0, 0, 29, 31, 0, 59, 65, 0, 0, 0, 30, 0, 44, 0, 0, 0, 0, 0, 41, 63, 23, 6, 8, 66, 0, 0, 0, 9, 37, 40, 73, 16, 32, 0, 0, 0, 0, 0, 49, 0, 45, 0, 0, 0, 60, 42, 0, 2, 55, 0, 0, 81, 0, 68, 0, 26, 52, 51, 0, 12, 7, 28, 0, 10, 61, 34,
		 50, 0, 42, 44, 27, 81, 0, 0, 0, 0, 28, 64, 0, 34, 0, 0, 0, 0, 1, 14, 63, 41, 0, 0, 0, 0, 0, 0, 0, 0, 0, 51, 11, 66, 0, 74, 0, 0, 38, 76, 0, 10, 57, 0, 0, 7, 0, 23, 5, 72, 0, 0, 0, 0, 0, 0, 0, 0, 0, 53, 6, 30, 25, 0, 0, 0, 0, 35, 0, 75, 59, 0, 0, 0, 0, 32, 68, 18, 31, 0, 37,
		 36, 54, 34, 80, 72, 44, 29, 39, 17, 21, 33, 0, 48, 3, 73, 25, 66, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 37, 0, 27, 77, 78, 0, 24, 19, 0, 4, 0, 52, 0, 1, 0, 35, 31, 0, 79, 59, 51, 0, 16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 61, 0, 0, 68, 57, 53, 11, 76, 0, 64, 10, 12, 18, 43, 50, 63, 71, 9, 22, 32,
		 38, 37, 35, 32, 0, 51, 28, 77, 0, 17, 2, 50, 79, 8, 49, 61, 43, 52, 59, 71, 0, 10, 36, 0, 0, 0, 0, 0, 0, 0, 0, 60, 30, 44, 5, 0, 40, 0, 15, 0, 21, 0, 63, 0, 45, 0, 70, 33, 18, 14, 0, 0, 0, 0, 0, 0, 0, 0, 64, 54, 0, 65, 76, 9, 72, 66, 78, 27, 1, 23, 19, 12, 0, 57, 20, 58, 0, 25, 69, 81, 41,
		 40, 4, 67, 18, 42, 2, 1, 60, 10, 5, 0, 46, 28, 71, 11, 75, 0, 19, 20, 0, 72, 15, 0, 0, 0, 0, 0, 0, 0, 0, 39, 0, 74, 64, 47, 0, 51, 0, 0, 0, 8, 0, 0, 0, 61, 0, 37, 22, 35, 0, 30, 0, 0, 0, 0, 0, 0, 0, 0, 62, 53, 0, 68, 45, 0, 44, 21, 43, 54, 80, 0, 59, 73, 14, 38, 23, 7, 3, 78, 13, 77,
		 70, 43, 3, 58, 21, 0, 11, 0, 47, 23, 12, 51, 40, 0, 32, 81, 0, 64, 55, 0, 0, 0, 56, 0, 0, 0, 0, 0, 0, 0, 0, 0, 49, 36, 13, 19, 22, 0, 74, 6, 0, 57, 73, 0, 78, 15, 34, 41, 53, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 59, 29, 0, 16, 38, 0, 63, 60, 65, 25, 24, 0, 4, 0, 26, 61, 42, 80, 27,
		 81, 69, 30, 73, 53, 0, 0, 61, 0, 59, 0, 57, 9, 10, 0, 36, 0, 0, 0, 60, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 68, 0, 41, 45, 0, 63, 11, 0, 47, 67, 71, 62, 3, 0, 77, 49, 0, 46, 23, 0, 75, 0, 0, 0, 0, 0, 0, 0, 0, 0, 51, 15, 0, 0, 0, 55, 0, 58, 35, 56, 0, 5, 0, 16, 0, 0, 29, 65, 1, 33, 40,
		 59, 13, 52, 24, 57, 19, 79, 0, 0, 76, 20, 15, 29, 0, 42, 68, 0, 54, 25, 0, 0, 0, 0, 0, 0, 0, 69, 0, 80, 0, 65, 23, 14, 38, 58, 16, 75, 0, 48, 17, 0, 32, 44, 0, 18, 21, 78, 39, 1, 11, 81, 0, 64, 0, 9, 0, 0, 0, 0, 0, 0, 0, 56, 62, 0, 7, 34, 0, 73, 36, 50, 6, 0, 0, 5, 51, 45, 35, 2, 60, 49,
		 0, 12, 68, 50, 23, 71, 0, 55, 9, 0, 69, 0, 47, 65, 0, 0, 16, 0, 0, 37, 0, 0, 0, 0, 6, 45, 0, 0, 32, 46, 61, 0, 0, 48, 34, 21, 36, 81, 0, 20, 27, 14, 0, 39, 58, 10, 73, 5, 0, 0, 38, 13, 40, 0, 0, 74, 3, 0, 0, 0, 0, 22, 0, 0, 67, 0, 0, 4, 31, 0, 77, 0, 15, 54, 0, 66, 30, 59, 76, 75, 0,
		 7, 0, 15, 0, 0, 0, 27, 0, 65, 38, 72, 74, 0, 0, 0, 0, 0, 0, 18, 0, 0, 0, 0, 42, 5, 40, 31, 33, 0, 3, 9, 0, 25, 57, 50, 0, 54, 46, 80, 0, 12, 0, 37, 60, 64, 0, 45, 6, 29, 0, 71, 28, 0, 26, 43, 77, 79, 34, 0, 0, 0, 0, 47, 0, 0, 0, 0, 0, 0, 32, 61, 69, 62, 0, 44, 0, 0, 0, 56, 0, 48,
		 41, 48, 0, 75, 0, 78, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 0, 0, 23, 73, 33, 67, 11, 0, 66, 43, 81, 35, 22, 8, 0, 68, 24, 10, 50, 72, 29, 53, 0, 62, 4, 77, 65, 58, 61, 0, 7, 56, 63, 2, 37, 0, 0, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 52, 0, 19, 0, 39, 36,
		 53, 23, 10, 78, 32, 0, 67, 0, 34, 71, 30, 28, 37, 48, 0, 0, 9, 0, 0, 0, 0, 24, 40, 80, 4, 44, 15, 0, 0, 61, 0, 0, 69, 13, 43, 0, 0, 0, 75, 8, 0, 76, 38, 0, 0, 0, 36, 55, 77, 0, 0, 12, 0, 0, 17, 58, 16, 5, 47, 65, 0, 0, 0, 0, 3, 0, 0, 66, 52, 19, 54, 21, 33, 0, 70, 0, 31, 22, 59, 72, 60,
		 45, 62, 44, 42, 31, 52, 16, 0, 77, 2, 61, 26, 67, 64, 0, 0, 0, 0, 0, 53, 0, 0, 0, 0, 55, 0, 19, 0, 0, 60, 0, 48, 0, 74, 73, 20, 30, 69, 0, 28, 0, 15, 0, 3, 27, 76, 46, 56, 0, 4, 0, 39, 0, 0, 79, 0, 12, 0, 0, 0, 0, 54, 0, 0, 0, 0, 0, 68, 6, 25, 34, 9, 29, 0, 51, 41, 8, 7, 11, 37, 47,
		 58, 71, 9, 57, 63, 60, 0, 51, 19, 52, 25, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 47, 59, 0, 0, 8, 29, 0, 2, 18, 75, 0, 39, 0, 37, 40, 0, 21, 26, 23, 0, 74, 6, 10, 0, 28, 13, 0, 79, 0, 53, 61, 3, 0, 72, 48, 0, 0, 46, 77, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 45, 30, 38, 80, 0, 65, 54, 12, 16, 50, 44,
		 35, 74, 38, 1, 55, 0, 68, 21, 7, 33, 0, 40, 15, 66, 0, 0, 0, 0, 0, 70, 78, 67, 0, 6, 73, 52, 46, 0, 0, 10, 0, 57, 54, 0, 36, 0, 12, 0, 0, 11, 44, 16, 0, 0, 53, 0, 51, 0, 71, 5, 0, 26, 0, 0, 19, 41, 81, 59, 0, 25, 13, 32, 0, 0, 0, 0, 0, 20, 14, 39, 0, 2, 18, 58, 62, 0, 61, 45, 30, 4, 76,
		 28, 3, 0, 36, 12, 64, 47, 0, 15, 65, 10, 0, 0, 0, 0, 0, 0, 0, 0, 50, 0, 0, 32, 30, 38, 43, 18, 72, 76, 42, 0, 0, 62, 22, 51, 27, 77, 19, 34, 5, 0, 73, 55, 59, 63, 25, 40, 37, 58, 0, 0, 68, 29, 48, 8, 14, 70, 20, 71, 0, 0, 31, 0, 0, 0, 0, 0, 0, 0, 0, 49, 26, 53, 0, 78, 35, 21, 9, 0, 46, 69,
		 48, 0, 39, 46, 0, 5, 50, 69, 0, 0, 43, 0, 0, 0, 0, 0, 13, 76, 0, 12, 42, 81, 25, 2, 45, 0, 77, 0, 68, 53, 0, 80, 79, 32, 78, 65, 31, 0, 7, 9, 72, 60, 40, 0, 49, 38, 57, 11, 8, 18, 0, 14, 30, 0, 61, 0, 51, 52, 62, 55, 26, 29, 0, 44, 71, 0, 0, 0, 0, 0, 67, 0, 0, 36, 19, 17, 0, 34, 64, 0, 75,
		 66, 0, 11, 0, 70, 73, 0, 0, 0, 0, 0, 0, 0, 0, 46, 62, 29, 0, 0, 0, 21, 9, 0, 79, 7, 16, 39, 59, 35, 0, 12, 31, 33, 17, 4, 30, 81, 18, 42, 0, 0, 0, 61, 14, 43, 44, 69, 78, 6, 1, 74, 0, 19, 45, 50, 64, 24, 75, 0, 27, 3, 0, 0, 0, 13, 58, 8, 0, 0, 0, 0, 0, 0, 0, 0, 26, 25, 0, 48, 0, 52,
		 0, 0, 0, 0, 20, 0, 8, 0, 0, 72, 0, 0, 0, 14, 0, 18, 39, 0, 60, 28, 69, 0, 37, 26, 0, 5, 13, 0, 3, 34, 15, 19, 58, 6, 29, 0, 17, 1, 62, 22, 41, 67, 33, 78, 51, 0, 23, 65, 63, 64, 35, 50, 70, 0, 30, 68, 0, 10, 74, 0, 2, 53, 21, 0, 59, 48, 0, 46, 0, 0, 0, 16, 0, 0, 66, 0, 43, 0, 0, 0, 0,
		 30, 18, 24, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 78, 0, 36, 8, 61, 58, 11, 51, 31, 0, 0, 10, 65, 71, 49, 1, 46, 56, 44, 55, 26, 14, 2, 52, 70, 47, 54, 50, 25, 57, 48, 17, 21, 67, 20, 27, 16, 80, 33, 66, 15, 40, 0, 0, 76, 69, 45, 4, 37, 28, 53, 0, 73, 0, 29, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 23, 74,
		 33, 42, 20, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 81, 10, 18, 59, 0, 0, 0, 50, 3, 69, 26, 4, 9, 0, 0, 48, 0, 7, 23, 40, 77, 73, 0, 22, 0, 16, 0, 27, 0, 19, 0, 41, 68, 38, 49, 63, 0, 11, 0, 0, 78, 60, 44, 25, 31, 37, 0, 0, 0, 21, 1, 46, 52, 0, 0, 74, 0, 0, 0, 0, 0, 0, 0, 0, 15, 76, 56,
		 25, 67, 17, 38, 44, 7, 0, 0, 0, 0, 0, 0, 22, 74, 45, 39, 37, 0, 0, 0, 0, 49, 30, 36, 81, 27, 62, 0, 0, 0, 2, 0, 68, 9, 65, 6, 60, 0, 0, 15, 35, 66, 0, 0, 47, 20, 14, 28, 42, 0, 58, 0, 0, 0, 59, 50, 29, 53, 19, 34, 0, 0, 0, 0, 56, 26, 71, 72, 40, 0, 0, 0, 0, 0, 0, 77, 41, 11, 80, 43, 79,
		 79, 66, 14, 60, 0, 13, 0, 0, 49, 0, 0, 0, 0, 0, 54, 76, 20, 57, 0, 24, 2, 0, 61, 40, 0, 58, 72, 0, 5, 0, 44, 39, 36, 11, 69, 10, 0, 64, 29, 33, 0, 37, 67, 30, 0, 16, 26, 80, 75, 23, 21, 0, 71, 0, 74, 56, 0, 55, 12, 0, 27, 9, 0, 19, 48, 77, 25, 0, 0, 0, 0, 0, 28, 0, 0, 47, 0, 8, 35, 7, 6,
		 0, 59, 0, 0, 0, 0, 0, 0, 50, 0, 0, 0, 5, 78, 7, 3, 19, 79, 0, 0, 51, 0, 60, 43, 0, 14, 20, 42, 26, 17, 28, 74, 72, 25, 71, 37, 0, 4, 76, 32, 73, 77, 75, 58, 0, 13, 62, 64, 61, 30, 56, 67, 18, 33, 6, 63, 0, 8, 23, 0, 16, 0, 0, 39, 57, 81, 2, 29, 22, 0, 0, 0, 69, 0, 0, 0, 0, 0, 0, 34, 0,
		 65, 29, 80, 0, 0, 0, 0, 16, 0, 0, 0, 0, 0, 46, 9, 28, 26, 36, 12, 0, 57, 0, 68, 19, 66, 75, 78, 0, 33, 56, 62, 61, 67, 47, 81, 54, 50, 25, 69, 31, 0, 38, 10, 63, 1, 72, 48, 76, 27, 74, 45, 4, 17, 0, 51, 18, 2, 11, 43, 0, 70, 0, 71, 7, 35, 6, 24, 5, 0, 0, 0, 0, 0, 64, 0, 0, 0, 0, 23, 40, 22,
		 19, 0, 56, 0, 0, 0, 46, 26, 63, 0, 68, 0, 27, 0, 47, 41, 2, 33, 13, 54, 0, 25, 23, 0, 29, 6, 22, 51, 0, 32, 16, 66, 38, 52, 60, 80, 57, 61, 65, 74, 40, 3, 21, 44, 62, 8, 5, 59, 34, 35, 37, 15, 0, 79, 24, 7, 76, 0, 77, 64, 0, 20, 81, 75, 50, 28, 9, 0, 49, 0, 4, 0, 71, 39, 14, 0, 0, 0, 72, 0, 17,
		 0, 0, 0, 0, 41, 0, 0, 30, 0, 42, 80, 0, 64, 16, 69, 40, 61, 35, 39, 0, 77, 45, 46, 67, 37, 47, 7, 0, 0, 63, 22, 18, 78, 15, 55, 43, 0, 9, 8, 68, 59, 20, 5, 71, 0, 52, 2, 44, 24, 50, 51, 81, 0, 0, 66, 33, 57, 14, 28, 3, 65, 0, 17, 36, 58, 54, 70, 34, 60, 0, 76, 11, 0, 19, 0, 0, 38, 0, 0, 0, 0,
		 0, 0, 0, 0, 0, 6, 74, 0, 28, 60, 67, 58, 0, 75, 38, 63, 62, 65, 35, 64, 0, 76, 5, 33, 16, 34, 0, 27, 21, 0, 30, 4, 20, 70, 24, 29, 23, 2, 81, 43, 0, 45, 11, 7, 42, 47, 66, 12, 22, 57, 36, 0, 39, 73, 0, 72, 54, 79, 40, 13, 0, 1, 41, 15, 14, 37, 10, 80, 0, 53, 51, 61, 31, 0, 3, 44, 0, 0, 0, 0, 0,
		 0, 0, 0, 0, 24, 37, 57, 0, 55, 0, 77, 72, 34, 17, 23, 71, 11, 73, 28, 10, 0, 59, 0, 1, 56, 38, 70, 0, 58, 45, 3, 50, 19, 75, 64, 76, 48, 41, 79, 51, 49, 12, 14, 54, 39, 40, 31, 43, 9, 29, 78, 60, 25, 0, 26, 61, 15, 47, 0, 21, 0, 62, 4, 16, 66, 67, 68, 42, 44, 69, 20, 0, 52, 0, 18, 63, 36, 0, 0, 0, 0};


var wierdBoardOriginal = new[]
{
	0, 0, 0, 0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0, 0, 0, 0,
	0, 0, 0, 2, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0, 0, 0, 0
};


var wierdBoard = new[]
{
	0, 0, 2, 0, 1, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 2, 0, 1, 0,
	1, 0, 0, 0, 0, 0, 0, 0, 2,
	0, 0, 0, 0, 2, 0, 1, 0, 0,
	0, 1, 0, 0, 0, 0, 0, 2, 0,
	2, 0, 0, 0, 0, 1, 0, 0, 0,
	0, 0, 0, 2, 0, 0, 0, 0, 1,
	0, 0, 1, 0, 0, 0, 2, 0, 0,
	0, 2, 0, 1, 0, 0, 0, 0, 0
};

Console.OutputEncoding = System.Text.Encoding.UTF8;
var solver = WFCSolver.FromList(wierdBoard, 9, 9, 3, solver =>
	{
		solver.WithGroup((0, 0), (1, 0), (2, 0), (0, 1), (0, 2), (1, 2), (0, 3), (1, 3))
		.WithGroup((2, 1), (2, 2), (2, 3), (2, 4), (2, 5), (0, 5), (3, 5), (0, 4), (1, 4))
		.WithGroup((0, 6), (0, 7), (0, 8), (1, 6), (2, 6), (2, 7), (2, 8), (3, 6))
		.WithGroup((3, 0), (3, 1), (3, 2), (3, 3), (3, 4), (4, 4), (4, 0), (5, 0), (5, 1))
		.WithGroup((4, 1), (4, 2), (4, 3), (5, 3), (5, 4), (6, 4))
		.WithGroup((4, 5), (4, 6), (4, 7), (4, 8), (3, 8), (5, 6), (6, 6), (6, 5), (6, 7))
		.WithGroup((6, 8), (7, 8), (8, 8), (7, 7), (8, 7), (7, 6), (8, 6), (7, 5), (7, 4))
		.WithGroup((5, 2), (6, 2), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0), (8, 0), (8, 1))
		.WithGroup((7, 1), (7, 2), (8, 2), (8, 3), (8, 4), (8, 5))
		.WithCollapseRule(new CollapseConsecutiveNeighbours())
		.WithPrintWithStates(true)
		.UseBacktracking(true);
	}
);

var bfSolver = BruteForceSolver.FromList(wierdBoardOriginal, 9, 9, 3, solver =>
{
	solver.WithGroup((0, 0), (1, 0), (2, 0), (0, 1), (0, 2), (1, 2), (0, 3), (1, 3))
		.WithGroup((2, 1), (2, 2), (2, 3), (2, 4), (2, 5), (0, 5), (3, 5), (0, 4), (1, 4))
		.WithGroup((0, 6), (0, 7), (0, 8), (1, 6), (2, 6), (2, 7), (2, 8), (3, 6))
		.WithGroup((3, 0), (3, 1), (3, 2), (3, 3), (3, 4), (4, 4), (4, 0), (5, 0), (5, 1))
		.WithGroup((4, 1), (4, 2), (4, 3), (5, 3), (5, 4), (6, 4))
		.WithGroup((4, 5), (4, 6), (4, 7), (4, 8), (3, 8), (5, 6), (6, 6), (6, 5), (6, 7))
		.WithGroup((6, 8), (7, 8), (8, 8), (7, 7), (8, 7), (7, 6), (8, 6), (7, 5), (7, 4))
		.WithGroup((5, 2), (6, 2), (6, 3), (6, 2), (6, 1), (6, 0), (7, 0), (8, 0), (8, 1))
		.WithGroup((7, 1), (7, 2), (8, 2), (8, 3), (8, 4), (8, 5));
});

string file = null;
if (args.Length == 1)
	file = args[0];

if (file.StartsWith('"') && file.EndsWith('"'))
	file = file[1..^1];

var cli = SudokuCli.Create(file);
cli.Run();