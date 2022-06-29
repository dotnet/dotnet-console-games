using System.Threading;

namespace Console_Monsters;

public static class Renderer
{
	public static StringBuilder LastMapRender = new StringBuilder();

	public static void RenderWorldMapView()
	{
		Console.CursorVisible = false;

		var (width, height) = GetWidthAndHeight();
		int heightCutOff = height - maptext.Length - 3;
		int midWidth = width / 2;
		int midHeight = heightCutOff / 2;

		StringBuilder sb = new(width * height);
		for (int j = 0; j < height; j++)
		{
			if (OperatingSystem.IsWindows() && j == height - 1)
			{
				break;
			}

			for (int i = 0; i < width; i++)
			{
				// console area (below map)
				if (j >= heightCutOff)
				{
					int line = j - heightCutOff - 1;
					int character = i - 1;
					if (i < width - 1 && character >= 0 && line >= 0 && line < maptext.Length && character < maptext[line].Length)
					{
						char ch = maptext[line][character];
						sb.Append(char.IsWhiteSpace(ch) ? ' ' : ch);
					}
					else
					{
						sb.Append(' ');
					}
					continue;
				}

				// map outline
				if (i is 0 && j is 0)
				{
					sb.Append('╔');
					continue;
				}
				if (i is 0 && j == heightCutOff - 1)
				{
					sb.Append('╚');
					continue;
				}
				if (i == width - 1 && j is 0)
				{
					sb.Append('╗');
					continue;
				}
				if (i == width - 1 && j == heightCutOff - 1)
				{
					sb.Append('╝');
					continue;
				}
				if (i is 0 || i == width - 1)
				{
					sb.Append('║');
					continue;
				}
				if (j is 0 || j == heightCutOff - 1)
				{
					sb.Append('═');
					continue;
				}

				// character
				if (i > midWidth - 4 && i < midWidth + 4 && j > midHeight - 3 && j < midHeight + 3)
				{
					int ci = i - (midWidth - 3);
					int cj = j - (midHeight - 2);
					string characterMapRender = character.Render;
					sb.Append(characterMapRender[cj * (Sprites.Width + 1) + ci]);
					continue;
				}

				// tiles

				// compute the map location that this screen pixel represents
				int mapI = i - midWidth + character.I + 3;
				int mapJ = j - midHeight + character.J + 2;

				// compute the coordinates of the tile
				int tileI = mapI < 0 ? (mapI - (Sprites.Width - 1)) / Sprites.Width : mapI / Sprites.Width;
				int tileJ = mapJ < 0 ? (mapJ - (Sprites.Height - 1)) / Sprites.Height : mapJ / Sprites.Height;

				// compute the coordinates of the pixel within the tile's sprite
				int pixelI = mapI < 0 ? (Sprites.Width - 1) + ((mapI + 1) % Sprites.Width) : (mapI % Sprites.Width);
				int pixelJ = mapJ < 0 ? (Sprites.Height - 1) + ((mapJ + 1) % Sprites.Height) : (mapJ % Sprites.Height);

				// render pixel from map tile
				string tileRender = map.GetMapTileRender(tileI, tileJ);
				char c = tileRender[pixelJ * (Sprites.Width + 1) + pixelI];
				sb.Append(char.IsWhiteSpace(c) ? ' ' : c);
			}
			if (!OperatingSystem.IsWindows() && j < height - 1)
			{
				sb.AppendLine();
			}
		}
		Console.SetCursorPosition(0, 0);
		Console.Write(sb);
		SleepAfterRender();
	}

