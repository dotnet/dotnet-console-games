using System;
//using Website.Games.Console_Monsters.Screens;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters.Utilities;

public static class ConsoleHelpers
{
	public static async Task<(int Width, int Height)> GetWidthAndHeight()
	{
		while (true)
		{
			int width = Statics.Console.WindowWidth;
			int height = Statics.Console.WindowHeight;
			if (Statics.OperatingSystem.IsWindows())
			{
				try
				{
					if (Statics.Console.BufferHeight != height) Statics.Console.BufferHeight = height;
					if (Statics.Console.BufferWidth != width) Statics.Console.BufferWidth = width;
				}
				catch (Exception)
				{
					await Statics.Console.Clear();
					continue;
				}
			}
			return (width, height);
		}
	}
}
