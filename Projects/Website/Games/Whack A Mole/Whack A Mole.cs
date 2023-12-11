using System;
using System.Threading.Tasks;

namespace Website.Games.Whack_A_Mole;

public class Whack_A_Mole
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		bool escape = false;

		string Board =
			@" ╔═══╦═══════╗ ╔═══╦═══════╗ ╔═══╦═══════╗" + '\n' +
			@" ║ 7 ║       ║ ║ 8 ║       ║ ║ 9 ║       ║" + '\n' +
			@" ╚═══╣       ║ ╚═══╣       ║ ╚═══╣       ║" + '\n' +
			@"     ║       ║     ║       ║     ║       ║" + '\n' +
			@"     ║       ║     ║       ║     ║       ║" + '\n' +
			@"     ╚═══════╝     ╚═══════╝     ╚═══════╝" + '\n' +
			@" ╔═══╦═══════╗ ╔═══╦═══════╗ ╔═══╦═══════╗" + '\n' +
			@" ║ 4 ║       ║ ║ 5 ║       ║ ║ 6 ║       ║" + '\n' +
			@" ╚═══╣       ║ ╚═══╣       ║ ╚═══╣       ║" + '\n' +
			@"     ║       ║     ║       ║     ║       ║" + '\n' +
			@"     ║       ║     ║       ║     ║       ║" + '\n' +
			@"     ╚═══════╝     ╚═══════╝     ╚═══════╝" + '\n' +
			@" ╔═══╦═══════╗ ╔═══╦═══════╗ ╔═══╦═══════╗" + '\n' +
			@" ║ 1 ║       ║ ║ 2 ║       ║ ║ 3 ║       ║" + '\n' +
			@" ╚═══╣       ║ ╚═══╣       ║ ╚═══╣       ║" + '\n' +
			@"     ║       ║     ║       ║     ║       ║" + '\n' +
			@"     ║       ║     ║       ║     ║       ║" + '\n' +
			@"     ╚═══════╝     ╚═══════╝     ╚═══════╝";

		string JavaNoob =
			@" ╔══─┐ " + '\n' +
			@" │o-o│ " + '\n' +
			@"┌└───┘┐" + '\n' +
			@"││ J ││";

		string Empty =
			@"       " + '\n' +
			@"       " + '\n' +
			@"       " + '\n' +
			@"       ";

		TimeSpan playTime = TimeSpan.FromSeconds(30);

		if (OperatingSystem.IsWindows())
		{
			Console.WindowWidth = Math.Max(Console.WindowWidth, 50);
			Console.WindowHeight = Math.Max(Console.WindowHeight, 22);
		}

		while (!escape)
		{
			await Console.Clear();
			await Console.WriteLine("Whack A Mole (Java Noob Edition)");
			await Console.WriteLine();
			await Console.WriteLine(
				$"You have {(int)playTime.TotalSeconds} seconds to whack as many Java noobs as you " +
				"can before the timer runs out. Use the number keys 1-9 to whack. Are you ready? ");
			await Console.WriteLine();
			await Console.WriteLine("Play [enter], or quit [escape]?");
		GetInput:
			switch ((await Console.ReadKey(true)).Key)
			{
				case ConsoleKey.Enter:  await Play();  break;
				case ConsoleKey.Escape: escape = true; break;
				default: goto GetInput;
			}
		}
		await Console.Clear();
		await Console.WriteLine("Whack A Mole was closed...");
		await Console.Refresh();

		async Task Play()
		{
			await Console.Clear();
			await Console.WriteLine("Whack A Mole (Java Noob Edition)");
			await Console.WriteLine();
			await Console.WriteLine(Board);
			DateTime start = DateTime.Now;
			int score = 0;
			int moleLocation = Random.Shared.Next(1, 10);
			Console.CursorVisible = false;
			while (DateTime.Now - start < playTime)
			{
				var (left, top) = Map(moleLocation);
				await Console.SetCursorPosition(left, top);
				await Render(JavaNoob);
				int selection;
			GetInput:
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.D1 or ConsoleKey.NumPad1: selection = 1; break;
					case ConsoleKey.D2 or ConsoleKey.NumPad2: selection = 2; break;
					case ConsoleKey.D3 or ConsoleKey.NumPad3: selection = 3; break;
					case ConsoleKey.D4 or ConsoleKey.NumPad4: selection = 4; break;
					case ConsoleKey.D5 or ConsoleKey.NumPad5: selection = 5; break;
					case ConsoleKey.D6 or ConsoleKey.NumPad6: selection = 6; break;
					case ConsoleKey.D7 or ConsoleKey.NumPad7: selection = 7; break;
					case ConsoleKey.D8 or ConsoleKey.NumPad8: selection = 8; break;
					case ConsoleKey.D9 or ConsoleKey.NumPad9: selection = 9; break;
					case ConsoleKey.Escape:
						await Console.Clear();
						await Console.WriteLine("Whack A Mole was closed...");
						await Console.Refresh();
						escape = true;
						return;
					default: goto GetInput;
				}
				if (moleLocation == selection)
				{
					score++;
					await Console.SetCursorPosition(left, top);
					await Render(Empty);
					int newMoleLocation = Random.Shared.Next(1, 9);
					moleLocation = newMoleLocation >= moleLocation ? newMoleLocation + 1 : newMoleLocation;
				}
			}
			Console.CursorVisible = true;
			await Console.Clear();
			await Console.WriteLine("Whack A Mole (Java Noob Edition)");
			await Console.WriteLine();
			await Console.WriteLine(Board);
			await Console.WriteLine();
			await Console.WriteLine("Game Over. Score: " + score);
			await Console.WriteLine("Hopefully those Java noobs will learn their lesson and start using C#.");
			await Console.WriteLine();
			await Console.WriteLine("Press [Enter] To Continue...");
			await Console.ReadLine();
		}

		(int Left, int Top) Map(int hole) =>
			hole switch
			{
				1 => (06, 15),
				2 => (20, 15),
				3 => (34, 15),
				4 => (06, 09),
				5 => (20, 09),
				6 => (34, 09),
				7 => (06, 03),
				8 => (20, 03),
				9 => (34, 03),
				_ => throw new NotImplementedException(),
			};

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
}
