using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
	enum Direction
	{
		Up = 1,
		Right = 2,
		Down = 3,
		Left = 4,
	}

	#region Ascii

	static class Ascii
	{
		public static readonly string[] Renders = new string[]
		{
			// 0
			@"           ╔══════╗        " + '\n' +
			@"           ║      ║        " + '\n' +
			@"           ╚╗    ╔╝        " + '\n' +
			@"    ╔═══╗   ╚╗  ╔╝   ╔═══╗ " + '\n' +
			@"    ║   ╚═══╗╚══╝╔═══╝   ║ " + '\n' +
			@"    ║       ║    ║       ║ " + '\n' +
			@"    ║   ╔═══╝╔══╗╚═══╗   ║ " + '\n' +
			@"    ╚═══╝   ╔╝  ╚╗   ╚═══╝ " + '\n' +
			@"           ╔╝    ╚╗        " + '\n' +
			@"           ║      ║        " + '\n' +
			@"           ╚══════╝        ",
			// 1
			@"           ╔══════╗        " + '\n' +
			@"           ║██████║        " + '\n' +
			@"           ╚╗████╔╝        " + '\n' +
			@"    ╔═══╗   ╚╗██╔╝   ╔═══╗ " + '\n' +
			@"    ║   ╚═══╗╚══╝╔═══╝   ║ " + '\n' +
			@"    ║       ║    ║       ║ " + '\n' +
			@"    ║   ╔═══╝╔══╗╚═══╗   ║ " + '\n' +
			@"    ╚═══╝   ╔╝  ╚╗   ╚═══╝ " + '\n' +
			@"           ╔╝    ╚╗        " + '\n' +
			@"           ║      ║        " + '\n' +
			@"           ╚══════╝        ",
			// 2
			@"           ╔══════╗        " + '\n' +
			@"           ║      ║        " + '\n' +
			@"           ╚╗    ╔╝        " + '\n' +
			@"    ╔═══╗   ╚╗  ╔╝   ╔═══╗ " + '\n' +
			@"    ║   ╚═══╗╚══╝╔═══╝███║ " + '\n' +
			@"    ║       ║    ║███████║ " + '\n' +
			@"    ║   ╔═══╝╔══╗╚═══╗███║ " + '\n' +
			@"    ╚═══╝   ╔╝  ╚╗   ╚═══╝ " + '\n' +
			@"           ╔╝    ╚╗        " + '\n' +
			@"           ║      ║        " + '\n' +
			@"           ╚══════╝        ",
			// 3
			@"           ╔══════╗        " + '\n' +
			@"           ║      ║        " + '\n' +
			@"           ╚╗    ╔╝        " + '\n' +
			@"    ╔═══╗   ╚╗  ╔╝   ╔═══╗ " + '\n' +
			@"    ║   ╚═══╗╚══╝╔═══╝   ║ " + '\n' +
			@"    ║       ║    ║       ║ " + '\n' +
			@"    ║   ╔═══╝╔══╗╚═══╗   ║ " + '\n' +
			@"    ╚═══╝   ╔╝██╚╗   ╚═══╝ " + '\n' +
			@"           ╔╝████╚╗        " + '\n' +
			@"           ║██████║        " + '\n' +
			@"           ╚══════╝        ",
			// 4
			@"           ╔══════╗        " + '\n' +
			@"           ║      ║        " + '\n' +
			@"           ╚╗    ╔╝        " + '\n' +
			@"    ╔═══╗   ╚╗  ╔╝   ╔═══╗ " + '\n' +
			@"    ║███╚═══╗╚══╝╔═══╝   ║ " + '\n' +
			@"    ║███████║    ║       ║ " + '\n' +
			@"    ║███╔═══╝╔══╗╚═══╗   ║ " + '\n' +
			@"    ╚═══╝   ╔╝  ╚╗   ╚═══╝ " + '\n' +
			@"           ╔╝    ╚╗        " + '\n' +
			@"           ║      ║        " + '\n' +
			@"           ╚══════╝        ",


		};
	}

	#endregion

	static readonly bool CursorVisible = Console.CursorVisible;
	static readonly Random random = new Random();
	static readonly TimeSpan buttonPress = TimeSpan.FromMilliseconds(500);
	static readonly TimeSpan animationDelay = TimeSpan.FromMilliseconds(200);
	static int score = 0;
	static readonly List<Direction> pattern = new List<Direction>();

	static void Main()
	{
		try
		{
			Console.CursorVisible = false;
			Clear();
			Render(Ascii.Renders[default]);
			while (true)
			{
				Thread.Sleep(buttonPress);
				pattern.Add((Direction)random.Next(1, 5));
				AnimateCurrentPattern();
				for (int i = 0; i < pattern.Count; i++)
				{
					GetInput:
					switch (Console.ReadKey(true).Key)
					{
						case ConsoleKey.UpArrow:
							if (pattern[i] != Direction.Up) goto GameOver;
							break;
						case ConsoleKey.RightArrow:
							if (pattern[i] != Direction.Right) goto GameOver;
							break;
						case ConsoleKey.DownArrow:
							if (pattern[i] != Direction.Down) goto GameOver;
							break;
						case ConsoleKey.LeftArrow:
							if (pattern[i] != Direction.Left) goto GameOver;
							break;
						case ConsoleKey.Escape:
							Console.Clear();
							Console.Write("Simon was closed.");
							return;
						default: goto GetInput;
					}
					score++;
					Clear();
					Render(Ascii.Renders[(int)pattern[i]]);
					Thread.Sleep(buttonPress);
					Clear();
					Render(Ascii.Renders[default]);
				}
			}
			GameOver:
			Console.Clear();
			Console.Write("Game Over. Score: " + score + ".");
		}
		finally
		{
			Console.CursorVisible = CursorVisible;
		}
	}

	static void Clear()
	{
		Console.SetCursorPosition(0, 0);
		Console.WriteLine();
		Console.WriteLine("    Simon");
		Console.WriteLine();
		int left = Console.CursorLeft;
		int top = Console.CursorTop;
		Render(Ascii.Renders[default]);
		Console.SetCursorPosition(left, top);
	}

	static void AnimateCurrentPattern()
	{
		for (int i = 0; i < pattern.Count; i++)
		{
			Clear();
			Render(Ascii.Renders[(int)pattern[i]]);
			Thread.Sleep(buttonPress);
			Clear();
			Render(Ascii.Renders[default]);
			Thread.Sleep(animationDelay);
		}
		Clear();
		Render(Ascii.Renders[default]);
	}

	static void Render(string @string)
	{
		int x = Console.CursorLeft;
		int y = Console.CursorTop;
		foreach (char c in @string)
		{
			if (c is '\n')
			{
				Console.SetCursorPosition(x, ++y);
			}
			else
			{
				Console.Write(c);
			}
		}
	}
}
