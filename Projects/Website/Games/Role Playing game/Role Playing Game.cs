using System;
using System.Text;
using System.Threading.Tasks;

namespace Website.Games.Role_Playing_Game;

public class Role_Playing_Game
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		Exception? exception = null;

		Random random = new();
		Character character;
		char[][] map;
		DateTime previoiusRender = DateTime.Now;
		int movesSinceLastBattle = 0;
		bool gameRunning = true;
		const double randomBattleChance = 1d / 10d;
		const int movesBeforeRandomBattle = 4;

		string[] maptext = new[]
		{
			"Move: arrow keys or (w, a, s, d)",
			"Check Status: [enter]",
			"Quit: [escape]",
		};

		string[] defaultCombatText = new string[]
		{
			"1) attack",
			"2) run",
			"3) check status",
		};

		string[] combatText = defaultCombatText;

		try
		{
			Console.CursorVisible = false;
			Initialize();
			await OpeningScreen();
			while (gameRunning)
			{
				await UpdateCharacter();
				await HandleMapUserInput();
				if (gameRunning)
				{
					await RenderWorldMapView();
					await SleepAfterRender();
				}
			}
		}
		catch (Exception e)
		{
			exception = e;
			throw;
		}
		finally
		{
			await Console.Clear();
			await Console.WriteLine(exception?.ToString() ?? "Role Playing Game was closed.");
			Console.CursorVisible = true;
			await Console.Refresh();
		}

		void Initialize()
		{
			map = Maps.Town;
			character = new();
			{
				var (i, j) = FindTileInMap(map, 'X')!.Value;
				character.I = i * 7;
				character.J = j * 4;
			}
			character.MapAnimation = Sprites.IdleRight;
		}

		async Task OpeningScreen()
		{
			await Console.SetCursorPosition(0, 0);
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine(" Role Playing Game");
			await Console.WriteLine();
			await Console.WriteLine(" You are about to embark on an epic adventure.");
			await Console.WriteLine();
			await Console.WriteLine(" Go find and kill the king in the castle... because why not?");
			await Console.WriteLine();
			await Console.WriteLine(" Note: This game is a work in progress.");
			await Console.WriteLine();
			await Console.Write(" Press [enter] to begin...");
			await PressEnterToContiue();
		}

		async Task UpdateCharacter()
		{
			if (character.MapAnimation == Sprites.RunUp && character.MapAnimationFrame is 2 or 4 or 6) character.J--;
			if (character.MapAnimation == Sprites.RunDown && character.MapAnimationFrame is 2 or 4 or 6) character.J++;
			if (character.MapAnimation == Sprites.RunLeft) character.I--;
			if (character.MapAnimation == Sprites.RunRight) character.I++;
			character.MapAnimationFrame++;

			if (character.Moved)
			{
				await HandleCharacterMoved();
				character.Moved = false;
			}
		}

		async Task HandleCharacterMoved()
		{
			movesSinceLastBattle++;
			switch (map[character.TileJ][character.TileI])
			{
				case 'i': await SleepAtInn(); break;
				case 's': await ShopAtStore(); break;
				case 'c': await OpenChest(); break;
				case '0': TransitionMapToTown(); break;
				case '1': TransitionMapToField(); break;
				case '2': TransitionMapToCastle(); break;
				case 'g': await FightGuardBoss(); break;
				case ' ': await ChanceForRandomBattle(); break;
				case 'k': await FightKing(); break;
				case 'h': await HiddenWaterFountain(); break;
			}
		}

		async Task RenderStatusString()
		{
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine(" Status");
			await Console.WriteLine();
			await Console.WriteLine($" Level:      {character.Level}");
			await Console.WriteLine($" Experience: {character.Experience}/{character.ExperienceToNextLevel}");
			await Console.WriteLine($" Health:     {character.Health}/{character.MaxHealth}");
			await Console.WriteLine($" Gold:       {character.Gold}");
			await Console.WriteLine($" Damage:     {character.Damage}");
			await Console.WriteLine();
			await Console.Write(" Press [enter] to continue...");
			await PressEnterToContiue();
		}

		async Task RenderDeathScreen()
		{
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine(" You died...");
			await Console.WriteLine();
			await Console.WriteLine(" Game Over.");
			await Console.WriteLine();
			await Console.Write(" Press [enter] to continue...");
			await PressEnterToContiue();
		}

		async Task PressEnterToContiue()
		{
		GetInput:
			ConsoleKey key = (await Console.ReadKey(true)).Key;
			switch (key)
			{
				case ConsoleKey.Enter:
					return;
				case ConsoleKey.Escape:
					gameRunning = false;
					return;
				default:
					goto GetInput;
			}
		}

		async Task OpenChest()
		{
			character.Gold++;
			map[character.TileJ][character.TileI] = 'e';
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine(" You found a chest! You open it and find some gold. :)");
			await Console.WriteLine();
			await Console.WriteLine($" Gold: {character.Gold}");
			await Console.WriteLine();
			await Console.Write(" Press [enter] to continue...");
			await PressEnterToContiue();
		}

		async Task SleepAtInn()
		{
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine(" You enter the inn and stay the night...");
			await Console.WriteLine();
			await Console.WriteLine(" ZzzZzzZzz...");
			await Console.WriteLine();
			await Console.WriteLine(" Your health is restored.");
			await Console.WriteLine();
			await Console.Write(" Press [enter] to continue...");
			character.Health = character.MaxHealth;
			await PressEnterToContiue();
		}

		async Task HiddenWaterFountain()
		{
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine(" You walked into the wall and found");
			await Console.WriteLine(" a hidden water fountain that sprays");
			await Console.WriteLine(" Hawaiian Punch.");
			await Console.WriteLine();
			await Console.WriteLine(" Your health is restored.");
			await Console.WriteLine();
			await Console.Write(" Press [enter] to continue...");
			character.Health = character.MaxHealth;
			await PressEnterToContiue();
		}

		void TransitionMapToTown()
		{
			map = Maps.Town;
			var (i, j) = FindTileInMap(map, '1')!.Value;
			character.I = i * 7;
			character.J = j * 4;
		}

		void TransitionMapToField()
		{
			char c = map == Maps.Town ? '0' : '2';
			map = Maps.Field;
			var (i, j) = FindTileInMap(map, c)!.Value;
			character.I = i * 7;
			character.J = j * 4;
		}

		void TransitionMapToCastle()
		{
			map = Maps.Castle;
			var (i, j) = FindTileInMap(map, '1')!.Value;
			character.I = i * 7;
			character.J = j * 4;
		}

		async Task ShopAtStore()
		{
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine(" You enter the store...");
			await Console.WriteLine();
			if (character.Gold >= 6)
			{
				int damage = character.Gold / 6;
				character.Gold -= damage * 6;
				character.Damage += damage;
				await Console.WriteLine($" You pay {damage * 6} gold to train your kung fu.");
				await Console.WriteLine();
				await Console.WriteLine($" You gained +{damage} damage on your attacks.");
			}
			else if (character.Damage >= 3)
			{
				await Console.WriteLine($" \"You have learned all that I can teach you.\"");
			}
			else
			{
				await Console.WriteLine($" \"Bring me 6 gold and I will teach you kung fu.\"");
			}
			await Console.WriteLine();
			await Console.Write(" Press [enter] to continue...");
			await PressEnterToContiue();
		}

		async Task ChanceForRandomBattle()
		{
			if (map == Maps.Town)
			{
				return;
			}
			if (movesSinceLastBattle > movesBeforeRandomBattle && random.NextDouble() < randomBattleChance)
			{
				bool ranAway = await Battle(map == Maps.Castle ? EnemyType.Guard : EnemyType.Boar);//, out _);
				if (!gameRunning)
				{
					return;
				}
			}
		}

		async Task FightGuardBoss()
		{
			bool ranAway = await Battle(EnemyType.GuardBoss);//, out bool ranAway);
			if (!gameRunning)
			{
				return;
			}
			else if (ranAway)
			{
				character.J++;
				character.MapAnimation = Sprites.RunDown;
			}
			else
			{
				map[character.TileJ][character.TileI] = ' ';
			}
		}

		async Task FightKing()
		{
			bool ranAway = await Battle(EnemyType.FinalBoss);//, out bool ranAway);
			if (!gameRunning)
			{
				return;
			}
			else if (ranAway)
			{
				character.J++;
				character.MapAnimation = Sprites.RunDown;
			}
			else
			{
				await Console.Clear();
				await Console.WriteLine();
				await Console.WriteLine(" You beat the king!");
				await Console.WriteLine();
				await Console.WriteLine(" Woohoo! Good job or whatever.");
				await Console.WriteLine();
				await Console.WriteLine(" Game Over.");
				await Console.WriteLine();
				await Console.Write(" Press [enter] to continue...");
				await PressEnterToContiue();
				gameRunning = false;
				return;
			}
		}

		async Task HandleMapUserInput()
		{
			while (await Console.KeyAvailable())
			{
				ConsoleKey key = (await Console.ReadKey(true)).Key;
				switch (key)
				{
					case
						ConsoleKey.UpArrow or ConsoleKey.W or
						ConsoleKey.DownArrow or ConsoleKey.S or
						ConsoleKey.LeftArrow or ConsoleKey.A or
						ConsoleKey.RightArrow or ConsoleKey.D:
						if (character.IsIdle)
						{
							var (tileI, tileJ) = key switch
							{
								ConsoleKey.UpArrow or ConsoleKey.W => (character.TileI, character.TileJ - 1),
								ConsoleKey.DownArrow or ConsoleKey.S => (character.TileI, character.TileJ + 1),
								ConsoleKey.LeftArrow or ConsoleKey.A => (character.TileI - 1, character.TileJ),
								ConsoleKey.RightArrow or ConsoleKey.D => (character.TileI + 1, character.TileJ),
								_ => throw new Exception("bug"),
							};
							if (Maps.IsValidCharacterMapTile(map, tileI, tileJ))
							{
								switch (key)
								{
									case ConsoleKey.UpArrow or ConsoleKey.W:
										character.J--;
										character.MapAnimation = Sprites.RunUp;
										break;
									case ConsoleKey.DownArrow or ConsoleKey.S:
										character.J++;
										character.MapAnimation = Sprites.RunDown;
										break;
									case ConsoleKey.LeftArrow or ConsoleKey.A:
										character.MapAnimation = Sprites.RunLeft;
										break;
									case ConsoleKey.RightArrow or ConsoleKey.D:
										character.MapAnimation = Sprites.RunRight;
										break;
								}
							}
						}
						break;
					case ConsoleKey.Enter:
						await RenderStatusString();
						break;
					case ConsoleKey.Escape:
						gameRunning = false;
						return;
				}
			}
		}

		async Task<bool> Battle(EnemyType enemyType)//, out bool ranAway)
		{
			movesSinceLastBattle = 0;
			bool ranAway = false;

			int enemyHealth = enemyType switch
			{
				EnemyType.Boar => 03,
				EnemyType.GuardBoss => 20,
				EnemyType.Guard => 10,
				EnemyType.FinalBoss => 60,
				_ => 1,
			};

			switch (enemyType)
			{
				case EnemyType.Boar:
					combatText = new string[]
					{
						"You were attacked by a wild boar!",
						"1) attack",
						"2) run",
						"3) check status",
					};
					break;
				case EnemyType.GuardBoss:
					if (character.Level < 2)
					{
						combatText = new string[]
						{
							"You approached the castle guard.",
							"He looks tough. You should probably",
							"run away and come back when you are",
							"stronger.",
							"1) attack",
							"2) run",
							"3) check status",
						};
					}
					else
					{
						combatText = new string[]
						{
							"You approached the castle guard.",
							"1) attack",
							"2) run",
							"3) check status",
						};
					}
					break;
				case EnemyType.Guard:
					combatText = new string[]
					{
						"You were attacked by a castle guard!",
						"1) attack",
						"2) run",
						"3) check status",
					};
					break;
				case EnemyType.FinalBoss:
					if (character.Level < 3)
					{
						combatText = new string[]
						{
							"You approached the evil king.",
							"He looks tough. You should probably",
							"run away and come back when you are",
							"stronger.",
							"1) attack",
							"2) run",
							"3) check status",
						};
					}
					else
					{
						combatText = new string[]
						{
							"You approached the evil king.",
							"1) attack",
							"2) run",
							"3) check status",
						};
					}
					break;
			}

			int frameLeft = 0;
			int frameRight = 0;

			string[] animationLeft = Sprites.IdleRight;
			string[] animationRight = enemyType switch
			{
				EnemyType.Boar => Sprites.IdleBoar,
				EnemyType.Guard => Sprites.IdleLeft,
				EnemyType.GuardBoss => Sprites.IdleLeft,
				EnemyType.FinalBoss => Sprites.IdleLeft,
				_ => new[] { Sprites.Error },
			};

			bool pendingConfirmation = false;

			while (true)
			{
				if (animationLeft == Sprites.GetUpAnimationRight && frameLeft == animationLeft.Length - 1)
				{
					frameLeft = 0;
					animationLeft = Sprites.IdleRight;
				}
				else if (animationLeft == Sprites.IdleRight || frameLeft < animationLeft.Length - 1)
				{
					frameLeft++;
				}
				if (frameLeft >= animationLeft.Length) frameLeft = 0;
				frameRight++;
				if (frameRight >= animationRight.Length) frameRight = 0;
				while (await Console.KeyAvailable())
				{
					ConsoleKey key = (await Console.ReadKey(true)).Key;
					switch (key)
					{
						case ConsoleKey.D1 or ConsoleKey.NumPad1:
							if (!pendingConfirmation)
							{
								switch (random.Next(2))
								{
									case 0:
										frameLeft = 0;
										animationLeft = Sprites.PunchRight;
										combatText = new string[]
										{
											"You attacked and did damage!",
											"",
											"Press [enter] to continue...",
										};
										enemyHealth -= character.Damage;
										break;
									case 1:
										frameLeft = 0;
										animationLeft = Sprites.FallLeft;
										combatText = new string[]
										{
											"You were attacked, but the enemy was",
											"faster and you took damage!",
											"",
											"Press [enter] to continue...",
										};
										character.Health--;
										break;
								}
								pendingConfirmation = true;
							}
							break;
						case ConsoleKey.D2 or ConsoleKey.NumPad2:
							if (!pendingConfirmation)
							{
								bool success = enemyType switch
								{
									EnemyType.Boar => random.Next(10) < 9,
									EnemyType.Guard => random.Next(10) < 7,
									_ => true,
								};
								if (success)
								{
									await Console.Clear();
									await Console.WriteLine();
									await Console.WriteLine(" You ran away.");
									await Console.WriteLine();
									await Console.Write(" Press [enter] to continue...");
									await PressEnterToContiue();
									ranAway = true;
									return ranAway;
								}
								else
								{
									frameLeft = 0;
									animationLeft = Sprites.FallLeft;
									combatText = new string[]
									{
										"You tried to run away but the enemy",
										"attacked you from behind and you took",
										"damage.",
										"",
										"Press [enter] to continue...",
									};
									character.Health--;
									pendingConfirmation = true;
								}
							}
							break;
						case ConsoleKey.D3 or ConsoleKey.NumPad3:
							if (!pendingConfirmation)
							{
								await RenderStatusString();
								if (!gameRunning)
								{
									return ranAway;
								}
							}
							break;
						case ConsoleKey.Enter:
							if (pendingConfirmation)
							{
								pendingConfirmation = false;
								if (animationLeft == Sprites.FallLeft && frameLeft == animationLeft.Length - 1)
								{
									frameLeft = 0;
									animationLeft = Sprites.GetUpAnimationRight;
								}
								else
								{
									frameLeft = 0;
									animationLeft = Sprites.IdleRight;
								}
								combatText = defaultCombatText;
								if (character.Health <= 0)
								{
									await RenderDeathScreen();
									gameRunning = false;
									return ranAway;
								}
								if (enemyHealth <= 0)
								{
									if (enemyType is EnemyType.FinalBoss)
									{
										return ranAway;
									}
									int experienceGain = enemyType switch
									{
										EnemyType.Boar => 1,
										EnemyType.GuardBoss => 20,
										EnemyType.Guard => 10,
										EnemyType.FinalBoss => 9001, // ITS OVER 9000!
										_ => 0,
									};
									await Console.Clear();
									await Console.WriteLine();
									await Console.WriteLine(" You defeated the enemy!");
									await Console.WriteLine();
									await Console.WriteLine($" You gained {experienceGain} experience.");
									await Console.WriteLine();
									character.Experience += experienceGain;
									if (character.Experience >= character.ExperienceToNextLevel)
									{
										character.Level++;
										character.Experience = 0;
										character.ExperienceToNextLevel *= 2;
										await Console.WriteLine($" You grew to level {character.Level}.");
										await Console.WriteLine();
									}
									await Console.WriteLine();
									await Console.Write(" Press [enter] to continue...");
									await PressEnterToContiue();
									return ranAway;
								}
							}
							break;
						case ConsoleKey.Escape:
							gameRunning = false;
							return ranAway;
					}
				}
				await RenderBattleView(animationLeft[frameLeft], animationRight[frameRight]);
				await SleepAfterRender();
			}
		}

		async Task SleepAfterRender()
		{
			// frame rate control
			// battle view is currently targeting 30 frames per second
			DateTime now = DateTime.Now;
			TimeSpan sleep = TimeSpan.FromMilliseconds(33) - (now - previoiusRender);
			if (sleep > TimeSpan.Zero)
			{
				await Console.RefreshAndDelay(sleep);
			}
			else
			{
				await Console.Refresh();
			}
			previoiusRender = DateTime.Now;
		}

		(int I, int J)? FindTileInMap(char[][] map, char c)
		{
			for (int j = 0; j < map.Length; j++)
			{
				for (int i = 0; i < map[j].Length; i++)
				{
					if (map[j][i] == c)
					{
						return (i, j);
					}
				}
			}
			return null;
		}

		async Task RenderWorldMapView()
		{
			Console.CursorVisible = false;

			var (width, height) = await GetWidthAndHeight();
			int heightCutOff = (int)(height * .80);
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
					if (i > midWidth - 4 && i < midWidth + 4 && j > midHeight - 2 && j < midHeight + 3)
					{
						int ci = i - (midWidth - 3);
						int cj = j - (midHeight - 1);
						string characterMapRender = character.Render;
						sb.Append(characterMapRender[cj * 8 + ci]);
						continue;
					}

					// tiles

					// compute the map location that this screen pixel represents
					int mapI = i - midWidth + character.I + 3;
					int mapJ = j - midHeight + character.J + 1;

					// compute the coordinates of the tile
					int tileI = mapI < 0 ? (mapI - 6) / 7 : mapI / 7;
					int tileJ = mapJ < 0 ? (mapJ - 3) / 4 : mapJ / 4;

					// compute the coordinates of the pixel within the tile's sprite
					int pixelI = mapI < 0 ? 6 + ((mapI + 1) % 7) : (mapI % 7);
					int pixelJ = mapJ < 0 ? 3 + ((mapJ + 1) % 4) : (mapJ % 4);

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
			await Console.SetCursorPosition(0, 0);
			await Console.Write(sb);
		}

		async Task RenderBattleView(string spriteLeft, string spriteRight)
		{
			Console.CursorVisible = false;

			var (width, height) = await GetWidthAndHeight();
			int midWidth = width / 2;
			int thirdHeight = height / 3;
			int textStartJ = thirdHeight + 7;

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
					if (j >= textStartJ)
					{
						int line = j - textStartJ - 1;
						int character = i - 1;
						if (i < width - 1 && character >= 0 && line >= 0 && line < combatText.Length && character < combatText[line].Length)
						{
							char ch = combatText[line][character];
							sb.Append(char.IsWhiteSpace(ch) ? ' ' : ch);
							continue;
						}
					}

					// character
					if (i > midWidth - 4 - 10 && i < midWidth + 4 - 10 && j > thirdHeight - 2 && j < thirdHeight + 3)
					{
						int ci = i - (midWidth - 3) + 10;
						int cj = j - (thirdHeight - 1);
						string characterMapRender = spriteLeft;
						sb.Append(characterMapRender[cj * 8 + ci]);
						continue;
					}

					// enemy
					if (i > midWidth - 4 + 10 && i < midWidth + 4 + 10 && j > thirdHeight - 2 && j < thirdHeight + 3)
					{
						int ci = i - (midWidth - 3) - 10;
						int cj = j - (thirdHeight - 1);
						string characterMapRender = spriteRight;
						sb.Append(characterMapRender[cj * 8 + ci]);
						continue;
					}

					sb.Append(' ');
				}
				if (!OperatingSystem.IsWindows() && j < height - 1)
				{
					sb.AppendLine();
				}
			}
			await Console.SetCursorPosition(0, 0);
			await Console.Write(sb);
		}

		async Task<(int Width, int Height)> GetWidthAndHeight()
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
					await Console.Clear();
					goto RestartRender;
				}
			}
			return (width, height);
		}
	}

	public enum EnemyType
	{
		Boar,
		Guard,
		GuardBoss,
		FinalBoss,
	}
}
