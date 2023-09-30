using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Games.Shmup;

public class Shmup
{
	public readonly BlazorConsole Console = new();

	internal static bool closeRequested = false;
	internal static Stopwatch stopwatch = new();
	internal static bool pauseUpdates = false;

	internal static int gameWidth = 80;
	internal static int gameHeight = 40;
	internal static int intendedMinConsoleWidth = gameWidth + 3;
	internal static int intendedMinConsoleHeight = gameHeight + 3;
	internal static char[,] frameBuffer = new char[gameWidth, gameHeight];
	internal static string topBorder = '┏' + new string('━', gameWidth) + '┓';
	internal static string bottomBorder = '┗' + new string('━', gameWidth) + '┛';

	internal static int consoleWidth = intendedMinConsoleWidth;
	internal static int consoleHeight = intendedMinConsoleHeight;
	internal static StringBuilder render = new(gameWidth * gameHeight);

	internal static long score = 0;
	internal static int update = 0;
	internal static bool isDead = false;
	internal static Player player = new()
	{
		X = gameWidth / 2,
		Y = gameHeight / 4,
	};
	internal static List<PlayerBullet> playerBullets = new();
	internal static List<PlayerBullet> explodingBullets = new();
	internal static List<IEnemy> enemies = new();
	internal static bool playing = false;
	internal static bool waitingForInput = true;

	internal static bool w_down = false;
	internal static bool a_down = false;
	internal static bool s_down = false;
	internal static bool d_down = false;

	internal static bool uparrow_down = false;
	internal static bool leftarrow_down = false;
	internal static bool downarrow_down = false;
	internal static bool rightarrow_down = false;

	internal static bool spacebar_down = false;

	internal static bool ui_u_down = false;
	internal static bool ui_d_down = false;
	internal static bool ui_l_down = false;
	internal static bool ui_r_down = false;

	internal static bool ui_shoot_down = false;

