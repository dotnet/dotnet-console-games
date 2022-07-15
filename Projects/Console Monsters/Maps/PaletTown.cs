namespace Console_Monsters.Maps;

class PaletTown : MapBase
{
	public Scientist scientist;

	public PaletTown()
	{
		scientist = new();
	}

	private readonly char[][] spriteSheet = new char[][]
		{
			"ffffffffff11ffffffff".ToCharArray(),
			"fg                gf".ToCharArray(),
			"fg  bbbb    cccc  gf".ToCharArray(),
			"fg  bbbb    cccc  gf".ToCharArray(),
			"fg sb2bb   scccc  gf".ToCharArray(),
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

	public override char[][] SpriteSheet => spriteSheet;

	public override string GetMapTileRender(int i, int j)
	{
		if (j < 0 || j >= SpriteSheet.Length || i < 0 || i >= SpriteSheet[j].Length)
		{
			return Sprites.Open;
		}

		string b((int I, int J) offset) => Sprites.House[j - offset.J, i - offset.I];
		string c((int I, int J) offset) => Sprites.House[j - offset.J, i - offset.I];

		return SpriteSheet[j][i] switch
		{
			// spawn
			'X' => Sprites.Open,
			// actions
			'0' => Sprites.Door,
			'1' => Sprites.ArrowHeavyUp,
			'2' => Sprites.Door,
			// Buildings
			'b' => b(FindTileInMap('b')!.Value),
			'c' => c(FindTileInMap('c')!.Value),
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
			'z' => Sprites.Door,
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
			's' or 'a' => true,
			'ś' => true,
			'o' => true,
			_ => false,
		};
	}

	public override void InteractWithMapTile(int i, int j)
	{
		if (j >= 0 && j < SpriteSheet.Length && i >= 0 && i < SpriteSheet[j].Length)
		{
			switch (SpriteSheet[j][i])
			{
				case 's' or 'a':
					promptText = new string[]
					{
						"Sign Says:",
						"Hello! I am sign. :P",
					};
					break;
				case 'ś':
					promptText = new string[]
					{
						"Sign #2 Says:",
						"Hello! I am sign #2. :P",
					};
					break;
				case 'o':
					promptText = scientist.Dialogue;
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
			'X' => true,
			'g' => true,
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
				map = new Center1();
				map.SpawnCharacterOn('0');
				break;
			case '1':
				map = new Route1();
				map.SpawnCharacterOn('0');
				break;
			case '2':
				map = new House1();
				map.SpawnCharacterOn('0');
				break;
		}
	}
}
