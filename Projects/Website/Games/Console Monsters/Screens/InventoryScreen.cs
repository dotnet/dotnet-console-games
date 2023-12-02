using System.Linq;
using System.Text;
using static Website.Games.Console_Monsters.Statics;
//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;
using Website.Games.Console_Monsters.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters.Screens;

internal class InventoryScreen
{
	public static async Task Render()
	{
		Statics.Console.CursorVisible = false;

		var (width, height) = await ConsoleHelpers.GetWidthAndHeight();

		string monsterDetails;
		bool nextDone = false;
		bool currentDone = false;

		int minWidth = 4;
		int minHeight = 2;
		int maxHeight = height - MapText.Length - 3;

		int nextMonster = 1;
		int nextMonsterWidth;
		int nextMonsterHeight = 0;
		int currentMonster = 0;
		int currentMonsterWidth;
		int currentMonsterHeight;
		int monsterWidthSpacing = 35;
		int monsterHeightSpacing = 1;
		int[] monsterSpriteIndex = new int[6];

		int itemBorderGap = 4;
		int scrollBorder = 2;
		int startIndex = 0;
		string itemInfo;
		string itemCount;
		string[] itemSprite;
		List<ItemBase> items;
		int inventoryWidth = (width / 2) + minWidth;
		int inventoryHeight = minHeight;
		int inventoryHeightSpacing = Sprites.Height + 1;
		int spriteIndex = 0;
		int itemIndex = 0;

		if (partyMonsters.Count < 1)
		{
			currentMonster = -1;
		}

//#warning TODO: optimize
		items = PlayerInventory.Distinct().ToList();

		if (SelectedPlayerInventoryItem >= maxHeight / inventoryHeightSpacing)
		{
			startIndex = SelectedPlayerInventoryItem - (maxHeight / inventoryHeightSpacing) + 1;
		}

		StringBuilder sb = new(width * height);
		for (int j = 0; j < maxHeight; j++)
		{
			for (int i = 0; i < width; i++)
			{
				// rendering monsters 
				if (currentMonster < partyMonsters.Count)
				{
					currentMonsterWidth = partyMonsters[currentMonster].Sprite[0].Length;
					currentMonsterHeight = partyMonsters[currentMonster].Sprite.GetLength(0);

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
						sb.Append(partyMonsters[currentMonster].Sprite[j - minHeight - monsterHeightSpacing][monsterSpriteIndex[currentMonster]]);
						monsterSpriteIndex[currentMonster]++;
						continue;
					}

					if (i == minWidth && j == minHeight + currentMonsterHeight + monsterHeightSpacing && j < maxHeight - 1)
					{
						monsterDetails = $"{partyMonsters[currentMonster].Name}  HP:{partyMonsters[currentMonster].CurrentHP}  Level:{partyMonsters[currentMonster].Level}";
						sb.Append(monsterDetails);
						i += monsterDetails.Length - 1;
						currentDone = true;
						continue;
					}


					if (nextMonster < partyMonsters.Count)
					{
						nextMonsterWidth = partyMonsters[nextMonster].Sprite[0].Length;
						nextMonsterHeight = partyMonsters[nextMonster].Sprite.GetLength(0);

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
							sb.Append(partyMonsters[nextMonster].Sprite[j - minHeight - monsterHeightSpacing][monsterSpriteIndex[nextMonster]]);
							monsterSpriteIndex[nextMonster]++;
							continue;
						}

						if (i == minWidth + monsterWidthSpacing &&
							j == minHeight + nextMonsterHeight + monsterHeightSpacing && j < maxHeight - 1)
						{
							monsterDetails = $"{partyMonsters[nextMonster].Name}  HP:{partyMonsters[nextMonster].CurrentHP}";
							sb.Append(monsterDetails);
							i += monsterDetails.Length - 1;
							nextDone = true;
							continue;
						}
					}

					if (currentDone && nextDone)
					{
						monsterHeightSpacing += currentMonsterHeight > nextMonsterHeight ? currentMonsterHeight : nextMonsterHeight;
						monsterHeightSpacing += Sprites.Height;
						currentMonster += 2;
						nextMonster += 2;
						currentDone = false;
						nextDone = false;
					}
				}

				{   // border for selected item
					if (i == inventoryWidth - 1)
					{
						if (j == inventoryHeight + ((SelectedPlayerInventoryItem - startIndex) * inventoryHeightSpacing) - 1)
						{
							sb.Append('╔'); // ┌╔
							continue;
						}
						if (j > inventoryHeight + ((SelectedPlayerInventoryItem - startIndex) * inventoryHeightSpacing) - 1 &&
							j < inventoryHeight + ((SelectedPlayerInventoryItem - startIndex) * inventoryHeightSpacing) + Sprites.Height)
						{
							sb.Append('║'); // │║
							continue;
						}
						if (j == inventoryHeight + ((SelectedPlayerInventoryItem - startIndex) * inventoryHeightSpacing) + Sprites.Height)
						{
							sb.Append('╚'); // └╚
							continue;
						}
					}
					if (i >= inventoryWidth && i < width - itemBorderGap)
					{
						if (j == inventoryHeight + ((SelectedPlayerInventoryItem - startIndex) * inventoryHeightSpacing) - 1 ||
							j == inventoryHeight + ((SelectedPlayerInventoryItem - startIndex) * inventoryHeightSpacing) + Sprites.Height)
						{
							sb.Append('═'); // ─═
							continue;
						}
					}
					if (i == width - itemBorderGap)
					{
						if (j == inventoryHeight + ((SelectedPlayerInventoryItem - startIndex) * inventoryHeightSpacing) - 1)
						{
							sb.Append('╗'); // ┐╗
							continue;
						}
						if (j > inventoryHeight + ((SelectedPlayerInventoryItem - startIndex) * inventoryHeightSpacing) - 1 &&
							j < inventoryHeight + ((SelectedPlayerInventoryItem - startIndex) * inventoryHeightSpacing) + Sprites.Height)
						{
							sb.Append('║'); // │║
							continue;
						}
						if (j == inventoryHeight + ((SelectedPlayerInventoryItem - startIndex) * inventoryHeightSpacing) + Sprites.Height)
						{
							sb.Append('╝'); // ┘╝
							continue;
						}
					}
				}

				// rendering items
				if (items.Count > 0 && (itemIndex + startIndex) < items.Count)
				{
					if (i >= inventoryWidth && i < inventoryWidth + Sprites.Width &&
						j >= inventoryHeight + (itemIndex * inventoryHeightSpacing) && j < inventoryHeight + (itemIndex * inventoryHeightSpacing) + Sprites.Height && j < maxHeight - 1)
					{
						itemSprite = items[itemIndex + startIndex].Sprite.Split('\n');

						sb.Append(itemSprite[spriteIndex]);
						i += itemSprite[spriteIndex].Length - 1;
						spriteIndex++;

						if (spriteIndex == Sprites.Height)
						{
							itemCount = $"x{PlayerInventory[items[itemIndex + startIndex]]}";
							sb.Append(itemCount);
							i += itemCount.Length;

							spriteIndex = 0;
							itemIndex++;
						}
						continue;
					}

					if (i == inventoryWidth + Sprites.Width + 1 && j == inventoryHeight + (itemIndex * inventoryHeightSpacing) + Sprites.Height / 2 && j < maxHeight - 1)
					{
						string ellipsis = "...";
						itemInfo = $"{items[itemIndex + startIndex].Name} | {items[itemIndex + startIndex].Description}";
						if (i + itemInfo.Length > width - itemBorderGap)
						{
							//shorten info if too long
							itemInfo = $"{itemInfo[..(width - i - itemBorderGap - ellipsis.Length)]}{ellipsis}";
						}
						sb.Append(itemInfo);
						i += itemInfo.Length - 1;
						continue;
					}
				}

				// rendering scroll bar
				if (items.Count * Sprites.Height > maxHeight - scrollBorder)
				{
					if (i == width - 2)
					{
						if (j is 1)
						{
							sb.Append('▲');
							continue;
						}
						if (j > 1 && j < maxHeight - scrollBorder)
						{
							if (j >= 1 + (SelectedPlayerInventoryItem * inventoryHeightSpacing) && j <= 1 + ((SelectedPlayerInventoryItem + 1) * inventoryHeightSpacing))
							{
								sb.Append('█');
								continue;
							}
							if (SelectedPlayerInventoryItem >= maxHeight / inventoryHeightSpacing && j > maxHeight - inventoryHeightSpacing - (scrollBorder * 2))
							{
								sb.Append('█');
								continue;
							}

							sb.Append('│');
							continue;
						}
						if (j == maxHeight - scrollBorder)
						{
							sb.Append('▼');
							continue;
						}
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
					if (j is 0)
					{
						sb.Append('╤'); // ╦
						continue;
					}
					if (j == maxHeight - 1)
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
				if (currentMonster < partyMonsters.Count)
				{
					monsterSpriteIndex[currentMonster] = 0;
				}
			}
		}
		await Statics.Console.SetCursorPosition(0, 0);
		await Statics.Console.Write(sb);
	}
}
