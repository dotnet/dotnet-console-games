using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
	enum Direction
	{
		Up    = 1,
		Right = 2,
		Down  = 3,
		Left  = 4,
	}

	static readonly Random random = new Random();
	static readonly TimeSpan buttonPress = TimeSpan.FromMilliseconds(500);
	static readonly TimeSpan animationDelay = TimeSpan.FromMilliseconds(200);

	static int score = 0;
	static readonly List<Direction> pattern = new List<Direction>();

	static readonly string[] simonRenders = new string[]
	{
		#region Renders

		// 0
		@"     _.-""""""-._"  + '\n' +
		@"   .'         `."   + '\n' +
		@"  /  '.     .'  \"  + '\n' +
		@" |     '. .'     |" + '\n' +
		@" |       X       |" + '\n' +
		@" |     .' '.     |" + '\n' +
		@"  \  .'     '.  /"  + '\n' +
		@"   `._       _.'"   + '\n' +
		@"      `-...-'",
		// 1
		@"     _.-""""""-._"  + '\n' +
		@"   .'█████████`."   + '\n' +
		@"  /  '███████'  \"  + '\n' +
		@" |     '███'     |" + '\n' +
		@" |       █       |" + '\n' +
		@" |     .' '.     |" + '\n' +
		@"  \  .'     '.  /"  + '\n' +
		@"   `._       _.'"   + '\n' +
		@"      `-...-'",
		// 2
		@"     _.-""""""-._"  + '\n' +
		@"   .'         `."   + '\n' +
		@"  /  '.     .'██\"  + '\n' +
		@" |     '. .'█████|" + '\n' +
		@" |       ████████|" + '\n' +
		@" |     .' '.█████|" + '\n' +
		@"  \  .'     '.██/"  + '\n' +
		@"   `._       _.'"   + '\n' +
		@"      `-...-'",
		// 3
		@"     _.-""""""-._"  + '\n' +
		@"   .'         `."   + '\n' +
		@"  /  '.     .'  \"  + '\n' +
		@" |     '. .'     |" + '\n' +
		@" |       █       |" + '\n' +
		@" |     .███.     |" + '\n' +
		@"  \  .███████.  /"  + '\n' +
		@"   `.█████████.'"   + '\n' +
		@"      `-...-'",
		// 4
		@"     _.-""""""-._"  + '\n' +
		@"   .'         `."   + '\n' +
		@"  /██'.     .'  \"  + '\n' +
		@" |█████'. .'     |" + '\n' +
		@" |████████       |" + '\n' +
		@" |█████.' '.     |" + '\n' +
		@"  \██.'     '.  /"  + '\n' +
		@"   `._       _.'"   + '\n' +
		@"      `-...-'",

		#endregion
	};

	static void Main()
	{
		Console.CursorVisible = false;
		Clear();
		Render(simonRenders[default]);
		while (true)
		{
			Thread.Sleep(buttonPress);
			pattern.Add((Direction)random.Next(1, 5));
			AnimateCurrentPattern();
			for (int i = 0; i < pattern.Count; i++)
			{
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
				}
				score++;
				Clear();
				Render(simonRenders[(int)pattern[i]]);
				Thread.Sleep(buttonPress);
				Clear();
				Render(simonRenders[default]);
			}
		}
	GameOver:
		Console.Clear();
		Console.Write("Game Over. Score: " + score + ".");
	}

	static void Clear()
	{
		Console.Clear();
		Console.WriteLine("Simon");
		Console.WriteLine();
	}

	static void AnimateCurrentPattern()
	{
		for (int i = 0; i < pattern.Count; i++)
		{
			Clear();
			Render(simonRenders[(int)pattern[i]]);
			Thread.Sleep(buttonPress);
			Clear();
			Render(simonRenders[default]);
			Thread.Sleep(animationDelay);
		}
		Clear();
		Render(simonRenders[default]);
	}

	static void Render(string @string, bool renderSpace = true)
	{
		int x = Console.CursorLeft;
		int y = Console.CursorTop;
		foreach (char c in @string)
			if (c is '\n') Console.SetCursorPosition(x, ++y);
			else if (!(c is ' ') || renderSpace) Console.Write(c);
			else Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
	}
}
