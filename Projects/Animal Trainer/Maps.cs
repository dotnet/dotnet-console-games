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
			'X' => Sprites.Open,
			'0' => Sprites.ArrowDown,
			'1' => Sprites.ArrowUp,
			'w' => Sprites.Water,
			'g' => Sprites.GrassDec,
			'G' => Sprites.Grass,
			'W' => Sprites.Wall_0000,
			'b' => Sprites.BuildingSmall,
			't' => Sprites.Tree,
			'T' => Sprites.Tree2,
			'r' => Sprites.HalfRock,
			' ' => Sprites.Open,
			'v' => Sprites.VetSmall,
			'S' => Sprites.Store,
			'f' => Sprites.Fence,
			'c' => Sprites.Chest,
			'e' => Sprites.EmptyChest,
			'B' => Sprites.Barrels1,
			'm' => Sprites.Mountain,
			'p' => Sprites.Mountain2,
			's' => Sprites.Sign,
			'h' => Sprites.Wall_0000,
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
			'g' => true,
			'2' => true,
			'X' => true,
			'k' => true,
			'h' => true,
			'G' => true,
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
		"gggT              fg".ToCharArray(),
		"gggT              fg".ToCharArray(),
		"gggT          GGGGfg".ToCharArray(),
		"gggTTTrrrrTTTTGGGGfg".ToCharArray(),
		"gggf          GGGGfg".ToCharArray(),
		"gggf          GGGGfg".ToCharArray(),
		"gggf              fg".ToCharArray(),
		"gggf              fg".ToCharArray(),
		"gggf              fg".ToCharArray(),
		"gggf              fg".ToCharArray(),
		"gggf              fg".ToCharArray(),
		"gggf              fg".ToCharArray(),
		"gggf              fg".ToCharArray(),
		"gggf              fg".ToCharArray(),
		"gggf              fg".ToCharArray(),
		"gggf              fg".ToCharArray(),
		"gggf              fg".ToCharArray(),
		"gggT              fg".ToCharArray(),
		"gggT              fg".ToCharArray(),
		"gggT              fg".ToCharArray(),
		"gggT              fg".ToCharArray(),
		"gggT              fg".ToCharArray(),
		"gggT              fg".ToCharArray(),
		"Tfffffffff  ffffffff".ToCharArray(),
		"Tggggggggf  fggggggg".ToCharArray(),
		"fggggggggf00fggggggg".ToCharArray(),
	};

	internal static readonly char[][] PaletTown = new char[][]
	{
		"ffffffffff11ffffffff".ToCharArray(),
		"f                  f".ToCharArray(),
		"f                  f".ToCharArray(),
		"f                  f".ToCharArray(),
		"f                  f".ToCharArray(),
		"f                  f".ToCharArray(),
		"f                  f".ToCharArray(),
		"f                  f".ToCharArray(),
		"f                  f".ToCharArray(),
		"f    sb       sb   f".ToCharArray(),
		"f                  f".ToCharArray(),
		"f    fffs          f".ToCharArray(),
		"f         X    v   f".ToCharArray(),
		"f                  f".ToCharArray(),
		"f   WWWWWW   ffsf  f".ToCharArray(),
		"f   WwwwwW         f".ToCharArray(),
		"ff  WwwwwWffffffffff".ToCharArray(),
		"ffffWwwwwWffffffffff".ToCharArray(),
	};
}
