using System;
using System.Threading.Tasks;
using Towel;
using Towel.DataStructures;

namespace Website.Games.Battleship;

public class Battleship
{
	public readonly BlazorConsole Console = new();
	public BlazorConsole OperatingSystem;

	public Battleship()
	{
		OperatingSystem = Console;
	}

	public async Task Run()
	{
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
		(int Row, int Column) selection = default;
		bool selecting = false;
		Func<Task>? renderMessage = null;

		try
		{
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			await Console.Clear();
			consoleSize = ConsoleSize();

			while (!escape)
			{
				offense = new bool[boardHeight, boardWidth];
				offenseShips = new Ship[boardHeight, boardWidth];
				defense = new bool[boardHeight, boardWidth];
				defenseShips = new Ship[boardHeight, boardWidth];

				// introduction screen
				await Console.Clear();
				renderMessage = async () =>
				{
					await Console.WriteLine();
					await Console.WriteLine("  This is a guessing game where you will place your battle ships");
					await Console.WriteLine("  on a grid, and then shoot locations of the enemy grid trying");
					await Console.WriteLine("  to find and sink all of their ships.The first player to sink");
					await Console.WriteLine("  all the enemy ships wins.");
					await Console.WriteLine();
					await Console.WriteLine("  Press [escape] at any time to close the game.");
					await Console.WriteLine();
					await Console.WriteLine("  Press [enter] to begin...");
				};
				await RenderMainView();
				await GetEnterOrEscape();
				if (escape)
				{
					return;
				}

				// ship placement
				await Console.Clear();
				await PlaceDefenseShips();
				if (escape)
				{
					return;
				}
				RandomizeOffenseShips();
				renderMessage = async () =>
				{
					await Console.WriteLine();
					await Console.WriteLine("  The enemy has placed their ships.");
					await Console.WriteLine();
					await Console.WriteLine("  Press [enter] to continue...");
				};
				await RenderMainView();

				// shooting phase
				selection = (boardHeight / 2, boardWidth / 2);
				await Console.Clear();
				renderMessage = async () =>
				{
					await Console.WriteLine();
					await Console.WriteLine("  Choose your shots.");
					await Console.WriteLine();
					await Console.WriteLine("  Hit: ##");
					await Console.WriteLine("  Miss: XX");
					await Console.WriteLine("  Use arrow keys to aim.");
					await Console.WriteLine("  Use [enter] to fire at the location.");
				};
				selecting = true;
				while (!Won(defenseShips, defense) && !Won(offenseShips, offense))
				{
					await ChooseOffense();
					if (escape)
					{
						return;
					}
					RandomlyChooseDefense();
					await RenderMainView();
				}
				selecting = false;

				// game over
				await Console.Clear();
				renderMessage = async () =>
				{
					await Console.WriteLine();
					switch ((Won(defenseShips, defense), Won(offenseShips, offense)))
					{
						case (true, true):
							await Console.WriteLine("  Draw! All ships were sunk.");
							break;
						case (false, true):
							await Console.WriteLine("  You Win! You sunk all the enemy ships.");
							break;
						case (true, false):
							await Console.WriteLine("  You Lose! The enemy sunk all your ships.");
							break;
					}
					await Console.WriteLine();
					await Console.WriteLine("  Play again [enter] or quit [escape]?");
				};
				await RenderMainView(showEnemyShips: true);
				await GetEnterOrEscape();
			}
		}
		finally
		{
			Console.CursorVisible = true;
			Console.ResetColor();
			await Console.Clear();
			await Console.Write("Battleship was closed.");
			await Console.Refresh();
		}

		async Task PlaceDefenseShips()
		{
			placing = true;
			foreach (Ship ship in Enum.GetValues<Ship>())
			{
				renderMessage = async () =>
				{
					await Console.WriteLine();
					await Console.WriteLine($"  Place your {ship} on the grid.");
					await Console.WriteLine();
					await Console.WriteLine("  Use arrow keys to move the ship.");
					await Console.WriteLine("  Use [spacebar] to rotate the ship.");
					await Console.WriteLine("  Use [enter] to place the ship in a valid location.");
				};

				int size = (int)ship.GetTag("size").Value!;
				placement = (ship, size, 0, 0, true);
				while (true)
				{
					await RenderMainView();
					switch ((await Console.ReadKey(true)).Key)
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
							placement.Row = Math.Min(placement.Row, boardHeight - (placement.Vertical ? size : 1));
							placement.Column = Math.Min(placement.Column, boardWidth - (!placement.Vertical ? size : 1));
							break;
						case ConsoleKey.Enter:
							if (IsValidPlacement())
							{
								for (int i = 0; i < placement.Size; i++)
								{
									defenseShips[placement.Row + (placement.Vertical ? i : 0), placement.Column + (!placement.Vertical ? i : 0)] = ship;
								}
								goto Continue;
							}
							break;
						case ConsoleKey.Escape:
							escape = true;
							return;
					}
				}
			Continue:
				continue;
			}
			placing = false;
		}

