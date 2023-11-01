using System;
using System.Collections.Generic;
using System.Threading;

TimeSpan buttonPress = TimeSpan.FromMilliseconds(500);
TimeSpan animationDelay = TimeSpan.FromMilliseconds(200);
int score = 0;
List<Direction> pattern = new();

string[] Renders = new string[]
{
	#region Frames
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
	#endregion
};

try
{
	Console.Clear();
	Console.CursorVisible = false;
	Clear();
	Render(Renders[default]);
	while (true)
	{
		Thread.Sleep(buttonPress);
		pattern.Add((Direction)Random.Shared.Next(1, 5));
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
			Render(Renders[(int)pattern[i]]);
			Thread.Sleep(buttonPress);
			Clear();
			Render(Renders[default]);
		}
	}
GameOver:
	Console.Clear();
	Console.Write("Game Over. Score: " + score + ".");
}
finally
{
	Console.CursorVisible = true;
}

void Clear()
{
	Console.SetCursorPosition(0, 0);
	Console.WriteLine();
	Console.WriteLine("    Simon");
	Console.WriteLine();
	int left = Console.CursorLeft;
	int top = Console.CursorTop;
	Render(Renders[default]);
	Console.SetCursorPosition(left, top);
}

void AnimateCurrentPattern()
{
	for (int i = 0; i < pattern.Count; i++)
	{
		Clear();
		Render(Renders[(int)pattern[i]]);
		Thread.Sleep(buttonPress);
		Clear();
		Render(Renders[default]);
		Thread.Sleep(animationDelay);
	}
	Clear();
	Render(Renders[default]);
}

void Render(string @string)
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

enum Direction
{
	Up = 1,
	Right = 2,
	Down = 3,
	Left = 4,
}
