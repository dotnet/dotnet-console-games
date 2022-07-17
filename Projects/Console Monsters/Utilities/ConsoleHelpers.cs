﻿namespace Console_Monsters.Utilities;

public static class ConsoleHelpers
{
	public static (int Width, int Height) GetWidthAndHeight()
	{
		while (true)
		{
			int width = Console.WindowWidth;
			int height = Console.WindowHeight;
			if (OperatingSystem.IsWindows())
			{
				try
				{
					if (Console.BufferHeight != height) Console.BufferHeight = height;
					if (Console.BufferWidth != width) Console.BufferWidth = width;
				}
				catch (Exception)
				{
					try
					{
						Console.Clear();
					}
					catch
					{
						// intentionally left blank
					}
					continue;
				}
			}
			return (width, height);
		}
	}

	public static bool ClearIfConsoleResized(ref int previousWidth, ref int previousHeight)
	{
		var (width, height) = ConsoleHelpers.GetWidthAndHeight();
		if ((previousWidth, previousHeight) != (width, height))
		{
			(previousWidth, previousHeight) = (width, height);
			Console.Clear();
			return true;
		}
		return false;
	}
}
