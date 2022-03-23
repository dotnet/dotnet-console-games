using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Towel;
using static Towel.Statics;

namespace Website.Games.PacMan;

public class PacMan
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		#region Ascii

		// ╔═══════════════════╦═══════════════════╗
		// ║ · · · · · · · · · ║ · · · · · · · · · ║
		// ║ · ╔═╗ · ╔═════╗ · ║ · ╔═════╗ · ╔═╗ · ║
		// ║ + ╚═╝ · ╚═════╝ · ╨ · ╚═════╝ · ╚═╝ + ║
		// ║ · · · · · · · · · · · · · · · · · · · ║
		// ║ · ═══ · ╥ · ══════╦══════ · ╥ · ═══ · ║
		// ║ · · · · ║ · · · · ║ · · · · ║ · · · · ║
		// ╚═════╗ · ╠══════   ╨   ══════╣ · ╔═════╝
		//       ║ · ║                   ║ · ║
		// ══════╝ · ╨   ╔════---════╗   ╨ · ╚══════
		//         ·     ║ █ █   █ █ ║     ·        
		// ══════╗ · ╥   ║           ║   ╥ · ╔══════
		//       ║ · ║   ╚═══════════╝   ║ · ║
		//       ║ · ║       READY       ║ · ║
		// ╔═════╝ · ╨   ══════╦══════   ╨ · ╚═════╗
		// ║ · · · · · · · · · ║ · · · · · · · · · ║
		// ║ · ══╗ · ═══════ · ╨ · ═══════ · ╔══ · ║
		// ║ + · ║ · · · · · · █ · · · · · · ║ · + ║
		// ╠══ · ╨ · ╥ · ══════╦══════ · ╥ · ╨ · ══╣
		// ║ · · · · ║ · · · · ║ · · · · ║ · · · · ║
		// ║ · ══════╩══════ · ╨ · ══════╩══════ · ║
		// ║ · · · · · · · · · · · · · · · · · · · ║
		// ╚═══════════════════════════════════════╝

		string WallsString =
			"╔═══════════════════╦═══════════════════╗\n" +
			"║                   ║                   ║\n" +
			"║   ╔═╗   ╔═════╗   ║   ╔═════╗   ╔═╗   ║\n" +
			"║   ╚═╝   ╚═════╝   ╨   ╚═════╝   ╚═╝   ║\n" +
			"║                                       ║\n" +
			"║   ═══   ╥   ══════╦══════   ╥   ═══   ║\n" +
			"║         ║         ║         ║         ║\n" +
			"╚═════╗   ╠══════   ╨   ══════╣   ╔═════╝\n" +
			"      ║   ║                   ║   ║      \n" +
			"══════╝   ╨   ╔════   ════╗   ╨   ╚══════\n" +
			"              ║           ║              \n" +
			"══════╗   ╥   ║           ║   ╥   ╔══════\n" +
			"      ║   ║   ╚═══════════╝   ║   ║      \n" +
			"      ║   ║                   ║   ║      \n" +
			"╔═════╝   ╨   ══════╦══════   ╨   ╚═════╗\n" +
			"║                   ║                   ║\n" +
			"║   ══╗   ═══════   ╨   ═══════   ╔══   ║\n" +
			"║     ║                           ║     ║\n" +
			"╠══   ╨   ╥   ══════╦══════   ╥   ╨   ══╣\n" +
			"║         ║         ║         ║         ║\n" +
			"║   ══════╩══════   ╨   ══════╩══════   ║\n" +
			"║                                       ║\n" +
			"╚═══════════════════════════════════════╝";

		string GhostWallsString =
			"╔═══════════════════╦═══════════════════╗\n" +
			"║█                 █║█                 █║\n" +
			"║█ █╔═╗█ █╔═════╗█ █║█ █╔═════╗█ █╔═╗█ █║\n" +
			"║█ █╚═╝█ █╚═════╝█ █╨█ █╚═════╝█ █╚═╝█ █║\n" +
			"║█                                     █║\n" +
			"║█ █═══█ █╥█ █══════╦══════█ █╥█ █═══█ █║\n" +
			"║█       █║█       █║█       █║█       █║\n" +
			"╚═════╗█ █╠══════█ █╨█ █══════╣█ █╔═════╝\n" +
			"██████║█ █║█                 █║█ █║██████\n" +
			"══════╝█ █╨█ █╔════█ █════╗█ █╨█ █╚══════\n" +
			"             █║█         █║█             \n" +
			"══════╗█  ╥█ █║███████████║█ █╥█ █╔══════\n" +
			"██████║█  ║█ █╚═══════════╝█ █║█ █║██████\n" +
			"██████║█  ║█                 █║█ █║██████\n" +
			"╔═════╝█  ╨█ █══════╦══════█ █╨█ █╚═════╗\n" +
			"║█                 █║█                 █║\n" +
			"║█ █══╗█ █═══════█ █╨█ █═══════█ █╔══█ █║\n" +
			"║█   █║█                         █║█   █║\n" +
			"╠══█ █╨█ █╥█ █══════╦══════█ █╥█ █╨█ █══╣\n" +
			"║█       █║█       █║█       █║█       █║\n" +
			"║█ █══════╩══════█ █╨█ █══════╩══════█ █║\n" +
			"║█                                     █║\n" +
			"╚═══════════════════════════════════════╝";

		string DotsString =
			"                                         \n" +
			"  · · · · · · · · ·   · · · · · · · · ·  \n" +
			"  ·     ·         ·   ·         ·     ·  \n" +
			"  +     ·         ·   ·         ·     +  \n" +
			"  · · · · · · · · · · · · · · · · · · ·  \n" +
			"  ·     ·   ·               ·   ·     ·  \n" +
			"  · · · ·   · · · ·   · · · ·   · · · ·  \n" +
			"        ·                       ·        \n" +
			"        ·                       ·        \n" +
			"        ·                       ·        \n" +
			"        ·                       ·        \n" +
			"        ·                       ·        \n" +
			"        ·                       ·        \n" +
			"        ·                       ·        \n" +
			"        ·                       ·        \n" +
			"  · · · · · · · · ·   · · · · · · · · ·  \n" +
			"  ·     ·         ·   ·         ·     ·  \n" +
			"  + ·   · · · · · ·   · · · · · ·   · +  \n" +
			"    ·   ·   ·               ·   ·   ·    \n" +
			"  · · · ·   · · · ·   · · · ·   · · · ·  \n" +
			"  ·               ·   ·               ·  \n" +
			"  · · · · · · · · · · · · · · · · · · ·  \n" +
			"                                         ";

		string[] PacManAnimations =
		{
			"\"' '\"",
			"n. .n",
			")>- ->",
			"(<- -<",
		};

		#endregion

		int OriginalWindowWidth = Console.WindowWidth;
		int OriginalWindowHeight = Console.WindowHeight;
		ConsoleColor OriginalBackgroundColor = Console.BackgroundColor;
		ConsoleColor OriginalForegroundColor = Console.ForegroundColor;

		char[,] Dots;
		int Score;
		(int X, int Y) PacManPosition;
		Direction? PacManMovingDirection = default;
		int? PacManMovingFrame = default;
		const int FramesToMoveHorizontal = 6;
		const int FramesToMoveVertical = 6;
		Ghost[] Ghosts;
		const int GhostWeakTime = 200;
		Random Random = new();
		(int X, int Y)[] Locations = GetLocations();

		await Console.Clear();
		try
		{
			if (OperatingSystem.IsWindows())
			{
				Console.WindowWidth = 50;
				Console.WindowHeight = 30;
			}
			Console.CursorVisible = false;
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Score = 0;
		NextRound:
			await Console.Clear();
			SetUpDots();
			PacManPosition = (20, 17);

			Ghost a = new();
			a.Position = a.StartPosition = (16, 10);
			a.Color = ConsoleColor.Red;
			a.FramesToUpdate = 6;
			a.Update = () => UpdateGhost(a);

			Ghost b = new();
			b.Position = b.StartPosition = (18, 10);
			b.Color = ConsoleColor.DarkGreen;
			b.Destination = GetRandomLocation();
			b.FramesToUpdate = 6;
			b.Update = () => UpdateGhost(b);

			Ghost c = new();
			c.Position = c.StartPosition = (22, 10);
			c.Color = ConsoleColor.Magenta;
			c.FramesToUpdate = 12;
			c.Update = () => UpdateGhost(c);

			Ghost d = new();
			d.Position = d.StartPosition = (24, 10);
			d.Color = ConsoleColor.DarkCyan;
			d.Destination = GetRandomLocation();
			d.FramesToUpdate = 12;
			d.Update = () => UpdateGhost(d);

			Ghosts = new Ghost[] { a, b, c, d, };

			await RenderWalls();
			await RenderGate();
			await RenderDots();
			await RenderReady();
			await RenderPacMan();
			await RenderGhosts();
			await RenderScore();
			if (await GetStartingDirectionInput())
			{
				return; // user hit escape
			}
			PacManMovingFrame = 0;
			await EraseReady();
			while (CountDots() > 0)
			{
				if (await HandleInput())
				{
					return; // user hit escape
				}
				await UpdatePacMan();
				UpdateGhosts();
				await RenderScore();
				await RenderDots();
				await RenderPacMan();
				await RenderGhosts();
				foreach (Ghost ghost in Ghosts)
				{
					if (ghost.Position == PacManPosition)
					{
						if (ghost.Weak)
						{
							ghost.Position = ghost.StartPosition;
							ghost.Weak = false;
							Score += 10;
						}
						else
						{
							await Console.SetCursorPosition(0, 24);
							await Console.WriteLine("Game Over!");
							await Console.WriteLine("Play Again [enter], or quit [escape]?");
						GetInput:
							switch ((await Console.ReadKey(true)).Key)
							{
								case ConsoleKey.Enter: goto NextRound;
								case ConsoleKey.Escape: await Console.Clear(); return;
								default: goto GetInput;
							}
						}
					}
				}
				await Console.RefreshAndDelay(TimeSpan.FromMilliseconds(10));
			}
			goto NextRound;
		}
		finally
		{
			Console.CursorVisible = true;
			if (OperatingSystem.IsWindows())
			{
				Console.WindowWidth = OriginalWindowWidth;
				Console.WindowHeight = OriginalWindowHeight;
			}
			Console.BackgroundColor = OriginalBackgroundColor;
			Console.ForegroundColor = OriginalForegroundColor;
		}

		async Task<bool> GetStartingDirectionInput()
		{
		GetInput:
			ConsoleKey key = (await Console.ReadKey(true)).Key;
			switch (key)
			{
				case ConsoleKey.LeftArrow: PacManMovingDirection = Direction.Left; break;
				case ConsoleKey.RightArrow: PacManMovingDirection = Direction.Right; break;
				case ConsoleKey.Escape: await Console.Clear(); await Console.Write("PacMan was closed."); await Console.Refresh(); return true;
				default: goto GetInput;
			}
			return false;
		}

		async Task<bool> HandleInput()
		{
			bool moved = false;
			void TrySetPacManDirection(Direction direction)
			{
				if (!moved &&
					PacManMovingDirection != direction &&
					CanMove(PacManPosition.X, PacManPosition.Y, direction))
				{
					PacManMovingDirection = direction;
					PacManMovingFrame = 0;
					moved = true;
				}
			}
			while (await Console.KeyAvailable())
			{
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.UpArrow: TrySetPacManDirection(Direction.Up); break;
					case ConsoleKey.DownArrow: TrySetPacManDirection(Direction.Down); break;
					case ConsoleKey.LeftArrow: TrySetPacManDirection(Direction.Left); break;
					case ConsoleKey.RightArrow: TrySetPacManDirection(Direction.Right); break;
					case ConsoleKey.Escape:
						await Console.Clear();
						await Console.Write("PPacMan was closed.");
						await Console.Refresh();
						return true;
				}
			}
			return false;
		}

		char BoardAt(int x, int y) => WallsString[y * 42 + x];

		bool IsWall(int x, int y) => BoardAt(x, y) is not ' ';

		bool CanMove(int x, int y, Direction direction) => direction switch
		{
			Direction.Up =>
				!IsWall(x - 1, y - 1) &&
				!IsWall(x, y - 1) &&
				!IsWall(x + 1, y - 1),
			Direction.Down =>
				!IsWall(x - 1, y + 1) &&
				!IsWall(x, y + 1) &&
				!IsWall(x + 1, y + 1),
			Direction.Left =>
				x - 2 < 0 || !IsWall(x - 2, y),
			Direction.Right =>
				x + 2 > 40 || !IsWall(x + 2, y),
			_ => throw new NotImplementedException(),
		};

		void SetUpDots()
		{
			string[] rows = DotsString.Split("\n");
			int rowCount = rows.Length;
			int columnCount = rows[0].Length;
			Dots = new char[columnCount, rowCount];
			for (int row = 0; row < rowCount; row++)
			{
				for (int column = 0; column < columnCount; column++)
				{
					Dots[column, row] = rows[row][column];
				}
			}
		}

		int CountDots()
		{
			int count = 0;
			int columnCount = Dots.GetLength(0);
			int rowCount = Dots.GetLength(1);
			for (int row = 0; row < rowCount; row++)
			{
				for (int column = 0; column < columnCount; column++)
				{
					if (!char.IsWhiteSpace(Dots[column, row]))
					{
						count++;
					}
				}
			}
			return count;
		}

		async Task UpdatePacMan()
		{
			if (PacManMovingDirection.HasValue)
			{
				if ((PacManMovingDirection == Direction.Left || PacManMovingDirection == Direction.Right) && PacManMovingFrame >= FramesToMoveHorizontal ||
					(PacManMovingDirection == Direction.Up || PacManMovingDirection == Direction.Down) && PacManMovingFrame >= FramesToMoveVertical)
				{
					PacManMovingFrame = 0;
					int x_adjust =
						PacManMovingDirection == Direction.Left ? -1 :
						PacManMovingDirection == Direction.Right ? 1 :
						0;
					int y_adjust =
						PacManMovingDirection == Direction.Up ? -1 :
						PacManMovingDirection == Direction.Down ? 1 :
						0;
					await Console.SetCursorPosition(PacManPosition.X, PacManPosition.Y);
					await Console.Write(" ");
					PacManPosition = (PacManPosition.X + x_adjust, PacManPosition.Y + y_adjust);
					if (PacManPosition.X < 0)
					{
						PacManPosition.X = 40;
					}
					else if (PacManPosition.X > 40)
					{
						PacManPosition.X = 0;
					}
					if (Dots[PacManPosition.X, PacManPosition.Y] is '·')
					{
						Dots[PacManPosition.X, PacManPosition.Y] = ' ';
						Score += 1;
					}
					if (Dots[PacManPosition.X, PacManPosition.Y] is '+')
					{
						foreach (Ghost ghost in Ghosts)
						{
							ghost.Weak = true;
							ghost.WeakTime = 0;
						}
						Dots[PacManPosition.X, PacManPosition.Y] = ' ';
						Score += 3;
					}
					if (!CanMove(PacManPosition.X, PacManPosition.Y, PacManMovingDirection.Value))
					{
						PacManMovingDirection = null;
					}
				}
				else
				{
					PacManMovingFrame++;
				}
			}
		}

		async Task RenderReady()
		{
			await Console.SetCursorPosition(18, 13);
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Black;
			await Console.Write("READY");
			Console.ResetColor();
		}

		async Task EraseReady()
		{
			await Console.SetCursorPosition(18, 13);
			await Console.Write("     ");
		}

		async Task RenderScore()
		{
			await Console.SetCursorPosition(0, 23);
			await Console.Write("Score: " + Score);
		}

		async Task RenderGate()
		{
			await Console.SetCursorPosition(19, 9);
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.BackgroundColor = ConsoleColor.Black;
			await Console.Write("---");
			Console.ResetColor();
		}

		async Task RenderWalls()
		{
			await Console.SetCursorPosition(0, 0);
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.BackgroundColor = ConsoleColor.Black;
			await Render(WallsString, false);
			Console.ResetColor();
		}

		async Task RenderDots()
		{
			await Console.SetCursorPosition(0, 0);
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.BackgroundColor = ConsoleColor.Black;
			for (int row = 0; row < Dots.GetLength(1); row++)
			{
				for (int column = 0; column < Dots.GetLength(0); column++)
				{
					if (!char.IsWhiteSpace(Dots[column, row]))
					{
						await Console.SetCursorPosition(column, row);
						await Console.Write(Dots[column, row]);
					}
				}
			}
			Console.ResetColor();
		}

		async Task RenderPacMan()
		{
			await Console.SetCursorPosition(PacManPosition.X, PacManPosition.Y);
			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.Yellow;
			if (PacManMovingDirection.HasValue && PacManMovingFrame.HasValue)
			{
				int frame = (int)PacManMovingFrame % PacManAnimations[(int)PacManMovingDirection].Length;
				await Console.Write(PacManAnimations[(int)PacManMovingDirection][frame]);
			}
			else
			{
				await Console.Write(' ');
			}
			Console.ResetColor();
		}

		async Task RenderGhosts()
		{
			foreach (Ghost ghost in Ghosts)
			{
				await Console.SetCursorPosition(ghost.Position.X, ghost.Position.Y);
				Console.ForegroundColor = ConsoleColor.White;
				Console.BackgroundColor = ghost.Weak ? ConsoleColor.Blue : ghost.Color;
				await Console.Write('"');
				Console.ResetColor();
			}
		}

		async Task Render(string @string, bool renderSpace = true)
		{
			int x = Console.CursorLeft;
			int y = Console.CursorTop;
			foreach (char c in @string)
			{
				if (c is '\n')
				{
					await Console.SetCursorPosition(x, ++y);
				}
				else if (c is not ' ' || renderSpace)
				{
					await Console.Write(c);
				}
				else
				{
					await Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
				}
			}
		}

		void UpdateGhosts()
		{
			foreach (Ghost ghost in Ghosts)
			{
				ghost.Update!();
			}
		}

		async Task UpdateGhost(Ghost ghost)
		{
			if (ghost.Destination.HasValue && ghost.Destination == ghost.Position)
			{
				ghost.Destination = GetRandomLocation();
			}
			if (ghost.Weak)
			{
				ghost.WeakTime++;
				if (ghost.WeakTime > GhostWeakTime)
				{
					ghost.Weak = false;
				}
			}
			else if (ghost.UpdateFrame < ghost.FramesToUpdate)
			{
				ghost.UpdateFrame++;
			}
			else
			{
				await Console.SetCursorPosition(ghost.Position.X, ghost.Position.Y);
				await Console.Write(' ');
				ghost.Position = GetGhostNextMove(ghost.Position, ghost.Destination ?? PacManPosition);
				ghost.UpdateFrame = 0;
			}
		}

		(int X, int Y)[] GetLocations()
		{
			List<(int X, int Y)> list = new();
			int x = 0;
			int y = 0;
			foreach (char c in GhostWallsString)
			{
				if (c is '\n')
				{
					x = 0;
					y++;
				}
				else
				{
					if (c is ' ')
					{
						list.Add((x, y));
					}
					x++;
				}
			}
			return list.ToArray();
		}

		(int X, int Y) GetRandomLocation() => Random.Choose(Locations);

		(int X, int Y) GetGhostNextMove((int X, int Y) position, (int X, int Y) destination)
		{
			HashSet<(int X, int Y)> alreadyUsed = new();

			char BoardAt(int x, int y) => GhostWallsString[y * 42 + x];

			bool IsWall(int x, int y) => BoardAt(x, y) is not ' ';

			void Neighbors((int X, int Y) currentLocation, Action<(int X, int Y)> neighbors)
			{
				void HandleNeighbor(int x, int y)
				{
					if (!alreadyUsed.Contains((x, y)) && x >= 0 && x <= 40 && !IsWall(x, y))
					{
						alreadyUsed.Add((x, y));
						neighbors((x, y));
					}
				}

				int x = currentLocation.X;
				int y = currentLocation.Y;
				HandleNeighbor(x - 1, y); // left
				HandleNeighbor(x, y + 1); // up
				HandleNeighbor(x + 1, y); // right
				HandleNeighbor(x, y - 1); // down
			}

			int Heuristic((int X, int Y) node)
			{
				int x = node.X - PacManPosition.X;
				int y = node.Y - PacManPosition.Y;
				return x * x + y * y;
			}

			Action<Action<(int X, int Y)>> path = SearchGraph(position, Neighbors, Heuristic, node => node == destination)!;
			(int X, int Y)[] array = path.ToArray();
			return array[1];
		}
	}

	class Ghost
	{
		public (int X, int Y) StartPosition;
		public (int X, int Y) Position;
		public bool Weak;
		public int WeakTime;
		public ConsoleColor Color;
		public Func<Task>? Update;
		public int UpdateFrame;
		public int FramesToUpdate;
		public (int X, int Y)? Destination;
	}

	enum Direction
	{
		Up = 0,
		Down = 1,
		Left = 2,
		Right = 3,
	}
}
