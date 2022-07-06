using System;
using System.Collections.Generic;
using System.Threading;

string[] Tank = new string[]
{
	#region Frames
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
	#endregion
};

string[] TankShooting = new string[]
{
	#region Frames
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
	#endregion
};

string[] TankExploding = new string[]
{
	#region Frames
	// Ka...
	@" ___ " + "\n" +
	@"|___|" + "\n" +
	@"[ooo]" + "\n",
	// Boom
	@"█████" + "\n" +
	@"█████" + "\n" +
	@"█████" + "\n",
	// Dead
	@"     " + "\n" +
	@"     " + "\n" +
	@"     " + "\n",
	#endregion
};

char[] Bullet = new char[]
{
	#region Frames
	default,
	'^', // Up
	'v', // Down
	'<', // Left
	'>', // Right
	#endregion
};

string Map =
	#region Frames
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

var Tanks = new List<Tank>();
var AllTanks = new List<Tank>();
var Player = new Tank() { X = 08, Y = 05, IsPlayer = true };
var random = new Random();

Tanks.Add(Player);
Tanks.Add(new Tank() { X = 08, Y = 21, });
Tanks.Add(new Tank() { X = 66, Y = 05, });
Tanks.Add(new Tank() { X = 66, Y = 21, });
AllTanks.AddRange(Tanks);

Console.CursorVisible = false;
if (OperatingSystem.IsWindows())
{
	Console.WindowWidth = Math.Max(Console.WindowWidth, 90);
	Console.WindowHeight = Math.Max(Console.WindowHeight, 35);
}
Console.Clear();
Console.SetCursorPosition(0, 0);
Render(Map);
Console.WriteLine();
Console.WriteLine("Use the (W, A, S, D) keys to move and the arrow keys to shoot.");

#region Render

static void Render(string @string, bool renderSpace = false)
{
	int x = Console.CursorLeft;
	int y = Console.CursorTop;
	foreach (char c in @string)
		if (c is '\n') Console.SetCursorPosition(x, ++y);
		else if (c is not ' ' || renderSpace) Console.Write(c);
		else Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
}

#endregion

