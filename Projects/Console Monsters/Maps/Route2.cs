namespace Console_Monsters.Maps;

class Route2 : MapBase
{
	public Camper camper;

	public Route2()
	{
		camper = new();
	}

	private static readonly char[][] spriteSheet = new char[][]
		{
			"TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT".ToCharArray(),
			"TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT".ToCharArray(),
			"TTTTaaaaaaaaaTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT".ToCharArray(),
			"TTTTaaaaaaaaaTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT".ToCharArray(),
			"TTTTaaaaaaaaaTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT".ToCharArray(),
			"TTTTaaaaaaaaaffffffffffffffffffffffffffffffffffffffffffffTTTTT".ToCharArray(),
			"TTTTaaaaaaaaagggggggggggTTTTf    ggggggggg       GGGGGGGfTTTTT".ToCharArray(),
			"TTTTaaaaaabaagggggggggggTTTTf    ggggggggg       GGGGGGGfTTTTT".ToCharArray(),
			"TTTTTfggggggggggggggggggTTTTf    TTTTTTTTTTTTTTTTGGGGGGGfTTTTT".ToCharArray(),
			"TTTTTfggggggggggggggggggTTTTf    GGGGGGGGgggggggT       fTTTTT".ToCharArray(),
			"TTTTTfgggggggggggggggggTTTTTf    GGGGGGGGgggggggT       fTTTTT".ToCharArray(),
			"TTTTTfgggggggggggTTTTTTTTTTTTTTTTTGGGGGGGgggggggT       ffffff".ToCharArray(),
			"TTTTTfgggggggggggTgg             TTTTTTTT       T       1     ".ToCharArray(),
			"TTTTTfgggggggggggTgg             GGGGGGGG       T       1     ".ToCharArray(),
			"TTTTTfgggggggggggTgg    TTTTTs   GGGGGGGG      eTTTTTTTTffffff".ToCharArray(),
			"TTTTTfggggggggggggggggggggggT X  TTTTTTTTTTTTTTTTgggggggfTTTTT".ToCharArray(),
			"TTTTTfffffffffffffffffffffffff00fffffffffffffffffffffffffTTTTT".ToCharArray(),
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
			// no actions
			's' => Sprites.SignARight,
			'f' => Sprites.Fence,
			'g' => Sprites.GrassDec,
			'G' => Sprites.Grass,
			't' => Sprites.Tree,
			'T' => Sprites.Tree2,
			'a' => Sprites.Camping6x9.Get(Subtract((i, j), FindTileInMap('a')!.Value).Reverse()),
			'b' => camper.Sprite,
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
