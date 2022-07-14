using System;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Games.Connect_4;

public class Connect_4
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		Exception? exception = null;

		bool?[,] board = new bool?[7, 6];
		bool player1Turn;
		bool player1MovesFirst = true;
		Random random = new();

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
					await RenderBoard();
					int i = 0;
					await Console.SetCursorPosition(moveMinI, moveJ);
					await Console.Write('v');
				GetPlayerInput:
					switch ((await Console.ReadKey(true)).Key)
					{
						case ConsoleKey.LeftArrow:
							await Console.SetCursorPosition(i * 2 + moveMinI, moveJ);
							await Console.Write(' ');
							i = Math.Max(0, i - 1);
							await Console.SetCursorPosition(i * 2 + moveMinI, moveJ);
							await Console.Write('v');
							goto GetPlayerInput;
						case ConsoleKey.RightArrow:
							await Console.SetCursorPosition(i * 2 + moveMinI, moveJ);
							await Console.Write(' ');
							i = Math.Min(board.GetLength(0) - 1, i + 1);
							await Console.SetCursorPosition(i * 2 + moveMinI, moveJ);
							await Console.Write('v');
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
							await Console.Clear();
							return;
						default: goto GetPlayerInput;
					}
					if (CheckFor4(move.I, move.J))
					{
						await RenderBoard();
						await Console.WriteLine();
						await Console.WriteLine("   You Win!");
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
						await RenderBoard();
						await Console.WriteLine();
						await Console.WriteLine($"   You Lose!");
						goto PlayAgainCheck;
					}
				}
				if (CheckForDraw())
				{
					await RenderBoard();
					await Console.WriteLine();
					await Console.WriteLine($"   Draw!");
					goto PlayAgainCheck;
				}
				else
				{
					player1Turn = !player1Turn;
				}
			}
		PlayAgainCheck:
			await Console.WriteLine("   Play Again [enter], or quit [escape]?");
		GetInput:
			switch ((await Console.ReadKey(true)).Key)
			{
				case ConsoleKey.Enter: goto PlayAgain;
				case ConsoleKey.Escape: await Console.Clear(); return;
				default: goto GetInput;
			}
		}
		catch (Exception e)
		{
			exception = e;
			throw;
		}
		finally
		{
			Console.CursorVisible = true;
			await Console.Clear();
			await Console.WriteLine(exception?.ToString() ?? "Connect 4 was closed.");
			await Console.Refresh();
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

		async Task RenderBoard()
		{
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine();
			await Console.WriteLine();
			await Console.WriteLine("   ╔" + new string('-', board.GetLength(0) * 2 + 1) + "╗");
			await Console.Write("   ║ ");
			int iOffset = Console.CursorLeft;
			int jOffset = Console.CursorTop;
			await Console.WriteLine(new string(' ', board.GetLength(0) * 2) + "║");
			for (int j = 1; j < board.GetLength(1) * 2; j++)
			{
				await Console.WriteLine("   ║" + new string(' ', board.GetLength(0) * 2 + 1) + "║");
			}
			await Console.WriteLine("   ╚" + new string('═', board.GetLength(0) * 2 + 1) + "╝");
			int iFinal = Console.CursorLeft;
			int jFinal = Console.CursorTop;
			for (int i = 0; i < board.GetLength(0); i++)
			{
				for (int j = 0; j < board.GetLength(1); j++)
				{
					await Console.SetCursorPosition(i * 2 + iOffset, (board.GetLength(1) - j) * 2 + jOffset - 1);
					if (board[i, j] == true)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						await Console.Write('█');
						Console.ResetColor();
					}
					else if (board[i, j] == false)
					{
						Console.ForegroundColor = ConsoleColor.Blue;
						await Console.Write('█');
						Console.ResetColor();
					}
					else
					{
						Console.ResetColor();
						await Console.Write(' ');
					}
				}
			}
			await Console.SetCursorPosition(iFinal, jFinal);
		}

		bool CheckFor4(int i, int j)
		{
			bool player = board[i, j]!.Value;
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
	}
}
