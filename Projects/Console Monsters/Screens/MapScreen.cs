namespace Console_Monsters.Screens;

internal static class MapScreen
{
	public static void Render()
	{
		Console.CursorVisible = false;

		var (width, height) = ConsoleHelpers.GetWidthAndHeight();
		int heightCutOff = height - MapText.Length - 3;
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
					if (i < width - 1 && character >= 0 && line >= 0 && line < MapText.Length && character < MapText[line].Length)
					{
						char ch = MapText[line][character];
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

				// message prompt if there is one
				if (PromptText is not null)
				{
					if (i is 10 && j == midHeight + 4)
					{
						sb.Append('╔');
						continue;
					}
					if (i is 10 && j == heightCutOff - 3)
					{
						sb.Append('╚');
						continue;
					}
					if (i == width - 11 && j == midHeight + 4)
					{
						sb.Append('╗');
						continue;
					}
					if (i == width - 11 && j == heightCutOff - 3)
					{
						sb.Append('╝');
						continue;
					}
					if ((i is 10 || i == width - 11) && j > midHeight + 4 && j < heightCutOff - 3)
					{
						sb.Append('║');
						continue;
					}
					if ((j == heightCutOff - 3 || j == midHeight + 4) && i > 10 && i < width - 11)
					{
						sb.Append('═');
						continue;
					}
					if (i > 10 && i < width - 11 && j > midHeight + 4 && j < heightCutOff - 3)
					{
						if (j - (midHeight + 5) < PromptText.Length)
						{
							string line = PromptText[j - (midHeight + 5)];
							if (i - 11 < line.Length)
							{
								sb.Append(line[i - 11]);
								continue;
							}
						}
						sb.Append(' ');
						continue;
					}
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
				string tileRender = Map.GetMapTileRender(tileI, tileJ);
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
	}

}