	public async Task Run()
	{
		if (OperatingSystem.IsWindows() && (consoleWidth < intendedMinConsoleWidth || consoleHeight < intendedMinConsoleHeight))
		{
			try
			{
				Console.WindowWidth = intendedMinConsoleWidth;
				Console.WindowHeight = intendedMinConsoleHeight;
			}
			catch
			{
				// nothing
			}
			consoleWidth = Console.WindowWidth;
			consoleHeight = Console.WindowHeight;
		}
		await Console.Clear();
		if (Console.OutputEncoding != Encoding.UTF8)
		{
			Console.OutputEncoding = Encoding.UTF8;
		}
		while (!closeRequested)
		{
			Initialize();
			while (!closeRequested && playing)
			{
				await Update();
				if (closeRequested)
				{
					return;
				}
				await Render();
				await SleepAfterRender();
			}
		}

		void Initialize()
		{
			score = 0;
			update = 0;
			isDead = false;
			player = new()
			{
				X = gameWidth / 2,
				Y = gameHeight / 4,
			};
			playerBullets = new();
			explodingBullets = new();
			enemies = new();
			playing = true;
			waitingForInput = true;
		}

		async Task Update()
		{
			bool u = false;
			bool d = false;
			bool l = false;
			bool r = false;
			bool shoot = false;
			while (await Console.KeyAvailable())
			{
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.Escape: closeRequested = true; return;
					case ConsoleKey.Enter: playing = !isDead; return;
					case ConsoleKey.W or ConsoleKey.UpArrow: u = true; break;
					case ConsoleKey.A or ConsoleKey.LeftArrow: l = true; break;
					case ConsoleKey.S or ConsoleKey.DownArrow: d = true; break;
					case ConsoleKey.D or ConsoleKey.RightArrow: r = true; break;
					case ConsoleKey.Spacebar: shoot = true; break;
				}
			}
			if (Console.IsWindows())
			{
				//if (User32_dll.GetAsyncKeyState((int)ConsoleKey.Escape) is not 0)
				//{
				//	closeRequested = true;
				//	return;
				//}

				//if (isDead)
				//{
				//	playing = !(User32_dll.GetAsyncKeyState((int)ConsoleKey.Enter) is not 0);
				//}

				u = u || w_down;
				l = l || a_down;
				d = d || s_down;
				r = r || d_down;

				u = u || uparrow_down;
				l = l || leftarrow_down;
				d = d || downarrow_down;
				r = r || rightarrow_down;

				u = u || ui_u_down;
				l = l || ui_l_down;
				d = d || ui_d_down;
				r = r || ui_r_down;

				shoot = shoot || spacebar_down;

				shoot = shoot || ui_shoot_down;

				if (waitingForInput)
				{
					waitingForInput =  !(u || d || l || r || shoot);
				}
			}

			if (pauseUpdates)
			{
				return;
			}

			if (isDead)
			{
				return;
			}

			if (waitingForInput)
			{
				return;
			}

			update++;

			if (update % 50 is 0)
			{
				SpawnARandomEnemy();
			}

			for (int i = 0; i < playerBullets.Count; i++)
			{
				playerBullets[i].Y++;
			}

			foreach (IEnemy enemy in enemies)
			{
				enemy.Update();
			}

			player.State = Player.States.None;
			if (l && !r)
			{
				player.X = Math.Max(0, player.X - 1);
				player.State |= Player.States.Left;
			}
			if (r && !l)
			{
				player.X = Math.Min(gameWidth - 1, player.X + 1);
				player.State |= Player.States.Right;
			}
			if (u && !d)
			{
				player.Y = Math.Min(gameHeight - 1, player.Y + 1);
				player.State |= Player.States.Up;
			}
			if (d && !u)
			{
				player.Y = Math.Max(0, player.Y - 1);
				player.State |= Player.States.Down;
			}
			if (shoot)
			{
				playerBullets.Add(new() { X = (int)player.X - 2, Y = (int)player.Y });
				playerBullets.Add(new() { X = (int)player.X + 2, Y = (int)player.Y });
			}

			explodingBullets.Clear();

			for (int i = 0; i < playerBullets.Count; i++)
			{
				PlayerBullet bullet = playerBullets[i];
				bool exploded = false;
				IEnemy[] enemiesClone = enemies.ToArray();
				for (int j = 0; j < enemiesClone.Length; j++)
				{
					if (enemiesClone[j].CollidingWith(bullet.X, bullet.Y))
					{
						if (!exploded)
						{
							playerBullets.RemoveAt(i);
							explodingBullets.Add(bullet);
							i--;
							exploded = true;
						}
						enemiesClone[j].Shot();
					}
				}
				if (!exploded && (bullet.X < 0 || bullet.Y < 0 || bullet.X >= gameWidth || bullet.Y >= gameHeight))
				{
					playerBullets.RemoveAt(i);
					i--;
				}
			}

			foreach (IEnemy enemy in enemies)
			{
				if (enemy.CollidingWith((int)player.X, (int)player.Y))
				{
					isDead = true;
					return;
				}
			}

			for (int i = 0; i < enemies.Count; i++)
			{
				if (enemies[i].IsOutOfBounds())
				{
					enemies.RemoveAt(i);
					i--;
				}
			}
		}

		void SpawnARandomEnemy()
		{
			if (Random.Shared.Next(2) is 0)
			{
				enemies.Add(new Tank()
				{
					X = Random.Shared.Next(gameWidth - 10) + 5,
					Y = gameHeight + Tank.YMax,
					YVelocity = -1f / 10f,
				});
			}
			else if (Random.Shared.Next(2) is 0)
			{
				enemies.Add(new Helicopter()
				{
					X = -Helicopter.XMax,
					XVelocity = 1f / 3f,
					Y = Random.Shared.Next(gameHeight - 10) + 5,
				});
			}
			else if (Random.Shared.Next(3) is 0 or 1)
			{
				enemies.Add(new UFO1()
				{
					X = Random.Shared.Next(gameWidth - 10) + 5,
					Y = gameHeight + UFO1.YMax,
				});
			}
			else
			{
				enemies.Add(new UFO2());
			}
		}

