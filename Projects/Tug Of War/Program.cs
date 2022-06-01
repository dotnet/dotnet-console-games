using System;
using System.Threading;

try
{
	while (true)
	{
		int position = 0;
		const int displacement = 10;
		string L() => new(' ', displacement + position + 4);
		string R() => new(' ', displacement - position + 4);
		string Ground =
			new string(' ', 2) +
			new string('-', displacement + (15 - displacement) + 2) +
			new string('=', displacement * 2 + 2) +
			new string('-', displacement + (15 - displacement) + 2) +
			new string(' ', 2);
		bool frame_a = false;
		Console.Clear();
		Console.WriteLine(@"
  Tug Of War

  Out pull your opponent in a rope pulling
  competition. Mash the [left]+[right] arrow
  keys and/or the [A]+[D] keys to pull on the
  rope. First player to pull the center of the
  rope into their boundary wins.

  Choose Your Opponent:
  [1] Easy.......2 mashes per second
  [2] Medium.....4 mashes per second
  [3] Hard.......8 mashes per second
  [4] Harder....16 mashes per second
  [escape] give up");
		int? requiredMash = null;
		while (requiredMash is null)
		{
			Console.CursorVisible = false;
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.D1 or ConsoleKey.NumPad1: requiredMash = 02; break;
				case ConsoleKey.D2 or ConsoleKey.NumPad2: requiredMash = 04; break;
				case ConsoleKey.D3 or ConsoleKey.NumPad3: requiredMash = 08; break;
				case ConsoleKey.D4 or ConsoleKey.NumPad4: requiredMash = 16; break;
				case ConsoleKey.Escape: return;
			}
		}
		Console.Clear();
		int mash = 0;
		int presses = 0;
		int sleeps = 0;
		ConsoleKey lastKey = default;
		DateTime start = DateTime.Now;
		while (true)
		{
			while (Console.KeyAvailable)
			{
				ConsoleKey key = Console.ReadKey(true).Key;
				if (key is ConsoleKey.Escape)
				{
					return;
				}
				else if (lastKey is not default(ConsoleKey) &&
					key is ConsoleKey.A or ConsoleKey.D or ConsoleKey.LeftArrow or ConsoleKey.RightArrow &&
					key != lastKey)
				{
					presses++;
					mash++;
					lastKey = default;
				}
				else if (key is ConsoleKey.A or ConsoleKey.D or ConsoleKey.LeftArrow or ConsoleKey.RightArrow)
				{
					lastKey = key;
				}
			}
			if (sleeps is 2)
			{
				position = mash < requiredMash.Value
					? position + 1
					: position - 1;
				sleeps = 0;
				mash = 0;
				if (Math.Abs(position) >= displacement)
				{
					break;
				}
			}
			Console.CursorVisible = false;
			Console.SetCursorPosition(0, 0);
			Console.WriteLine();
			Console.WriteLine("  Tug Of War");
			Console.WriteLine();
			Console.Write(frame_a
				?
				$@"{L()}o                             o {R()}{"\n"}" +
				$@"{L()}LL-------------+-------------JJ\{R()}{"\n"}" +
				$@"{L()}\\                            //{R()}{"\n"}" +
				$@"{L()}| \                          / |{R()}{"\n"}"
				:
				$@"{L()} o                             o{R()}{"\n"}" +
				$@"{L()}/LL-------------+-------------JJ{R()}{"\n"}" +
				$@"{L()}\\                            //{R()}{"\n"}" +
				$@"{L()}| \                          / |{R()}{"\n"}");
			Console.WriteLine(Ground);
			Console.WriteLine();
			Console.WriteLine(frame_a
				? "           *** Mash [A]+[D] or [Left]+[Right] ***"
				: "           ''' Mash [A]+[D] or [Left]+[Right] '''");
			Thread.Sleep(500);
			sleeps++;
			frame_a = !frame_a;
		}
		bool win = position < 0;
		double seconds = (DateTime.Now - start).TotalSeconds;
		double average = presses / seconds;
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine("  Tug Of War");
		Console.WriteLine();
		Console.Write(win
			?
			$@"{L()}o                               {R()}{"\n"}" +
			$@"{L()}LL------------+------.  o___    {R()}{"\n"}" +
			$@"{L()}\\                    \//   \\__{R()}{"\n"}" +
			$@"{L()}| \                    \_____\  {R()}{"\n"}"
			:
			$@"{L()}                               o{R()}{"\n"}" +
			$@"{L()}    ___o  .------+------------JJ{R()}{"\n"}" +
			$@"{L()}__//   \\/                    //{R()}{"\n"}" +
			$@"{L()}  /_____/                    / |{R()}{"\n"}");
		Console.WriteLine(Ground);
		Console.WriteLine();
		Console.WriteLine("  You " + (win ? "Win!" : "Lose!"));
		Console.WriteLine($"  Average: {average:0.##} mashes per second");
		Console.WriteLine("  [enter] return to menu");
		Console.WriteLine("  [escape] exit game");
		bool enterPressed = false;
		while (!enterPressed)
		{
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.Enter: enterPressed = true; break;
				case ConsoleKey.Escape: return;
			}
		}
	}
}
finally
{
	Console.CursorVisible = true;
	Console.Clear();
	Console.Write("Tug Of War was closed.");
}