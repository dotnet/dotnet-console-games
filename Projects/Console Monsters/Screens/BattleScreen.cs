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

				// message prompt if there is one
				if (PromptBattleText is not null)
				{
					if (i is 10 && j == midHeight + 6)
					{
						sb.Append('╔');
						continue;
					}
					if (i is 10 && j == heightCutOff - 3)
					{
						sb.Append('╚');
						continue;
					}
					if (i == width - 11 && j == midHeight + 6)
					{
						sb.Append('╗');
						continue;
					}
					if (i == width - 11 && j == heightCutOff - 3)
					{
						sb.Append('╝');
						continue;
					}
					if ((i is 10 || i == width - 11) && j > midHeight + 6 && j < heightCutOff - 3)
					{
						sb.Append('║');
						continue;
					}
					if ((j == heightCutOff - 3 || j == midHeight + 6) && i > 10 && i < width - 11)
					{
						sb.Append('═');
						continue;
					}
					if (i > 10 && i < width - 11 && j > midHeight + 8 && j < heightCutOff - 3)
					{
						if (j - (midHeight + 9) < PromptBattleText.Length)
						{
							string line = PromptBattleText[j - (midHeight + 9)];
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

				//Opponent Monster (MONSTER B)
				if (i > midWidth - (Sprites.BattleSpriteWidth / 4) * 1 &&
					i < midWidth + (Sprites.BattleSpriteWidth / 4) * 3 + 3 &&
					j < midHeight - 7 &&
					j > midHeight - spriteheight )
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
	public static void DrawStats(bool playerTurn, MonsterBase PlayerMonster, MonsterBase OpponentMonster)
	{
		//TEMP                    34
		Console.SetCursorPosition(63, 26);
		Console.WriteLine($"HP:{CalculatePercentage(PlayerMonster.CurrentHP, PlayerMonster.MaximumHP)}%  Energy:{PlayerMonster.CurrentEnergy}  ");

		Console.SetCursorPosition(102, 13);
		Console.WriteLine($"HP:{CalculatePercentage(OpponentMonster.CurrentHP, OpponentMonster.MaximumHP)}%  Energy:{OpponentMonster.CurrentEnergy}  ");

		Console.SetCursorPosition(35, 5);
		string turn;
		if (playerTurn)
		{
			turn = "Player Turn";
		}
		else
		{
			turn = "CPU Turn   ";
		}
		Console.WriteLine(turn);

		static double CalculatePercentage(double currentHP, double maxHP)
		{
			return currentHP / maxHP * 100;
		}
	}
}