		async Task ChooseOffense()
		{
			while (true)
			{
				await RenderMainView();
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.UpArrow:
						selection.Row = Math.Max(0, selection.Row - 1);
						break;
					case ConsoleKey.DownArrow:
						selection.Row = Math.Min(boardHeight - 1, selection.Row + 1);
						break;
					case ConsoleKey.LeftArrow:
						selection.Column = Math.Max(0, selection.Column - 1);
						break;
					case ConsoleKey.RightArrow:
						selection.Column = Math.Min(boardWidth - 1, selection.Column + 1);
						break;
					case ConsoleKey.Enter:
						if (!offense[selection.Row, selection.Column])
						{
							offense[selection.Row, selection.Column] = true;
							placing = false;
							return;
						}
						break;
					case ConsoleKey.Escape:
						escape = true;
						placing = false;
						return;
				}
			}
		}

		void RandomlyChooseDefense()
		{
			if (Random.Shared.Next(9) is 0)
			{
				for (int r = 0; r < boardHeight; r++)
				{
					for (int c = 0; c < boardHeight; c++)
					{
						if (!defense[r, c] && defenseShips[r, c] is not 0)
						{
							defense[r, c] = true;
							return;
						}
					}
				}
			}
			else
			{
				ListArray<(int Row, int Column)> openlocations = new();
				for (int r = 0; r < boardHeight; r++)
				{
					for (int c = 0; c < boardHeight; c++)
					{
						if (!defense[r, c])
						{
							openlocations.Add((r, c));
						}
					}
				}
				var (row, column) = openlocations[Random.Shared.Next(openlocations.Count)];
				defense[row, column] = true;
			}
		}

		bool IsValidPlacement()
		{
			for (int i = 0; i < placement.Size; i++)
			{
				if (defenseShips[placement.Row + (placement.Vertical ? i : 0), placement.Column + (!placement.Vertical ? i : 0)] is not 0)
				{
					return false;
				}
			}
			return true;
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

		bool Won(Ship[,] shipBoard, bool[,] shotBoard)
		{
			for (int r = 0; r < boardHeight; r++)
			{
				for (int c = 0; c < boardWidth; c++)
				{
					if (shipBoard[r, c] is not 0 && !shotBoard[r, c])
					{
						return false;
					}
				}
			}
			return true;
		}

		async Task RenderMainView(bool showEnemyShips = false)
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
				await Console.Clear();
				consoleSize = ConsoleSize();
			}

			await Console.SetCursorPosition(0, 0);
			await Console.WriteLine();
			await Console.WriteLine("  Battleship");
			await Console.WriteLine();
			for (int r = 0; r < boardHeight * 2 + 1; r++)
			{
				int br = (r - 1) / 2;
				await Console.Write("  ");
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
						Console.BackgroundColor = IsValidPlacement() ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed;
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
						Console.BackgroundColor = IsValidPlacement() ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed;
					}
					else if (defenseShips[br, bc] is not 0 &&
						((r - 1) % 2 is 0 || ((r - 1) % 2 is 1 && v)) &&
						((c - 1) % 2 is 0 || ((c - 1) % 2 is 1 && h)))
					{
						Console.BackgroundColor = ConsoleColor.DarkGray;
					}
					await Console.Write(RenderBoardTile(r, c, defense, defenseShips));
					if (Console.BackgroundColor is not ConsoleColor.Black)
					{
						Console.BackgroundColor = ConsoleColor.Black;
					}
				}
				await Console.Write("  ");
				for (int c = 0; c < boardWidth * 2 + 1; c++)
				{
					int bc = (c - 1) / 2;
					bool v = br + 1 < boardHeight && offenseShips[br, bc] == offenseShips[br + 1, bc];
					bool h = bc + 1 < boardWidth && offenseShips[br, bc] == offenseShips[br, bc + 1];
					if (showEnemyShips &&
						offenseShips[br, bc] is not 0 &&
						((r - 1) % 2 is 0 || ((r - 1) % 2 is 1 && v)) &&
						((c - 1) % 2 is 0 || ((c - 1) % 2 is 1 && h)))
					{
						Console.BackgroundColor = ConsoleColor.DarkGray;
					}
					else if (selecting && selection == (br, bc) &&
						(r - 1) % 2 is 0 &&
						(c - 1) % 2 is 0)
					{
						Console.BackgroundColor = ConsoleColor.DarkYellow;
					}
					await Console.Write(RenderBoardTile(r, c, offense, offenseShips));
					if (Console.BackgroundColor is not ConsoleColor.Black)
					{
						Console.BackgroundColor = ConsoleColor.Black;
					}
				}
				await Console.WriteLine();
			}
			if (renderMessage is not null)
			{
				await renderMessage();
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

		async Task GetEnterOrEscape()
		{
		GetEnterOrEscape:
			switch ((await Console.ReadKey(true)).Key)
			{
				case ConsoleKey.Enter: break;
				case ConsoleKey.Escape: escape = true; break;
				default: goto GetEnterOrEscape;
			}
		}

		(int BufferHeight, int BufferWidth, int WindowHeight, int WindowWidth) ConsoleSize() =>
			(Console.BufferHeight, Console.BufferWidth, Console.WindowHeight, Console.WindowWidth);
	}

	enum Ship
	{
		[Tag("size", 5)] Carrier = 1,
		[Tag("size", 4)] Battleship = 2,
		[Tag("size", 3)] Cruiser = 3,
		[Tag("size", 3)] Submarine = 4,
		[Tag("size", 2)] Destroyer = 5,
	}
}
