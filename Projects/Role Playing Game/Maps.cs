namespace Role_Playing_Game;

public static class Maps
{
	public static string GetMapTileRender(char[][] map, int tileI, int tileJ)
	{
		if (tileJ < 0 || tileJ >= map.Length || tileI < 0 || tileI >= map[tileJ].Length)
		{
			if (map == Field) return Sprites.Mountain;
			return Sprites.Open;
		}
		return map[tileJ][tileI] switch
		{
			'w' => Sprites.Water,
			'W' => Sprites.Wall_0000,
			'b' => Sprites.Building,
			't' => Sprites.Tree,
			' ' or 'X' => Sprites.Open,
			'i' => Sprites.Inn,
			's' => Sprites.Store,
			'f' => Sprites.Fence,
			'c' => Sprites.Chest,
			'e' => Sprites.EmptyChest,
			'B' => Sprites.Barrels1,
			'1' => tileJ < map.Length / 2 ? Sprites.ArrowUp : Sprites.ArrowDown,
			'm' => Sprites.Mountain,
			'0' => Sprites.Town,
			'g' => Sprites.Guard,
			'2' => Sprites.Castle,
			'p' => Sprites.Mountain2,
			'T' => Sprites.Tree2,
			'k' => Sprites.King,
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
			's' => true,
			'c' => true,
			'e' => true,
			'1' => true,
			'0' => true,
			'g' => true,
			'2' => true,
			'X' => true,
			'k' => true,
			'h' => true,
			_ => false,
		};
	}

	public static readonly char[][] Town = new char[][]
	{
		"   WWWWWWWWWWWWWWWWW111WWWWWWWWWWWWWWWWW   ".ToCharArray(),
		"wwwWbbbfbbbB             Bbffbffbffb  cWwww".ToCharArray(),
		"wwwW                                   Wwww".ToCharArray(),
		"wwwW     Bbfb                          Wwww".ToCharArray(),
		"wwwWcb                          BbbbB  Wwww".ToCharArray(),
		"wwwW           Bi    c    sB           Wwww".ToCharArray(),
		"wwwW                                   Wwww".ToCharArray(),
		"wwwWb   T            X           BbfbfbWwww".ToCharArray(),
		"wwwW                                  cWwww".ToCharArray(),
		"wwwWbffbfbffbfbfbbfb   bbfbfbffbfbbfbfbWwww".ToCharArray(),
		"   WWWWWWWWWWWWWWWWW111WWWWWWWWWWWWWWWWW   ".ToCharArray(),
	};

	public static readonly char[][] Field = new char[][]
	{
		"mmmpmmmmpmmmmmpmmmmmpmmmmmpmmmpmmmpmmmpmm".ToCharArray(),
		"mmpppppppmmmpppmmmpppppmmppmmmpmmmmpppmmm".ToCharArray(),
		"mmpmmpmmpmppmmpmpmmpmmpmmmmmmpppmmpmpmmmp".ToCharArray(),
		"TTTTTc     mpmm    cTT           m2mcmmpp".ToCharArray(),
		"TTTT        mm                    g   mmm".ToCharArray(),
		"TTT   TT                 mm           mpm".ToCharArray(),
		"TTT           TTT      mmmm     TT    ppm".ToCharArray(),
		"www      T              mm     TTT    www".ToCharArray(),
		"www          TT    ww           T     www".ToCharArray(),
		"www                 ww  TTT         wwwww".ToCharArray(),
		"www   w0w      Tww                 mmmmmm".ToCharArray(),
		"wwww          wwwwwww      TT   cmmmmmmmm".ToCharArray(),
		"wwwwwwwwwwwwwwwwwwwwwTTTTTTTTTTTTmmmmmmmm".ToCharArray(),
		"wwwwwwwwwwwwwwwwwwwwTTTTTTTTTTTTTTmmmmmmm".ToCharArray(),
		"wwwwwwwwwwwwwwwwwwwTTTTTTTTTTTTTTTTmmmmmm".ToCharArray(),
	};

	public static readonly char[][] Castle = new char[][]
	{
		"WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW".ToCharArray(),
		"WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW".ToCharArray(),
		"WWc                WkW                cWW".ToCharArray(),
		"WW                 W W                 WW".ToCharArray(),
		"WW                                     WW".ToCharArray(),
		"WW       W      h       W      W       WW".ToCharArray(),
		"WW                                     WW".ToCharArray(),
		"WW                                     WW".ToCharArray(),
		"WW       W      W       W      W       WW".ToCharArray(),
		"WW                                     WW".ToCharArray(),
		"WW                                     WW".ToCharArray(),
		"WW       W      W       W      W       WW".ToCharArray(),
		"WW                                     WW".ToCharArray(),
		"WWc                                   cWW".ToCharArray(),
		"WWWWWWWWWWWWWWWWWWW   WWWWWWWWWWWWWWWWWWW".ToCharArray(),
		"WWWWWWWWWWWWWWWWWWW111WWWWWWWWWWWWWWWWWWW".ToCharArray(),
	};
}
