namespace Console_Monsters.Screens;

public static class BattleTransition
{
	private static TimeSpan Delay = TimeSpan.FromTicks(7500);

	private static Action[] Transitions = new[]
		{
			LeftToRight,
			RightToLeft,
			LeftToRightBlocks,
			RightToLeftBlocks,
			Swirl,
		};

	public static void Random()
	{
		Action transition = Transitions[BattleTransitionRandom.Next(Transitions.Length)];
		transition?.Invoke();
	}

	public static void LeftToRight()
	{
		var (width, height) = ConsoleHelpers.GetWidthAndHeight();
		height = OperatingSystem.IsWindows() ? height - 1 : height;
		try
		{
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					Console.SetCursorPosition(i, j);
					Console.Write('█');
				}
				Thread.Sleep(Delay);
			}
			Thread.Sleep(Delay);
		}
		catch
		{
			// intentionally blank. the user likely resized the console window
		}
	}

	public static void RightToLeft()
	{
		var (width, height) = ConsoleHelpers.GetWidthAndHeight();
		height = OperatingSystem.IsWindows() ? height - 1 : height;
		try
		{
			for (int i = width - 1; i >= 0; i--)
			{
				for (int j = 0; j < height - 1; j++)
				{
					Console.SetCursorPosition(i, j);
					Console.Write('█');
				}
				Thread.Sleep(Delay);
			}
			Thread.Sleep(Delay);
		}
		catch
		{
			// intentionally blank. the user likely resized the console window
		}
	}

	public static void LeftToRightBlocks()
	{
		const int BlockWidth = Sprites.Width;
		const int BlockHeight = Sprites.Height;
		var (width, height) = ConsoleHelpers.GetWidthAndHeight();
		height = OperatingSystem.IsWindows() ? height - 1 : height;
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
							Console.SetCursorPosition(i + blockI, j + blockJ);
							Console.Write('█');
						}
					}
					Thread.Sleep(Delay);
				}
				
			}
			Thread.Sleep(Delay);
		}
		catch
		{
			// intentionally blank. the user likely resized the console window
		}
	}

	public static void RightToLeftBlocks()
	{
		const int BlockWidth = Sprites.Width;
		const int BlockHeight = Sprites.Height;
		var (width, height) = ConsoleHelpers.GetWidthAndHeight();
		height = OperatingSystem.IsWindows() ? height - 1 : height;
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
							Console.SetCursorPosition(i - blockI, j + blockJ);
							Console.Write('█');
						}
					}
					Thread.Sleep(Delay);
				}
			}
			Thread.Sleep(Delay);
		}
		catch
		{
			// intentionally blank. the user likely resized the console window
		}
	}

	public static void Swirl()
	{
		var (width, height) = ConsoleHelpers.GetWidthAndHeight();
		height = OperatingSystem.IsWindows() ? height - 1 : height;
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
					Console.SetCursorPosition(i, high);
					Console.Write('█');
				}
				high++;
				Thread.Sleep(Delay);
				for (int j = high; j <= low; j++)
				{
					Console.SetCursorPosition(right, j);
					Console.Write('█');
				}
				right--;
				Thread.Sleep(Delay);
				for (int i = right; i >= left; i--)
				{
					Console.SetCursorPosition(i, low);
					Console.Write('█');
				}
				low--;
				Thread.Sleep(Delay);
				for (int j = low; j >= high; j--)
				{
					Console.SetCursorPosition(left, j);
					Console.Write('█');
				}
				left++;
				Thread.Sleep(Delay);
			}
			Thread.Sleep(Delay);
		}
		catch
		{
			// intentionally blank. the user likely resized the console window
		}
	}
}
