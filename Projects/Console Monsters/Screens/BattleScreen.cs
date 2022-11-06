namespace Console_Monsters.Screens;

public static class BattleScreen
{
	public static void Render(MonsterBase monsterA, MonsterBase monsterB)
	{
		int spriteheight = Sprites.BattleSpriteHeight + 1;

		Console.CursorVisible = false;

		var (width, height) = ConsoleHelpers.GetWidthAndHeight();
		int heightCutOff = height - BattleText.Length - 3;
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
					if (i < width - 1 && character >= 0 && line >= 0 && line < BattleText.Length && character < BattleText[line].Length)
					{
						char ch = BattleText[line][character];
						sb.Append(char.IsWhiteSpace(ch) ? ' ' : ch);
					}
					else
					{
						sb.Append(' ');
					}
					continue;
				}

				// battle screen outline
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

				// message prompt if there is one
				if (PromptBattleText is not null)
				{
					int leftBattleMenuBox = (midWidth - Sprites.BattleSpriteWidth) + 4; // Dependant on outer border
					int rightBattleMenuBox = (midWidth + Sprites.BattleSpriteWidth) - 4;

					int centerBattleMenuBoxOffset = (midWidth + Sprites.BattleSpriteWidth) - 60;

					int topBattleMenuBox = midHeight + 6; // Dependant on screen height
					int bottomBattleMenuBox = heightCutOff - 3;

					if ((i == leftBattleMenuBox && j == topBattleMenuBox) || i == centerBattleMenuBoxOffset && j == topBattleMenuBox)
					{
						sb.Append('╔');
						continue;
					}
					if ((i == leftBattleMenuBox && j == bottomBattleMenuBox) || i == centerBattleMenuBoxOffset && j == bottomBattleMenuBox)
					{
						sb.Append('╚');
						continue;
					}
					if (i == rightBattleMenuBox && j == topBattleMenuBox)
					{
						sb.Append('╗');
						continue;
					}
					if (i == rightBattleMenuBox && j == bottomBattleMenuBox)
					{
						sb.Append('╝');
						continue;
					}
					if ((i == leftBattleMenuBox || i == rightBattleMenuBox || i == centerBattleMenuBoxOffset) && j > topBattleMenuBox && j < bottomBattleMenuBox)
					{
						sb.Append('║');
						continue;
					}
					if ((j == bottomBattleMenuBox || j == topBattleMenuBox) && i > leftBattleMenuBox && i < rightBattleMenuBox)
					{
						sb.Append('═');
						continue;
					}
#warning TODO: MAJOR REWORK 
					if (i > leftBattleMenuBox && i < rightBattleMenuBox && j > topBattleMenuBox && j < bottomBattleMenuBox) //Are we at the box?
					{
						int leftBattleMenuTextOffset = leftBattleMenuBox + 2; // For now has to be 2, NEEDS REWORK
						int topBattleMenuTextOffset = topBattleMenuBox + 1;
						if (j - (topBattleMenuTextOffset) < PromptBattleText.Length) //Are we inside the box?
						{
							string line = PromptBattleText[j - (topBattleMenuTextOffset)];
							if (i - leftBattleMenuTextOffset < line.Length - 1)
							{
								sb.Append(line[i - 10]);
								continue;
							}
						}
						sb.Append(' ');
						continue;
					}
				}

				//Opponent Monster (MONSTER B)
				if (i > midWidth - (Sprites.BattleSpriteWidth / 4) * 1 &&
					i < midWidth + (Sprites.BattleSpriteWidth / 4) * 3 + 3 &&
					j < midHeight - 7 &&
					j > midHeight - spriteheight)
				{
					int spriteJ = j - (midHeight - spriteheight) + 1 - (Sprites.BattleSpriteHeight - monsterB.Sprite.Length) / 2;
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

				//Player Monster (MONSTER A)
				if (i > midWidth - (Sprites.BattleSpriteWidth / 4) * 3 - 3 &&
					i < midWidth + (Sprites.BattleSpriteWidth / 4) * 1 &&
					j < midHeight + spriteheight - 15 &&
					j > midHeight - 8)
				{
					int spriteJ = j - midHeight + 8 - (Sprites.BattleSpriteHeight - monsterB.Sprite.Length) / 2;
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
			if (!OperatingSystem.IsWindows() && j < height - 1)
			{
				sb.AppendLine();
			}
		}
		Console.SetCursorPosition(0, 0);
		Console.Write(sb);
	}

	// For giving more offest to the battle text
#warning TODO: FIX
	private static string[] NewBattleText(string[] oldBattleText)
	{
		int offset = 4;
		string BattleTextOffset = string.Empty;

		for (int i = 0; i < offset; i++)
		{
			BattleTextOffset.Concat(" ");
		}

		for (int i = 0; i < oldBattleText.Length; i++)
		{
			oldBattleText[i] = BattleTextOffset + oldBattleText[i];
		}

		return oldBattleText;
	}

	public static void DrawStats(bool playerTurn, MonsterBase PlayerMonster, MonsterBase OpponentMonster)
	{
		Console.SetCursorPosition(66, 23);
		Console.WriteLine($"{PlayerMonster.Name}");
		Console.SetCursorPosition(77, 23);
		Console.WriteLine($"LVL:{PlayerMonster.Level}");
		Console.SetCursorPosition(66, 24);
		Console.WriteLine($"HP:     {DrawBar(PlayerMonster.CurrentHP, PlayerMonster.MaximumHP)} {(int)PlayerMonster.CurrentHP}/{PlayerMonster.MaximumHP}");
		Console.SetCursorPosition(66, 25);
		Console.WriteLine($"Energy: {DrawBar(PlayerMonster.CurrentEnergy, PlayerMonster.MaximumEnergy)} {PlayerMonster.CurrentEnergy}/{PlayerMonster.MaximumEnergy}");

		Console.SetCursorPosition(104, 10);
		Console.WriteLine($"{OpponentMonster.Name}");
		Console.SetCursorPosition(114, 10);
		Console.WriteLine($"LVL:{OpponentMonster.Level}");
		Console.SetCursorPosition(104, 11);
		Console.WriteLine($"HP:    {DrawBar(OpponentMonster.CurrentHP, OpponentMonster.MaximumHP)}");
		Console.SetCursorPosition(104, 12);
		Console.WriteLine($"Energy:{DrawBar(OpponentMonster.CurrentEnergy, OpponentMonster.MaximumEnergy)}");

		string turn;
		if (playerTurn)
		{
			turn = "Player Turn";
		}
		else
		{
			turn = "CPU Turn   ";
		}
		Console.SetCursorPosition(35, 5);
		Console.WriteLine(turn);
	}

	public static string DrawBar(double current, double max)
	{
		const char block = '■';
		const char empty = ' ';
		string final = string.Empty;

		int barLength = 20;
		//double tenpercent = (int)max / 10;
		double fivepercent = max / 10 / 2;

		final += "[";
		for (int i = 0; i < barLength; i++)
		{
			if (current >= fivepercent)
				final += block;
			else
				final += empty;
			current -= fivepercent;
		}
		final += "]";

		return final;
	}
}
