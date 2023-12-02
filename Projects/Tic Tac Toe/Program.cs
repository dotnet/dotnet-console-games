using System;
using System.Collections.Generic;

bool closeRequested = false;
bool playerTurn = true;
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
			PlayerTurn();
			if (CheckForThree('X'))
			{
				EndGame("  You Win.");
				break;
			}
		}
		else
		{
			ComputerTurn();
			if (CheckForThree('O'))
			{
				EndGame("  You Lose.");
				break;
			}
		}
		playerTurn = !playerTurn;
		if (CheckForFullBoard())
		{
			EndGame("  Draw.");
			break;
		}
	}
	if (!closeRequested)
	{
		Console.WriteLine();
		Console.WriteLine("  Play Again [enter], or quit [escape]?");
	GetInput:
		Console.CursorVisible = false;
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.Enter: break;
			case ConsoleKey.Escape:
				closeRequested = true;
				Console.Clear();
				break;
			default: goto GetInput;
		}
	}
}
Console.CursorVisible = true;

void PlayerTurn()
{
	var (row, column) = (0, 0);
	bool moved = false;
	while (!moved && !closeRequested)
	{
		Console.Clear();
		RenderBoard();
		Console.WriteLine();
		Console.WriteLine("  Use the arrow and enter keys to select a move.");
		Console.SetCursorPosition(column * 4 + 4, row * 2 + 4);
		Console.CursorVisible = true;
		switch (Console.ReadKey(true).Key)
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
				Console.Clear();
				closeRequested = true;
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
	int index = Random.Shared.Next(0, possibleMoves.Count);
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

void RenderBoard()
{
	Console.WriteLine($"""

		  Tic Tac Toe

		  ╔═══╦═══╦═══╗
		  ║ {board[0, 0]} ║ {board[0, 1]} ║ {board[0, 2]} ║
		  ╠═══╬═══╬═══╣
		  ║ {board[1, 0]} ║ {board[1, 1]} ║ {board[1, 2]} ║
		  ╠═══╬═══╬═══╣
		  ║ {board[2, 0]} ║ {board[2, 1]} ║ {board[2, 2]} ║
		  ╚═══╩═══╩═══╝
		""");
}

void EndGame(string message)
{
	Console.Clear();
	RenderBoard();
	Console.WriteLine();
	Console.Write(message);
}