	static (int Width, int Height) GetWidthAndHeight()
	{
	RestartRender:
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
				goto RestartRender;
			}
		}
		return (width, height);
	}

	static void SleepAfterRender()
	{
		// frame rate control targeting 30 frames per second
		DateTime now = DateTime.Now;
		TimeSpan sleep = TimeSpan.FromMilliseconds(33) - (now - previoiusRender);
		if (sleep > TimeSpan.Zero)
		{
			Thread.Sleep(sleep);
		}
		previoiusRender = DateTime.Now;
	}

	public static void RenderBattleTransition()
	{
		int width = Console.WindowWidth;
		int height = Console.WindowHeight;

		Console.ForegroundColor = ConsoleColor.White;

		LastMapRender.AppendLine();

		Random rnd = new();

		switch (rnd.Next(1, 1))
		{
			case 1:
				Console.WriteLine(LastMapRender);
				LeftRightBlockTransition(height, width);
				break;
			case 2:

				break;
		}
	}

	public static void LeftRightBlockTransition(int height, int width)
	{
		const string Block =
			@"███████" + "\n" +
			@"███████" + "\n" +
			@"███████" + "\n" +
			@"███████" + "\n" +
			@"███████";

		for (int j = 0; j < height; j += 5)
		{
			for (int i = 0; i < width; i += 7)
			{
				int h = 0;
				Console.SetCursorPosition(i, j);
				for (int k = 0; k < Block.Length; k++)
				{
					try
					{
						if (Block[k] == '\n')
						{
							h++;
							Console.SetCursorPosition(i, j + h);
						}
						else
						{
							Console.Write(Block[k]);
						}
					}
					catch { }
				}
				//Thread.Sleep(1);
			}
			if (j < height - 1)
			{
				Console.WriteLine();
			}
		}
	}

	public static void RenderBattleView()
	{
		int spriteheight = Sprites.BattleSpriteHeight + 1;

		Console.CursorVisible = false;
		Console.BackgroundColor = ConsoleColor.Black;
		Console.ForegroundColor = ConsoleColor.Gray;

		var (width, height) = GetWidthAndHeight();
		int heightCutOff = height - maptext.Length - 3;
		int midWidth = width / 2;
		int midHeight = heightCutOff / 2;

		StringBuilder sb = new(width * height);
		for (int j = 0; j < height; j++)
		{
			if (OperatingSystem.IsWindows() && j == height - 1)
			{
				break;
			}

			for (int i = 0; i < width; i++)
			{
				// console area (below map)
				if (j >= heightCutOff)
				{
					int line = j - heightCutOff - 1;
					int character = i - 1;
					if (i < width - 1 && character >= 0 && line >= 0 && line < maptext.Length && character < maptext[line].Length)
					{
						char ch = maptext[line][character];
						sb.Append(char.IsWhiteSpace(ch) ? ' ' : ch);
					}
					else
					{
						sb.Append(' ');
					}
					continue;
				}

				// map outline
				if (i == midWidth - Sprites.BattleSpriteWidth && j == midHeight - spriteheight)
				{
					sb.Append('╔');
					continue;
				}
				if (i == midWidth - Sprites.BattleSpriteWidth && j == midHeight + spriteheight)
				{
					sb.Append('╚');
					continue;
				}
				if (i == midWidth + Sprites.BattleSpriteWidth && j == midHeight - spriteheight)
				{
					sb.Append('╗');
					continue;
				}
				if (i == midWidth + Sprites.BattleSpriteWidth && j == midHeight + spriteheight)
				{
					sb.Append('╝');
					continue;
				}
				if ((i == midWidth - Sprites.BattleSpriteWidth || i == midWidth + Sprites.BattleSpriteWidth) && (j > midHeight - spriteheight && j < midHeight + spriteheight))
				{
					sb.Append('║');
					continue;
				}
				if ((j == midHeight - spriteheight || j == midHeight + spriteheight) && (i > midWidth - Sprites.BattleSpriteWidth && i < midWidth + Sprites.BattleSpriteWidth))
				{
					sb.Append('═');
					continue;
				}

				MonsterBase monsterA = 


				if (i > midWidth - (Sprites.BattleSpriteWidth / 4) * 3 - 3 &&
					i < midWidth + (Sprites.BattleSpriteWidth / 4) * 1 &&
					j < midHeight + spriteheight &&
					j > midHeight)
				{
					sb.Append(monsterA.Sprite[j - midHeight][i - (midWidth - (Sprites.BattleSpriteWidth / 4) * 3 - 3)]);
					continue;
				}

				sb.Append(' ');
			}
			if (!OperatingSystem.IsWindows() && j < height - 1)
			{
				sb.AppendLine();
			}
		}
		Console.SetCursorPosition(0, 0);
		Console.Write(sb);
		SleepAfterRender();
	}


}
