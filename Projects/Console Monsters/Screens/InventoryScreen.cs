namespace Console_Monsters.Screens;

internal class InventoryScreen
{
	public static void Render()
	{
		Console.CursorVisible = false;

		var (width, height) = ConsoleHelpers.GetWidthAndHeight();

		int minWidth = 4;
		int minHeight = 2;
		int maxHeight = height - MapText.Length - 3;
		int inventoryWidth = (width / 2) + minWidth;
		int inventorySpacing = Sprites.Height + 1;

		int relativeVisualPosition = 0;
		string monsterDetails = string.Empty;
		int[] monsterSpriteIndex = new int[6];
		(int Width, int Height) monsterSpacing = (35, 1);
		(int Index, int Width, int Height) leftMonster = (0, 0, 0);
		(int Index, int Width, int Height) rightMonster = (1, 0, 0);
		(bool LeftMonster, bool RightMonster) rendered = (false, false);
		(int ID, int Index, string[] Sprite, string Description, string Count) item = (0, 0, Array.Empty<string>(), string.Empty, string.Empty);

#warning TODO: optimize
		List<ItemBase> allItems = PlayerInventory.Distinct().ToList();

		if (partyMonsters.Count < 1)
		{
			leftMonster.Index = -1;
		}

		if (SelectedPlayerInventoryItem >= maxHeight / inventorySpacing)
		{
			relativeVisualPosition = SelectedPlayerInventoryItem - (maxHeight / inventorySpacing) + 1;
		}


		StringBuilder sb = new(width * height);
		for (int j = 0; j < maxHeight; j++)
		{
			for (int i = 0; i < width; i++)
			{
				#region Monsters
				// rendering monsters 
				if (leftMonster.Index < partyMonsters.Count)
				{
					leftMonster.Width = partyMonsters[leftMonster.Index].Sprite[0].Length;
					leftMonster.Height = partyMonsters[leftMonster.Index].Sprite.GetLength(0);

					if (i >= minWidth && i <= minWidth + leftMonster.Width &&
						j >= minHeight + monsterSpacing.Height && j < minHeight + leftMonster.Height + monsterSpacing.Height && j < maxHeight - 1)
					{
						if (j == minHeight + monsterSpacing.Height + leftMonster.Height - 1 && monsterSpriteIndex[leftMonster.Index] == leftMonster.Width)
						{
							sb.Append(' ');
							continue;
						}
						if (monsterSpriteIndex[leftMonster.Index] == leftMonster.Width)
						{
							monsterSpriteIndex[leftMonster.Index] = 0;
							sb.Append(' ');
							continue;
						}
						sb.Append(partyMonsters[leftMonster.Index].Sprite[j - minHeight - monsterSpacing.Height][monsterSpriteIndex[leftMonster.Index]]);
						monsterSpriteIndex[leftMonster.Index]++;
						continue;
					}

					if (i == minWidth && j == minHeight + leftMonster.Height + monsterSpacing.Height && j < maxHeight - 1)
					{
						monsterDetails = $"{partyMonsters[leftMonster.Index].Name}  HP:{(int)partyMonsters[leftMonster.Index].CurrentHP}  Level:{partyMonsters[leftMonster.Index].Level}";

						sb.Append(monsterDetails);
						i += monsterDetails.Length - 1;
						rendered.LeftMonster = true;
						continue;
					}


					if (rightMonster.Index < partyMonsters.Count)
					{
						rightMonster.Width = partyMonsters[rightMonster.Index].Sprite[0].Length;
						rightMonster.Height = partyMonsters[rightMonster.Index].Sprite.GetLength(0);

						if (i >= minWidth + monsterSpacing.Width && i <= minWidth + rightMonster.Width + monsterSpacing.Width &&
							j >= minHeight + monsterSpacing.Height && j < minHeight + rightMonster.Height + monsterSpacing.Height && j < maxHeight - 1)
						{
							if (j == minHeight + monsterSpacing.Height + rightMonster.Height - 1 && monsterSpriteIndex[rightMonster.Index] == rightMonster.Width)
							{
								sb.Append(' ');
								continue;
							}
							if (monsterSpriteIndex[rightMonster.Index] == rightMonster.Width)
							{
								monsterSpriteIndex[rightMonster.Index] = 0;
								sb.Append(' ');
								continue;
							}
							sb.Append(partyMonsters[rightMonster.Index].Sprite[j - minHeight - monsterSpacing.Height][monsterSpriteIndex[rightMonster.Index]]);
							monsterSpriteIndex[rightMonster.Index]++;
							continue;
						}

						if (i == minWidth + monsterSpacing.Width &&
							j == minHeight + rightMonster.Height + monsterSpacing.Height && j < maxHeight - 1)
						{
							monsterDetails = $"{partyMonsters[rightMonster.Index].Name}  HP:{partyMonsters[rightMonster.Index].CurrentHP}";
							sb.Append(monsterDetails);
							i += monsterDetails.Length - 1;
							rendered.RightMonster = true;
							continue;
						}
					}

					if (rendered.LeftMonster && rendered.RightMonster)
					{
						monsterSpacing.Height += leftMonster.Height > rightMonster.Height ? leftMonster.Height : rightMonster.Height;
						monsterSpacing.Height += Sprites.Height;
						rightMonster.Index += 2;
						leftMonster.Index += 2;
						rendered = (false, false);
					}
				}

				if (leftMonster.Index < partyMonsters.Count)
				{
					monsterSpriteIndex[leftMonster.Index] = 0;
				}
				#endregion

				#region Items
				{
					// rendering items
					int itemAbsoluteIndex = item.ID + relativeVisualPosition;
					if (allItems.Count > 0 && itemAbsoluteIndex < allItems.Count)
					{
						if (i >= inventoryWidth && i < inventoryWidth + Sprites.Width &&
							j >= minHeight + (item.ID * inventorySpacing) && j < minHeight + (item.ID * inventorySpacing) + Sprites.Height && j < maxHeight - 1)
						{
							item.Sprite = allItems[itemAbsoluteIndex].Sprite.Split('\n');

							sb.Append(item.Sprite[item.Index]);
							i += item.Sprite[item.Index].Length - 1;
							item.Index++;

							if (item.Index == Sprites.Height)
							{
								item.Count = $"x{PlayerInventory[allItems[itemAbsoluteIndex]]}";
								sb.Append(item.Count);
								i += item.Count.Length;

								item.Index = 0;
								item.ID++;
							}
							continue;
						}

						if (i == inventoryWidth + Sprites.Width + 1 && j == minHeight + (item.ID * inventorySpacing) + Sprites.Height / 2 && j < maxHeight - 1)
						{
							string ellipsis = "...";
							string itemName = allItems[itemAbsoluteIndex].Name;
							string itemInfo = allItems[itemAbsoluteIndex].Description;
							int currentDescriptionIndex = ItemDescriptionScrollFrame / 100;

							item.Description = $"{itemName} | {itemInfo}";

							if (i + item.Description.Length > width - 4)
							{
								if (j == minHeight + ((SelectedPlayerInventoryItem - relativeVisualPosition) * inventorySpacing) + Sprites.Height / 2)
								{
									if (currentDescriptionIndex >= itemInfo.Length)
									{
										ItemDescriptionScrollFrame = 0;
									}

									item.Description = $"{itemName} | {itemInfo[currentDescriptionIndex..]} {itemInfo[..currentDescriptionIndex]}";
								}
								item.Description = $"{item.Description[..(width - i - 4 - ellipsis.Length)]}{ellipsis}";
							}

							sb.Append(item.Description);
							i += item.Description.Length - 1;
							continue;
						}
					}

					// rendering scroll bar
					if (allItems.Count * Sprites.Height > maxHeight - 2)
					{
						if (i == width - 2)
						{
							if (j == 1)
							{
								sb.Append('▲');
								continue;
							}
							if (j > 1 && j < maxHeight - 2)
							{
								if (j >= 1 + (SelectedPlayerInventoryItem * inventorySpacing) && j <= 1 + ((SelectedPlayerInventoryItem + 1) * inventorySpacing) ||
									j > maxHeight - inventorySpacing - 2 && SelectedPlayerInventoryItem >= maxHeight / inventorySpacing)
								{
									sb.Append('█');
									continue;
								}

								sb.Append('│');
								continue;
							}
							if (j == maxHeight - 2)
							{
								sb.Append('▼');
								continue;
							}
						}
					}
				}
				#endregion

				#region Borders
				// border for selected item
				{
					bool left = i == inventoryWidth - 1;
					if (left || i == width - 4)
					{
						if (j == minHeight + ((SelectedPlayerInventoryItem - relativeVisualPosition) * inventorySpacing) - 1)
						{
							sb.Append(left ? '╔' : '╗');
							continue;
						}
						if (j == minHeight + ((SelectedPlayerInventoryItem - relativeVisualPosition) * inventorySpacing) + Sprites.Height)
						{
							if (j < maxHeight - 1)
							{
								sb.Append(left ? '╚' : '╝');
								continue;
							}
							if (j == maxHeight - 1)
							{
								sb.Append('╩');
								continue;
							}
						}
						if (j > minHeight + ((SelectedPlayerInventoryItem - relativeVisualPosition) * inventorySpacing) - 1 &&
							j < minHeight + ((SelectedPlayerInventoryItem - relativeVisualPosition) * inventorySpacing) + Sprites.Height)
						{
							sb.Append('║');
							continue;
						}
					}

					if (i >= inventoryWidth && i < width - 4 &&
					   (j == minHeight + ((SelectedPlayerInventoryItem - relativeVisualPosition) * inventorySpacing) - 1 ||
						j == minHeight + ((SelectedPlayerInventoryItem - relativeVisualPosition) * inventorySpacing) + Sprites.Height))
					{
						sb.Append('═');
						continue;
					}
				}

				// border
				{
					if (i == width / 2)
					{
						if (j > 0 && j < maxHeight - 1)
						{
							sb.Append('│');
							continue;
						}
						if (j is 0)
						{
							sb.Append('╤');
							continue;
						}
						if (j == maxHeight - 1)
						{
							sb.Append('╧');
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
				}
				#endregion

				if (!OperatingSystem.IsWindows() && j < height - 1)
				{
					sb.AppendLine();
				}
			}
		}

		Console.SetCursorPosition(0, 0);
		Console.Write(sb);
	}
}
