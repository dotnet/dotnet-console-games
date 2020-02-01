using System;
using System.Diagnostics;
using System.Threading;

class Program
{
	static class Ascii
	{
		#region Ascii

		public static readonly string[] IdleAnimation = new string[]
		{
			// 0
			@"        " + '\n' +
			@"        " + '\n' +
			@"    O   " + '\n' +
			@"   L|(  " + '\n' +
			@"    |   " + '\n' +
			@"   ( \  ",
			// 1
			@"        " + '\n' +
			@"        " + '\n' +
			@"    o   " + '\n' +
			@"   ((L  " + '\n' +
			@"    |   " + '\n' +
			@"   / )  ",
		};

		public static readonly string[] PunchAnimation = new string[]
		{
			// 1
			@"         " + '\n' +
			@"         " + '\n' +
			@"   _o_.  " + '\n' +
			@"   (|    " + '\n' +
			@"    |    " + '\n' +
			@"   > \   ",
			// 2
			@"         " + '\n' +
			@"         " + '\n' +
			@"    o__. " + '\n' +
			@"   (|    " + '\n' +
			@"    |    " + '\n' +
			@"   / >   ",
			// 3
			@"         " + '\n' +
			@"         " + '\n' +
			@"    O___." + '\n' +
			@"   L(    " + '\n' +
			@"    |    " + '\n' +
			@"   / >   ",
			// 4
			@"         " + '\n' +
			@"         " + '\n' +
			@"    o_   " + '\n' +
			@"   L( \  " + '\n' +
			@"    |    " + '\n' +
			@"   > \   ",
			// 5
			@"         " + '\n' +
			@"         " + '\n' +
			@"    o_   " + '\n' +
			@"   L( >  " + '\n' +
			@"    |    " + '\n' +
			@"   > \   ",
			// 6
			@"         " + '\n' +
			@"         " + '\n' +
			@"    o    " + '\n' +
			@"   (|)   " + '\n' +
			@"    |    " + '\n' +
			@"   / \   ",
		};

		#endregion
	}

	static readonly int height = Console.WindowHeight;
	static readonly int width = Console.WindowWidth;

	static readonly TimeSpan sleep = TimeSpan.FromMilliseconds(10);
	static readonly TimeSpan timeSpanIdle = TimeSpan.FromMilliseconds(400);
	static readonly TimeSpan timeSpanPunch = TimeSpan.FromMilliseconds(100);
	static readonly Stopwatch stopwatchIdle = new Stopwatch();
	static readonly Stopwatch stopwatchPunch = new Stopwatch();

	static int? idleFrame = 0;
	static int? punchFrame = null;

	static void Main()
	{
		Console.WriteLine("This game is still under development.");
		Console.WriteLine("Press Enter To Continue...");
		Console.ReadLine();
		Console.Clear();

		Console.CursorVisible = false;
		stopwatchIdle.Restart();
		Console.SetCursorPosition(0, 0);
		Render(Ascii.IdleAnimation[idleFrame.Value]);
		while (true)
		{
			#region Input

			bool skipUpdate = false;
			if (Console.KeyAvailable)
			{
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.RightArrow:
						if (idleFrame.HasValue)
						{
							Console.SetCursorPosition(0, 0);
							Erase(Ascii.IdleAnimation[idleFrame.Value]);
							idleFrame = null;
							punchFrame = 0;
							Console.SetCursorPosition(0, 0);
							Render(Ascii.PunchAnimation[punchFrame.Value]);
							stopwatchPunch.Restart();
							skipUpdate = true;
						}
						break;
					case ConsoleKey.Escape:
						Console.Clear();
						Console.Write("Fighter was closed.");
						return;
				}
			}
			while (Console.KeyAvailable)
			{
				Console.ReadKey(true);
			}

			#endregion

			#region Update

			if (!skipUpdate)
			{
				if (idleFrame.HasValue && stopwatchIdle.Elapsed > timeSpanIdle)
				{
					Console.SetCursorPosition(0, 0);
					Erase(Ascii.IdleAnimation[idleFrame.Value]);
					idleFrame = idleFrame is 0 ? 1 : 0;
					Console.SetCursorPosition(0, 0);
					Render(Ascii.IdleAnimation[idleFrame.Value]);
					stopwatchIdle.Restart();
				}
				if (punchFrame.HasValue && stopwatchPunch.Elapsed > timeSpanPunch)
				{
					Console.SetCursorPosition(0, 0);
					Erase(Ascii.PunchAnimation[punchFrame.Value]);
					punchFrame++;
					if (punchFrame >= Ascii.PunchAnimation.Length)
					{
						idleFrame = 0;
						punchFrame = null;
						Console.SetCursorPosition(0, 0);
						Render(Ascii.IdleAnimation[idleFrame.Value]);
						stopwatchIdle.Restart();
					}
					else
					{
						Console.SetCursorPosition(0, 0);
						Render(Ascii.PunchAnimation[punchFrame.Value]);
						stopwatchPunch.Restart();
					}
				}
			}

			#endregion

			Thread.Sleep(sleep);
		}
	}

	static void Render(string @string, bool renderSpace = false)
	{
		int x = Console.CursorLeft;
		int y = Console.CursorTop;
		foreach (char c in @string)
			if (c is '\n')
				Console.SetCursorPosition(x, ++y);
			else if (Console.CursorLeft < width - 1 && (!(c is ' ') || renderSpace))
				Console.Write(c);
			else if (Console.CursorLeft < width - 1 && Console.CursorTop < height - 1)
				Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
	}

	static void Erase(string @string)
	{
		int x = Console.CursorLeft;
		int y = Console.CursorTop;
		foreach (char c in @string)
			if (c is '\n')
				Console.SetCursorPosition(x, ++y);
			else if (Console.CursorLeft < width - 1 && !(c is ' '))
				Console.Write(' ');
			else if (Console.CursorLeft < width - 1 && Console.CursorTop < height - 1)
				Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
	}
}
