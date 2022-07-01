namespace Console_Monsters.Maps;

class Route1 : Map
{
	public override char[][] SpriteSheet => new char[][]
		{
			"gggfgggggf33fgggggfg".ToCharArray(),
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

	public override string GetMapTileRender(int tileI, int tileJ)
	{
		char[][] s = map.SpriteSheet;
		if (tileJ < 0 || tileJ >= s.Length || tileI < 0 || tileI >= s[tileJ].Length)
		{
			return Sprites.Open;
		}
		return s[tileJ][tileI] switch
		{
			//Game 
			'X' => Sprites.Open,
			'0' => Sprites.ArrowDown,
			'1' => Sprites.ArrowUp,
			'2' => Sprites.ArrowDown,
			'3' => Sprites.ArrowUp,

			//Buildings
			'b' => Sprites.BuildingSmall,
			'v' => Sprites.VetSmall,
			'S' => Sprites.Store,
			'd' => Sprites.Door,
			'o' => Sprites.LowWindowSideLeft,
			'l' => Sprites.LowWindow,
			'h' => Sprites.BuildingLeft,
			'u' => Sprites.BuildingBaseLeft,
			'y' => Sprites.BuildingRight,
			'U' => Sprites.BuildingBaseRight,
			'M' => Sprites.MiddleRoof,
			'R' => Sprites.TopRoofLeft,
			'j' => Sprites.TopRoofRight,
			'k' => Sprites.MiddleWindow,

			//Decor
			's' => Sprites.Sign,
			'ś' => Sprites.Sign,
			'f' => Sprites.Fence,
			'F' => Sprites.FenceLow,

			//Nature
			'w' => Sprites.Water,
			'g' => Sprites.GrassDec,
			'G' => Sprites.Grass,
			't' => Sprites.Tree,
			'T' => Sprites.Tree2,
			'r' => Sprites.HalfRock,
			'Ş' => Sprites.HalfRockStairs,
			'ş' => Sprites.HalfRockStairsGrass,
			'ŕ' => Sprites.HalfRockGrass,
			'm' => Sprites.Mountain,
			'p' => Sprites.Mountain2,

			//NPCs
			'n' => Sprites.NPC1,

			//Extra
			'W' => Sprites.Wall_0000,
			'x' => Sprites.Open,
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
				if (s[j][i] == 's')
				{
					Console.Clear();
					Console.WriteLine();
					Console.WriteLine("Sign Says:");
					Console.WriteLine();
					Console.WriteLine();
					Console.Write(" Press [enter] to continue...");
					PressEnterToContiue();
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
			'v' => true,
			'c' => true,
			'e' => true,
			'3' => true,
			'2' => true,
			'1' => true,
			'0' => true,
			'o' => true,
			'g' => true,
			'X' => true,
			'Ş' => true,
			'ş' => true,
			'G' => true,
			'd' => true,
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
				map = new PaletTown();
				SpawnCharacterOn('1');
				break;
			case '3':
				map = new Route2();
				SpawnCharacterOn('2');
				break;
			case 'G':
				if (!DisableBattle && Random.Shared.Next(2) is 0) // BATTLE CHANCE
				{
					Console.Clear();
					if (!DisableBattleTransition)
						Renderer.RenderBattleTransition();
					Renderer.RenderBattleView();
					PressEnterToContiue();
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Clear();
				}
				break;
		}
	}
}
