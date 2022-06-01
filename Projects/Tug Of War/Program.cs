using System;
using System.Threading;

Restart:
int position = 0;
int displacement = 10;
bool frame_a = false;
Console.Clear();
Console.WriteLine(@"
  Tug Of War

  Out pull your opponent in a rope pulling
  competition. Mash the left and right arrow
  keys to pull on the rope. First player to
  pull the center into their boundary wins.

  Choose Your Opponent:
  [1] Easy.......1 mashes per second
  [2] Medium.....2 mashes per second
  [3] Hard.......4 mashes per second
  [4] Harder.....8 mashes per second
  [escape] give up");
int? requiredMash = null;
while (requiredMash is null)
{
	Console.CursorVisible = false;
	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.D1 or ConsoleKey.NumPad1: requiredMash = 1; break;
		case ConsoleKey.D2 or ConsoleKey.NumPad2: requiredMash = 2; break;
		case ConsoleKey.D3 or ConsoleKey.NumPad3: requiredMash = 4; break;
		case ConsoleKey.D4 or ConsoleKey.NumPad4: requiredMash = 8; break;
		case ConsoleKey.Escape: return;
	}
}
Console.Clear();
int mash = 0;
int sleeps = 0;
ConsoleKey lastKey = default;
while (true)
{
	while (Console.KeyAvailable)
	{
		ConsoleKey key = Console.ReadKey(true).Key;
		switch (key, lastKey)
		{
			case (ConsoleKey.Escape, _): return;
			case (ConsoleKey.LeftArrow, ConsoleKey.RightArrow) or
				 (ConsoleKey.RightArrow, ConsoleKey.LeftArrow):
				mash++; lastKey = default; break;
			default: lastKey = key; break;
		}
	}
	if (sleeps is 2)
	{
		position = mash < requiredMash.Value
			? position + 1
			: position - 1;
		sleeps = 0;
		mash = 0;
	}
	Console.CursorVisible = false;
	Console.SetCursorPosition(0, 0);
	Console.WriteLine();
	Console.WriteLine("  Tug Of War");
	Console.WriteLine();
	string l = new(' ', displacement + position + 4);
	string r = new(' ', displacement - position + 4);
	Console.Write((frame_a = !frame_a)
		?
		$@"{l}o                             o {r}{"\n"}" +
		$@"{l}LL-------------+-------------JJ\{r}{"\n"}" +
		$@"{l}\\                            //{r}{"\n"}" +
		$@"{l}| \                          / |{r}{"\n"}"
		:
		$@"{l} o                             o{r}{"\n"}" +
		$@"{l}/LL-------------+-------------JJ{r}{"\n"}" +
		$@"{l}\\                            //{r}{"\n"}" +
		$@"{l}| \                          / |{r}{"\n"}");
	Console.WriteLine(
		new string(' ', 2) +
		new string('-', displacement + (15 - displacement) + 2) +
		new string('=', displacement * 2 + 2) +
		new string('-', displacement + (15 - displacement) + 2) +
		new string(' ', 2));
	if (Math.Abs(position) >= displacement)
	{
		break;
	}
	Thread.Sleep(500);
	sleeps++;
}
#warning TODO: win/loss frame
Console.WriteLine();
Console.WriteLine("  You " + (position < 0 ? "Win!" : "Lose!"));
Console.WriteLine("  [enter] return to menu");
Console.WriteLine("  [escape] exit game");
GetEnterOrEscape:
switch (Console.ReadKey(true).Key)
{
	case ConsoleKey.Enter: goto Restart;
	case ConsoleKey.Escape: return;
	default: goto GetEnterOrEscape;
}