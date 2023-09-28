using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Shmup;

internal static class Program
{
	internal static bool closeRequested = false;
	internal static Stopwatch stopwatch = new();
	internal static bool pauseUpdates = false;

	internal static int gameWidth = 60;
	internal static int gameHeight = 40;
	internal static int intendedMinConsoleWidth = gameWidth + 3;
	internal static int intendedMinConsoleHeight = gameHeight + 3;
	internal static char[,] frameBuffer = new char[gameWidth, gameHeight];
	internal static string topBorder = '┏' + new string('━', gameWidth) + '┓';
	internal static string bottomBorder = '┗' + new string('━', gameWidth) + '┛';

	internal static int update = 0;

	internal static int consoleWidth = Console.WindowWidth;
	internal static int consoleHeight = Console.WindowHeight;

	internal static Player player = new()
	{
		X = gameWidth / 2,
		Y = gameHeight / 4,
	};

	internal static List<PlayerBullet> playerBullets = new();

	internal static List<IEnemy> enemies = new();

	internal static void Main()
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
		Console.Clear();
		if (Console.OutputEncoding != Encoding.UTF8)
		{
			Console.OutputEncoding = Encoding.UTF8;
		}
		while (!closeRequested)
		{
			Update();
			if (closeRequested)
			{
				return;
			}
			Render();
			SleepAfterRender();
		}
	}

	internal static void Update()
	{
		if (pauseUpdates)
		{
			return;
		}

		update++;

		if (update % 60 is 0)
		{
			enemies.Add(new Helicopter()
			{
				X = -Helicopter.XMax,
				XVelocity = 1f / 3f,
				Y = Random.Shared.Next(gameHeight - 10) + 5,
			});
		}

		if (update % 100 is 0)
		{
			enemies.Add(new Tank()
			{
				X = Random.Shared.Next(gameWidth - 10) + 5,
				Y = gameHeight + Tank.YMax,
				YVelocity = -1f / 10f,
			});
		}

		for (int i = 0; i < playerBullets.Count; i++)
		{
			playerBullets[i].Y++;
		}

		foreach (IEnemy enemy in enemies)
		{
			enemy.Update();
		}

		bool u = false;
		bool d = false;
		bool l = false;
		bool r = false;
		bool shoot = false;
		while (Console.KeyAvailable)
		{
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.Escape: closeRequested = true; return;
				case ConsoleKey.W or ConsoleKey.UpArrow: u = true; break;
				case ConsoleKey.A or ConsoleKey.LeftArrow: l = true; break;
				case ConsoleKey.S or ConsoleKey.DownArrow: d = true; break;
				case ConsoleKey.D or ConsoleKey.RightArrow: r = true; break;
				case ConsoleKey.Spacebar: shoot = true; break;
			}
		}
		if (OperatingSystem.IsWindows())
		{
			u = u || User32_dll.GetAsyncKeyState((int)ConsoleKey.W) is not 0;
			l = l || User32_dll.GetAsyncKeyState((int)ConsoleKey.A) is not 0;
			d = d || User32_dll.GetAsyncKeyState((int)ConsoleKey.S) is not 0;
			r = r || User32_dll.GetAsyncKeyState((int)ConsoleKey.D) is not 0;

			u = u || User32_dll.GetAsyncKeyState((int)ConsoleKey.UpArrow) is not 0;
			l = l || User32_dll.GetAsyncKeyState((int)ConsoleKey.LeftArrow) is not 0;
			d = d || User32_dll.GetAsyncKeyState((int)ConsoleKey.DownArrow) is not 0;
			r = r || User32_dll.GetAsyncKeyState((int)ConsoleKey.RightArrow) is not 0;

			shoot = shoot || User32_dll.GetAsyncKeyState((int)ConsoleKey.Spacebar) is not 0;
		}
		if (l && !r)
		{
			player.X = Math.Max(0, player.X - 1);
		}
		if (r && !l)
		{
			player.X = Math.Min(gameWidth - 1, player.X + 1);
		}
		if (u && !d)
		{
			player.Y = Math.Min(gameHeight - 1, player.Y + 1);
		}
		if (d && !u)
		{
			player.Y = Math.Max(0, player.Y - 1);
		}
		if (shoot)
		{
			playerBullets.Add(new() { X = (int)player.X - 2, Y = (int)player.Y });
			playerBullets.Add(new() { X = (int)player.X + 2, Y = (int)player.Y });
		}

		for (int i = 0; i < playerBullets.Count; i++)
		{
			PlayerBullet bullet = playerBullets[i];
			if (bullet.X < 0 || bullet.Y < 0 || bullet.X >= gameWidth || bullet.Y >= gameHeight)
			{
				playerBullets.RemoveAt(i);
				i--;
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

	internal static void Render()
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
			Console.Clear();
		}
		if (consoleWidth < intendedMinConsoleWidth || consoleHeight < intendedMinConsoleHeight)
		{
			Console.Clear();
			Console.Write($"Console too small at {consoleWidth}w x {consoleHeight}h. Please increase to at least {intendedMinConsoleWidth}w x {intendedMinConsoleHeight}h.");
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
		foreach (var bullet in playerBullets)
		{
			frameBuffer[bullet.X, bullet.Y] = '^';
		}
		StringBuilder render = new();
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
		try
		{
			Console.CursorVisible = false;
			Console.SetCursorPosition(0, 0);
			Console.Write(render);
		}
		catch
		{
			retry++;
			goto Retry;
		}
	}

	internal static void ClearFrameBuffer()
	{
		for (int x = 0; x < gameWidth; x++)
		{
			for (int y = 0; y < gameHeight; y++)
			{
				frameBuffer[x, y] = ' ';
			}
		}
	}

	internal static void SleepAfterRender()
	{
		TimeSpan sleep = TimeSpan.FromSeconds(1d / 120d) - stopwatch.Elapsed;
		if (sleep > TimeSpan.Zero)
		{
			Thread.Sleep(sleep);
		}
		stopwatch.Restart();
	}

	internal static class User32_dll
	{
		[DllImport("user32.dll")]
		internal static extern short GetAsyncKeyState(int vKey);
	}
}
