namespace Animal_Trainer;

public static class Maps
{
	public static string GetMapTileRender(char[][] map, int tileI, int tileJ)
	{
		if (tileJ < 0 || tileJ >= map.Length || tileI < 0 || tileI >= map[tileJ].Length)
		{
			return Sprites.Open;
		}
		return map[tileJ][tileI] switch
		{
			'w' => Sprites.Water,
			'g' => Sprites.GrassDec,
			'G' => Sprites.Grass,
			'W' => Sprites.Wall_0000,
			'b' => Sprites.BuildingSmall,
			't' => Sprites.Tree,
			'T' => Sprites.Tree2,
			'r' => Sprites.HalfRock,
			' ' or 'X' => Sprites.Open,
			'i' => Sprites.InnSmall,
			'S' => Sprites.Store,
			'f' => Sprites.Fence,
			'c' => Sprites.Chest,
			'e' => Sprites.EmptyChest,
			'B' => Sprites.Barrels1,
			'1' => tileJ < map.Length / 2 ? Sprites.ArrowUp : Sprites.ArrowDown,
			'm' => Sprites.Mountain,
			'0' => Sprites.Town,
			'2' => Sprites.Castle,
			'p' => Sprites.Mountain2,
			's' => Sprites.Sign,
			'h' => Sprites.Wall_0000,
			_ => Sprites.Error,
		};
	}

	public static bool IsValidCharacterMapTile(char[][] map, int tileI, int tileJ)
	{
		if (tileJ < 0 || tileJ >= map.Length || tileI < 0 || tileI >= map[tileJ].Length)
		{
			return false;
		}
		return map[tileJ][tileI] switch
		{
			' ' => true,
			'i' => true,
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

	public static readonly char[][] RouteOne1 = new char[][] // Not finished
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

	public static readonly char[][] PaletTown = new char[][]
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
		"f         X    i   f".ToCharArray(),
		"f                  f".ToCharArray(),
		"f   WWWWWW   ffsf  f".ToCharArray(),
		"f   WwwwwW         f".ToCharArray(),
		"ff  WwwwwWffffffffff".ToCharArray(),
		"ffffWwwwwWffffffffff".ToCharArray(),
	};
}
