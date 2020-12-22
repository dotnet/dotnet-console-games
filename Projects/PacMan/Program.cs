using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
	const int NumberOfGhosts = 4;
	const int FramesToMoveHorizontal = 6;
	const int FramesToMoveVertical = 6;
	static (int X, int Y)[] GhostPositions;

	static void Main()
	{
		Console.Clear();
		Console.WriteLine("NOTE: This game is still in development.");
		Console.WriteLine("Press Enter To Continue...");
		Console.ReadLine();
		try
		{
			Console.CursorVisible = false;
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Score = 0;
			NextRound:
			Console.Clear();
			SetUpDots();
			PacManPosition = (20, 17);
			GhostPositions = new (int X, int Y)[]
			{
				(16, 10),
				(18, 10),
				(22, 10),
				(24, 10),
			};
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
				RenderScore();
				RenderPacMan();
				Thread.Sleep(TimeSpan.FromMilliseconds(50));
			}
			goto NextRound;
		}
		finally
		{
			// Revert Changes To Console
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

	#region Input Handling

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
				case ConsoleKey.UpArrow:    TrySetPacManDirection(Direction.Up);    break;
				case ConsoleKey.DownArrow:  TrySetPacManDirection(Direction.Down);  break;
				case ConsoleKey.LeftArrow:  TrySetPacManDirection(Direction.Left);  break;
				case ConsoleKey.RightArrow: TrySetPacManDirection(Direction.Right); break;
				case ConsoleKey.Escape:
					Console.Clear();
					Console.Write("PPacMan was closed.");
					return true;
			}
		}
		return false;
	}

	#endregion

	#region Helpers

	static char BoardAt(int x, int y) => WallsString[y * 42 + x];
	static bool IsWall(int x, int y) => !(BoardAt(x, y) is ' ');
	static bool CanMove(int x, int y, Direction direction) => direction switch
	{
		Direction.Up    =>
			!IsWall(x - 1, y - 1) &&
			!IsWall(x,     y - 1) &&
			!IsWall(x + 1, y - 1),
		Direction.Down  =>
			!IsWall(x - 1, y + 1) &&
			!IsWall(x,     y + 1) &&
			!IsWall(x + 1, y + 1),
		Direction.Left  =>
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

	#endregion

	#region Updating

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


	#endregion

	#region Rendering

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
			Render(DotsString, false);
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
		for (int i = 0; i < NumberOfGhosts; i++)
		{
			Console.SetCursorPosition(GhostPositions[i].X, GhostPositions[i].Y);
			var ghostColor = i switch
			{
				0 => ConsoleColor.Red,
				1 => ConsoleColor.DarkGreen,
				2 => ConsoleColor.Magenta,
				3 => ConsoleColor.DarkCyan,
				_ => throw new NotImplementedException(),
			};
			WithColors(ConsoleColor.White, ghostColor, () => Console.Write('"'));
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

	#endregion
}
