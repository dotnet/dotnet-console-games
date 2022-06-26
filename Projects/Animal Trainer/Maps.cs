namespace Animal_Trainer;

internal static class Maps
{
	internal static (int I, int J) ScreenToTile(int i, int j)
	{
		int tilei = i < 0 ? (i - 6) / 7 : i / 7;
		int tilej = j < 0 ? (j - 4) / 5 : j / 5;
		return (tilei, tilej);
	}

	internal static void TransitionMapToTown()
	{
		map = Maps.PaletTown;
		var (i, j) = Maps.FindTileInMap(map, '1')!.Value;
		character.I = i * 7;
		character.J = j * 5;
	}

	internal static void TransitionMapToField()
	{
		map = Maps.RouteOne1;
		var (i, j) = Maps.FindTileInMap(map, '0')!.Value;
		character.I = i * 7;
		character.J = j * 5;
	}

	internal static (int I, int J)? FindTileInMap(char[][] map, char c)
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

	internal static string GetMapTileRender(char[][] map, int tileI, int tileJ)
	{
		if (tileJ < 0 || tileJ >= map.Length || tileI < 0 || tileI >= map[tileJ].Length)
		{
			return Sprites.Open;
		}
		return map[tileJ][tileI] switch
		{
			//Game 
			'X' => Sprites.Open,
			'0' => Sprites.ArrowDown,
			'1' => Sprites.ArrowUp,

			//Buildings
			'b' => Sprites.BuildingSmall,
			'v' => Sprites.VetSmall,
			'S' => Sprites.Store,
			'd' => Sprites.Door,
			'o' => Sprites.LowWindowSideLeft,
			'l' => Sprites.LowWindow,
			'u' => Sprites.SideRoofLeft,
			'y' => Sprites.SideRoofRight,
			'M' => Sprites.MiddleRoof,
			'R' => Sprites.TopRoofLeft,
			'j' => Sprites.TopRoofRight,
			'k' => Sprites.MiddleWindow,

			//Decor
			's' => Sprites.Sign,
			'f' => Sprites.Fence,

			//Nature
			'w' => Sprites.Water,
			'g' => Sprites.GrassDec,
			'G' => Sprites.Grass,
			't' => Sprites.Tree,
			'T' => Sprites.Tree2,
			'r' => Sprites.HalfRock,
			'm' => Sprites.Mountain,
			'p' => Sprites.Mountain2,

			//Extra
			'W' => Sprites.Wall_0000,
			' ' => Sprites.Open,
			_ => Sprites.Error,
		};
	}

	internal static bool IsValidCharacterMapTile(char[][] map, int tileI, int tileJ)
	{
		if (tileJ < 0 || tileJ >= map.Length || tileI < 0 || tileI >= map[tileJ].Length)
		{
			return false;
		}
		return map[tileJ][tileI] switch
		{
			' ' => true,
			'v' => true,
			'c' => true,
			'e' => true,
			'1' => true,
			'0' => true,
			'o' => true,
			'g' => true,
			'2' => true,
			'X' => true,
			'k' => true,
			'h' => true,
			'G' => true,
			'd' => true,
			_ => false,
		};
	}

	internal static readonly char[][] RouteOne1 = new char[][] // Not finished
	{
		"gggfgggggf  fgggggfg".ToCharArray(),
		"gggfgggggf  fgggggfg".ToCharArray(),
		"gggfgggggf  fgggggfg".ToCharArray(),
		"gggfgggggf  fgggggfg".ToCharArray(),
		"gggfgggggf  fgggggfg".ToCharArray(),
		"gggfffffff  fffffffg".ToCharArray(),
		"gggfgggggg  ggggggfg".ToCharArray(),
		"gggfgggggg  ggggggfg".ToCharArray(),
		"gggfgggggT        fg".ToCharArray(),
		"gggfrrrrrTrrrr    fg".ToCharArray(),
		"gggTgggggTGGGGGGGGfg".ToCharArray(),
		"gggTgggggTGGGGGGGGfg".ToCharArray(),
		"gggTgggggTGGGGGGGGfg".ToCharArray(),
		"gggTrrrrrTGGGGGGGGfg".ToCharArray(),
		"gggTgggggggg      fg".ToCharArray(),
		"gggTgggggggg      fg".ToCharArray(),
		"gggTgggggggg  GGGGfg".ToCharArray(),
		"gggTTTrrrrTTTTGGGGfg".ToCharArray(),
		"gggfgggggggg  GGGGfg".ToCharArray(),
		"gggfgggggggg  GGGGfg".ToCharArray(),
		"gggfgg        ggggfg".ToCharArray(),
		"gggfgg        ggggfg".ToCharArray(),
		"gggfgg  ggggggggggfg".ToCharArray(),
		"gggfr rrr rrrrrrrrfg".ToCharArray(),
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
		"gggTgggggfGGfgggggTg".ToCharArray(),
		"gggfgggggf00fgggggfg".ToCharArray(),
	};

	internal static readonly char[][] PaletTown = new char[][]
	{
		"ffffffffff11ffffffff".ToCharArray(),
		"fg                gf".ToCharArray(),
		"fg                gf".ToCharArray(),
		"fg                gf".ToCharArray(),
		"fg                gf".ToCharArray(),
		"fg                gf".ToCharArray(),
		"fg                gf".ToCharArray(),
		"fg        RMMMMj  gf".ToCharArray(),
		"fg  fffs  u    y  gf".ToCharArray(),
		"fg  gggg   k kk   gf".ToCharArray(),
		"fg  gggg olldlll  gf".ToCharArray(),
		"fg          X     gf".ToCharArray(),
		"fg        fffsff  gf".ToCharArray(),
		"fgggWWWW  gggggg  gf".ToCharArray(),
		"fgggWwwW  gggggg  gf".ToCharArray(),
		"fgggWwwW          gf".ToCharArray(),
		"ffggWwwWffffffffffff".ToCharArray(),
		"ffffWwwWffffffffffff".ToCharArray(),
	};
}
