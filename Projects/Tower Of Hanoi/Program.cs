using System;
using System.Collections.Generic;
using System.Globalization;

Exception? exception = null;

int disks;
int minimumNumberOfMoves;
List<int>[] stacks;
int moves;
int? source;
State state;

try
{
	Console.BackgroundColor = ConsoleColor.Black;
	Console.ForegroundColor = ConsoleColor.White;
Menu:
	Console.CursorVisible = false;
	Console.Clear();
	Console.WriteLine();
	Console.WriteLine("  Tower Of Hanoi");
	Console.WriteLine();
	Console.WriteLine("  This is a puzzle game where you need to");
	Console.WriteLine("  move all the disks in the left stack to");
	Console.WriteLine("  the right stack. You can only move one");
	Console.WriteLine("  disk at a time from one stack to another");
	Console.WriteLine("  stack, and you may never place a disk on");
	Console.WriteLine("  top of a smaller disk on the same stack.");
	Console.WriteLine();
	Console.WriteLine("  [enter] to continue");
	Console.Write("  [escape] exit game");
GetEnter:
	Console.CursorVisible = false;
	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.Enter: break;
		case ConsoleKey.Escape: return;
		default: goto GetEnter;
	}
	Console.Clear();
	Console.WriteLine();
	Console.WriteLine("  Tower Of Hanoi");
	Console.WriteLine();
	Console.WriteLine("  The more disks, the harder the puzzle.");
	Console.WriteLine();
	Console.WriteLine("  Select the number of disks:");
	Console.WriteLine("  [3] 3 disks");
	Console.WriteLine("  [4] 4 disks");
	Console.WriteLine("  [5] 5 disks");
	Console.WriteLine("  [6] 6 disks");
	Console.WriteLine("  [7] 7 disks");
	Console.WriteLine("  [8] 8 disks");
	Console.WriteLine("  [escape] exit game");
GetDiskCount:
	Console.CursorVisible = false;
	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.D3 or ConsoleKey.NumPad3: disks = 3; break;
		case ConsoleKey.D4 or ConsoleKey.NumPad4: disks = 4; break;
		case ConsoleKey.D5 or ConsoleKey.NumPad5: disks = 5; break;
		case ConsoleKey.D6 or ConsoleKey.NumPad6: disks = 6; break;
		case ConsoleKey.D7 or ConsoleKey.NumPad3: disks = 7; break;
		case ConsoleKey.D8 or ConsoleKey.NumPad8: disks = 8; break;
		case ConsoleKey.Escape: return;
		default: goto GetDiskCount;
	}
Restart:
	state = State.ChooseSource;
	minimumNumberOfMoves = (int)Math.Pow(2, disks) - 1;
	stacks = new List<int>[] { new(), new(), new() };
	for (int i = disks; i > 0; i--)
	{
		stacks[0].Add(i);
	}
	moves = 0;
	source = null;
	Console.Clear();
	while (stacks[2].Count != disks)
	{
		Render();
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.Escape: return;
			case ConsoleKey.D1 or ConsoleKey.NumPad1: HandleStackButtonPress(0); break;
			case ConsoleKey.D2 or ConsoleKey.NumPad2: HandleStackButtonPress(1); break;
			case ConsoleKey.D3 or ConsoleKey.NumPad3: HandleStackButtonPress(2); break;
			case ConsoleKey.End: goto Menu;
			case ConsoleKey.Home: goto Restart;
		}
	}
	state = State.Win;
	Render();
GetEnterOrEscape:
	Console.CursorVisible = false;
	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.Enter: goto Menu;
		case ConsoleKey.Escape: return;
		default: goto GetEnterOrEscape;
	}
}
catch (Exception e)
{
	exception = e;
	throw;
}
finally
{
	Console.CursorVisible = true;
	Console.ResetColor();
	Console.Clear();
	Console.WriteLine(exception?.ToString() ?? "Tower Of Hanoi was closed.");
}

