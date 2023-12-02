using System;
using static Website.Games.Console_Monsters.Statics;
//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Utilities;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters.Screens;

public static class BattleTransition
{
	private static readonly TimeSpan Delay = TimeSpan.FromMilliseconds(1);

	private static readonly Func<Task>[] Transitions = new Func<Task>[]
		{
			LeftToRight,
			RightToLeft,
			LeftToRightBlocks,
			RightToLeftBlocks,
			Swirl,
		};

	public static async Task Random()
	{
		Func<Task> transition = Transitions[BattleTransitionRandom.Next(Transitions.Length)];
		await transition?.Invoke()!;
	}

	public static async Task LeftToRight()
	{
		var (width, height) = await ConsoleHelpers.GetWidthAndHeight();
		height = Statics.OperatingSystem.IsWindows() ? height - 1 : height;
		try
		{
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					await Statics.Console.SetCursorPosition(i, j);
					await Statics.Console.Write('█');
				}
				await Statics.Console.RefreshAndDelay(Delay);
			}
			await Statics.Console.RefreshAndDelay(Delay);
		}
		catch
		{
			// intentionally blank. the user likely resized the console window
		}
	}

	public static async Task RightToLeft()
	{
		var (width, height) = await ConsoleHelpers.GetWidthAndHeight();
		height = Statics.OperatingSystem.IsWindows() ? height - 1 : height;
		try
		{
			for (int i = width - 1; i >= 0; i--)
			{
				for (int j = 0; j < height - 1; j++)
				{
					await Statics.Console.SetCursorPosition(i, j);
					await Statics.Console.Write('█');
				}
				await Statics.Console.RefreshAndDelay(Delay);
			}
			await Statics.Console.RefreshAndDelay(Delay);
		}
		catch
		{
			// intentionally blank. the user likely resized the console window
		}
	}

	public static async Task LeftToRightBlocks()
	{
		const int BlockWidth = Sprites.Width;
		const int BlockHeight = Sprites.Height;
		var (width, height) = await ConsoleHelpers.GetWidthAndHeight();
		height = Statics.OperatingSystem.IsWindows() ? height - 1 : height;
		try
		{
			for (int j = 0; j < height; j += BlockHeight)
			{
				for (int i = 0; i < width; i += BlockWidth)
				{
					for (int blockI = 0; blockI < BlockWidth && i + blockI < width; blockI++)
					{
						for (int blockJ = 0; blockJ < BlockHeight && j + blockJ < height; blockJ++)
						{
							await Statics.Console.SetCursorPosition(i + blockI, j + blockJ);
							await Statics.Console.Write('█');
						}
					}
					await Statics.Console.RefreshAndDelay(Delay);
				}
				
			}
			await Statics.Console.RefreshAndDelay(Delay);
		}
		catch
		{
			// intentionally blank. the user likely resized the console window
		}
	}

	public static async Task RightToLeftBlocks()
	{
		const int BlockWidth = Sprites.Width;
		const int BlockHeight = Sprites.Height;
		var (width, height) = await ConsoleHelpers.GetWidthAndHeight();
		height = Statics.OperatingSystem.IsWindows() ? height - 1 : height;
		try
		{
			for (int j = 0; j < height; j += BlockHeight)
			{
				for (int i = width - 1; i >= 0; i -= BlockWidth)
				{
					for (int blockI = 0; blockI < BlockWidth && i - blockI >= 0; blockI++)
					{
						for (int blockJ = 0; blockJ < BlockHeight && j + blockJ < height; blockJ++)
						{
							await Statics.Console.SetCursorPosition(i - blockI, j + blockJ);
							await Statics.Console.Write('█');
						}
					}
					await Statics.Console.RefreshAndDelay(Delay);
				}
			}
			await Statics.Console.RefreshAndDelay(Delay);
		}
		catch
		{
			// intentionally blank. the user likely resized the console window
		}
	}

	public static async Task Swirl()
	{
		var (width, height) = await ConsoleHelpers.GetWidthAndHeight();
		height = Statics.OperatingSystem.IsWindows() ? height - 1 : height;
		try
		{
			int high = 0;
			int low = height - 1;
			int left = 0;
			int right = width - 1;
			while (high < low && left < right)
			{
				for (int i = left; i <= right; i++)
				{
					await Statics.Console.SetCursorPosition(i, high);
					await Statics.Console.Write('█');
				}
				high++;
				await Statics.Console.RefreshAndDelay(Delay);
				for (int j = high; j <= low; j++)
				{
					await Statics.Console.SetCursorPosition(right, j);
					await Statics.Console.Write('█');
				}
				right--;
				await Statics.Console.RefreshAndDelay(Delay);
				for (int i = right; i >= left; i--)
				{
					await Statics.Console.SetCursorPosition(i, low);
					await Statics.Console.Write('█');
				}
				low--;
				await Statics.Console.RefreshAndDelay(Delay);
				for (int j = low; j >= high; j--)
				{
					await Statics.Console.SetCursorPosition(left, j);
					await Statics.Console.Write('█');
				}
				left++;
				await Statics.Console.RefreshAndDelay(Delay);
			}
			await Statics.Console.RefreshAndDelay(Delay);
		}
		catch
		{
			// intentionally blank. the user likely resized the console window
		}
	}
}