while (Tanks.Contains(Player) && Tanks.Count > 1)
{
	#region Tank Updates

	foreach (var tank in Tanks)
	{
		#region Shooting Update

		if (tank.IsShooting)
		{
			tank.Bullet = new Bullet()
			{
				X = tank.Direction switch
				{
					Direction.Left => tank.X - 3,
					Direction.Right => tank.X + 3,
					_ => tank.X,
				},
				Y = tank.Direction switch
				{
					Direction.Up => tank.Y - 2,
					Direction.Down => tank.Y + 2,
					_ => tank.Y,
				},
				Direction = tank.Direction,
			};
			tank.IsShooting = false;
			continue;
		}

		#endregion

		#region Exploding Update

		if (tank.IsExploding)
		{
			tank.ExplodingFrame++;
			Console.SetCursorPosition(tank.X - 2, tank.Y - 1);
			Render(tank.ExplodingFrame > 9
				? TankExploding[2]
				: TankExploding[tank.ExplodingFrame % 2], true);
			continue;
		}

		#endregion

		#region MoveCheck

		bool MoveCheck(Tank movingTank, int X, int Y)
		{
			foreach (var tank in Tanks)
			{
				if (tank == movingTank)
				{
					continue;
				}
				if (Math.Abs(tank.X - X) <= 4 && Math.Abs(tank.Y - Y) <= 2)
				{
					return false; // collision with another tank
				}
			}
			if (X < 3 || X > 71 || Y < 2 || Y > 25)
			{
				return false; // collision with border walls
			}
			if (3 < X && X < 13 && 11 < Y && Y < 15)
			{
				return false; // collision with left blockade
			}
			if (34 < X && X < 40 && 2 < Y && Y < 8)
			{
				return false; // collision with top blockade
			}
			if (34 < X && X < 40 && 19 < Y && Y < 25)
			{
				return false; // collision with bottom blockade
			}
			if (61 < X && X < 71 && 11 < Y && Y < 15)
			{
				return false; // collision with right blockade
			}
			return true;
		}

		#endregion

		#region Move

		void TryMove(Direction direction)
		{
			switch (direction)
			{
				case Direction.Up:
					if (MoveCheck(tank, tank.X, tank.Y - 1))
					{
						Console.SetCursorPosition(tank.X - 2, tank.Y + 1);
						Console.Write(' ');
						Console.SetCursorPosition(tank.X - 1, tank.Y + 1);
						Console.Write(' ');
						Console.SetCursorPosition(tank.X, tank.Y + 1);
						Console.Write(' ');
						Console.SetCursorPosition(tank.X + 1, tank.Y + 1);
						Console.Write(' ');
						Console.SetCursorPosition(tank.X + 2, tank.Y + 1);
						Console.Write(' ');
						tank.Y--;
					}
					break;
				case Direction.Down:
					if (MoveCheck(tank, tank.X, tank.Y + 1))
					{
						Console.SetCursorPosition(tank.X - 2, tank.Y - 1);
						Console.Write(' ');
						Console.SetCursorPosition(tank.X - 1, tank.Y - 1);
						Console.Write(' ');
						Console.SetCursorPosition(tank.X, tank.Y - 1);
						Console.Write(' ');
						Console.SetCursorPosition(tank.X + 1, tank.Y - 1);
						Console.Write(' ');
						Console.SetCursorPosition(tank.X + 2, tank.Y - 1);
						Console.Write(' ');
						tank.Y++;
					}
					break;
				case Direction.Left:
					if (MoveCheck(tank, tank.X - 1, tank.Y))
					{
						Console.SetCursorPosition(tank.X + 2, tank.Y - 1);
						Console.Write(' ');
						Console.SetCursorPosition(tank.X + 2, tank.Y);
						Console.Write(' ');
						Console.SetCursorPosition(tank.X + 2, tank.Y + 1);
						Console.Write(' ');
						tank.X--;
					}
					break;
				case Direction.Right:
					if (MoveCheck(tank, tank.X + 1, tank.Y))
					{
						Console.SetCursorPosition(tank.X - 2, tank.Y - 1);
						Console.Write(' ');
						Console.SetCursorPosition(tank.X - 2, tank.Y);
						Console.Write(' ');
						Console.SetCursorPosition(tank.X - 2, tank.Y + 1);
						Console.Write(' ');
						tank.X++;
					}
					break;
			}
		}

		#endregion

		if (tank.IsPlayer)
		{
			#region Player Controlled

			Direction? move = null;
			Direction? shoot = null;

			while (Console.KeyAvailable)
			{
				switch (Console.ReadKey(true).Key)
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
						return;
				}
				while (Console.KeyAvailable)
				{
					Console.ReadKey(true);
				}
			}

			tank.IsShooting = shoot.HasValue && shoot.Value is not Direction.Null && tank.Bullet is null;
			if (tank.IsShooting)
			{
				tank.Direction = shoot ?? tank.Direction;
			}

			if (move.HasValue)
				TryMove(move.Value);

			#endregion
		}
		else
		{
			#region Computer Controled

			int randomIndex = random.Next(0, 6);
			if (randomIndex < 4)
				TryMove((Direction)randomIndex + 1);

			if (tank.Bullet is null)
			{
				Direction shoot = Math.Abs(tank.X - Player.X) > Math.Abs(tank.Y - Player.Y)
					? (tank.X < Player.X ? Direction.Right : Direction.Left)
					: (tank.Y > Player.Y ? Direction.Up : Direction.Down);
				tank.Direction = shoot;
				tank.IsShooting = true;
			}

			#endregion
		}

		#region Render Tank

		Console.SetCursorPosition(tank.X - 2, tank.Y - 1);
		Render(tank.IsExploding
			? TankExploding[tank.ExplodingFrame % 2]
			: tank.IsShooting
				? TankShooting[(int)tank.Direction]
				: Tank[(int)tank.Direction],
			true);

		#endregion
	}

	#endregion

	#region Bullet Updates

	bool BulletCollisionCheck(Bullet bullet, out Tank collidingTank)
	{
		collidingTank = null;
		foreach (var tank in Tanks)
		{
			if (Math.Abs(bullet.X - tank.X) < 3 && Math.Abs(bullet.Y - tank.Y) < 2)
			{
				collidingTank = tank;
				return true;
			}
		}
		if (bullet.X is 0 || bullet.X is 74 || bullet.Y is 0 || bullet.Y is 27)
		{
			return true;
		}
		if (5 < bullet.X && bullet.X < 11 && bullet.Y == 13)
		{
			return true; // collision with left blockade
		}
		if (bullet.X == 37 && 3 < bullet.Y && bullet.Y < 7)
		{
			return true; // collision with top blockade
		}
		if (bullet.X == 37 && 20 < bullet.Y && bullet.Y < 24)
		{
			return true; // collision with bottom blockade
		}
		if (63 < bullet.X && bullet.X < 69 && bullet.Y == 13)
		{
			return true; // collision with right blockade
		}
		return false;
	}

	foreach (var tank in AllTanks)
	{
		if (tank.Bullet is not null)
		{
			var bullet = tank.Bullet;
			Console.SetCursorPosition(bullet.X, bullet.Y);
			Console.Write(' ');
			switch (bullet.Direction)
			{
				case Direction.Up: bullet.Y--; break;
				case Direction.Down: bullet.Y++; break;
				case Direction.Left: bullet.X--; break;
				case Direction.Right: bullet.X++; break;
			}
			Console.SetCursorPosition(bullet.X, bullet.Y);
			bool collision = BulletCollisionCheck(bullet, out Tank collisionTank);
			Console.Write(collision
				? '█'
				: Bullet[(int)bullet.Direction]);
			if (collision)
			{
				if (collisionTank is not null && --collisionTank.Health <= 0)
				{
					collisionTank.ExplodingFrame = 1;
				}
				tank.Bullet = null;
			}
		}
	}

	#region Removing Dead Tanks

	for (int i = 0; i < Tanks.Count; i++)
	{
		if (Tanks[i].ExplodingFrame > 10)
		{
			Tanks.RemoveAt(i);
			i--;
		}
	}

	#endregion

	#endregion

	Console.SetCursorPosition(0, 0);
	Render(Map);
	Thread.Sleep(TimeSpan.FromMilliseconds(80));
}

Console.SetCursorPosition(0, 33);
Console.Write(Tanks.Contains(Player)
	? "You Win."
	: "You Lose.");
Console.ReadLine();

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
	public Bullet Bullet;
	public bool IsShooting;
	public int ExplodingFrame;
	public bool IsExploding => ExplodingFrame > 0;
}

class Bullet
{
	public int X;
	public int Y;
	public Direction Direction;
}
