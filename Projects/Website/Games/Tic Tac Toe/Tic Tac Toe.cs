﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Website.Games.Tic_Tac_Toe;

public class Tic_Tac_Toe
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		bool closeRequested = false;
		bool playerTurn = true;
		Random random = new();
		char[,] board;

		while (!closeRequested)
		{
			board = new char[3, 3]
			{
				{ ' ', ' ', ' ', },
				{ ' ', ' ', ' ', },
				{ ' ', ' ', ' ', },
			};
			while (!closeRequested)
			{
				if (playerTurn)
				{
					await PlayerTurn();
					if (CheckForThree('X'))
					{
						await EndGame("You Win.");
						break;
					}
				}
				else
				{
					ComputerTurn();
					if (CheckForThree('O'))
					{
						await EndGame("You Lose.");
						break;
					}
				}
				playerTurn = !playerTurn;
				if (CheckForFullBoard())
				{
					await EndGame("Draw.");
					break;
				}
			}
			if (!closeRequested)
			{
				await Console.WriteLine();
				await Console.WriteLine("Play Again [enter], or quit [escape]?");
			GetInput:
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.Enter: break;
					case ConsoleKey.Escape:
						closeRequested = true;
						await Console.Clear();
						await Console.WriteLine("Tic Tac Toe was closed.");
						await Console.Refresh();
						break;
					default: goto GetInput;
				}
			}
		}

		async Task PlayerTurn()
		{
			var (row, column) = (0, 0);
			bool moved = false;
			while (!moved && !closeRequested)
			{
				await Console.Clear();
				await RenderBoard();
				await Console.WriteLine();
				await Console.WriteLine("Choose a valid position and press enter.");
				await Console.SetCursorPosition(column * 6 + 1, row * 4 + 1);
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.UpArrow:    row = row <= 0 ? 2 : row - 1; break;
					case ConsoleKey.DownArrow:  row = row >= 2 ? 0 : row + 1; break;
					case ConsoleKey.LeftArrow:  column = column <= 0 ? 2 : column - 1; break;
					case ConsoleKey.RightArrow: column = column >= 2 ? 0 : column + 1; break;
					case ConsoleKey.Enter:
						if (board[row, column] is ' ')
						{
							board[row, column] = 'X';
							moved = true;
						}
						break;
					case ConsoleKey.Escape:
						await Console.Clear();
						closeRequested = true;
						await Console.WriteLine("Tic Tac Toe was closed.");
						await Console.Refresh();
						break;
				}
			}
		}

		void ComputerTurn()
		{
			var possibleMoves = new List<(int X, int Y)>();
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (board[i, j] == ' ')
					{
						possibleMoves.Add((i, j));
					}
				}
			}
			int index = random.Next(0, possibleMoves.Count);
			var (X, Y) = possibleMoves[index];
			board[X, Y] = 'O';
		}

		bool CheckForThree(char c) =>
			board[0, 0] == c && board[1, 0] == c && board[2, 0] == c ||
			board[0, 1] == c && board[1, 1] == c && board[2, 1] == c ||
			board[0, 2] == c && board[1, 2] == c && board[2, 2] == c ||
			board[0, 0] == c && board[0, 1] == c && board[0, 2] == c ||
			board[1, 0] == c && board[1, 1] == c && board[1, 2] == c ||
			board[2, 0] == c && board[2, 1] == c && board[2, 2] == c ||
			board[0, 0] == c && board[1, 1] == c && board[2, 2] == c ||
			board[2, 0] == c && board[1, 1] == c && board[0, 2] == c;

		bool CheckForFullBoard() =>
			board[0, 0] != ' ' && board[1, 0] != ' ' && board[2, 0] != ' ' &&
			board[0, 1] != ' ' && board[1, 1] != ' ' && board[2, 1] != ' ' &&
			board[0, 2] != ' ' && board[1, 2] != ' ' && board[2, 2] != ' ';

		async Task RenderBoard()
		{
			await Console.WriteLine();
			await Console.WriteLine($" {board[0, 0]}  ║  {board[0, 1]}  ║  {board[0, 2]}");
			await Console.WriteLine("    ║     ║");
			await Console.WriteLine(" ═══╬═════╬═══");
			await Console.WriteLine("    ║     ║");
			await Console.WriteLine($" {board[1, 0]}  ║  {board[1, 1]}  ║  {board[1, 2]}");
			await Console.WriteLine("    ║     ║");
			await Console.WriteLine(" ═══╬═════╬═══");
			await Console.WriteLine("    ║     ║");
			await Console.WriteLine($" {board[2, 0]}  ║  {board[2, 1]}  ║  {board[2, 2]}");
		}

		async Task EndGame(string message)
		{
			await Console.Clear();
			await RenderBoard();
			await Console.WriteLine();
			await Console.Write(message);
		}
	}
}
