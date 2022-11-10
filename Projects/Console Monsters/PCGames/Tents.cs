using static System.Formats.Asn1.AsnWriter;
using System.Globalization;
using Towel.DataStructures;

namespace Console_Monsters.PCGames;
public class Tents
{
	static Tile[,] map;
	static int[] columnTents;
	static int[] rowTents;
	static (int Top, int Left) selection;
	static bool escape = false;
	static int consoleWidth;
	static int consoleHeight;

	static char[,] TentSprite = StringToSprite(
		@"  \/  " + "\n" +
		@"  /\  " + "\n" +
		@" //\\ ");

	static char[,] TreeSprite = StringToSprite(
		@" (@@) " + "\n" +
		@"(@@@@)" + "\n" +
		@"  ||  ");

	static char[,] OpenSprite = StringToSprite(
		@"      " + "\n" +
		@"      " + "\n" +
		@"      ");

	public static void Run()
	{
		Console.Clear();
		Exception? exception = null;
		escape = false;

		try
		{
			consoleHeight = Console.WindowHeight;
			consoleWidth =  Console.WindowWidth;
		PlayAgain:
			selection = (0, 0);
			InitializeMapAndCounts(6, 6);
			while (!escape && !IsSolved())
			{
				RenderBoard();
				RenderPlayingMessage();
				HandleInput();
			}
			if (escape) return;
			Console.Clear();
			selection = (-1, -1);
			RenderBoard();
			RenderSolvedMessage();
		GetEnterOrEscape:
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.Enter: goto PlayAgain;
				case ConsoleKey.Escape: return;
				default: goto GetEnterOrEscape;
			}
		}
		catch (Exception e)
		{
			exception = e;
			throw;
		}
		finally
		{
			Console.ResetColor();
			Console.Clear();
			PCGamesScreen.Render();
		}
	}

	static char[,] StringToSprite(string s)
	{
		string[] splits = s.Split('\n');
		char[,] sprite = new char[splits.Length, splits[0].Length];
		for (int i = 0; i < sprite.GetLength(0); i++)
		{
			for (int j = 0; j < sprite.GetLength(1); j++)
			{
				sprite[i, j] = splits[i][j];
			}
		}
		return sprite;
	}

	static void InitializeMapAndCounts(int rows, int columns)
	{
		map = new Tile[rows, columns];
		columnTents = new int[columns];
		rowTents = new int[rows];
		// generate random map
		HashSet<(int Top, int Left)> unavailable = new();
		while (unavailable.Count < rows * columns)
		{
		Continue:
			int next = Random.Shared.Next(0, rows * columns - unavailable.Count);
			for (int i = 0, k = -1; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					if (!unavailable.Contains((i, j)))
					{
						k++;
					}
					if (k == next)
					{
						int availableTreeLocations = 0;
						/* N */
						if (i > 0 && !unavailable.Contains((i - 1, j))) availableTreeLocations++;
						/* S */
						if (i < rows - 1 && !unavailable.Contains((i + 1, j))) availableTreeLocations++;
						/* E */
						if (j < columns - 1 && !unavailable.Contains((i, j + 1))) availableTreeLocations++;
						/* W */
						if (j > 0 && !unavailable.Contains((i, j - 1))) availableTreeLocations++;
						if (availableTreeLocations is 0)
						{
							unavailable.Add((i, j));
						}
						else
						{
							map[i, j] = Tile.Tent;
							unavailable.Add((i, j));
							Random.Shared.Next(availableTreeLocations);
							/* N */
							if (i > 0 && !unavailable.Contains((i - 1, j)) && --availableTreeLocations is 0) { unavailable.Add((i - 1, j)); map[i - 1, j] = Tile.Tree; }
							/* S */
							if (i < rows - 1 && !unavailable.Contains((i + 1, j)) && --availableTreeLocations is 0) { unavailable.Add((i + 1, j)); map[i + 1, j] = Tile.Tree; }
							/* E */
							if (j < columns - 1 && !unavailable.Contains((i, j + 1)) && --availableTreeLocations is 0) { unavailable.Add((i, j + 1)); map[i, j + 1] = Tile.Tree; }
							/* W */
							if (j > 0 && !unavailable.Contains((i, j - 1)) && --availableTreeLocations is 0) { unavailable.Add((i, j - 1)); map[i, j - 1] = Tile.Tree; }
							if (i > 0 && j > 0) unavailable.Add((i - 1, j - 1));
							if (i > 0) unavailable.Add((i - 1, j));
							if (i < rows - 1) unavailable.Add((i + 1, j));
							if (i < rows - 1 && j < columns - 1) unavailable.Add((i + 1, j + 1));
							if (j > 0) unavailable.Add((i, j - 1));
							if (j < columns - 1) unavailable.Add((i, j + 1));
							if (i < rows - 1 && j > 0) unavailable.Add((i + 1, j - 1));
							if (i > 0 && j < columns - 1) unavailable.Add((i - 1, j + 1));
						}
						goto Continue;
					}
				}
			}
		}
		// count tents per column
		columnTents = new int[columns];
		for (int i = 0; i < columns; i++)
		{
			int tentCount = 0;
			for (int j = 0; j < rows; j++)
			{
				if (map[j, i] is Tile.Tent)
				{
					tentCount++;
				}
			}
			columnTents[i] = tentCount;
		}
		// count tents per row
		for (int i = 0; i < rows; i++)
		{
			int tentCount = 0;
			for (int j = 0; j < columns; j++)
			{
				if (map[i, j] is Tile.Tent)
				{
					tentCount++;
				}
			}
			rowTents[i] = tentCount;
		}
		// clear tents from map
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				if (map[i, j] is Tile.Tent)
				{
					map[i, j] = Tile.Empty;
				}
			}
		}
	}

	static void HandleInput()
	{
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.W or ConsoleKey.UpArrow: selection.Top = Math.Max(0, selection.Top - 1); break;
			case ConsoleKey.S or ConsoleKey.DownArrow: selection.Top = Math.Min(map.GetLength(0) - 1, selection.Top + 1); break;
			case ConsoleKey.A or ConsoleKey.LeftArrow: selection.Left = Math.Max(0, selection.Left - 1); break;
			case ConsoleKey.D or ConsoleKey.RightArrow: selection.Left = Math.Min(map.GetLength(1) - 1, selection.Left + 1); break;
			case ConsoleKey.Enter:
				switch (map[selection.Top, selection.Left])
				{
					case Tile.Tent: map[selection.Top, selection.Left] = Tile.Empty; break;
					case Tile.Empty: map[selection.Top, selection.Left] = Tile.Tent; break;
				}
				break;
			case ConsoleKey.Escape: escape = true; break;
		}
	}

	static void RenderBoard()
	{
		if (consoleHeight != Console.WindowHeight || consoleWidth != Console.WindowWidth)
		{
			Console.Clear();
			consoleHeight = Console.WindowHeight;
			consoleWidth = Console.WindowWidth;
		}
		int boardIndexPixelHeight = OpenSprite.GetLength(0) + 1;
		int boardIndexPixelWidth = OpenSprite.GetLength(1) + 1;
		int boardPixelsTall = map.GetLength(0) * (OpenSprite.GetLength(0) + 1) + 1;
		int boardPixelsWide = map.GetLength(1) * (OpenSprite.GetLength(1) + 1) + 1;
		StringBuilder render = new();
		Console.CursorVisible = false;
		Console.SetCursorPosition(0, 0);
		render.AppendLine();
		render.AppendLine("  Tents");
		render.AppendLine();
		for (int i = 0, mapi = 0, tilei = 0; i < boardPixelsTall; i++, mapi = i / boardIndexPixelHeight, tilei = (i - 1) % boardIndexPixelHeight)
		{
			render.Append("  ");
			for (int j = 0, mapj = 0, tilej = 0; j < boardPixelsWide; j++, mapj = j / boardIndexPixelWidth, tilej = (j - 1) % boardIndexPixelWidth)
			{
				if (i is 0 && j is 0) render.Append('╔');
				else if (i is 0 && j == boardPixelsWide - 1) render.Append('╗');
				else if (i == boardPixelsTall - 1 && j is 0) render.Append('╚');
				else if (i == boardPixelsTall - 1 && j == boardPixelsWide - 1) render.Append('╝');
				else if (j % boardIndexPixelWidth is 0 && i is 0) render.Append('╦');
				else if (j is 0 && i % boardIndexPixelHeight is 0) render.Append('╠');
				else if (j == boardPixelsWide - 1 && i % boardIndexPixelHeight is 0) render.Append('╣');
				else if (j % boardIndexPixelWidth is 0 && i == boardPixelsTall - 1) render.Append('╩');
				else if (j % boardIndexPixelWidth is 0 && i % boardIndexPixelHeight is 0) render.Append('╬');
				else if (i % boardIndexPixelHeight is 0) render.Append('═');
				else if (j % boardIndexPixelWidth is 0) render.Append('║');
				else
				{
					char c = GetSprite(map[mapi, mapj])[tilei, tilej];
					if (selection == (mapi, mapj))
					{
						if (render.Length > 0)
						{
							Console.Write(render);
							render.Clear();
						}
						if (map[mapi, mapj] is Tile.Tent && !IsValidTent(mapi, mapj))
						{
							Console.ForegroundColor = ConsoleColor.DarkRed;
						}
						else
						{
							Console.ForegroundColor = ConsoleColor.Black;
						}
						Console.BackgroundColor = ConsoleColor.White;
						Console.Write(c);
						Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.White;
					}
					else if (map[mapi, mapj] is Tile.Tent && !IsValidTent(mapi, mapj))
					{
						if (render.Length > 0)
						{
							Console.Write(render);
							render.Clear();
						}
						Console.BackgroundColor = ConsoleColor.DarkRed;
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write(c);
						Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.White;
					}
					else
					{
						render.Append(c);
					}
				}
			}
			if (tilei is 1)
			{
				render.Append($" {rowTents[mapi]}");
			}
			render.AppendLine();
		}
		render.Append("  ");
		for (int i = 0; i < map.GetLength(1); i++)
		{
			render.Append("   ");
			string count = columnTents[i].ToString(CultureInfo.InvariantCulture);
			render.Append(count);
			render.Append(new string(' ', boardIndexPixelWidth - (3 + count.Length)));
		}
		render.AppendLine();
		Console.Write(render);
	}

	static void RenderPlayingMessage()
	{
		StringBuilder render = new();
		render.AppendLine();
		render.AppendLine("  Place a tent above, below, left, or right of every tree.");
		render.AppendLine("  Do not exceed the expected tent count per row or column.");
		render.AppendLine("  Tents may not touch other tents even diagonally.");
		render.AppendLine("  Press [WASD] or [arrow keys] to select tiles.");
		render.AppendLine("  Press [enter] to place or remove tents.");
		render.AppendLine("  Press [escape] to exit.");
		Console.Write(render);
	}

	static void RenderSolvedMessage()
	{
		StringBuilder render = new();
		render.AppendLine();
		render.AppendLine("  ******* You solved the puzzle! *******");
		render.AppendLine("  Press [enter] to play again.");
		render.AppendLine("  Press [escape] to exit.");
		Console.Write(render);
	}

	static char[,] GetSprite(Tile tile) => tile switch
	{
		Tile.Empty => OpenSprite,
		Tile.Tree => TreeSprite,
		Tile.Tent => TentSprite,
		_ => throw new NotImplementedException()
	};

	static bool IsValidTent(int top, int left)
	{
		// column and row counts
		{
			int tentCount = 0;
			for (int i = 0; i < map.GetLength(0); i++)
			{
				if (map[i, left] is Tile.Tent)
				{
					tentCount++;
				}
			}
			if (tentCount > columnTents[left]) return false;
		}
		{
			int tentCount = 0;
			for (int i = 0; i < map.GetLength(1); i++)
			{
				if (map[top, i] is Tile.Tent)
				{
					tentCount++;
				}
			}
			if (tentCount > rowTents[top]) return false;
		}
		// touching another tent
		if (top > 0 && left > 0 && map[top - 1, left - 1] is Tile.Tent) return false;
		if (left > 0 && map[top, left - 1] is Tile.Tent) return false;
		if (top > 0 && map[top - 1, left] is Tile.Tent) return false;
		if (top < map.GetLength(0) - 1 && map[top + 1, left] is Tile.Tent) return false;
		if (left < map.GetLength(1) - 1 && map[top, left + 1] is Tile.Tent) return false;
		if (top < map.GetLength(0) - 1 && left < map.GetLength(1) - 1 && map[top + 1, left + 1] is Tile.Tent) return false;
		if (top > 0 && left < map.GetLength(1) - 1 && map[top - 1, left + 1] is Tile.Tent) return false;
		if (left > 0 && top < map.GetLength(0) - 1 && map[top + 1, left - 1] is Tile.Tent) return false;
		// adjecent to a tree
		if (top > 0 && map[top - 1, left] is Tile.Tree) return true;
		if (left > 0 && map[top, left - 1] is Tile.Tree) return true;
		if (top < map.GetLength(0) - 1 && map[top + 1, left] is Tile.Tree) return true;
		if (left < map.GetLength(1) - 1 && map[top, left + 1] is Tile.Tree) return true;
		return false;
	}

	static bool IsSolved()
	{
		// tents per column
		for (int i = 0; i < map.GetLength(1); i++)
		{
			int tentCount = 0;
			for (int j = 0; j < map.GetLength(0); j++)
			{
				if (map[j, i] is Tile.Tent)
				{
					tentCount++;
				}
			}
			if (columnTents[i] != tentCount) return false;
		}
		// tents per row
		for (int i = 0; i < map.GetLength(0); i++)
		{
			int tentCount = 0;
			for (int j = 0; j < map.GetLength(1); j++)
			{
				if (map[i, j] is Tile.Tent)
				{
					tentCount++;
				}
			}
			if (rowTents[i] != tentCount) return false;
		}
		// validate tent placements
		for (int i = 0; i < map.GetLength(0); i++)
		{
			for (int j = 0; j < map.GetLength(1); j++)
			{
				if (map[i, j] is Tile.Tent && !IsValidTent(i, j))
				{
					return false;
				}
			}
		}
		return true;
	}

	public enum Tile
	{
		Empty = 0,
		Tree = 1,
		Tent = 2,
	}
}