		async Task Render()
		{
			const int maxRetryCount = 10;
			int retry = 0;
		Retry:
			if (retry > maxRetryCount)
			{
				return;
			}
			if (consoleWidth != Console.WindowWidth || consoleHeight != Console.WindowHeight)
			{
				consoleWidth = Console.WindowWidth;
				consoleHeight = Console.WindowHeight;
				await Console.Clear();
			}
			if (consoleWidth < intendedMinConsoleWidth || consoleHeight < intendedMinConsoleHeight)
			{
				await Console.Clear();
				await Console.Write($"Console too small at {consoleWidth}w x {consoleHeight}h. Please increase to at least {intendedMinConsoleWidth}w x {intendedMinConsoleHeight}h.");
				pauseUpdates = true;
				return;
			}
			pauseUpdates = false;
			ClearFrameBuffer();
			player.Render();
			foreach (IEnemy enemy in enemies)
			{
				enemy.Render();
			}
			foreach (PlayerBullet bullet in playerBullets)
			{
				if (bullet.X >= 0 && bullet.X < gameWidth && bullet.Y >= 0 && bullet.Y < gameHeight)
				{
					frameBuffer[bullet.X, bullet.Y] = '^';
				}
			}
			foreach (PlayerBullet explode in explodingBullets)
			{
				if (explode.X >= 0 && explode.X < gameWidth && explode.Y >= 0 && explode.Y < gameHeight)
				{
					frameBuffer[explode.X, explode.Y] = '#';
				}
			}
			render.Clear();
			render.AppendLine(topBorder);
			for (int y = gameHeight - 1; y >= 0; y--)
			{
				render.Append('┃');
				for (int x = 0; x < gameWidth; x++)
				{
					render.Append(frameBuffer[x, y]);
				}
				render.AppendLine("┃");
			}
			render.AppendLine(bottomBorder);
			render.AppendLine($"Score: {score}                             ");
			if (waitingForInput)
			{
				render.AppendLine("Press [WASD] or [SPACEBAR] to start...  ");
			}
			if (isDead)
			{
				render.AppendLine("YOU DIED! Press [ENTER] to play again...");
			}
			else
			{
				render.AppendLine("                                        ");
			}
			try
			{
				Console.CursorVisible = false;
				await Console.SetCursorPosition(0, 0);
				await Console.Write(render);
			}
			catch
			{
				retry++;
				goto Retry;
			}
		}

		void ClearFrameBuffer()
		{
			for (int x = 0; x < gameWidth; x++)
			{
				for (int y = 0; y < gameHeight; y++)
				{
					frameBuffer[x, y] = ' ';
				}
			}
		}

