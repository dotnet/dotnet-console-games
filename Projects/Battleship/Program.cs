using System;
using Towel;
using Towel.DataStructures;

const int boardHeight = 10;
const int boardWidth = 10;
bool[,] offense;
Ship[,] offenseShips;
bool[,] defense;
Ship[,] defenseShips;
(int BufferHeight, int BufferWidth, int WindowHeight, int WindowWidth) consoleSize;
bool placing = false;
(Ship Ship, int Size, int Row, int Column, bool Vertical) placement = default;
bool escape = false;

try
{
	while (!escape)
	{
		offense = new bool[boardHeight, boardWidth];
		offenseShips = new Ship[boardHeight, boardWidth];
		defense = new bool[boardHeight, boardWidth];
		defenseShips = new Ship[boardHeight, boardWidth];
		Console.BackgroundColor = ConsoleColor.Black;
		Console.ForegroundColor = ConsoleColor.White;
		Console.Clear();
		consoleSize = ConsoleSize();
		PlaseDefenseShips();
		RandomizeOffenseShips();
		RenderMainView();
		Console.ReadKey(true);
	}
}
finally
{
	Console.CursorVisible = true;
	Console.ResetColor();
	Console.Clear();
	Console.Write("Battleship was closed.");
}

void PlaseDefenseShips()
{
	placing = true;
	foreach (Ship ship in Enum.GetValues<Ship>())
	{
		int size = (int)ship.GetTag("size").Value!;
		placement = (ship, size, 0, 0, true);
		while (true)
		{
			RenderMainView();
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.UpArrow:
					placement.Row = Math.Max(placement.Row - 1, 0);
					break;
				case ConsoleKey.DownArrow:
					placement.Row = Math.Min(placement.Row + 1, boardHeight - (placement.Vertical ? size : 1));
					placement.Column = Math.Min(placement.Column, boardWidth - (!placement.Vertical ? size : 1));
					break;
				case ConsoleKey.LeftArrow:
					placement.Column = Math.Max(placement.Column - 1, 0);
					break;
				case ConsoleKey.RightArrow:
					placement.Row = Math.Min(placement.Row, boardHeight - (placement.Vertical ? size : 1));
					placement.Column = Math.Min(placement.Column + 1, boardWidth - (!placement.Vertical ? size : 1));
					break;
				case ConsoleKey.Spacebar:
					placement.Vertical = !placement.Vertical;
					placement.Row    = Math.Min(placement.Row, boardHeight - (placement.Vertical ? size : 1));
					placement.Column = Math.Min(placement.Column, boardWidth  - (!placement.Vertical ? size : 1));
					break;
				case ConsoleKey.Enter:
					goto Continue;
				case ConsoleKey.Escape:
					escape = true;
					return;
			}
		}
	Continue:
		continue;
	}
}

void RandomizeOffenseShips()
{
	foreach (Ship ship in Enum.GetValues<Ship>())
	{
		int size = (int)ship.GetTag("size").Value!;
		ListArray<(int Row, int Column, bool Vertical)> locations = new();
		for (int r = 0; r < boardHeight - size; r++)
		{
			for (int c = 0; c < boardWidth; c++)
			{
				bool vertical = true;
				bool horizontal = true;
				for (int i = 0; i < size; i++)
				{
					if (r + size > boardHeight || offenseShips[r + i, c] is not 0)
					{
						vertical = false;
					}
					if (c + size > boardWidth || offenseShips[r, c + i] is not 0)
					{
						horizontal = false;
					}
				}
				if (vertical)
				{
					locations.Add((r, c, true));
				}
				if (horizontal)
				{
					locations.Add((r, c, false));
				}
			}
		}
		var (Row, Column, Vertical) = locations[Random.Shared.Next(0, locations.Count)];
		for (int i = 0; i < size; i++)
		{
			offenseShips[Row + (Vertical ? i : 0), Column + (!Vertical ? i : 0)] = ship;
		}
	}
}

