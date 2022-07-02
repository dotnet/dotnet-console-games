using System;
using System.Linq;
using System.Text;
using System.Threading;
using static Website.Games.Console_Monsters._using;
using Website.Games.Console_Monsters.Maps;
using Website.Games.Console_Monsters.Monsters;
using Website.Games.Console_Monsters.Bases;
using Website.Games.Console_Monsters.NPCs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters;

public static class Renderer
{
	public static StringBuilder LastMapRender = new();

	public static async Task RenderWorldMapView()
	{
		_using.Console.CursorVisible = false;

		var (width, height) = await GetWidthAndHeight();
		int heightCutOff = height - MapText.Length - 3;
		int midWidth = width / 2;
		int midHeight = heightCutOff / 2;

		StringBuilder sb = new(width * height);
		for (int j = 0; j < height; j++)
		{
			if (_using.OperatingSystem.IsWindows() && j == height - 1)
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
				if (promptText is not null)
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
						if (j - (midHeight + 5) < promptText.Length)
						{
							string line = promptText[j - (midHeight + 5)];
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
				string tileRender = map.GetMapTileRender(tileI, tileJ);
				char c = tileRender[pixelJ * (Sprites.Width + 1) + pixelI];
				sb.Append(char.IsWhiteSpace(c) ? ' ' : c);
			}
			if (!_using.OperatingSystem.IsWindows() && j < height - 1)
			{
				sb.AppendLine();
			}
		}
		await _using.Console.SetCursorPosition(0, 0);
		await _using.Console.Write(sb);
		await SleepAfterRender();
	}


	public static async Task<(int Width, int Height)> GetWidthAndHeight()
	{
	RestartRender:
		int width = _using.Console.WindowWidth;
		int height = _using.Console.WindowHeight;
		if (_using.OperatingSystem.IsWindows())
		{
			try
			{
				if (_using.Console.BufferHeight != height) _using.Console.BufferHeight = height;
				if (_using.Console.BufferWidth != width) _using.Console.BufferWidth = width;
			}
			catch (Exception)
			{
				await _using.Console.Clear();
				goto RestartRender;
			}
		}
		return (width, height);
	}

	static async Task SleepAfterRender()
	{
		// frame rate control targeting 30 frames per second
		DateTime now = DateTime.Now;
		TimeSpan sleep = TimeSpan.FromMilliseconds(33) - (now - previoiusRender);
		if (sleep > TimeSpan.Zero)
		{
			await _using.Console.RefreshAndDelay(sleep);
		}
		else
		{
			await _using.Console.Refresh();
		}
		previoiusRender = DateTime.Now;
	}

	public static async Task RenderBattleTransition()
	{
		int width = _using.Console.WindowWidth;
		int height = _using.Console.WindowHeight;

		_using.Console.ForegroundColor = ConsoleColor.White;


		Random rnd = new();

		switch (rnd.Next(1, 1))
		{
			case 1:
				await LeftRightBlockTransition(height, width);
				break;
			case 2:

				break;
			default:

				break;
		}
	}

