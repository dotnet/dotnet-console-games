namespace Console_Monsters.Maps;

class Route1 : MapBase
{
	private readonly char[][] spriteSheet = new char[][]
		{
			"gggfgggggf11fgggggfg".ToCharArray(),
			"gggfgggggf  fgggggfg".ToCharArray(),
			"gggfgggggf  fgggggfg".ToCharArray(),
			"gggfgggggf  fgggggfg".ToCharArray(),
			"gggfgggggf  fgggggfg".ToCharArray(),
			"gggfffffff  fffffffg".ToCharArray(),
			"gggfgggggg  ggggggfg".ToCharArray(),
			"gggfgggggg  ggggggfg".ToCharArray(),
			"gggfgggggT        fg".ToCharArray(),
			"gggfŕŕŕŕŕTrrrr    fg".ToCharArray(),
			"gggTgggggTGGGGGGGGfg".ToCharArray(),
			"gggTgggggTGGGGGGGGfg".ToCharArray(),
			"gggTgggggTGGGGGGGGfg".ToCharArray(),
			"gggTŕŕŕŕŕTGGGGGGGGfg".ToCharArray(),
			"gggTgggggggg      fg".ToCharArray(),
			"gggTgggggggg      fg".ToCharArray(),
			"gggTgggggggg  GGGGfg".ToCharArray(),
			"gggTTTŕŕŕŕTTTTGGGGfg".ToCharArray(),
			"gggfgggggggg  GGGGfg".ToCharArray(),
			"gggfgggggggg  GGGGfg".ToCharArray(),
			"gggfgg        ggggfg".ToCharArray(),
			"gggfgg        ggggfg".ToCharArray(),
			"gggfgg  ggggggggggfg".ToCharArray(),
			"gggfŕşrrŕşŕŕŕŕŕŕŕŕfg".ToCharArray(),
			"gggfgg            fg".ToCharArray(),
			"gggfgg            fg".ToCharArray(),
			"gggfgg      GGGG  fg".ToCharArray(),
			"gggfTTTTTTTTGGGGrrfg".ToCharArray(),
			"gggfggggggggGGGG  fg".ToCharArray(),
			"gggfggggggggGGGG  fg".ToCharArray(),
			"gggf              fg".ToCharArray(),
			"gggTrr   srrrrrrrrTg".ToCharArray(),
			"gggTggGGGG  ggGGGGTg".ToCharArray(),
			"gggTggGGGG  ggGGGGTg".ToCharArray(),
			"gggTggGGGG  GGGGggTg".ToCharArray(),
			"gggTGGGGgg  GGGGggTg".ToCharArray(),
			"gggTffffffGGffffffTg".ToCharArray(),
			"gggTgggggfGGfgggggTg".ToCharArray(),
			"gggTgggggfGGfgggggTg".ToCharArray(),
			"gggfgggggf00fgggggfg".ToCharArray(),
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
					promptText = new string[]
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
				map = new PaletTown();
				map.SpawnCharacterOn('1');
				break;
			case '1':
				map = new Route2();
				map.SpawnCharacterOn('0');
				break;
			case 'G':
				if (!DisableBattle && Random.Shared.Next(2) is 0) // BATTLE CHANCE
				{
					Console.Clear();
					if (!DisableBattleTransition)
					{
						BattleTransition.Random();
					}
					BattleScreen.Render(PlayerMonster, OpponentMonster);
					Battle();
					ConsoleHelper.PressToContinue();
				}
				break;
		}
	}
}
