using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Website.Games.Tower_Of_Hanoi;

public class Tower_Of_Hanoi
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
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
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine("  Tower Of Hanoi");
			await Console.WriteLine();
			await Console.WriteLine("  This is a puzzle game where you need to");
			await Console.WriteLine("  move all the disks in the left stack to");
			await Console.WriteLine("  the right stack. You can only move one");
			await Console.WriteLine("  disk at a time from one stack to another");
			await Console.WriteLine("  stack, and you may never place a disk on");
			await Console.WriteLine("  top of a smaller disk on the same stack.");
			await Console.WriteLine();
			await Console.WriteLine("  [enter] to continue");
			await Console.Write("  [escape] exit game");
		GetEnter:
			Console.CursorVisible = false;
			switch ((await Console.ReadKey(true)).Key)
			{
				case ConsoleKey.Enter: break;
				case ConsoleKey.Escape: return;
				default: goto GetEnter;
			}
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine("  Tower Of Hanoi");
			await Console.WriteLine();
			await Console.WriteLine("  The more disks, the harder the puzzle.");
			await Console.WriteLine();
			await Console.WriteLine("  Select the number of disks:");
			await Console.WriteLine("  [3] 3 disks");
			await Console.WriteLine("  [4] 4 disks");
			await Console.WriteLine("  [5] 5 disks");
			await Console.WriteLine("  [6] 6 disks");
			await Console.WriteLine("  [7] 7 disks");
			await Console.WriteLine("  [8] 8 disks");
			await Console.WriteLine("  [escape] exit game");
		GetDiskCount:
			Console.CursorVisible = false;
			switch ((await Console.ReadKey(true)).Key)
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
			await Console.Clear();
			while (stacks[2].Count != disks)
			{
				await Render();
				switch ((await Console.ReadKey(true)).Key)
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
			await Render();
		GetEnterOrEscape:
			Console.CursorVisible = false;
			switch ((await Console.ReadKey(true)).Key)
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
			await Console.Clear();
			await Console.WriteLine(exception?.ToString() ?? "Tower Of Hanoi was closed.");
			await Console.Refresh();
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

		async Task Render()
		{
			Console.CursorVisible = false;
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine("  Tower Of Hanoi");
			await Console.WriteLine();
			await Console.WriteLine($"  Minimum Moves: {minimumNumberOfMoves}");
			await Console.WriteLine();
			await Console.WriteLine($"  Moves: {moves}");
			await Console.WriteLine();
			for (int i = disks - 1; i >= 0; i--)
			{
				for (int j = 0; j < stacks.Length; j++)
				{
					await Console.Write("  ");
					await RenderDisk(stacks[j].Count > i ? stacks[j][i] : null);
				}
				await Console.WriteLine();
			}
			string towerBase = new string('─', disks) + '┴' + new string('─', disks);
			await Console.WriteLine($"  {towerBase}  {towerBase}  {towerBase}");
			await Console.WriteLine($"  {RenderBelowBase(0)}  {RenderBelowBase(1)}  {RenderBelowBase(2)}");
			await Console.WriteLine();
			switch (state)
			{
				case State.ChooseSource:
					await Console.WriteLine("  [1], [2], or [3] select source stack");
					await Console.WriteLine("  [home] restart current puzzle");
					await Console.WriteLine("  [end] back to menu");
					await Console.Write("  [escape] exit game");
					break;
				case State.InvalidTarget:
					await Console.WriteLine("  You may not place a disk on top of a");
					await Console.WriteLine("  smaller disk on the same stack.");
					await Console.WriteLine();
					goto ChooseTarget;
				case State.ChooseTarget:
				ChooseTarget:
					await Console.WriteLine("  [1], [2], or [3] select target stack");
					await Console.WriteLine("  [home] restart current puzzle");
					await Console.WriteLine("  [end] back to menu");
					await Console.Write("  [escape] exit game");
					break;
				case State.Win:
					await Console.WriteLine("  You solved the puzzle!");
					await Console.WriteLine("  [enter] return to menu");
					await Console.Write("  [escape] exit game");
					break;
			}
		}

		string RenderBelowBase(int stack) =>
			stack == source
				? new string('^', disks - 1) + $"[{(stack + 1).ToString(CultureInfo.InvariantCulture)}]" + new string('^', disks - 1)
				: new string(' ', disks - 1) + $"[{(stack + 1).ToString(CultureInfo.InvariantCulture)}]" + new string(' ', disks - 1);

		async Task RenderDisk(int? disk)
		{
			if (disk is null)
			{
				await Console.Write(new string(' ', disks) + '│' + new string(' ', disks));
			}
			else
			{
				await Console.Write(new string(' ', disks - disk.Value));
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
				await Console.Write(new string(' ', disk.Value));
				await Console.Write('│');
				await Console.Write(new string(' ', disk.Value));
				Console.BackgroundColor = ConsoleColor.Black;
				await Console.Write(new string(' ', disks - disk.Value));
			}
		}
	}

	enum State
	{
		ChooseSource,
		ChooseTarget,
		InvalidTarget,
		Win,
	}
}
