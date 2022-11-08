namespace Console_Monsters.Maps;

class Route1 : MapBase
{
	private static readonly char[][] spriteSheet = new char[][]
		{
			"ggggggggggggfgggggf11fgggggfgggggggggg".ToCharArray(),
			"ggggggggggggfgggggf  fgggggfgggggggggg".ToCharArray(),
			"ggggggggggggfgggggf  fgggggfgggggggggg".ToCharArray(),
			"ggggggggggggfgggggf  fgggggfgggggggggg".ToCharArray(),
			"ggggggggggggfgggggf  fgggggfgggggggggg".ToCharArray(),
			"ggggggggggggfffffff  fffffffgggggggggg".ToCharArray(),
			"ggggggggggggfgggggg  ggggggfgggggggggg".ToCharArray(),
			"ggggggggggggfgggggg  ggggggfgggggggggg".ToCharArray(),
			"ggggggggggggfgggggT        fgggggggggg".ToCharArray(),
			"ggggggggggggfŕŕŕŕŕTrrrr    fgggggggggg".ToCharArray(),
			"ggggggggggggTgggggTGGGGGGGGfgggggggggg".ToCharArray(),
			"ggggggggggggTgggggTGGGGGGGGfgggggggggg".ToCharArray(),
			"ggggggggggggTgggggTGGGGGGGGfgggggggggg".ToCharArray(),
			"ggggggggggggTŕŕŕŕŕTGGGGGGGGfgggggggggg".ToCharArray(),
			"ggggggggggggTgggggggg      fgggggggggg".ToCharArray(),
			"ggggggggggggTgggggggg      fgggggggggg".ToCharArray(),
			"ggggggggggggTgggggggg  GGGGfgggggggggg".ToCharArray(),
			"ggggggggggggTTTŕŕŕŕTTTTGGGGfgggggggggg".ToCharArray(),
			"ggggggggggggfgggggggg  GGGGfgggggggggg".ToCharArray(),
			"ggggggggggggfgggggggg  GGGGfgggggggggg".ToCharArray(),
			"ggggggggggggfgg        ggggfgggggggggg".ToCharArray(),
			"ggggggggggggfgg        ggggfgggggggggg".ToCharArray(),
			"ggggggggggggfgg  ggggggggggfgggggggggg".ToCharArray(),
			"ggggggggggggfŕşrrŕşŕŕŕŕŕŕŕŕfgggggggggg".ToCharArray(),
			"ggggggggggggfgg            fgggggggggg".ToCharArray(),
			"ggggggggggggfgg            fgggggggggg".ToCharArray(),
			"ggggggggggggfgg      GGGG  fgggggggggg".ToCharArray(),
			"ggggggggggggfTTTTTTTTGGGGrrfgggggggggg".ToCharArray(),
			"ggggggggggggfggggggggGGGG  fgggggggggg".ToCharArray(),
			"ggggggggggggfggggggggGGGG  fgggggggggg".ToCharArray(),
			"ggggggggggggf              fgggggggggg".ToCharArray(),
			"ggggggggggggTrr   srrrrrrrrTgggggggggg".ToCharArray(),
			"ggggggggggggTggGGGG  ggGGGGTgggggggggg".ToCharArray(),
			"ggggggggggggTggGGGG  ggGGGGTgggggggggg".ToCharArray(),
			"ggggggggggggTggGGGG  GGGGggTgggggggggg".ToCharArray(),
			"ggggggggggggTGGGGgg  GGGGggTgggggggggg".ToCharArray(),
			"ggggggggggggTffffffGGffffffTgggggggggg".ToCharArray(),
			"ggggggggggggTgggggfGGfgggggTgggggggggg".ToCharArray(),
			"ggggggggggggTgggggfGGfgggggTgggggggggg".ToCharArray(),
			"ggggggggggggfgggggf00fgggggfgggggggggg".ToCharArray(),

		};

	public override char[][] SpriteSheet => spriteSheet;

	public override string GetMapTileRender(int i, int j)
	{
		if (j < 0 || j >= SpriteSheet.Length || i < 0 || i >= SpriteSheet[j].Length)
		{
			return Sprites.Open;
		}
		return SpriteSheet[j][i] switch
		{
			// actions
			'0' => Sprites.ArrowHeavyDown,
			'1' => Sprites.ArrowHeavyUp,
			// Decor
			's' => Sprites.SignALeft,
			'f' => Sprites.Fence,
			// Nature
			'g' => Sprites.GrassDec,
			'G' => Sprites.Grass,
			'T' => Sprites.Tree2,
			'r' => Sprites.HalfRock,
			'ŕ' => Sprites.HalfRockGrass,
			'ş' => Sprites.HalfRockStairsGrass,
			// Extra
			' ' => Sprites.Open,
			_ => Sprites.Error,
		};
	}

	public override bool CanInteractWithMapTile(int i, int j)
	{
		if (j < 0 || j >= SpriteSheet.Length || i < 0 || i >= SpriteSheet[j].Length)
		{
			return false;
		}
		return SpriteSheet[j][i] switch
		{
			's' => true,
			_ => false,
		};
	}

	public override void InteractWithMapTile(int i, int j)
	{
		if (j >= 0 && j < SpriteSheet.Length && i >= 0 && i < SpriteSheet[j].Length)
		{
			switch (SpriteSheet[j][i])
			{
				case 's':
					PromptText = new string[]
					{
						"Sign Says:",
						"Hello! I am a sign. :P",
					};
					break;
			}
		}
	}

	public override bool IsValidCharacterMapTile(int i, int j)
	{
		if (j < 0 || j >= SpriteSheet.Length || i < 0 || i >= SpriteSheet[j].Length)
		{
			return false;
		}
		char c = SpriteSheet[j][i];
		return c switch
		{
			' ' => true,
			'0' => true,
			'1' => true,
			'g' => true,
			'G' => true,
			'ş' => true,
			_ => false,
		};
	}

	public override void PerformTileAction(int i, int j)
	{
		if (j < 0 || j >= SpriteSheet.Length || i < 0 || i >= SpriteSheet[j].Length)
		{
			return;
		}
		switch (SpriteSheet[j][i])
		{
			case '0':
				Map = new PaletTown();
				Map.SpawnPlayerOn('1');
				break;
			case '1':
				Map = new Route2();
				Map.SpawnPlayerOn('0');
				break;
			case 'G':
				//SpawnType = "Grass";
				if (!DisableBattle && IsAnyAvailableMonster() && GetFirstAvailableMonster() != new _ErrorMonster() && Random.Shared.Next(2) is 0) // BATTLE CHANCE
				{
					Console.Clear();
					if (!DisableBattleTransition)
					{
						BattleTransition.Random();
					}
					Type[] typesThatCanSpawn =
					{
						typeof(Fox),
						//typeof(Ant),
						//typeof(Goat),
						//typeof(Owl),
						//typeof(Pig),
						//typeof(Pinecone),
						//typeof(Ladybug),
					};
					Type typeOfMonsterToSpawn = Random.Shared.Choose(typesThatCanSpawn);
					MonsterBase OpponentMonster = (MonsterBase)Activator.CreateInstance(typeOfMonsterToSpawn)!;
					MonsterBase.WildMonster(OpponentMonster);
					MonsterBase PlayerMonster = GetFirstAvailableMonster();
					BattleScreen.Render(PlayerMonster, OpponentMonster);
					Battle(PlayerMonster, OpponentMonster);
					ConsoleHelper.PressToContinue();
				}
				break;
		}
	}
}