	public static async Task LeftRightBlockTransition(int height, int width)
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
				await _using.Console.SetCursorPosition(i, j);
				for (int k = 0; k < Block.Length; k++)
				{
					try
					{
						if (Block[k] == '\n')
						{
							h++;
							await _using.Console.SetCursorPosition(i, j + h);
						}
						else
						{
							await _using.Console.Write(Block[k]);
						}
					}
					catch { }
				}
				//Thread.Sleep(1);
			}
			if (j < height - 1)
			{
				await _using.Console.WriteLine();
			}
		}
	}

	public static async Task RenderInventoryView()
	{
		_using.Console.CursorVisible = false;

		var (width, height) = await GetWidthAndHeight();

		string monsterDetails;
		bool nextDone = false;
		bool currentDone = false;

		int minWidth = 4;
		int minHeight = 2;
		int maxHeight = height - MapText.Length - 3;

		int nextMonster = 1;
		int nextMonsterWidth = 0;
		int nextMonsterHeight = 0;
		int currentMonster = 0;
		int currentMonsterWidth = 0;
		int currentMonsterHeight = 0;
		int monsterWidthSpacing = 35;
		int monsterHeightSpacing = 1;
		int[] monsterSpriteIndex = new int[6];

		string itemInfo;
		string itemCount;
		string[] itemSprite;
		List<Items> items = new();
		int inventoryWidth = (width / 2) + minWidth;
		int inventoryHeight = minHeight * 2;
		int inventoryHeightSpacing = Sprites.Height + 1;
		int spriteIndex = 0;
		int itemIndex = 0;

		if (activeMonsters.Count < 1)
		{
			currentMonster = -1;
		}

		Inventory.AddToStorage(Items.MonsterBox);
		Inventory.AddToStorage(Items.XPBerries);
		Inventory.AddToStorage(Items.HealthPotion);
		Inventory.AddToStorage(Items.HealthPotion);

		if (!Inventory.IsEmpty())
		{
			items = Inventory.GetListOfItems();
		}

		StringBuilder sb = new(width * height);
		for (int j = 0; j < maxHeight; j++)
		{
			for (int i = 0; i < width; i++)
			{
				// rendering monsters 
				if (currentMonster < activeMonsters.Count)
				{
					currentMonsterWidth = activeMonsters[currentMonster].Sprite[0].Length;
					currentMonsterHeight = activeMonsters[currentMonster].Sprite.GetLength(0);

					if (i >= minWidth && i <= minWidth + currentMonsterWidth &&
						j >= minHeight + monsterHeightSpacing && j < minHeight + currentMonsterHeight + monsterHeightSpacing && j < maxHeight - 1)
					{
						if (j == minHeight + monsterHeightSpacing + currentMonsterHeight - 1 && monsterSpriteIndex[currentMonster] == currentMonsterWidth)
						{
							sb.Append(' ');
							continue;
						}
						if (monsterSpriteIndex[currentMonster] == currentMonsterWidth)
						{
							monsterSpriteIndex[currentMonster] = 0;
							sb.Append(' ');
							continue;
						}
						sb.Append(activeMonsters[currentMonster].Sprite[j - minHeight - monsterHeightSpacing][monsterSpriteIndex[currentMonster]]);
						monsterSpriteIndex[currentMonster]++;
						continue;
					}

					if (i == minWidth && j == minHeight + currentMonsterHeight + monsterHeightSpacing && j < maxHeight - 1)
					{
						monsterDetails = $"{activeMonsters[currentMonster].Name}  HP:{activeMonsters[currentMonster].CurrentHP}";
						sb.Append(monsterDetails);
						i += monsterDetails.Length - 1;
						currentDone = true;
						continue;
					}


					if (nextMonster < activeMonsters.Count)
					{
						nextMonsterWidth = activeMonsters[nextMonster].Sprite[0].Length;
						nextMonsterHeight = activeMonsters[nextMonster].Sprite.GetLength(0);

						if (i >= minWidth + monsterWidthSpacing && i <= minWidth + nextMonsterWidth + monsterWidthSpacing &&
							j >= minHeight + monsterHeightSpacing && j < minHeight + nextMonsterHeight + monsterHeightSpacing && j < maxHeight - 1)
						{
							if (j == minHeight + monsterHeightSpacing + nextMonsterHeight - 1 && monsterSpriteIndex[nextMonster] == nextMonsterWidth)
							{
								sb.Append(' ');
								continue;
							}
							if (monsterSpriteIndex[nextMonster] == nextMonsterWidth)
							{
								monsterSpriteIndex[nextMonster] = 0;
								sb.Append(' ');
								continue;
							}
							sb.Append(activeMonsters[nextMonster].Sprite[j - minHeight - monsterHeightSpacing][monsterSpriteIndex[nextMonster]]);
							monsterSpriteIndex[nextMonster]++;
							continue;
						}

						if (i == minWidth + monsterWidthSpacing &&
							j == minHeight + nextMonsterHeight + monsterHeightSpacing && j < maxHeight - 1)
						{
							monsterDetails = $"{activeMonsters[nextMonster].Name}  HP:{activeMonsters[nextMonster].CurrentHP}";
							sb.Append(monsterDetails);
							i += monsterDetails.Length - 1;
							nextDone = true;
							continue;
						}
					}

					if (currentDone && nextDone)
					{
						monsterHeightSpacing += currentMonsterHeight > nextMonsterHeight ? currentMonsterHeight : nextMonsterHeight;
						monsterHeightSpacing += 5;
						currentMonster += 2;
						nextMonster += 2;
						currentDone = false;
						nextDone = false;
					}
				}

				// rendering items
				if (items.Count > 0 && itemIndex < items.Count)
				{
					if (i >= inventoryWidth && i < inventoryWidth + Sprites.Width &&
						j >= inventoryHeight + (itemIndex * inventoryHeightSpacing) && j < inventoryHeight + (itemIndex * inventoryHeightSpacing) + Sprites.Height && j < maxHeight - 1)
					{
						itemSprite = ItemDetails[items[itemIndex]].Sprite.Split('\n');

						sb.Append(itemSprite[spriteIndex]);
						i += itemSprite[spriteIndex].Length - 1;
						spriteIndex++;

						if (spriteIndex == Sprites.Height)
						{
							itemCount = $"x{Inventory.GetStorageCount((Items)itemIndex)}";
							sb.Append(itemCount);
							i += itemCount.Length;

							spriteIndex = 0;
							itemIndex++;
						}
						continue;
					}

					if (i == inventoryWidth + Sprites.Width + 1 && j == inventoryHeight + (itemIndex * inventoryHeightSpacing) + Sprites.Height / 2 && j < maxHeight - 1)
					{
						itemInfo = $"{ItemDetails[items[itemIndex]].Name} | {ItemDetails[items[itemIndex]].Description}";
						if (i + itemInfo.Length > width)
						{
							//shorten info if too long
							itemInfo = $"{itemInfo[..(width - i - 4)]}..."; 
						}
						sb.Append(itemInfo);
						i += itemInfo.Length - 1;
						continue;
					}
				}

				// border
				if (i == width / 2)
				{
					if (j > 0 && j < maxHeight - 1)
					{
						sb.Append('│'); // ║
						continue;
					}
					if (j == 0)
					{
						sb.Append('╤'); // ╦
						continue;
					}
					if (j == maxHeight)
					{
						sb.Append('╧'); // ╩
						continue;
					}
				}
				if (j > 0 && i > 0 && j < maxHeight - 1 && i < width - 1)
				{
					sb.Append(' ');
					continue;
				}
				if (i is 0 && j is 0)
				{
					sb.Append('╔');
					continue;
				}
				if (i is 0 && j == maxHeight - 1)
				{
					sb.Append('╚');
					continue;
				}
				if (i == width - 1 && j is 0)
				{
					sb.Append('╗');
					continue;
				}
				if (i == width - 1 && j == maxHeight - 1)
				{
					sb.Append('╝');
					continue;
				}
				if (i is 0 || i == width - 1)
				{
					sb.Append('║');
					continue;
				}
				if (j is 0 || j == maxHeight - 1)
				{
					sb.Append('═');
					continue;
				}

				sb.AppendLine();
				if (currentMonster < activeMonsters.Count)
				{
					monsterSpriteIndex[currentMonster] = 0;
				}
			}
		}
		await _using.Console.SetCursorPosition(0, 0);
		await _using.Console.Write(sb);
		await SleepAfterRender();
	}
	public static async Task RenderBattleView()
	{
		int spriteheight = Sprites.BattleSpriteHeight + 1;

		MonsterBase monsterA = MonsterBase.GetRandom();
		MonsterBase monsterB = MonsterBase.GetRandom();

		_using.Console.CursorVisible = false;
		_using.Console.BackgroundColor = ConsoleColor.Black;
		_using.Console.ForegroundColor = ConsoleColor.White;

		var (width, height) = await GetWidthAndHeight();
		int heightCutOff = height - battletext.Length - 3;
		int midWidth = width / 2;
		int midHeight = heightCutOff / 2;

		StringBuilder sb = new(width * height);
		for (int j = 0; j < height; j++)
		{
			if (_using.OperatingSystem.IsWindows() && j == height - 1)
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
					if (i < width - 1 && character >= 0 && line >= 0 && line < battletext.Length && character < battletext[line].Length)
					{
						char ch = battletext[line][character];
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

				if (i > midWidth - (Sprites.BattleSpriteWidth / 4) * 1 &&
					i < midWidth + (Sprites.BattleSpriteWidth / 4) * 3 + 3 &&
					j < midHeight &&
					j > midHeight - spriteheight)
				{
					int spriteJ = j - (midHeight - spriteheight) - 1 - (Sprites.BattleSpriteHeight - monsterB.Sprite.Length) / 2;
					char c;
					if (spriteJ < 0 || spriteJ >= monsterB.Sprite.Length)
					{
						c = ' ';
					}
					else
					{
						int spriteI = (i - (midWidth - (Sprites.BattleSpriteWidth / 4) * 1) - 1) - (Sprites.BattleSpriteWidth - monsterB.Sprite[spriteJ].Length) / 2;
						if (spriteI < 0 || spriteI >= monsterB.Sprite[spriteJ].Length)
						{
							c = ' ';
						}
						else
						{
							c = monsterB.Sprite[spriteJ][spriteI];
						}
					}
					sb.Append(char.IsWhiteSpace(c) ? ' ' : c);
					continue;
				}

				if (i > midWidth - (Sprites.BattleSpriteWidth / 4) * 3 - 3 &&
					i < midWidth + (Sprites.BattleSpriteWidth / 4) * 1 &&
					j < midHeight + spriteheight &&
					j > midHeight)
				{
					int spriteJ = j - midHeight - 1 - (Sprites.BattleSpriteHeight - monsterB.Sprite.Length) / 2;
					char c;
					if (spriteJ < 0 || spriteJ >= monsterA.Sprite.Length)
					{
						c = ' ';
					}
					else
					{
						int spriteI = i - (midWidth - (Sprites.BattleSpriteWidth / 4) * 3 - 3) - 1 - (Sprites.BattleSpriteWidth - monsterA.Sprite[spriteJ].Length) / 2;
						if (spriteI < 0 || spriteI >= monsterA.Sprite[spriteJ].Length)
						{
							c = ' ';
						}
						else
						{
							c = monsterA.Sprite[spriteJ][spriteI];
						}
					}
					sb.Append(char.IsWhiteSpace(c) ? ' ' : c);
					continue;
				}

				sb.Append(' ');
			}
			if (!_using.OperatingSystem.IsWindows() && j < height - 1)
			{
				sb.AppendLine();
			}
		}
		await _using.Console.SetCursorPosition(0, 0);
		await _using.Console.Write(sb);
		await SleepAfterRender();
	}


}
