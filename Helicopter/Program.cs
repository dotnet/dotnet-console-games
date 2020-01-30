using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

class Program
{
	#region Type Declarations

	enum UFO
	{
		A = 1,
		B = 2,
		C = 3,
		D = 4,
		E = 5,
	}

	class PositionedObject2D
	{
		public int Left;
		public int Top;
	}

	class Explosion : PositionedObject2D
	{
		public int Frame;
	}

	class Enemy : PositionedObject2D
	{
		public UFO Type;
		public int Health;
	}

	static class Ascii
	{
		#region Ascii Renders

		public static readonly string bulletRender = "-";

		public static readonly string bulletImpactRender = "█";

		public static readonly string[] helicopterRenders = new string[]
		{
			// 0
			@"             " + '\n' +
			@"             " + '\n' +
			@"             ",
			// 1
			@"  ~~~~~+~~~~~" + '\n' +
			@"'\===<[_]L)  " + '\n' +
			@"     -'-`-   ",
			// 2
			@"  -----+-----" + '\n' +
			@"*\===<[_]L)  " + '\n' +
			@"     -'-`-   ",
		};

		public static string[] ufoRenders = new string[]
		{
			// default
			null,
			// A
			@"   __O__   " + '\n' +
			@"-=<_‗_‗_>=-",
			// B
			@"     _!_     " + '\n' +
			@"    /_O_\    " + '\n' +
			@"-==<_‗_‗_>==-",
			// C
			@"  _/\_  " + '\n' +
			@" /_OO_\ " + '\n' +
			@"() () ()",
			// D
			@" _!_!_ " + '\n' +
			@"|_o-o_|" + '\n' +
			@" ^^^^^ ",
			// E
			@" _!_ " + '\n' +
			@"(_o_)" + '\n' +
			@" ^^^ ",
		};

		public static readonly string[] explosionRenders = new string[]
		{
			// 0
			@"           " + '\n' +
			@"   █████   " + '\n' +
			@"   █████   " + '\n' +
			@"   █████   " + '\n' +
			@"           ",
			// 1
			@"           " + '\n' +
			@"           " + '\n' +
			@"     *     " + '\n' +
			@"           " + '\n' +
			@"           ",
			// 2
			@"           " + '\n' +
			@"     *     " + '\n' +
			@"    *#*    " + '\n' +
			@"     *     " + '\n' +
			@"           ",
			// 3
			@"           " + '\n' +
			@"    *#*    " + '\n' +
			@"   *#*#*   " + '\n' +
			@"    *#*    " + '\n' +
			@"           ",
			// 4
			@"     *     " + '\n' +
			@"   *#*#*   " + '\n' +
			@"  *#* *#*  " + '\n' +
			@"   *#*#*   " + '\n' +
			@"     *     ",
			// 5
			@"    *#*    " + '\n' +
			@"  *#* *#*  " + '\n' +
			@" *#*   *#* " + '\n' +
			@"  *#* *#*  " + '\n' +
			@"    *#*    ",
			// 6
			@"   *   *   " + '\n' +
			@" **     ** " + '\n' +
			@"**       **" + '\n' +
			@" **     ** " + '\n' +
			@"   *   *   ",
			// 7
			@"   *   *   " + '\n' +
			@" *       * " + '\n' +
			@"*         *" + '\n' +
			@" *       * " + '\n' +
			@"   *   *   ",
		};

		#endregion
	}

	#endregion

	static readonly int height = Console.WindowHeight;
	static readonly int width  = Console.WindowWidth;

	static readonly TimeSpan threadSleepTimeSpan = TimeSpan.FromMilliseconds(60);

	static readonly PositionedObject2D player = new PositionedObject2D { Left = 2, Top = height / 2, };
	static readonly List<Enemy> enemies = new List<Enemy>();
	static readonly List<PositionedObject2D> bullets = new List<PositionedObject2D>();
	static readonly List<Explosion> explosions = new List<Explosion>();
	static readonly Stopwatch stopwatchGame = new Stopwatch();
	static readonly Stopwatch stopwatchEnemySpawn = new Stopwatch();

	static int helicopterRender = 1;
	static int score = 0;
	static TimeSpan enemySpawnTimeSpan = TimeSpan.FromSeconds(20);

	static void Main()
	{
		Console.WriteLine("This game is still in development.");
		Console.WriteLine("Press Enter To Continue...");
		Console.ReadLine();
		Console.Clear();

		Console.CursorVisible = false;
		stopwatchGame.Restart();
		stopwatchEnemySpawn.Restart();
		while (true)
		{
			#region Window Resize

			if (height != Console.WindowHeight || width != Console.WindowWidth)
			{
				Console.Clear();
				Console.Write("Console window resized. Helicopter closed.");
				return;
			}

			#endregion

			#region Update Player

			if (Console.KeyAvailable)
			{
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.UpArrow:
						Console.SetCursorPosition(player.Left, player.Top);
						Render(Ascii.helicopterRenders[default], true);
						player.Top = Math.Max(player.Top - 1, 0);
						break;
					case ConsoleKey.DownArrow:
						Console.SetCursorPosition(player.Left, player.Top);
						Render(Ascii.helicopterRenders[default], true);
						player.Top = Math.Min(player.Top + 1, height - 3);
						break;
					case ConsoleKey.Escape:
						Console.Clear();
						Console.Write("Helicopter was closed.");
						return;
				}
			}
			while (Console.KeyAvailable)
			{
				Console.ReadKey(true);
			}

			#endregion

			#region Render Player

			Console.SetCursorPosition(player.Left, player.Top);
			Render(Ascii.helicopterRenders[helicopterRender = helicopterRender is 2 ? 1 : 2]);

			#endregion

			Thread.Sleep(threadSleepTimeSpan);
		}
	}

	static void Render(string @string, bool renderSpace = false)
	{
		int x = Console.CursorLeft;
		int y = Console.CursorTop;
		foreach (char c in @string)
			if (c is '\n') Console.SetCursorPosition(x, ++y);
			else if (!(c is ' ') || renderSpace) Console.Write(c);
			else Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
	}
}