void HandleStackButtonPress(int stack)
{
	if (source is null && stacks[stack].Count > 0)
	{
		source = stack;
		state = State.ChooseTarget;
	}
	else if (source is not null &&
		(stacks[stack].Count is 0 || stacks[source.Value][^1] < stacks[stack][^1]))
	{
		stacks[stack].Add(stacks[source.Value][^1]);
		stacks[source.Value].RemoveAt(stacks[source.Value].Count - 1);
		source = null;
		moves++;
		state = State.ChooseSource;
	}
	else if (source == stack)
	{
		source = null;
		state = State.ChooseSource;
	}
	else if (stacks[stack].Count is not 0)
	{
		state = State.InvalidTarget;
	}
}

void Render()
{
	Console.CursorVisible = false;
	Console.Clear();
	Console.WriteLine();
	Console.WriteLine("  Tower Of Hanoi");
	Console.WriteLine();
	Console.WriteLine($"  Minimum Moves: {minimumNumberOfMoves}");
	Console.WriteLine();
	Console.WriteLine($"  Moves: {moves}");
	Console.WriteLine();
	for (int i = disks - 1; i >= 0; i--)
	{
		for (int j = 0; j < stacks.Length; j++)
		{
			Console.Write("  ");
			RenderDisk(stacks[j].Count > i ? stacks[j][i] : null);
		}
		Console.WriteLine();
	}
	string towerBase = new string('─', disks) + '┴' + new string('─', disks);
	Console.WriteLine($"  {towerBase}  {towerBase}  {towerBase}");
	Console.WriteLine($"  {RenderBelowBase(0)}  {RenderBelowBase(1)}  {RenderBelowBase(2)}");
	Console.WriteLine();
	switch (state)
	{
		case State.ChooseSource:
			Console.WriteLine("  [1], [2], or [3] select source stack");
			Console.WriteLine("  [home] restart current puzzle");
			Console.WriteLine("  [end] back to menu");
			Console.Write("  [escape] exit game");
			break;
		case State.InvalidTarget:
			Console.WriteLine("  You may not place a disk on top of a");
			Console.WriteLine("  smaller disk on the same stack.");
			Console.WriteLine();
			goto ChooseTarget;
		case State.ChooseTarget:
			ChooseTarget:
			Console.WriteLine("  [1], [2], or [3] select target stack");
			Console.WriteLine("  [home] restart current puzzle");
			Console.WriteLine("  [end] back to menu");
			Console.Write("  [escape] exit game");
			break;
		case State.Win:
			Console.WriteLine("  You solved the puzzle!");
			Console.WriteLine("  [enter] return to menu");
			Console.Write("  [escape] exit game");
			break;
	}
}

string RenderBelowBase(int stack) =>
	stack == source
		? new string('^', disks - 1) + $"[{(stack + 1).ToString(CultureInfo.InvariantCulture)}]" + new string('^', disks - 1)
		: new string(' ', disks - 1) + $"[{(stack + 1).ToString(CultureInfo.InvariantCulture)}]" + new string(' ', disks - 1);

void RenderDisk(int? disk)
{
	if (disk is null)
	{
		Console.Write(new string(' ', disks) + '│' + new string(' ', disks));
	}
	else
	{
		Console.Write(new string(' ', disks - disk.Value));
		Console.BackgroundColor = disk switch
		{
			1 => ConsoleColor.Red,
			2 => ConsoleColor.Green,
			3 => ConsoleColor.Blue,
			4 => ConsoleColor.Magenta,
			5 => ConsoleColor.Cyan,
			6 => ConsoleColor.DarkYellow,
			7 => ConsoleColor.White,
			8 => ConsoleColor.DarkGray,
			_ => throw new NotImplementedException()
		};
		Console.Write(new string(' ', disk.Value));
		Console.Write('│');
		Console.Write(new string(' ', disk.Value));
		Console.BackgroundColor = ConsoleColor.Black;
		Console.Write(new string(' ', disks - disk.Value));
	}
}

enum State
{
	ChooseSource,
	ChooseTarget,
	InvalidTarget,
	Win,
}