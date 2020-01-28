using System;
using System.Collections.Generic;
using static System.Console;

class Program
{
	static bool gameOver = false;
	static bool playerTurn = true;

	static readonly Random random = new Random();

	static readonly char[,] board = new char[3, 3]
	{
		{ ' ', ' ', ' ', },
		{ ' ', ' ', ' ', },
		{ ' ', ' ', ' ', },
	};

	static void Main()
	{
		while (!gameOver)
		{
			if (playerTurn) PlayerMove();
			else ComputerMove();
			Check();
			playerTurn = !playerTurn;
		}
	}

	static bool CheckForThree(char c) =>
		board[0, 0] == c && board[1, 0] == c && board[2, 0] == c ||
		board[0, 1] == c && board[1, 1] == c && board[2, 1] == c ||
		board[0, 2] == c && board[1, 2] == c && board[2, 2] == c ||
		board[0, 0] == c && board[0, 1] == c && board[0, 2] == c ||
		board[1, 0] == c && board[1, 1] == c && board[1, 2] == c ||
		board[2, 0] == c && board[2, 1] == c && board[2, 2] == c ||
		board[0, 0] == c && board[1, 1] == c && board[2, 2] == c ||
		board[2, 0] == c && board[1, 1] == c && board[0, 2] == c;

	static bool CheckForFullBoard() =>
		board[0, 0] != ' ' && board[1, 0] != ' ' && board[2, 0] != ' ' &&
		board[0, 1] != ' ' && board[1, 1] != ' ' && board[2, 1] != ' ' &&
		board[0, 2] != ' ' && board[1, 2] != ' ' && board[2, 2] != ' ';

	static void Check()
	{
		if (CheckForThree('X'))
		{
			Clear();
			Write("You Win.");
			gameOver = true;
		}
		else if (CheckForThree('O'))
		{
			Clear();
			Write("You Lose.");
			gameOver = true;
		}
		else if (CheckForFullBoard())
		{
			Clear();
			Write("Draw.");
			gameOver = true;
		}
	}

	static void ComputerMove()
	{
		var possibleMoves = new List<(int X, int Y)>();
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++)
				if (board[i, j] == ' ')
					possibleMoves.Add((i, j));
		int index = random.Next(0, possibleMoves.Count);
		var (X, Y) = possibleMoves[index];
		board[X, Y] = 'O';
	}

	static void PlayerMove()
	{
		var (row, column) = (0, 0);
	PlayerMove:
		Clear();
		RenderBoard();
		WriteLine();
		WriteLine("Choose a valid position and press enter.");
		SetCursorPosition(column * 6 + 1, row * 4 + 1);
		switch (ReadKey(true).Key)
		{
			case ConsoleKey.UpArrow: row = Math.Abs((row - 1) % 3); goto PlayerMove;
			case ConsoleKey.DownArrow: row = Math.Abs((row + 1) % 3); goto PlayerMove;
			case ConsoleKey.LeftArrow: column = Math.Abs((column - 1) % 3); goto PlayerMove;
			case ConsoleKey.RightArrow: column = Math.Abs((column + 1) % 3); goto PlayerMove;
			case ConsoleKey.Enter: break;
			case ConsoleKey.Escape:
				Clear();
				Write("Tic Tac Toe was closed.");
				gameOver = true;
				break;
			default: goto PlayerMove;
		}
		if (!gameOver)
		{
			if (board[row, column] != ' ') goto PlayerMove;
			board[row, column] = 'X';
		}
	}

	static void RenderBoard()
	{
		WriteLine();
		WriteLine(
			$" {board[0, 0]}  |  {board[0, 1]}  |  {board[0, 2]}" + '\n' +
			"    |     |" + '\n' +
			" ---+-----+---" + '\n' +
			"    |     |" + '\n' +
			$" {board[1, 0]}  |  {board[1, 1]}  |  {board[1, 2]}" + '\n' +
			"    |     |" + '\n' +
			" ---+-----+---" + '\n' +
			"    |     |" + '\n' +
			$" {board[2, 0]}  |  {board[2, 1]}  |  {board[2, 2]}" + '\n');
	}
}
