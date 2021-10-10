namespace Role_Playing_Game
{
	public static class Maps
	{
		public static char[][] Town = new char[][]
		{
			// 1: field
			// b: building
			// B: barrels
			// c: chest
			// i: inn
			// s: store
			// w: water
			// W: wall
			"WWWWWWWWWWWWWWWWW111WWWWWWWWWWWWWWWWW".ToCharArray(),
			"WbbbfbbbB             Bbffbffbffb  cW".ToCharArray(),
			"W                                   W".ToCharArray(),
			"W     Bbfb                          W".ToCharArray(),
			"Wcb                          BbbbB  W".ToCharArray(),
			"W           Bi    c    sB           W".ToCharArray(),
			"W                                  cW".ToCharArray(),
			"Wb   t                        BbfbfbW".ToCharArray(),
			"W                                  cW".ToCharArray(),
			"Wbffbfbffbfbfbbfb   bbfbfbffbfbbfbfbW".ToCharArray(),
			"WWWWWWWWWWWWWWWWW111WWWWWWWWWWWWWWWWW".ToCharArray(),
			// out of bounds: open
		};

		public static char[][] Field = new char[][]
		{
			// 0: town
			// 2: castle
			// c: chest
			// g: guard (boss)
			// m: mountain
			// t: tree
			// w: water
			"mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm".ToCharArray(),
			"mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm".ToCharArray(),
			"mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm".ToCharArray(),
			"tttttc     mmmmc    tt           m2mcmmmm".ToCharArray(),
			"tttt        mm                    g   mmm".ToCharArray(),
			"ttt   tt                 mm           mmm".ToCharArray(),
			"ttt           ttt      mmmm     tt    mmm".ToCharArray(),
			"www      t              mm     ttt    www".ToCharArray(),
			"www          tt    ww           t     www".ToCharArray(),
			"www                 ww  ttt         wwwww".ToCharArray(),
			"www   w0w      tww                 mmmmmm".ToCharArray(),
			"wwww          wwwwwww      tt   cmmmmmmmm".ToCharArray(),
			"wwwwwwwwwwwwwwwwwwwwwttttttttttttmmmmmmmm".ToCharArray(),
			"wwwwwwwwwwwwwwwwwwwwttttttttttttttmmmmmmm".ToCharArray(),
			"wwwwwwwwwwwwwwwwwwwttttttttttttttttmmmmmm".ToCharArray(),
			// out of bounds: mountin
		};

		public static char[][] Castle = new char[][]
		{
			// 0: town
			// 2: castle
			// c: chest
			// g: guard (boss)
			// m: mountain
			// t: tree
			// w: water
			"WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW".ToCharArray(),
			"WWWWWWWWWWWWWWWWWWWW3WWWWWWWWWWWWWWWWWWWW".ToCharArray(),
			"WWc                W W                cWW".ToCharArray(),
			"WW                 W W                 WW".ToCharArray(),
			"WW                                     WW".ToCharArray(),
			"WW       W      W       W      W       WW".ToCharArray(),
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
			//out of bounds: open
		};
	}
}