void RenderMainView()
{
	Console.CursorVisible = false;
	if (OperatingSystem.IsWindows() && Console.BufferHeight != Console.WindowHeight)
	{
		Console.BufferHeight = Console.WindowHeight;
	}
	if (OperatingSystem.IsWindows() && Console.BufferWidth != Console.WindowWidth)
	{
		Console.BufferWidth = Console.WindowWidth;
	}
	if (consoleSize != ConsoleSize())
	{
		Console.Clear();
		consoleSize = ConsoleSize();
	}

	Console.SetCursorPosition(0, 0);
	Console.WriteLine();
	Console.WriteLine("  Battleship");
	Console.WriteLine();
	for (int r = 0; r < boardHeight * 2 + 1; r++)
	{
		int br = (r - 1) / 2;
		Console.Write("  ");
		for (int c = 0; c < boardWidth * 2 + 1; c++)
		{
			int bc = (c - 1) / 2;
			bool v = br + 1 < boardHeight && defenseShips[br, bc] == defenseShips[br + 1, bc];
			bool h = bc + 1 < boardWidth && defenseShips[br, bc] == defenseShips[br, bc + 1];
			if (placing &&
				placement.Vertical &&
				bc == placement.Column &&
				br >= placement.Row &&
				br < placement.Row + placement.Size &&
				(c - 1) % 2 is 0 &&
				!(br == placement.Row + placement.Size - 1 && (r - 1) % 2 is 1) &&
				r is not 0)
			{
				Console.BackgroundColor = ConsoleColor.DarkGreen;
			}
			else if (placing &&
				!placement.Vertical &&
				br == placement.Row &&
				bc >= placement.Column &&
				bc < placement.Column + placement.Size &&
				(r - 1) % 2 is 0 &&
				!(bc == placement.Column + placement.Size - 1 && (c - 1) % 2 is 1) &&
				c is not 0)
			{
				Console.BackgroundColor = ConsoleColor.DarkGreen;
			}
			else if (defenseShips[br, bc] is not 0 &&
				((r - 1) % 2 is 0 || ((r - 1) % 2 is 1 && v)) &&
				((c - 1) % 2 is 0 || ((c - 1) % 2 is 1 && h)))
			{
				Console.BackgroundColor = ConsoleColor.DarkGray;
			}
			Console.Write(RenderBoardTile(r, c, defense, defenseShips));
			Console.BackgroundColor = ConsoleColor.Black;
		}
		Console.Write("  ");
		for (int c = 0; c < boardWidth * 2 + 1; c++)
		{
			int bc = (c - 1) / 2;
			bool v = br + 1 < boardHeight && offenseShips[br, bc] == offenseShips[br + 1, bc];
			bool h = bc + 1 < boardWidth && offenseShips[br, bc] == offenseShips[br, bc + 1];
			if (offenseShips[br, bc] is not 0 &&
				((r - 1) % 2 is 0 || ((r - 1) % 2 is 1 && v)) &&
				((c - 1) % 2 is 0 || ((c - 1) % 2 is 1 && h)))
			{
				Console.BackgroundColor = ConsoleColor.DarkGray;
			}
			Console.Write(RenderBoardTile(r, c, offense, offenseShips));
			Console.BackgroundColor = ConsoleColor.Black;
		}
		Console.WriteLine();
	}

	string RenderBoardTile(int r, int c, bool[,] shots, Ship[,] ships)
	{
		const string hit = "##";
		const string miss = "XX";
		const string open = "  ";
		const int w = boardWidth * 2;
		const int h = boardHeight * 2;
		return (r, c, r % 2, c % 2) switch
		{
			(0, 0, _, _) => "┌",
			(h, 0, _, _) => "└",
			(0, w, _, _) => "┐",
			(h, w, _, _) => "┘",
			(0, _, 0, 0) => "┬",
			(_, 0, 0, 0) => "├",
			(_, w, 0, _) => "┤",
			(h, _, _, 0) => "┴",
			(_, _, 0, 0) => "┼",
			(_, _, 1, 0) => "│",
			(_, _, 0, 1) => "──",
			_ =>
				shots[(r - 1) / 2, (c - 1) / 2]
					? (ships[(r - 1) / 2, (c - 1) / 2] is not 0
						? hit
						: miss)
					: open,
		};
	}
}

(int BufferHeight, int BufferWidth, int WindowHeight, int WindowWidth) ConsoleSize() =>
	(Console.BufferHeight, Console.BufferWidth, Console.WindowHeight, Console.WindowWidth);

enum Ship
{
	[Tag("size", 5)] Carrier = 1,
	[Tag("size", 4)] Battleship = 2,
	[Tag("size", 3)] Cruiser = 3,
	[Tag("size", 3)] Submarine = 4,
	[Tag("size", 2)] Destroyer = 5,
}