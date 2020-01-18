using System;
using System.Collections.Generic;
using System.Threading;

static class Program
{
	enum Direction
	{
		Null = 0,
		Up = 1,
		Down = 2,
		Left = 3,
		Right = 4,
	}

	class Tank
	{
		public bool IsPlayer;
		public int Health = 4;
		public int X;
		public int Y;
		public Direction Direction = Direction.Down;
		public bool Shooting;
		public int ExplodingFrame;
		public bool IsExploding => ExplodingFrame > 0;

		#region Ascii

		public static string[] Ascii = new string[]
		{
			null,
			// Up
			@" _^_ " + "\n" +
			@"|___|" + "\n" +
			@"[ooo]" + "\n",
			// Down
			@" ___ " + "\n" +
			@"|_O_|" + "\n" +
			@"[ooo]" + "\n",
			// Left
			@"  __ " + "\n" +
			@"=|__|" + "\n" +
			@"[ooo]" + "\n",
			// Right
			@" __  " + "\n" +
			@"|__|=" + "\n" +
			@"[ooo]" + "\n",
		};

		public static string[] ShootingAscii = new string[]
		{
			null,
			// Up
			@" _█_ " + "\n" +
			@"|___|" + "\n" +
			@"[ooo]" + "\n",
			// Down
			@" ___ " + "\n" +
			@"|_█_|" + "\n" +
			@"[ooo]" + "\n",
			// Left
			@"  __ " + "\n" +
			@"█|__|" + "\n" +
			@"[ooo]" + "\n",
			// Right
			@" __  " + "\n" +
			@"|__|█" + "\n" +
			@"[ooo]" + "\n",
		};

		public static string[] ExplodingAscii = new string[]
		{
			// Ka...
			@" ___ " + "\n" +
			@"|___|" + "\n" +
			@"[ooo]" + "\n",
			// Boom
			@"█████" + "\n" +
			@"█████" + "\n" +
			@"█████" + "\n",
		};

		#endregion
	}

	#region Ascii

	static char[] BulletAscii = new char[]
	{
		default,
		'^', // Up
		'v', // Down
		'<', // Left
		'>', // Right
	};

	static string MapAscii =
		@"╔═════════════════════════════════════════════════════════════════════════╗" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                    ║                                    ║" + "\n" +
		@"║                                    ║                                    ║" + "\n" +
		@"║                                    ║                                    ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║     ═════                                                     ═════     ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                    ║                                    ║" + "\n" +
		@"║                                    ║                                    ║" + "\n" +
		@"║                                    ║                                    ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"║                                                                         ║" + "\n" +
		@"╚═════════════════════════════════════════════════════════════════════════╝" + "\n";

	#endregion

	static void Main()
	{
		var Tanks = new List<Tank>();
		var Bullets = new List<(int X, int Y)>();
		var Player = new Tank() { X = 08, Y = 05, IsPlayer = true };
		var random = new Random();

		Tanks.Add(Player);
		Tanks.Add(new Tank() { X = 08, Y = 21, });
		Tanks.Add(new Tank() { X = 66, Y = 05, });
		Tanks.Add(new Tank() { X = 66, Y = 21, });

		Console.WindowWidth = Math.Max(Console.WindowWidth, 90);
		Console.WindowHeight = Math.Max(Console.WindowHeight, 35);
		Console.Clear();

		while (Tanks.Contains(Player) && Tanks.Count > 1)
		{
			#region Tank Actions

			foreach (var tank in Tanks)
			{
				if (tank.IsPlayer)
				{
					Direction? move = null;
					Direction? shoot = null;

					while (Console.KeyAvailable)
					{
						switch (Console.ReadKey().Key)
						{
							// Move Direction
							case ConsoleKey.W: move = move.HasValue ? Direction.Null : Direction.Up; break;
							case ConsoleKey.S: move = move.HasValue ? Direction.Null : Direction.Down; break;
							case ConsoleKey.A: move = move.HasValue ? Direction.Null : Direction.Left; break;
							case ConsoleKey.D: move = move.HasValue ? Direction.Null : Direction.Right; break;

							// Shoot Direction
							case ConsoleKey.UpArrow: shoot = shoot.HasValue ? Direction.Null : Direction.Up; break;
							case ConsoleKey.DownArrow: shoot = shoot.HasValue ? Direction.Null : Direction.Down; break;
							case ConsoleKey.LeftArrow: shoot = shoot.HasValue ? Direction.Null : Direction.Left; break;
							case ConsoleKey.RightArrow: shoot = shoot.HasValue ? Direction.Null : Direction.Right; break;

							// Close Game
							case ConsoleKey.Escape:
								Console.Clear();
								Console.Write("Tanks was closed.");
								Environment.Exit(0);
								throw new Exception("Environment Exit Failed.");
						}
					}
				}
				else
				{

				}
			}

			#endregion

			#region Render

			static void Render(string @string, bool renderSpace = true)
			{
				int x = Console.CursorLeft;
				int y = Console.CursorTop;
				foreach (char c in @string)
					if (c is '\n') Console.SetCursorPosition(x, ++y);
					else if (!(c is ' ') || renderSpace) Console.Write(c);
					else Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
			}

			Console.SetCursorPosition(0, 0);
			Render(MapAscii);
			foreach (var tank in Tanks)
			{
				Console.CursorLeft = tank.X - 2;
				Console.CursorTop = tank.Y - 1;
				Render(tank.IsExploding
					? Tank.ExplodingAscii[tank.ExplodingFrame % 2]
					: tank.Shooting
						? Tank.ShootingAscii[(int)tank.Direction]
						: Tank.Ascii[(int)tank.Direction]);
			}

			#endregion

			Console.SetCursorPosition(0, 29);
			Console.WriteLine();
			Console.WriteLine("This game is still in development. Press Enter To close...");
			Console.ReadLine();
			return;

			Thread.Sleep(TimeSpan.FromMilliseconds(60));
		}

		Console.Write(Tanks.Contains(Player)
			? "You Win."
			: "You Lose.");
		Console.ReadLine();
	}
}

