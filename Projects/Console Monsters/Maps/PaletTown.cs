namespace Console_Monsters.Maps;

class PaletTown : MapBase
{
	public Scientist scientist;

	public PaletTown()
	{
		scientist = new();
	}

	public override char[][] SpriteSheet => new char[][]
		{
			"ffffffffff11ffffffff".ToCharArray(),
			"fg                gf".ToCharArray(),
			"fg  RMMj    RMMj  gf".ToCharArray(),
			"fg  hkky    hkky  gf".ToCharArray(),
			"fg su2lU   sudlU  gf".ToCharArray(),
			"fg  p             gf".ToCharArray(),
			"fg                gf".ToCharArray(),
			"fg        RMMMMj  gf".ToCharArray(),
			"fg  FFFa  hkkkky  gf".ToCharArray(),
			"fg  gggg  hkkkky  gf".ToCharArray(),
			"fg  gggg  ul0llU  gf".ToCharArray(),
			"fg         n      gf".ToCharArray(),
			"fg      X         gf".ToCharArray(),
			"fg  o     FFFśFF  gf".ToCharArray(),
			"fgggWWWW  gggggg  gf".ToCharArray(),
			"fgggWwwW  gggggg  gf".ToCharArray(),
			"fgggWwwW          gf".ToCharArray(),
			"ffggWwwWffffffffffff".ToCharArray(),
			"ffffWwwWffffffffffff".ToCharArray(),
			"ggggWwwWggggggfggggg".ToCharArray(),
			"ggggWwwWggggggfggggg".ToCharArray(),
			"ggggWwwWGGGGGGfggggg".ToCharArray(),
		};

	public override string GetMapTileRender(int tileI, int tileJ)
	{
		char[][] s = map.SpriteSheet;
		if (tileJ < 0 || tileJ >= s.Length || tileI < 0 || tileI >= s[tileJ].Length)
		{
			return Sprites.Open;
		}
		return s[tileJ][tileI] switch
		{
			// spawn
			'X' => Sprites.Open,
			// actions
			'0' => Sprites.Door,
			'1' => Sprites.ArrowHeavyUp,
			'2' => Sprites.Door,
			// Buildings
			'd' => Sprites.Door,
			'l' => Sprites.LowWindow,
			'h' => Sprites.BuildingLeft,
			'u' => Sprites.BuildingBaseLeft,
			'y' => Sprites.BuildingRight,
			'U' => Sprites.BuildingBaseRight,
			'M' => Sprites.MiddleRoof,
			'R' => Sprites.TopRoofLeft,
			'j' => Sprites.TopRoofRight,
			'k' => Sprites.MiddleWindow,
			// Decor
			'a' => Sprites.SignARight,
			's' => Sprites.SignALeft,
			'ś' => Sprites.SignALeft,
			'f' => Sprites.Fence,
			'F' => Sprites.FenceLow,
			// Nature
			'w' => Sprites.Water,
			'g' => Sprites.GrassDec,
			// NPCs
			'n' => Sprites.NPC1,
			'o' => scientist.Sprite,
			'p' => Sprites.NPC5,
			// Extra
			'W' => Sprites.Wall_0000,
			' ' => Sprites.Open,
			_ => Sprites.Error,
		};
	}

	public override void InteractWithMapTile(int tileI, int tileJ)
	{
		char[][] s = map.SpriteSheet;


		Interact(tileI, tileJ + 1);
		Interact(tileI, tileJ - 1);
		Interact(tileI - 1, tileJ);
		Interact(tileI + 1, tileJ);

		void Interact(int i, int j)
		{
			if (j >= 0 && j < s.Length && i >= 0 && i < s[j].Length)
			{
				if (s[j][i] is 's' or 'a')
				{
					promptText = new string[]
						{
							"Sign Says:",
							"Hello! I am sign. :P",
						};
				}
				if (s[j][i] is 'ś')
				{
					promptText = new string[]
						{
							"Sign #2 Says:",
							"Hello! I am sign #2. :P",
						};
				}
				if (s[j][i] is 'o')
				{
					promptText = scientist.Dialogue;
				}
			}
		}
	}

	public override bool IsValidCharacterMapTile(int tileI, int tileJ)
	{
		char[][] s = map.SpriteSheet;
		if (tileJ < 0 || tileJ >= s.Length || tileI < 0 || tileI >= s[tileJ].Length)
		{
			return false;
		}
		char c = s[tileJ][tileI];
		return c switch
		{
			' ' => true,
			'0' => true,
			'1' => true,
			'2' => true,
			'X' => true,
			'g' => true,
			_ => false,
		};
	}

	public override void PerformTileAction()
	{
		var (i, j) = WorldToTile(character.I, character.J);
		char[][] s = map.SpriteSheet;
		switch (s[j][i])
		{
			case '0':
				map = new Center1();
				SpawnCharacterOn('0');
				break;
			case '1':
				map = new Route1();
				SpawnCharacterOn('0');
				break;
			case '2':
				map = new House1();
				SpawnCharacterOn('0');
				break;
			case 'G':
				if (!DisableBattle && Random.Shared.Next(2) is 0) // BATTLE CHANCE
				{
					Console.Clear();
					if (!DisableBattleTransition)
					{
						Renderer.RenderBattleTransition();
					}
					Renderer.RenderBattleView();
					PressEnterToContiue();
				}
				break;
		}
	}
}
