using System;
using System.Text;
using System.Threading;

// NOTE:
// Most of the magic numbers are related to the sizes
// of the sprites, which are 7 width by 4 height.

namespace Role_Playing_Game;

public partial class Program
{
	static Character? _character;
	static char[][]? _map;
	static DateTime previoiusRender = DateTime.Now;
	static int movesSinceLastBattle;
	static bool gameRunning = true;
	const double randomBattleChance = 1d / 10d;
	const int movesBeforeRandomBattle = 4;

	static Character Character
	{
		get => _character!;
		set => _character = value;
	}

	static char[][] Map
	{
		get => _map!;
		set => _map = value;
	}

	private static readonly string[] maptext =
	[
		"Move: arrow keys or (w, a, s, d)",
		"Check Status: [enter]",
		"Quit: [escape]",
	];

	private static readonly string[] defaultCombatText =
	[
		"1) attack",
		"2) run",
		"3) check status",
	];

	private static string[]? _combatText;

	static string[] CombatText
	{
		get => _combatText!;
		set => _combatText = value;
	}

	public static void Main()
	{
		Exception? exception = null;
		try
		{
			Console.CursorVisible = false;
			Initialize();
			OpeningScreen();
			while (gameRunning)
			{
				UpdateCharacter();
				HandleMapUserInput();
				if (gameRunning)
				{
					RenderWorldMapView();
					SleepAfterRender();
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
			Console.Clear();
			Console.WriteLine(exception?.ToString() ?? "Role Playing Game was closed.");
			Console.CursorVisible = true;
		}
	}

	static void Initialize()
	{
		Map = Maps.Town;
		Character = new();
		{
			var (i, j) = FindTileInMap(Map, 'X')!.Value;
			Character.I = i * 7;
			Character.J = j * 4;
		}
		Character.MapAnimation = Sprites.IdleRight;
	}

	static void OpeningScreen()
	{
		Console.SetCursorPosition(0, 0);
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine(" Role Playing Game");
		Console.WriteLine();
		Console.WriteLine(" You are about to embark on an epic adventure.");
		Console.WriteLine();
		Console.WriteLine(" Go find and kill the king in the castle... because why not?");
		Console.WriteLine();
		Console.WriteLine(" Note: This game is a work in progress.");
		Console.WriteLine();
		Console.Write(" Press [enter] to begin...");
		PressEnterToContiue();
	}

	static void UpdateCharacter()
	{
		if (Character.MapAnimation == Sprites.RunUp   && Character.MapAnimationFrame is 2 or 4 or 6) Character.J--;
		if (Character.MapAnimation == Sprites.RunDown && Character.MapAnimationFrame is 2 or 4 or 6) Character.J++;
		if (Character.MapAnimation == Sprites.RunLeft)  Character.I--;
		if (Character.MapAnimation == Sprites.RunRight) Character.I++;
		Character.MapAnimationFrame++;

		if (Character.Moved)
		{
			HandleCharacterMoved();
			Character.Moved = false;
		}
	}

	static void HandleCharacterMoved()
	{
		movesSinceLastBattle++;
		switch (Map[Character.TileJ][Character.TileI])
		{
			case 'i': SleepAtInn();            break;
			case 's': ShopAtStore();           break;
			case 'c': OpenChest();             break;
			case '0': TransitionMapToTown();   break;
			case '1': TransitionMapToField();  break;
			case '2': TransitionMapToCastle(); break;
			case 'g': FightGuardBoss();        break;
			case ' ': ChanceForRandomBattle(); break;
			case 'k': FightKing();             break;
			case 'h': HiddenWaterFountain();   break;
		}
	}

	static void RenderStatusString()
	{
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine(" Status");
		Console.WriteLine();
		Console.WriteLine($" Level:      {Character.Level}");
		Console.WriteLine($" Experience: {Character.Experience}/{Character.ExperienceToNextLevel}");
		Console.WriteLine($" Health:     {Character.Health}/{Character.MaxHealth}");
		Console.WriteLine($" Gold:       {Character.Gold}");
		Console.WriteLine($" Damage:     {Character.Damage}");
		Console.WriteLine();
		Console.Write(" Press [enter] to continue...");
		PressEnterToContiue();
	}

	static void RenderDeathScreen()
	{
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine(" You died...");
		Console.WriteLine();
		Console.WriteLine(" Game Over.");
		Console.WriteLine();
		Console.Write(" Press [enter] to continue...");
		PressEnterToContiue();
	}

	static void PressEnterToContiue()
	{
	GetInput:
		ConsoleKey key = Console.ReadKey(true).Key;
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

	static void OpenChest()
	{
		Character.Gold++;
		Map[Character.TileJ][Character.TileI] = 'e';
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine(" You found a chest! You open it and find some gold. :)");
		Console.WriteLine();
		Console.WriteLine($" Gold: {Character.Gold}");
		Console.WriteLine();
		Console.Write(" Press [enter] to continue...");
		PressEnterToContiue();
	}

	static void SleepAtInn()
	{
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine(" You enter the inn and stay the night...");
		Console.WriteLine();
		Console.WriteLine(" ZzzZzzZzz...");
		Console.WriteLine();
		Console.WriteLine(" Your health is restored.");
		Console.WriteLine();
		Console.Write(" Press [enter] to continue...");
		Character.Health = Character.MaxHealth;
		PressEnterToContiue();
	}

	static void HiddenWaterFountain()
	{
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine(" You walked into the wall and found");
		Console.WriteLine(" a hidden water fountain that sprays");
		Console.WriteLine(" Hawaiian Punch.");
		Console.WriteLine();
		Console.WriteLine(" Your health is restored.");
		Console.WriteLine();
		Console.Write(" Press [enter] to continue...");
		Character.Health = Character.MaxHealth;
		PressEnterToContiue();
	}

	static void TransitionMapToTown()
	{
		Map = Maps.Town;
		var (i, j) = FindTileInMap(Map, '1')!.Value;
		Character.I = i * 7;
		Character.J = j * 4;
	}

	static void TransitionMapToField()
	{
		char c = Map == Maps.Town ? '0' : '2';
		Map = Maps.Field;
		var (i, j) = FindTileInMap(Map, c)!.Value;
		Character.I = i * 7;
		Character.J = j * 4;
	}

	static void TransitionMapToCastle()
	{
		Map = Maps.Castle;
		var (i, j) = FindTileInMap(Map, '1')!.Value;
		Character.I = i * 7;
		Character.J = j * 4;
	}

	static void ShopAtStore()
	{
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine(" You enter the store...");
		Console.WriteLine();
		if (Character.Gold >= 6)
		{
			int damage = Character.Gold / 6;
			Character.Gold -= damage * 6;
			Character.Damage += damage;
			Console.WriteLine($" You pay {damage * 6} gold to train your kung fu.");
			Console.WriteLine();
			Console.WriteLine($" You gained +{damage} damage on your attacks.");
		}
		else if (Character.Damage >= 3)
		{
			Console.WriteLine($" \"You have learned all that I can teach you.\"");
		}
		else
		{
			Console.WriteLine($" \"Bring me 6 gold and I will teach you kung fu.\"");
		}
		Console.WriteLine();
		Console.Write(" Press [enter] to continue...");
		PressEnterToContiue();
	}

	static void ChanceForRandomBattle()
	{
		if (Map == Maps.Town)
		{
			return;
		}
		if (movesSinceLastBattle > movesBeforeRandomBattle && Random.Shared.NextDouble() < randomBattleChance)
		{
			Battle(Map == Maps.Castle ? EnemyType.Guard : EnemyType.Boar, out _);
			if (!gameRunning)
			{
				return;
			}
		}
	}

	static void FightGuardBoss()
	{
		Battle(EnemyType.GuardBoss, out bool ranAway);
		if (!gameRunning)
		{
			return;
		}
		else if (ranAway)
		{
			Character.J++;
			Character.MapAnimation = Sprites.RunDown;
		}
		else
		{
			Map[Character.TileJ][Character.TileI] = ' ';
		}
	}

	static void FightKing()
	{
		Battle(EnemyType.FinalBoss, out bool ranAway);
		if (!gameRunning)
		{
			return;
		}
		else if (ranAway)
		{
			Character.J++;
			Character.MapAnimation = Sprites.RunDown;
		}
		else
		{
			Console.Clear();
			Console.WriteLine();
			Console.WriteLine(" You beat the king!");
			Console.WriteLine();
			Console.WriteLine(" Woohoo! Good job or whatever.");
			Console.WriteLine();
			Console.WriteLine(" Game Over.");
			Console.WriteLine();
			Console.Write(" Press [enter] to continue...");
			PressEnterToContiue();
			gameRunning = false;
			return;
		}
	}

	static void HandleMapUserInput()
	{
		while (Console.KeyAvailable)
		{
			ConsoleKey key = Console.ReadKey(true).Key;
			switch (key)
			{
				case
					ConsoleKey.UpArrow    or ConsoleKey.W or
					ConsoleKey.DownArrow  or ConsoleKey.S or
					ConsoleKey.LeftArrow  or ConsoleKey.A or
					ConsoleKey.RightArrow or ConsoleKey.D:
					if (Character.IsIdle)
					{
						var (tileI, tileJ) = key switch
						{
							ConsoleKey.UpArrow    or ConsoleKey.W => (Character.TileI, Character.TileJ - 1),
							ConsoleKey.DownArrow  or ConsoleKey.S => (Character.TileI, Character.TileJ + 1),
							ConsoleKey.LeftArrow  or ConsoleKey.A => (Character.TileI - 1, Character.TileJ),
							ConsoleKey.RightArrow or ConsoleKey.D => (Character.TileI + 1, Character.TileJ),
							_ => throw new Exception("bug"),
						};
						if (Maps.IsValidCharacterMapTile(Map, tileI, tileJ))
						{
							switch (key)
							{
								case ConsoleKey.UpArrow or ConsoleKey.W:
									Character.J--;
									Character.MapAnimation = Sprites.RunUp;
									break;
								case ConsoleKey.DownArrow or ConsoleKey.S:
									Character.J++;
									Character.MapAnimation = Sprites.RunDown;
									break;
								case ConsoleKey.LeftArrow or ConsoleKey.A:
									Character.MapAnimation = Sprites.RunLeft;
									break;
								case ConsoleKey.RightArrow or ConsoleKey.D:
									Character.MapAnimation = Sprites.RunRight;
									break;
							}
						}
					}
					break;
				case ConsoleKey.Enter:
					RenderStatusString();
					break;
				case ConsoleKey.Escape:
					gameRunning = false;
					return;
			}
		}
	}

	static void Battle(EnemyType enemyType, out bool ranAway)
	{
		movesSinceLastBattle = 0;
		ranAway = false;

		int enemyHealth = enemyType switch
		{
			EnemyType.Boar      => 03,
			EnemyType.GuardBoss => 20,
			EnemyType.Guard     => 10,
			EnemyType.FinalBoss => 60,
			_ => 1,
		};

		switch (enemyType)
		{
			case EnemyType.Boar:
				CombatText =
				[
					"You were attacked by a wild boar!",
					"1) attack",
					"2) run",
					"3) check status",
				];
				break;
			case EnemyType.GuardBoss:
				if (Character.Level < 2)
				{
					CombatText =
					[
						"You approached the castle guard.",
						"He looks tough. You should probably",
						"run away and come back when you are",
						"stronger.",
						"1) attack",
						"2) run",
						"3) check status",
					];
				}
				else
				{
					CombatText =
					[
						"You approached the castle guard.",
						"1) attack",
						"2) run",
						"3) check status",
					];
				}
				break;
			case EnemyType.Guard:
				CombatText =
				[
					"You were attacked by a castle guard!",
					"1) attack",
					"2) run",
					"3) check status",
				];
				break;
			case EnemyType.FinalBoss:
				if (Character.Level < 3)
				{
					CombatText =
					[
						"You approached the evil king.",
						"He looks tough. You should probably",
						"run away and come back when you are",
						"stronger.",
						"1) attack",
						"2) run",
						"3) check status",
					];
				}
				else
				{
					CombatText =
					[
						"You approached the evil king.",
						"1) attack",
						"2) run",
						"3) check status",
					];
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
				_ => [Sprites.Error],
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
			while (Console.KeyAvailable)
			{
				ConsoleKey key = Console.ReadKey(true).Key;
				switch (key)
				{
					case ConsoleKey.D1 or ConsoleKey.NumPad1:
						if (!pendingConfirmation)
						{
							switch (Random.Shared.Next(2))
							{
								case 0:
									frameLeft = 0;
									animationLeft = Sprites.PunchRight;
									CombatText =
									[
										"You attacked and did damage!",
										"",
										"Press [enter] to continue...",
									];
									enemyHealth -= Character.Damage;
									break;
								case 1:
									frameLeft = 0;
									animationLeft = Sprites.FallLeft;
									CombatText =
									[
										"You attacked, but the enemy was",
										"faster and you took damage!",
										"",
										"Press [enter] to continue...",
									];
									Character.Health--;
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
								EnemyType.Boar => Random.Shared.Next(10) < 9,
								EnemyType.Guard => Random.Shared.Next(10) < 7,
								_ => true,
							};
							if (success)
							{
								Console.Clear();
								Console.WriteLine();
								Console.WriteLine(" You ran away.");
								Console.WriteLine();
								Console.Write(" Press [enter] to continue...");
								PressEnterToContiue();
								ranAway = true;
								return;
							}
							else
							{
								frameLeft = 0;
								animationLeft = Sprites.FallLeft;
								CombatText =
								[
									"You tried to run away but the enemy",
									"attacked you from behind and you took",
									"damage.",
									"",
									"Press [enter] to continue...",
								];
								Character.Health--;
								pendingConfirmation = true;
							}
						}
						break;
					case ConsoleKey.D3 or ConsoleKey.NumPad3:
						if (!pendingConfirmation)
						{
							RenderStatusString();
							if (!gameRunning)
							{
								return;
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
							CombatText = defaultCombatText;
							if (Character.Health <= 0)
							{
								RenderDeathScreen();
								gameRunning = false;
								return;
							}
							if (enemyHealth <= 0)
							{
								if (enemyType is EnemyType.FinalBoss)
								{
									return;
								}
								int experienceGain = enemyType switch
								{
									EnemyType.Boar => 1,
									EnemyType.GuardBoss => 20,
									EnemyType.Guard => 10,
									EnemyType.FinalBoss => 9001, // ITS OVER 9000!
									_ => 0,
								};
								Console.Clear();
								Console.WriteLine();
								Console.WriteLine(" You defeated the enemy!");
								Console.WriteLine();
								Console.WriteLine($" You gained {experienceGain} experience.");
								Console.WriteLine();
								Character.Experience += experienceGain;
								if (Character.Experience >= Character.ExperienceToNextLevel)
								{
									Character.Level++;
									Character.Experience = 0;
									Character.ExperienceToNextLevel *= 2;
									Console.WriteLine($" You grew to level {Character.Level}.");
									Console.WriteLine();
								}
								Console.WriteLine();
								Console.Write(" Press [enter] to continue...");
								PressEnterToContiue();
								return;
							}
						}
						break;
					case ConsoleKey.Escape:
						gameRunning = false;
						return;
				}
			}
			RenderBattleView(animationLeft[frameLeft], animationRight[frameRight]);
			SleepAfterRender();
		}
	}

	static void SleepAfterRender()
	{
		// frame rate control
		// battle view is currently targeting 30 frames per second
		DateTime now = DateTime.Now;
		TimeSpan sleep = TimeSpan.FromMilliseconds(33) - (now - previoiusRender);
		if (sleep > TimeSpan.Zero)
		{
			Thread.Sleep(sleep);
		}
		previoiusRender = DateTime.Now;
	}

	static (int I, int J)? FindTileInMap(char[][] map, char c)
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

	static void RenderWorldMapView()
	{
		Console.CursorVisible = false;

		var (width, height) = GetWidthAndHeight();
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
					string characterMapRender = Character.Render;
					sb.Append(characterMapRender[cj * 8 + ci]);
					continue;
				}

				// tiles

				// compute the map location that this screen pixel represents
				int mapI = i - midWidth  + Character.I + 3;
				int mapJ = j - midHeight + Character.J + 1;

				// compute the coordinates of the tile
				int tileI = mapI < 0 ? (mapI - 6) / 7 : mapI / 7;
				int tileJ = mapJ < 0 ? (mapJ - 3) / 4 : mapJ / 4;

				// compute the coordinates of the pixel within the tile's sprite
				int pixelI = mapI < 0 ? 6 + ((mapI + 1) % 7) : (mapI % 7);
				int pixelJ = mapJ < 0 ? 3 + ((mapJ + 1) % 4) : (mapJ % 4);

				// render pixel from map tile
				string tileRender = Maps.GetMapTileRender(Map, tileI, tileJ);
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
	}

	static void RenderBattleView(string spriteLeft, string spriteRight)
	{
		Console.CursorVisible = false;

		var (width, height) = GetWidthAndHeight();
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
					if (i < width - 1 && character >= 0 && line >= 0 && line < CombatText.Length && character < CombatText[line].Length)
					{
						char ch = CombatText[line][character];
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
		Console.SetCursorPosition(0, 0);
		Console.Write(sb);
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
				if (Console.BufferWidth != width)   Console.BufferWidth = width;
			}
			catch (Exception)
			{
				Console.Clear();
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
