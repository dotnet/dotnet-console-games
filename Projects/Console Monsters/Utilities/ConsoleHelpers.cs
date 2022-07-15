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
					if (Console.BufferWidth != width) Console.BufferWidth = width;
				}
				catch (Exception)
				{
					Console.Clear();
					continue;
				}
			}
			return (width, height);
		}
	}
}