		async Task SleepAfterRender()
		{
			TimeSpan sleep = TimeSpan.FromSeconds(1d / 120d) - stopwatch.Elapsed;
			if (sleep > TimeSpan.Zero)
			{
				await Console.RefreshAndDelay(sleep);
			}
			stopwatch.Restart();
		}
	}

	internal class Player
	{
		[Flags]
		internal enum States
		{
			None  = 0,
			Up    = 1 << 0,
			Down  = 1 << 1,
			Left  = 1 << 2,
			Right = 1 << 3,
		}

		public float X;
		public float Y;
		public States State;

		static readonly string[] Sprite =
		{
			@"   ╱‾╲   ",
			@"  ╱╱‾╲╲  ",
			@" ╱'╲O╱'╲ ",
			@"╱ / ‾ \ ╲",
			@"╲_╱───╲_╱",
		};

		static readonly string[] SpriteUp =
		{
			@"   ╱‾╲   ",
			@"  ╱╱‾╲╲  ",
			@" ╱'╲O╱'╲ ",
			@"╱ / ‾ \ ╲",
			@"╲_╱───╲_╱",
			@"/V\   /V\",
		};

		static readonly string[] SpriteDown =
		{
			@"   ╱‾╲   ",
			@"  ╱╱‾╲╲  ",
			@"-╱'╲O╱'╲-",
			@"╱-/ ‾ \-╲",
			@"╲_╱───╲_╱",
		};

		static readonly string[] SpriteLeft =
		{
			@"   ╱╲   ",
			@"  ╱‾╲╲  ",
			@" ╱╲O╱'╲ ",
			@"╱/ ‾ \ ╲",
			@"╲╱───╲_╱",
		};

		static readonly string[] SpriteRight =
		{
			@"   ╱╲   ",
			@"  ╱╱‾╲  ",
			@" ╱'╲O╱╲ ",
			@"╱ / ‾ \╲",
			@"╲_╱───╲╱",
		};

		static readonly string[] SpriteUpLeft =
		{
			@"   ╱╲   ",
			@"  ╱‾╲╲  ",
			@" ╱╲O╱'╲ ",
			@"╱/ ‾ \ ╲",
			@"╲╱───╲_╱",
			@"/\   /V\",
		};

		static readonly string[] SpriteUpRight =
		{
			@"   ╱╲   ",
			@"  ╱╱‾╲  ",
			@" ╱'╲O╱╲ ",
			@"╱ / ‾ \╲",
			@"╲_╱───╲╱",
			@"/V\   /\",
		};

		static readonly string[] SpriteDownLeft =
		{
			@"   ╱╲   ",
			@"  ╱‾╲╲  ",
			@"-╱╲O╱'╲-",
			@"-/ ‾ \-╲",
			@"╲╱───╲_╱",
		};

		static readonly string[] SpriteDownRight =
		{
			@"   ╱╲   ",
			@"  ╱╱‾╲  ",
			@"-╱'╲O╱╲-",
			@"╱-/ ‾ \-",
			@"╲_╱───╲╱",
		};

		public void Render()
		{
			var (sprite, offset) = GetSpriteAndOffset();
			for (int y = 0; y < sprite.Length; y++)
			{
				int yo = (int)Y + y + offset.Y;
				int yi = sprite.Length - y - 1;
				if (yo >= 0 && yo < Shmup.frameBuffer.GetLength(1))
				{
					for (int x = 0; x < sprite[y].Length; x++)
					{
						int xo = (int)X + x + offset.X;
						if (xo >= 0 && xo < Shmup.frameBuffer.GetLength(0))
						{
							Shmup.frameBuffer[xo, yo] = sprite[yi][x];
						}
					}
				}
			}
		}

		internal (string[] Sprite, (int X, int Y) offset) GetSpriteAndOffset()
		{
			return State switch
			{
				States.None                => (Sprite, (-4, -2)),
				States.Up                  => (SpriteUp, (-4, -3)),
				States.Down                => (SpriteDown, (-4, -2)),
				States.Left                => (SpriteLeft, (-3, -2)),
				States.Right               => (SpriteRight, (-4, -2)),
				States.Up | States.Left    => (SpriteUpLeft, (-3, -3)),
				States.Up | States.Right   => (SpriteUpRight, (-4, -3)),
				States.Down | States.Left  => (SpriteDownLeft, (-3, -2)),
				States.Down | States.Right => (SpriteDownRight, (-4, -2)),
				_ => throw new NotImplementedException(),
			};
		}
	}

	internal class PlayerBullet
	{
		public int X;
		public int Y;
	}

	internal interface IEnemy
	{
		public void Shot();

		public void Render();

		public void Update();

		public bool CollidingWith(int x, int y);

		public bool IsOutOfBounds();
	}

	internal class Helicopter : IEnemy
	{
		public static int scorePerKill = 100;
		public int Health = 70;
		public float X;
		public float Y;
		public float XVelocity;
		public float YVelocity;
		private int Frame;
		private string[] Sprite = Random.Shared.Next(2) is 0 ? spriteA : spriteB;

		static readonly string[] spriteA =
		{
			@"  ~~~~~+~~~~~",
			@"'\===<[_]L)  ",
			@"     -'-`-   ",
		};

		static readonly string[] spriteB =
		{
			@"  -----+-----",
			@"*\===<[_]L)  ",
			@"     -'-`-   ",
		};

		internal static int XMax = Math.Max(spriteA.Max(s => s.Length), spriteB.Max(s => s.Length));
		internal static int YMax = Math.Max(spriteA.Length, spriteB.Length);

		public void Render()
		{
			for (int y = 0; y < Sprite.Length; y++)
			{
				int yo = (int)Y + y;
				int yi = Sprite.Length - y - 1;
				if (yo >= 0 && yo < Shmup.frameBuffer.GetLength(1))
				{
					for (int x = 0; x < Sprite[y].Length; x++)
					{
						int xo = (int)X + x;
						if (xo >= 0 && xo < Shmup.frameBuffer.GetLength(0))
						{
							if (Sprite[yi][x] is not ' ')
							{
								Shmup.frameBuffer[xo, yo] = Sprite[yi][x];
							}
						}
					}
				}
			}
		}

		public void Update()
		{
			Frame++;
			if (Frame > 10)
			{
				Sprite = Sprite == spriteB ? spriteA : spriteB;
				Frame = 0;
			}
			X += XVelocity;
			Y += YVelocity;
		}

		public bool CollidingWith(int x, int y)
		{
			int xo = x - (int)X;
			int yo = y - (int)Y;
			return
				yo >= 0 && yo < Sprite.Length &&
				xo >= 0 && xo < Sprite[yo].Length &&
				Sprite[yo][xo] is not ' ';
		}

		public bool IsOutOfBounds()
		{
			return
				XVelocity <= 0 && X < -XMax ||
				YVelocity <= 0 && Y < -YMax ||
				XVelocity >= 0 && X > Shmup.gameWidth + XMax ||
				YVelocity >= 0 && Y > Shmup.gameHeight + YMax;
		}

		public void Shot()
		{
			Health--;
			if (Health <= 0)
			{
				Shmup.enemies.Remove(this);
				Shmup.score += scorePerKill;
			}
		}
	}

	internal class Tank : IEnemy
	{
		public static int scorePerKill = 20;
		public int Health = 20;
		public float X;
		public float Y;
		public float XVelocity;
		public float YVelocity;
		private string[] Sprite;

		static readonly string[] spriteDown =
		{
			@" ___ ",
			@"|_O_|",
			@"[ooo]",
		};

		static readonly string[] spriteUp =
		{
			@" _^_ ",
			@"|___|",
			@"[ooo]",
		};

		static readonly string[] spriteLeft =
		{
			@"  __ ",
			@"=|__|",
			@"[ooo]",
		};

		static readonly string[] spriteRight =
		{
			@" __  ",
			@"|__|=",
			@"[ooo]",
		};

		internal static int XMax = new[] { spriteDown.Max(s => s.Length), spriteUp.Max(s => s.Length), spriteLeft.Max(s => s.Length), spriteRight.Max(s => s.Length), }.Max();
		internal static int YMax = new[] { spriteDown.Length, spriteUp.Length, spriteLeft.Length, spriteRight.Length, }.Max();

		public void Render()
		{
			for (int y = 0; y < Sprite.Length; y++)
			{
				int yo = (int)Y + y;
				int yi = Sprite.Length - y - 1;
				if (yo >= 0 && yo < Shmup.frameBuffer.GetLength(1))
				{
					for (int x = 0; x < Sprite[y].Length; x++)
					{
						int xo = (int)X + x;
						if (xo >= 0 && xo < Shmup.frameBuffer.GetLength(0))
						{
							if (Sprite[yi][x] is not ' ')
							{
								Shmup.frameBuffer[xo, yo] = Sprite[yi][x];
							}
						}
					}
				}
			}
		}

		public void Update()
		{
			int xDifToPlayer = (int)Shmup.player.X - (int)X;
			int yDifToPlayer = (int)Shmup.player.Y - (int)Y;

			Sprite = Math.Abs(xDifToPlayer) > Math.Abs(yDifToPlayer)
				? xDifToPlayer > 0 ? spriteRight : spriteLeft
				: yDifToPlayer > 0 ? spriteUp : spriteDown;

			X += XVelocity;
			Y += YVelocity;
		}

		public bool CollidingWith(int x, int y)
		{
			int xo = x - (int)X;
			int yo = y - (int)Y;
			return
				yo >= 0 && yo < Sprite.Length &&
				xo >= 0 && xo < Sprite[yo].Length &&
				Sprite[yo][xo] is not ' ';
		}

		public bool IsOutOfBounds()
		{
			return
				XVelocity <= 0 && X < -XMax ||
				YVelocity <= 0 && Y < -YMax ||
				XVelocity >= 0 && X > Shmup.gameWidth + XMax ||
				YVelocity >= 0 && Y > Shmup.gameHeight + YMax;
		}

		public void Shot()
		{
			Health--;
			if (Health <= 0)
			{
				Shmup.enemies.Remove(this);
				Shmup.score += scorePerKill;
			}
		}
	}

	internal class UFO1 : IEnemy
	{
		public static int scorePerKill = 10;
		public int Health = 10;
		public float X;
		public float Y;
		public float XVelocity = 1f / 8f;
		public float YVelocity = 1f / 8f;
		private static readonly string[] Sprite =
		{
			@" _!_ ",
			@"(_o_)",
			@" ''' ",
		};

		internal static int XMax = Sprite.Max(s => s.Length);
		internal static int YMax = Sprite.Length;

		public void Render()
		{
			for (int y = 0; y < Sprite.Length; y++)
			{
				int yo = (int)Y + y;
				int yi = Sprite.Length - y - 1;
				if (yo >= 0 && yo < Shmup.frameBuffer.GetLength(1))
				{
					for (int x = 0; x < Sprite[y].Length; x++)
					{
						int xo = (int)X + x;
						if (xo >= 0 && xo < Shmup.frameBuffer.GetLength(0))
						{
							if (Sprite[yi][x] is not ' ')
							{
								Shmup.frameBuffer[xo, yo] = Sprite[yi][x];
							}
						}
					}
				}
			}
		}

		public void Update()
		{
			if (Shmup.player.X < X)
			{
				X = Math.Max(Shmup.player.X, X - XVelocity);
			}
			else
			{
				X = Math.Min(Shmup.player.X, X + XVelocity);
			}
			if (Shmup.player.Y < Y)
			{
				Y = Math.Max(Shmup.player.Y, Y - YVelocity);
			}
			else
			{
				Y = Math.Min(Shmup.player.Y, Y + YVelocity);
			}
		}

		public bool CollidingWith(int x, int y)
		{
			int xo = x - (int)X;
			int yo = y - (int)Y;
			return
				yo >= 0 && yo < Sprite.Length &&
				xo >= 0 && xo < Sprite[yo].Length &&
				Sprite[yo][xo] is not ' ';
		}

		public bool IsOutOfBounds()
		{
			return
				XVelocity <= 0 && X < -XMax ||
				YVelocity <= 0 && Y < -YMax ||
				XVelocity >= 0 && X > Shmup.gameWidth + XMax ||
				YVelocity >= 0 && Y > Shmup.gameHeight + YMax;
		}

		public void Shot()
		{
			Health--;
			if (Health <= 0)
			{
				Shmup.enemies.Remove(this);
				Shmup.score += scorePerKill;
			}
		}
	}

	internal class UFO2 : IEnemy
	{
		public static int scorePerKill = 80;
		public int Health = 50;
		public float X;
		public float Y;
		public int UpdatesSinceTeleport;
		public int TeleportFrequency = 360;

		private static readonly string[] Sprite =
		{
			@"     _!_     ",
			@"    /_O_\    ",
			@"-==<_‗_‗_>==-",
		};

		internal static int XMax = Sprite.Max(s => s.Length);
		internal static int YMax = Sprite.Length;

		public UFO2()
		{
			X = Random.Shared.Next(Shmup.gameWidth - XMax) + XMax / 2;
			Y = Random.Shared.Next(Shmup.gameHeight - YMax) + YMax / 2;
		}

		public void Render()
		{
			for (int y = 0; y < Sprite.Length; y++)
			{
				int yo = (int)Y + y;
				int yi = Sprite.Length - y - 1;
				if (yo >= 0 && yo < Shmup.frameBuffer.GetLength(1))
				{
					for (int x = 0; x < Sprite[y].Length; x++)
					{
						int xo = (int)X + x;
						if (xo >= 0 && xo < Shmup.frameBuffer.GetLength(0))
						{
							if (Sprite[yi][x] is not ' ')
							{
								Shmup.frameBuffer[xo, yo] = Sprite[yi][x];
							}
						}
					}
				}
			}
		}

		public void Update()
		{
			UpdatesSinceTeleport++;
			if (UpdatesSinceTeleport > TeleportFrequency)
			{
				X = Random.Shared.Next(Shmup.gameWidth - XMax) + XMax / 2;
				Y = Random.Shared.Next(Shmup.gameHeight - YMax) + YMax / 2;
				UpdatesSinceTeleport = 0;
			}
		}

		public bool CollidingWith(int x, int y)
		{
			int xo = x - (int)X;
			int yo = y - (int)Y;
			return
				yo >= 0 && yo < Sprite.Length &&
				xo >= 0 && xo < Sprite[yo].Length &&
				Sprite[yo][xo] is not ' ';
		}

		public bool IsOutOfBounds()
		{
			return !
				(X > 0 &&
				X < Shmup.gameWidth &&
				Y > 0 &&
				Y < Shmup.gameHeight);
		}

		public void Shot()
		{
			Health--;
			if (Health <= 0)
			{
				Shmup.enemies.Remove(this);
				Shmup.score += scorePerKill;
			}
		}
	}
}
