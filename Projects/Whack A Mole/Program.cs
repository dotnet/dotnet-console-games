using System;

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

while (true)
{
	Console.Clear();
	Console.WriteLine("Whack A Mole (Java Noob Edition)");
	Console.WriteLine();
	Console.WriteLine(
		$"You have {(int)playTime.TotalSeconds} seconds to whack as many Java noobs as you " +
		"can before the timer runs out. Use the number keys 1-9 to whack. Are you ready? ");
	Console.WriteLine();
	Console.WriteLine("Play [enter], or quit [escape]?");
GetInput:
	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.Enter:
			Play();
			break;
		case ConsoleKey.Escape:
			Console.Clear();
			Console.WriteLine("Whack A Mole was closed...");
			Environment.Exit(0);
			break;
		default: goto GetInput;
	}
}

void Play()
{
	Console.Clear();
	Console.WriteLine("Whack A Mole (Java Noob Edition)");
	Console.WriteLine();
	Console.WriteLine(Board);
	DateTime start = DateTime.Now;
	int score = 0;
	Random random = new();
	int moleLocation = random.Next(1, 10);
	Console.CursorVisible = false;
	while (DateTime.Now - start < playTime)
	{
		var (left, top) = Map(moleLocation);
		Console.SetCursorPosition(left, top);
		Render(JavaNoob);
		int selection;
	GetInput:
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.D1: case ConsoleKey.NumPad1: selection = 1; break;
			case ConsoleKey.D2: case ConsoleKey.NumPad2: selection = 2; break;
			case ConsoleKey.D3: case ConsoleKey.NumPad3: selection = 3; break;
			case ConsoleKey.D4: case ConsoleKey.NumPad4: selection = 4; break;
			case ConsoleKey.D5: case ConsoleKey.NumPad5: selection = 5; break;
			case ConsoleKey.D6: case ConsoleKey.NumPad6: selection = 6; break;
			case ConsoleKey.D7: case ConsoleKey.NumPad7: selection = 7; break;
			case ConsoleKey.D8: case ConsoleKey.NumPad8: selection = 8; break;
			case ConsoleKey.D9: case ConsoleKey.NumPad9: selection = 9; break;
			case ConsoleKey.Escape:
				Console.Clear();
				Console.WriteLine("Whack A Mole was closed...");
				Environment.Exit(0);
				return;
			default: goto GetInput;
		}
		if (moleLocation == selection)
		{
			score++;
			Console.SetCursorPosition(left, top);
			Render(Empty);
			int newMoleLocation = random.Next(1, 9);
			moleLocation = newMoleLocation >= moleLocation ? newMoleLocation + 1 : newMoleLocation;
		}
	}
	Console.CursorVisible = true;
	Console.Clear();
	Console.WriteLine("Whack A Mole (Java Noob Edition)");
	Console.WriteLine();
	Console.WriteLine(Board);
	Console.WriteLine();
	Console.WriteLine("Game Over. Score: " + score);
	Console.WriteLine("Hopefully those Java noobs will learn their lesson and start using C#.");
	Console.WriteLine();
	Console.WriteLine("Press [Enter] To Continue...");
	Console.ReadLine();
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
