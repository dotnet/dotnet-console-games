namespace Console_Monsters.Utilities;

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
					if (Console.BufferWidth  != width)  Console.BufferWidth  = width;
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
		var (width, height) = GetWidthAndHeight();
		if ((previousWidth, previousHeight) != (width, height))
		{
			(previousWidth, previousHeight) = (width, height);
			Console.Clear();
			return true;
		}
		return false;
	}

	public static string ToDisplayString(this ConsoleKey key) => key switch
		{
			ConsoleKey.UpArrow    => "↑",
			ConsoleKey.DownArrow  => "↓",
			ConsoleKey.LeftArrow  => "←",
			ConsoleKey.RightArrow => "→",
			>= ConsoleKey.D0 and <= ConsoleKey.D9 => ('0' + (key - ConsoleKey.D0)).ToString(),
			>= ConsoleKey.NumPad0 and <= ConsoleKey.NumPad9 => ('0' + (key - ConsoleKey.NumPad0)).ToString(),
			_ => key.ToString(),
		};

	public static string ToDisplayString(this (ConsoleKey Main, ConsoleKey? Alternate) input) =>
		input.Alternate is null
			? $"{input.Main.ToDisplayString()}"
			: $"{input.Main.ToDisplayString()} or {input.Alternate.Value.ToDisplayString()}";
}
