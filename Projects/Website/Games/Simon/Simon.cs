using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Website.Games.Simon;

public class Simon
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		TimeSpan buttonPress = TimeSpan.FromMilliseconds(500);
		TimeSpan animationDelay = TimeSpan.FromMilliseconds(200);
		int score = 0;
		List<Direction> pattern = new();

		string[] Renders =
		[
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
		];

		try
		{
			await Console.Clear();
			Console.CursorVisible = false;
			await Clear();
			await Render(Renders[default]);
			while (true)
			{
				await Console.RefreshAndDelay(buttonPress);
				pattern.Add((Direction)Random.Shared.Next(1, 5));
				await AnimateCurrentPattern();
				for (int i = 0; i < pattern.Count; i++)
				{
				GetInput:
					switch ((await Console.ReadKey(true)).Key)
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
							await Console.Clear();
							await Console.Write("Simon was closed.");
							await Console.Refresh();
							return;
						default: goto GetInput;
					}
					score++;
					await Clear();
					await Render(Renders[(int)pattern[i]]);
					await Console.RefreshAndDelay(buttonPress);
					await Clear();
					await Render(Renders[default]);
				}
			}
		GameOver:
			await Console.Clear();
			await Console.Write("Game Over. Score: " + score + ".");
			await Console.Refresh();
		}
		finally
		{
			Console.CursorVisible = true;
			await Console.Refresh();
		}

		async Task Clear()
		{
			await Console.SetCursorPosition(0, 0);
			await Console.WriteLine();
			await Console.WriteLine("    Simon");
			await Console.WriteLine();
			int left = Console.CursorLeft;
			int top = Console.CursorTop;
			await Render(Renders[default]);
			await Console.SetCursorPosition(left, top);
		}

		async Task AnimateCurrentPattern()
		{
			for (int i = 0; i < pattern.Count; i++)
			{
				await Clear();
				await Render(Renders[(int)pattern[i]]);
				await Console.RefreshAndDelay(buttonPress);
				await Clear();
				await Render(Renders[default]);
				await Console.RefreshAndDelay(animationDelay);
			}
			await Clear();
			await Render(Renders[default]);
		}

		async Task Render(string @string)
		{
			int x = Console.CursorLeft;
			int y = Console.CursorTop;
			foreach (char c in @string)
			{
				if (c is '\n')
				{
					await Console.SetCursorPosition(x, ++y);
				}
				else
				{
					await Console.Write(c);
				}
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
}
