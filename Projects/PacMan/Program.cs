using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Towel;
using static Towel.Statics;

class Program
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

	static readonly string WallsString =
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

	static readonly string GhostWallsString =
		"╔═══════════════════╦═══════════════════╗\n" +
		"║█                 █║█                 █║\n" +
		"║█ █╔═╗█ █╔═════╗█ █║█ █╔═════╗█ █╔═╗█ █║\n" +
		"║█ █╚═╝█ █╚═════╝█ █╨█ █╚═════╝█ █╚═╝█ █║\n" +
		"║█                                     █║\n" +
		"║█ █═══█ █╥█ █══════╦══════█ █╥█ █═══█ █║\n" +
		"║█       █║        █║█       █║█       █║\n" +
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

	static readonly string DotsString =
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

	static readonly string[] PacManAnimations =
	{
		"\"' '\"",
		"n. .n",
		")>- ->",
		"(<- -<",
	};

	#endregion

	// Console Settings When Launched
	static readonly int OriginalWindowWidth = Console.WindowWidth;
	static readonly int OriginalWindowHeight = Console.WindowHeight;
	static readonly ConsoleColor OriginalBackgroundColor = Console.BackgroundColor;
	static readonly ConsoleColor OriginalForegroundColor = Console.ForegroundColor;

	enum Direction
	{
		Up = 0,
		Down = 1,
		Left = 2,
		Right = 3,
	}

	static char[,] Dots;
	static int Score;
	static (int X, int Y) PacManPosition;
	static Direction? PacManMovingDirection;
	static int? PacManMovingFrame;
	const int FramesToMoveHorizontal = 6;
	const int FramesToMoveVertical = 6;
	static Ghost[] Ghosts;
	const int GhostWeakTime = 200;
	static readonly Random Random = new Random();

	static void Main()
	{
		Console.Clear();
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
			Console.Clear();
			SetUpDots();
			PacManPosition = (20, 17);

			Ghost a = new Ghost();
			a.Position = a.StartPosition = (16, 10);
			a.Color = ConsoleColor.Red;
			a.FramesToUpdate = 6;
			a.Update = () => UpdateGhost(a);

			Ghost b = new Ghost();
			b.Position = b.StartPosition = (18, 10);
			b.Color = ConsoleColor.DarkGreen;
			b.Destination = GetRandomLocation();
			b.FramesToUpdate = 6;
			b.Update = () => UpdateGhost(b);

			Ghost c = new Ghost();
			c.Position = c.StartPosition = (22, 10);
			c.Color = ConsoleColor.Magenta;
			c.FramesToUpdate = 12;
			c.Update = () => UpdateGhost(c);

			Ghost d = new Ghost();
			d.Position = d.StartPosition = (24, 10);
			d.Color = ConsoleColor.DarkCyan;
			d.Destination = GetRandomLocation();
			d.FramesToUpdate = 6;
			d.Update = () => UpdateGhost(d);

			Ghosts = new Ghost[] { a, b, c, d, };

			RenderWalls();
			RenderGate();
			RenderDots();
			RenderReady();
			RenderPacMan();
			RenderGhosts();
			RenderScore();
			if (GetStartingDirectionInput())
			{
				return; // user hit escape
			}
			PacManMovingFrame = 0;
			EraseReady();
			while (CountDots() > 0)
			{
				if (HandleInput())
				{
					return; // user hit escape
				}
				UpdatePacMan();
				UpdateGhosts();
				RenderScore();
				RenderDots();
				RenderPacMan();
				RenderGhosts();
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
							Console.SetCursorPosition(0, 24);
							Console.WriteLine("Game Over!");
							Console.WriteLine("Play Again [enter], or quit [escape]?");
						GetInput:
							switch (Console.ReadKey(true).Key)
							{
								case ConsoleKey.Enter: goto NextRound;
								case ConsoleKey.Escape: Console.Clear(); return;
								default: goto GetInput;
							}
						}
					}
				}
				Thread.Sleep(TimeSpan.FromMilliseconds(50));
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
	}

	static bool GetStartingDirectionInput()
	{
	GetInput:
		ConsoleKey key = Console.ReadKey(true).Key;
		switch (key)
		{
			case ConsoleKey.LeftArrow: PacManMovingDirection = Direction.Left; break;
			case ConsoleKey.RightArrow: PacManMovingDirection = Direction.Right; break;
			case ConsoleKey.Escape: Console.Clear(); Console.Write("PacMan was closed."); return true;
			default: goto GetInput;
		}
		return false;
	}

	static bool HandleInput()
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
		while (Console.KeyAvailable)
		{
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.UpArrow: TrySetPacManDirection(Direction.Up); break;
				case ConsoleKey.DownArrow: TrySetPacManDirection(Direction.Down); break;
				case ConsoleKey.LeftArrow: TrySetPacManDirection(Direction.Left); break;
				case ConsoleKey.RightArrow: TrySetPacManDirection(Direction.Right); break;
				case ConsoleKey.Escape:
					Console.Clear();
					Console.Write("PPacMan was closed.");
					return true;
			}
		}
		return false;
	}

	static char BoardAt(int x, int y) => WallsString[y * 42 + x];

	static bool IsWall(int x, int y) => !(BoardAt(x, y) is ' ');

	static bool CanMove(int x, int y, Direction direction) => direction switch
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
			!IsWall(x - 2, y),
		Direction.Right =>
			!IsWall(x + 2, y),
		_ => throw new NotImplementedException(),
	};

	static void SetUpDots()
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

	static int CountDots()
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

	static void UpdatePacMan()
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
				Console.SetCursorPosition(PacManPosition.X, PacManPosition.Y);
				Console.Write(" ");
				PacManPosition = (PacManPosition.X + x_adjust, PacManPosition.Y + y_adjust);
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

	static void RenderReady()
	{
		Console.SetCursorPosition(18, 13);
		WithColors(ConsoleColor.White, ConsoleColor.Black, () =>
		{
			Console.Write("READY");
		});
	}

	static void EraseReady()
	{
		Console.SetCursorPosition(18, 13);
		Console.Write("     ");
	}

	static void RenderScore()
	{
		Console.SetCursorPosition(0, 23);
		Console.Write("Score: " + Score);
	}

	static void RenderGate()
	{
		Console.SetCursorPosition(19, 9);
		WithColors(ConsoleColor.Magenta, ConsoleColor.Black, () =>
		{
			Console.Write("---");
		});
	}

	static void RenderWalls()
	{
		Console.SetCursorPosition(0, 0);
		WithColors(ConsoleColor.Blue, ConsoleColor.Black, () =>
		{
			Render(WallsString, false);
		});
	}

	static void RenderDots()
	{
		Console.SetCursorPosition(0, 0);
		WithColors(ConsoleColor.DarkYellow, ConsoleColor.Black, () =>
		{
			for (int row = 0; row < Dots.GetLength(1); row++)
			{
				for (int column = 0; column < Dots.GetLength(0); column++)
				{
					if (!char.IsWhiteSpace(Dots[column, row]))
					{
						Console.SetCursorPosition(column, row);
						Console.Write(Dots[column, row]);
					}
				}
			}
		});
	}

	static void RenderPacMan()
	{
		Console.SetCursorPosition(PacManPosition.X, PacManPosition.Y);
		WithColors(ConsoleColor.Black, ConsoleColor.Yellow, () =>
		{
			if (PacManMovingDirection.HasValue && PacManMovingFrame.HasValue)
			{
				int frame = (int)PacManMovingFrame % PacManAnimations[(int)PacManMovingDirection].Length;
				Console.Write(PacManAnimations[(int)PacManMovingDirection][frame]);
			}
			else
			{
				Console.Write(' ');
			}
		});
	}

	static void RenderGhosts()
	{
		foreach (Ghost ghost in Ghosts)
		{
			Console.SetCursorPosition(ghost.Position.X, ghost.Position.Y);
			WithColors(ConsoleColor.White, ghost.Weak ? ConsoleColor.Blue : ghost.Color, () => Console.Write('"'));
		}
	}

	static void WithColors(ConsoleColor foreground, ConsoleColor background, Action action)
	{
		ConsoleColor originalForeground = Console.ForegroundColor;
		ConsoleColor originalBackground = Console.BackgroundColor;
		try
		{
			Console.ForegroundColor = foreground;
			Console.BackgroundColor = background;
			action();
		}
		finally
		{
			Console.ForegroundColor = originalForeground;
			Console.BackgroundColor = originalBackground;
		}
	}

	static void Render(string @string, bool renderSpace = true)
	{
		int x = Console.CursorLeft;
		int y = Console.CursorTop;
		foreach (char c in @string)
		{
			if (c is '\n')
			{
				Console.SetCursorPosition(x, ++y);
			}
			else if (!(c is ' ') || renderSpace)
			{
				Console.Write(c);
			}
			else
			{
				Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
			}
		}
	}

	static void UpdateGhosts()
	{
		foreach (Ghost ghost in Ghosts)
		{
			ghost.Update();
		}
	}

	static void UpdateGhost(Ghost ghost)
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
			Console.SetCursorPosition(ghost.Position.X, ghost.Position.Y);
			Console.Write(' ');
			ghost.Position = GetGhostNextMove(ghost.Position, ghost.Destination ?? PacManPosition);
			ghost.UpdateFrame = 0;
		}
	}

	static readonly (int X, int Y)[] Locations = GetLocations();

	static (int X, int Y)[] GetLocations()
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

	static (int X, int Y) GetRandomLocation() => Random.Choose(Locations);

	static (int X, int Y) GetGhostNextMove((int X, int Y) position, (int X, int Y) destination)
	{
		HashSet<(int X, int Y)> alreadyUsed = new();

		static char BoardAt(int x, int y) => GhostWallsString[y * 42 + x];

		static bool IsWall(int x, int y) => !(BoardAt(x, y) is ' ');

		void Neighbors((int X, int Y) currentLocation, Action<(int X, int Y)> neighbors)
		{
			void HandleNeighbor(int x, int y)
			{
				if (!alreadyUsed.Contains((x, y)) && !IsWall(x, y))
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

		Action<Action<(int X, int Y)>> path = SearchGraph(position, Neighbors, Heuristic, node => node == destination);
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
	public Action Update;
	public int UpdateFrame;
	public int FramesToUpdate;
	public (int X, int Y)? Destination;
}
