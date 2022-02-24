using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Console = Website.Console<Website.Games.Helicopter.Helicopter>;

namespace Website.Games.Helicopter;

public class Helicopter
{
	public static async Task Run()
	{

TimeSpan threadSleepTimeSpan = TimeSpan.FromMilliseconds(10);
TimeSpan helicopterTimeSpan = TimeSpan.FromMilliseconds(70);
TimeSpan ufoMovementTimeSpan = TimeSpan.FromMilliseconds(100);
TimeSpan enemySpawnTimeSpan = TimeSpan.FromSeconds(1.75);

List<UFO> ufos = new();
List<Bullet> bullets = new();
List<Explosion> explosions = new();
Stopwatch stopwatchGame = new();
Stopwatch stopwatchUFOSpawn = new();
Stopwatch stopwatchHelicopter = new();
Stopwatch stopwatchUFO = new();
Random random = new();

int score = 0;
bool bulletFrame = default;
bool helicopterRender = default;

#region Ascii Renders

string[] bulletRenders = new string[]
{
	" ", // 0
	"-", // 1
	"~", // 2
	"█", // 3
};

string[] helicopterRenders = new string[]
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

string[] ufoRenders = new string[]
{
	// 0
	@"   __O__   " + '\n' +
	@"-=<_‗_‗_>=-",
	// 1
	@"     _!_     " + '\n' +
	@"    /_O_\    " + '\n' +
	@"-==<_‗_‗_>==-",
	// 2
	@"  _/\_  " + '\n' +
	@" /_OO_\ " + '\n' +
	@"() () ()",
	// 3
	@" _!_!_ " + '\n' +
	@"|_o-o_|" + '\n' +
	@" ^^^^^ ",
	// 4
	@" _!_ " + '\n' +
	@"(_o_)" + '\n' +
	@" ^^^ ",
};

string[] explosionRenders = new string[]
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

await Console.Clear();
if (OperatingSystem.IsWindows())
{
	Console.WindowWidth = 100;
	Console.WindowHeight = 30;
}

int height = Console.WindowHeight;
int width = Console.WindowWidth;
Player player = new() { Left = 2, Top = height / 2, };

Console.CursorVisible = false;
stopwatchGame.Restart();
stopwatchUFOSpawn.Restart();
stopwatchHelicopter.Restart();
stopwatchUFO.Restart();
while (true)
{
	#region Window Resize

	if (height != Console.WindowHeight || width != Console.WindowWidth)
	{
		await Console.Clear();
		await Console.Write("Console window resized. Helicopter closed.");
		return;
	}

	#endregion

	#region Update UFOs

	if (stopwatchUFOSpawn.Elapsed > enemySpawnTimeSpan)
	{
		ufos.Add(new UFO
		{
			Health = 4,
			Frame = random.Next(5),
			Top = random.Next(height - 3),
			Left = width,
		});
		stopwatchUFOSpawn.Restart();
	}

	if (stopwatchUFO.Elapsed > ufoMovementTimeSpan)
	{
		foreach (UFO ufo in ufos)
		{
			if (ufo.Left < width)
			{
				await Console.SetCursorPosition(ufo.Left, ufo.Top);
				await Erase(ufoRenders[ufo.Frame]);
			}
			ufo.Left--;
			if (ufo.Left <= 0)
			{
				await Console.Clear();
				await Console.Write("Game Over. Score: " + score + ".");
				return;
			}
		}
		stopwatchUFO.Restart();
	}

	#endregion

	#region Update Player

	bool playerRenderRequired = false;
	if (await Console.KeyAvailable())
	{
		switch ((await Console.ReadKey(true)).Key)
		{
			case ConsoleKey.UpArrow:
				await Console.SetCursorPosition(player.Left, player.Top);
				await Render(helicopterRenders[default], true);
				player.Top = Math.Max(player.Top - 1, 0);
				playerRenderRequired = true;
				break;
			case ConsoleKey.DownArrow:
				await Console.SetCursorPosition(player.Left, player.Top);
				await Render(helicopterRenders[default], true);
				player.Top = Math.Min(player.Top + 1, height - 3);
				playerRenderRequired = true;
				break;
			case ConsoleKey.RightArrow:
				bullets.Add(new Bullet
				{
					Left = player.Left + 11,
					Top = player.Top + 1,
					Frame = (bulletFrame = !bulletFrame) ? 1 : 2,
				});
				break;
			case ConsoleKey.Escape:
				await Console.Clear();
				await Console.Write("Helicopter was closed.");
				return;
		}
	}
	while (await Console.KeyAvailable())
	{
		await Console.ReadKey(true);
	}

	#endregion

	#region Update Bullets

	HashSet<Bullet> bulletRemovals = new();
	foreach (Bullet bullet in bullets)
	{
		await Console.SetCursorPosition(bullet.Left, bullet.Top);
		await Console.Write(bulletRenders[default]);
		bullet.Left++;
		if (bullet.Left >= width || bullet.Frame is 3)
		{
			bulletRemovals.Add(bullet);
		}
		HashSet<UFO> ufoRemovals = new();
		foreach (UFO ufo in ufos)
		{
			if (ufo.Left <= bullet.Left &&
				ufo.Top <= bullet.Top &&
				CollisionCheck(
				(bulletRenders[bullet.Frame], bullet.Left, bullet.Top),
				(ufoRenders[ufo.Frame], ufo.Left, ufo.Top)))
			{
				bullet.Frame = 3;
				ufo.Health--;
				if (ufo.Health <= 0)
				{
					score += 100;
					await Console.SetCursorPosition(ufo.Left, ufo.Top);
					await Erase(ufoRenders[ufo.Frame]);
					ufoRemovals.Add(ufo);
					explosions.Add(new Explosion
					{
						Left = bullet.Left - 5,
						Top = Math.Max(bullet.Top - 2, 0),
					});
				}
			}
		}
		ufos.RemoveAll(ufoRemovals.Contains);
	}
	bullets.RemoveAll(bulletRemovals.Contains);

	#endregion

	#region Update & Render Explosions

	HashSet<Explosion> explosionRemovals = new();
	foreach (Explosion explosion in explosions)
	{
		if (explosion.Frame > 0)
		{
			await Console.SetCursorPosition(explosion.Left, explosion.Top);
			await Erase(explosionRenders[explosion.Frame - 1]);
		}
		if (explosion.Frame < explosionRenders.Length)
		{
			await Console.SetCursorPosition(explosion.Left, explosion.Top);
			await Render(explosionRenders[explosion.Frame]);
		}
		explosion.Frame++;
		if (explosion.Frame > explosionRenders.Length)
		{
			explosionRemovals.Add(explosion);
		}
	}
	explosions.RemoveAll(explosionRemovals.Contains);

	#endregion

	#region Render Player

	if (stopwatchHelicopter.Elapsed > helicopterTimeSpan)
	{
		helicopterRender = !helicopterRender;
		stopwatchHelicopter.Restart();
		playerRenderRequired = true;
	}
	if (playerRenderRequired)
	{
		await Console.SetCursorPosition(player.Left, player.Top);
		await Render(helicopterRenders[helicopterRender ? 1 : 2]);
	}

	#endregion

	#region Render UFOs

	foreach (UFO ufo in ufos)
	{
		if (ufo.Left < width)
		{
			await Console.SetCursorPosition(ufo.Left, ufo.Top);
			await Render(ufoRenders[ufo.Frame]);
		}
	}

	#endregion

	#region Render Bullets

	foreach (Bullet bullet in bullets)
	{
		await Console.SetCursorPosition(bullet.Left, bullet.Top);
		await Render(bulletRenders[bullet.Frame]);
	}

	#endregion

	await Console.RefreshAndDelay(threadSleepTimeSpan);
}

async Task Render(string @string, bool renderSpace = false)
{
	int x = Console.CursorLeft;
	int y = Console.CursorTop;
	foreach (char c in @string)
		if (c is '\n')
			await Console.SetCursorPosition(x, ++y);
		else if (Console.CursorLeft < width - 1 && (!(c is ' ') || renderSpace))
			await Console.Write(c);
		else if (Console.CursorLeft < width - 1 && Console.CursorTop < height - 1)
			await Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
}

async Task Erase(string @string)
{
	int x = Console.CursorLeft;
	int y = Console.CursorTop;
	foreach (char c in @string)
		if (c is '\n')
			await Console.SetCursorPosition(x, ++y);
		else if (Console.CursorLeft < width - 1 && !(c is ' '))
			await Console.Write(' ');
		else if (Console.CursorLeft < width - 1 && Console.CursorTop < height - 1)
			await Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
}

bool CollisionCheck((string String, int Left, int Top) A, (string String, int Left, int Top) B)
{
	char[,] buffer = new char[width, height];
	int left = A.Left;
	int top = A.Top;
	foreach (char c in A.String)
	{
		if (c is '\n')
		{
			left = A.Left;
			top++;
		}
		else if (left < width && top < height && c != ' ')
		{
			buffer[left++, top] = c;
		}
	}
	left = B.Left;
	top = B.Top;
	foreach (char c in B.String)
	{
		if (c is '\n')
		{
			left = A.Left;
			top++;
		}
		else if (left < width && top < height && c != ' ')
		{
			if (buffer[left, top] != default)
			{
				return true;
			}
			buffer[left++, top] = c;
		}
	}
	return false;
}
	}

	class Player
	{
		public int Left;
		public int Top;
	}

	class Bullet
	{
		public int Left;
		public int Top;
		public int Frame;
	}

	class Explosion
	{
		public int Left;
		public int Top;
		public int Frame;
	}

	class UFO
	{
		public int Frame;
		public int Left;
		public int Top;
		public int Health;
	}
}
