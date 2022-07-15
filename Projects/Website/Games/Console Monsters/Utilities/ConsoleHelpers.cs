using System;
using System.Linq;
using System.Text;
using System.Threading;
using static Website.Games.Console_Monsters.Statics;
//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Items;
using Website.Games.Console_Monsters.Maps;
using Website.Games.Console_Monsters.Monsters;
using Website.Games.Console_Monsters.Bases;
using Website.Games.Console_Monsters.Characters;
using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Screens.Menus;
using Website.Games.Console_Monsters.Enums;
using Website.Games.Console_Monsters.Utilities;
using System.Collections.Generic;
using Towel;
using static Towel.Statics;
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
