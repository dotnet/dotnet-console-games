using System.Threading;

namespace Animal_Trainer;

public static class Renderer
{
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
					sb.Append(characterMapRender[cj * 8 + ci]);
					continue;
				}

				// tiles

				// compute the map location that this screen pixel represents
				int mapI = i - midWidth + character.I + 3;
				int mapJ = j - midHeight + character.J + 2;

				// compute the coordinates of the tile
				int tileI = mapI < 0 ? (mapI - 6) / 7 : mapI / 7;
				int tileJ = mapJ < 0 ? (mapJ - 4) / 5 : mapJ / 5;

				// compute the coordinates of the pixel within the tile's sprite
				int pixelI = mapI < 0 ? 6 + ((mapI + 1) % 7) : (mapI % 7);
				int pixelJ = mapJ < 0 ? 4 + ((mapJ + 1) % 5) : (mapJ % 5);

				// render pixel from map tile
				string tileRender = Maps.GetMapTileRender(map, tileI, tileJ);
				char c = tileRender[pixelJ * 8 + pixelI];
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
			catch (ArgumentOutOfRangeException)
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
}
