using System;
using static Towel.Statics;
using System.Threading.Tasks;
using System.Globalization;

namespace Website.Games.Sliding_Puzzle;

public class Sliding_Puzzle
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		Exception? exception = null;

		const string menu = @"
  Sliding Puzzle

  Choose Puzzle Size:
  [1] 3 x 3
  [2] 4 x 4
  [3] 5 x 5
  [escape] close";

		string info = @"
  Solve the puzzle by getting the tiles in   
  least-to-greatest order with the space in  
  the lower right. Use the arrow keys or WASD
  to slide the tiles into the space. Press   
  [escape] to return to the menu.            ";

		string youWon = @"
  *************** You Won! ***************   
  Press [enter] to return to the menu...     
                                             
                                             
                                             ";

		try
		{
			while (true)
			{
			Menu:
				await Console.Clear();
				await Console.Write(menu);
				int[,]? board = null;
				var (row, column) = (0, 0);
				while (board is null)
				{
					Console.CursorVisible = false;
					switch ((await Console.ReadKey(true)).Key)
					{
						case ConsoleKey.D1 or ConsoleKey.NumPad1: board = new int[3, 3]; break;
						case ConsoleKey.D2 or ConsoleKey.NumPad2: board = new int[4, 4]; break;
						case ConsoleKey.D3 or ConsoleKey.NumPad3: board = new int[5, 5]; break;
						case ConsoleKey.Escape: return;
					}
				}
				Initialize(board);
				while (IsSolved(board))
				{
					Initialize(board);
				}
				await Console.Clear();
				while (!IsSolved(board))
				{
					await Render(board);
					await Console.Write(info);
					var space = FindFlatLength(board);
					Console.CursorVisible = false;
					switch ((await Console.ReadKey(true)).Key)
					{
						case ConsoleKey.A or ConsoleKey.LeftArrow:
							if (space.Column < board.GetLength(1) - 1)
							{
								(board[space.Row, space.Column], board[space.Row, space.Column + 1]) = (board[space.Row, space.Column + 1], board[space.Row, space.Column]);
							}
							break;
						case ConsoleKey.D or ConsoleKey.RightArrow:
							if (space.Column > 0)
							{
								(board[space.Row, space.Column], board[space.Row, space.Column - 1]) = (board[space.Row, space.Column - 1], board[space.Row, space.Column]);
							}
							break;
						case ConsoleKey.W or ConsoleKey.UpArrow:
							if (space.Row < board.GetLength(0) - 1)
							{
								(board[space.Row, space.Column], board[space.Row + 1, space.Column]) = (board[space.Row + 1, space.Column], board[space.Row, space.Column]);
							}
							break;
						case ConsoleKey.S or ConsoleKey.DownArrow:
							if (space.Row > 0)
							{
								(board[space.Row, space.Column], board[space.Row - 1, space.Column]) = (board[space.Row - 1, space.Column], board[space.Row, space.Column]);
							}
							break;
						case ConsoleKey.Escape:
							goto Menu;
					}
				}
				await Render(board);
				await Console.Write(youWon);
			GetEnterOrEscape:
				Console.CursorVisible = false;
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.Enter or ConsoleKey.Escape: break;
					default: goto GetEnterOrEscape;
				}
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
			Console.ResetColor();
			await Console.Clear();
			await Console.WriteLine(exception?.ToString() ?? "Sliding Puzzle was closed.");
			await Console.Refresh();
		}

		async Task Render(int[,] board)
		{
			int space = board.FlatLength();
			await Console.SetCursorPosition(0, 0);
			await Console.WriteLine();
			await Console.WriteLine("  Sliding Puzzle");
			await Console.WriteLine();
			int tileWidth = board.FlatLength().ToString().Length;
			await Console.WriteLine($"  ╔{new string('═', board.GetLength(1) * (tileWidth + 1) + 1)}╗");
			for (int r = 0; r < board.GetLength(0); r++)
			{
				await Console.Write("  ║ ");
				for (int c = 0; c < board.GetLength(1); c++)
				{
					if (board[r, c] == space)
					{
						await Console.Write(new string(' ', tileWidth));
					}
					else
					{
						string value = board[r, c].ToString(CultureInfo.InvariantCulture);
						if (value.Length < 2 && board.FlatLength() > 9)
						{
							await Console.Write('0');
						}
						await Console.Write(value);
					}
					await Console.Write(' ');
				}
				await Console.WriteLine('║');
			}
			await Console.WriteLine($"  ╚{new string('═', board.GetLength(1) * (tileWidth + 1) + 1)}╝");
		}

		static void Initialize(int[,] board)
		{
			for (int i = 0, k = 1; i < board.FlatLength(); i++)
			{
				board.FlatSet(i, k++);
			}
			Shuffle(0, board.FlatLength() - 1, i => board.FlatGet(i), (i, v) => board.FlatSet(i, v));
			if (!IsSolvable(board))
			{
				if (board[0, 0] != board.FlatLength() && board[0, 1] != board.FlatLength())
				{
					(board[0, 0], board[0, 1]) = (board[0, 1], board[0, 0]);
				}
				else
				{
					(board[1, 0], board[1, 1]) = (board[1, 1], board[1, 0]);
				}
			}
		}

		static bool IsSolvable(int[,] board) =>
			board.GetLength(1) % 2 is 1
				? Inversions(board) % 2 is 0
				: (Inversions(board) + board.GetLength(0) - (FindFlatLength(board).Row + 1)) % 2 is 0;

		static (int Row, int Column) FindFlatLength(int[,] board)
		{
			for (int r = 0; r < board.GetLength(0); r++)
			{
				for (int c = 0; c < board.GetLength(1); c++)
				{
					if (board[r, c] == board.FlatLength())
					{
						return (r, c);
					}
				}
			}
			throw new Exception("bug. could not find (board.FlatLength()) in board");
		}

		static bool IsSolved(int[,] board)
		{
			for (int i = 0; i < board.FlatLength(); i++)
			{
				for (int j = i + 1; j < board.FlatLength(); j++)
				{
					if (board.FlatGet(i) > board.FlatGet(j))
					{
						return false;
					}
				}
			}
			return true;
		}

		static int Inversions(int[,] board)
		{
			int inversions = 0;
			for (int i = 0; i < board.FlatLength(); i++)
			{
				for (int j = i + 1; j < board.FlatLength(); j++)
				{
					if (!(board.FlatGet(i) == board.FlatLength() || board.FlatGet(j) == board.FlatLength()) &&
						board.FlatGet(i) > board.FlatGet(j))
					{
						inversions++;
					}
				}
			}
			return inversions;
		}
	}
}

public static class Sliding_Puzzle_Extensions
{
	public static int FlatLength<T>(this T[,] array2d) =>
		array2d.GetLength(0) * array2d.GetLength(1);

	public static T FlatGet<T>(this T[,] array2d, int index) =>
		array2d[index / array2d.GetLength(0), index % array2d.GetLength(0)];

	public static void FlatSet<T>(this T[,] array2d, int index, T value) =>
		array2d[index / array2d.GetLength(0), index % array2d.GetLength(0)] = value;
}
