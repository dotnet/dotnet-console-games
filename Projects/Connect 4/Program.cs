using System;
using System.Linq;

bool?[,] board = new bool?[7, 6];
bool player1Turn;
bool player1MovesFirst = true;
Random random = new Random();

const int moveMinI = 5;
const int moveJ = 2;

try
{
	Console.CursorVisible = false;
PlayAgain:
	player1Turn = player1MovesFirst;
	player1MovesFirst = !player1MovesFirst;
	ResetBoard();
	while (true)
	{
		(int I, int J) move = default;
		if (player1Turn)
		{
			RenderBoard();
			int i = 0;
			Console.SetCursorPosition(moveMinI, moveJ);
			Console.Write('v');
		GetPlayerInput:
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.LeftArrow:
					Console.SetCursorPosition(i * 2 + moveMinI, moveJ);
					Console.Write(' ');
					i = Math.Max(0, i - 1);
					Console.SetCursorPosition(i * 2 + moveMinI, moveJ);
					Console.Write('v');
					goto GetPlayerInput;
				case ConsoleKey.RightArrow:
					Console.SetCursorPosition(i * 2 + moveMinI, moveJ);
					Console.Write(' ');
					i = Math.Min(board.GetLength(0) - 1, i + 1);
					Console.SetCursorPosition(i * 2 + moveMinI, moveJ);
					Console.Write('v');
					goto GetPlayerInput;
				case ConsoleKey.Enter:
					if (board[i, board.GetLength(1) - 1] != null)
					{
						goto GetPlayerInput;
					}
					for (int j = board.GetLength(1) - 1; ; j--)
					{
						if (j is 0 || board[i, j - 1].HasValue)
						{
							board[i, j] = true;
							move = (i, j);
							break;
						}
					}
					break;
				case ConsoleKey.Escape:
					Console.Clear();
					return;
				default: goto GetPlayerInput;
			}
			if (CheckFor4(move.I, move.J))
			{
				RenderBoard();
				Console.WriteLine();
				Console.WriteLine("   You Win!");
				goto PlayAgainCheck;
			}
		}
		else
		{
			int[] moves = Enumerable.Range(0, board.GetLength(0)).Where(i => !board[i, board.GetLength(1) - 1].HasValue).ToArray();
			int randomMove = moves[random.Next(moves.Length)];
			for (int j = board.GetLength(1) - 1; ; j--)
			{
				if (j is 0 || board[randomMove, j - 1].HasValue)
				{
					board[randomMove, j] = false;
					move = (randomMove, j);
					break;
				}
			}
			if (CheckFor4(move.I, move.J))
			{
				RenderBoard();
				Console.WriteLine();
				Console.WriteLine($"   You Lose!");
				goto PlayAgainCheck;
			}
		}
		if (CheckForDraw())
		{
			RenderBoard();
			Console.WriteLine();
			Console.WriteLine($"   Draw!");
			goto PlayAgainCheck;
		}
		else
		{
			player1Turn = !player1Turn;
		}
	}
PlayAgainCheck:
	Console.WriteLine("   Play Again [enter], or quit [escape]?");
GetInput:
	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.Enter: goto PlayAgain;
		case ConsoleKey.Escape: Console.Clear(); return;
		default: goto GetInput;
	}
}
finally
{
	Console.CursorVisible = true;
}

void ResetBoard()
{
	for (int i = 0; i < board.GetLength(0); i++)
	{
		for (int j = 0; j < board.GetLength(1); j++)
		{
			board[i, j] = null;
		}
	}
}

void RenderBoard()
{
	Console.Clear();
	Console.WriteLine();
	Console.WriteLine();
	Console.WriteLine();
	Console.WriteLine("   ╔" + new string('-', board.GetLength(0) * 2 + 1) + "╗");
	Console.Write("   ║ ");
	int iOffset = Console.CursorLeft;
	int jOffset = Console.CursorTop;
	Console.WriteLine(new string(' ', board.GetLength(0) * 2) + "║");
	for (int j = 1; j < board.GetLength(1) * 2; j++)
	{
		Console.WriteLine("   ║" + new string(' ', board.GetLength(0) * 2 + 1) + "║");
	}
	Console.WriteLine("   ╚" + new string('═', board.GetLength(0) * 2 + 1) + "╝");
	int iFinal = Console.CursorLeft;
	int jFinal = Console.CursorTop;
	for (int i = 0; i < board.GetLength(0); i++)
	{
		for (int j = 0; j < board.GetLength(1); j++)
		{
			Console.SetCursorPosition(i * 2 + iOffset, (board.GetLength(1) - j) * 2 + jOffset - 1);
			if (board[i, j] == true)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write('█');
				Console.ResetColor();
			}
			else if (board[i, j] == false)
			{
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.Write('█');
				Console.ResetColor();
			}
			else
			{
				Console.ResetColor();
				Console.Write(' ');
			}
		}
	}
	Console.SetCursorPosition(iFinal, jFinal);
}

bool CheckFor4(int i, int j)
{
	bool player = board[i, j].Value;
	{ // horizontal
		int inARow = 0;
		for (int _i = 0; _i < board.GetLength(0); _i++)
		{
			inARow = board[_i, j] == player ? inARow + 1 : 0;
			if (inARow >= 4) return true;
		}
	}
	{ // vertical
		int inARow = 0;
		for (int _j = 0; _j < board.GetLength(1); _j++)
		{
			inARow = board[i, _j] == player ? inARow + 1 : 0;
			if (inARow >= 4) return true;
		}
	}
	{ // postive slope diagonal
		int inARow = 0;
		int min = Math.Min(i, j);
		for (int _i = i - min, _j = j - min; _i < board.GetLength(0) && _j < board.GetLength(1); _i++, _j++)
		{
			inARow = board[_i, _j] == player ? inARow + 1 : 0;
			if (inARow >= 4) return true;
		}
	}
	{ // negative slope diagonal
		int inARow = 0;
		int offset = Math.Min(i, board.GetLength(1) - (j + 1));
		for (int _i = i - offset, _j = j + offset; _i < board.GetLength(0) && _j >= 0; _i++, _j--)
		{
			inARow = board[_i, _j] == player ? inARow + 1 : 0;
			if (inARow >= 4) return true;
		}
	}
	return false;
}

bool CheckForDraw()
{
	for (int i = 0; i < board.GetLength(0); i++)
	{
		if (!board[i, board.GetLength(1) - 1].HasValue)
		{
			return false;
		}
	}
	return true;
}