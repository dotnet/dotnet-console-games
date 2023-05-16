namespace Console_Monsters.Maps;

class Route2 : MapBase
{
	private Camper camper;

	public Route2()
	{
		camper = new();
	}

	private static readonly char[][] spriteSheet = new char[][]
		{
			"TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT".ToCharArray(),
			"TaaaaaaaaaTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT".ToCharArray(),
			"TaaaaaaaaaTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT".ToCharArray(),
			"TaaaaaaaaaTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT".ToCharArray(),
			"TaaaaaaaaaTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT".ToCharArray(),
			"TaaaaaaaaafffffffffffffffffffffffffffffffffffffffffffffffTTTTT".ToCharArray(),
			"TaaaaaabaagggggggfggGGGGTTTTf    ggggggggg       GGGGGGGfTTTTT".ToCharArray(),
			"TTTTTfgggggggggggfgggGGGTTTTf    ggggggggg       GGGGGGGfTTTTT".ToCharArray(),
			"     2gggggGGGGGGggggtGGTTTTf    TTTTTTTTTTTTTTTTGGGGGGGfTTTTT".ToCharArray(),
			"     2gggggGGGGGGfggggGGTTTTf    GGGGGGGGgggggggT       fTTTTT".ToCharArray(),
			"TTTTTffffffffGGGGfggggggTTTTf    GGGGGGGGgggggggT       fTTTTT".ToCharArray(),
			"TTTTTfGGGGG      TŕŕŕŕŕŕTTTTTTTTTTGGGGGGGgggggggT       ffffff".ToCharArray(),
			"TTTTTfGGGGG      TGGG            TTTTTTTT       T       1     ".ToCharArray(),
			"TTTTTf    TTTTTTTTGGG            GGGGGGGG       T       1     ".ToCharArray(),
			"TTTTTf    ggggGGGGGGGgggTTTTTs   GGGGGGGG      eTTTTTTTTffffff".ToCharArray(),
			"TTTTTf    ggggGGGGGGGgggTTTTT    TTTTTTTTTTTTTTTTgggggggfTTTTT".ToCharArray(),
			"TTTTTfffffffffffffffffffTTTTTf00fTTTTTTTTTTTTTTTTffffffffTTTTT".ToCharArray(),
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
			// spawn
			'X' => Sprites.Open,

			// Items
			'e' => Sprites.MonsterBoxPickableOnGround,
			'h' => Sprites.MonsterBox,

			// actions
			'0' => Sprites.ArrowHeavyDown,
			'1' => Sprites.ArrowHeavyRight,
			'2' => Sprites.ArrowHeavyLeft,
			'b' => camper.Sprite,

			// No actions
			's' => Sprites.SignALeft,
			'f' => Sprites.Fence,
			'a' => Sprites.Camping6x9.Get(Subtract((i, j), FindTileInMap('a')!.Value).Reverse()),

			// Nature
			'g' => Sprites.GrassDec,
			'G' => Sprites.Grass,
			't' => Sprites.Tree,
			'T' => Sprites.Tree2,
			'ŕ' => Sprites.HalfRockGrass,

			// Other
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
			'b' => true,
			_ => false,
		};
	}

	public override void InteractWithMapTile(int i, int j)
	{
		if (j >= 0 && j < SpriteSheet.Length && i >= 0 && i < SpriteSheet[j].Length)
		{
			switch (SpriteSheet[j][i])
			{
				case 'b':
					PromptText = camper.Dialogue;
					break;
				case 's':
					PromptText = new string[]
					{
						"Sign Says:",
						"Vejle Town <----- -----> Aalborg City",
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
			'2' => true,
			'g' => true,
			'X' => true,
			'G' => true,
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
				Map = new Route1();
				Map.SpawnPlayerOn('1');
				break;
			case '1':
				Map = new Western();
				Map.SpawnPlayerOn('0');
				break;
			case '2':
				
				break;
			case 'G':
				if (!DisableBattle && Random.Shared.Next(2) is 0) // BATTLE CHANCE
				{
					Console.Clear();
					if (!DisableBattleTransition)
					{
						BattleTransition.Random();
					}
					BattleScreen.Render(partyMonsters[0], MonsterBase.GetRandom());
					//Battle();
					ConsoleHelper.PressToContinue();
				}
				break;
		}
	}
}
