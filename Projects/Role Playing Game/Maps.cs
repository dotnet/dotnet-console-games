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
			"mmmmmmmmpmppmmpmpmmmmmmmmmmmmmmmmmmmmmmmm".ToCharArray(),
			"TTTTTc     mpmmc    TT           m2mcmmmm".ToCharArray(),
			"TTTT        mm                    g   mmm".ToCharArray(),
			"TTT   TT                 mm           mmm".ToCharArray(),
			"TTT           TTT      mmmm     TT    mmm".ToCharArray(),
			"www      T              mm     TTT    www".ToCharArray(),
			"www          TT    ww           T     www".ToCharArray(),
			"www                 ww  TTT         wwwww".ToCharArray(),
			"www   w0w      Tww                 mmmmmm".ToCharArray(),
			"wwww          wwwwwww      TT   cmmmmmmmm".ToCharArray(),
			"wwwwwwwwwwwwwwwwwwwwwTTTTTTTTTTTTmmmmmmmm".ToCharArray(),
			"wwwwwwwwwwwwwwwwwwwwTTTTTTTTTTTTTTmmmmmmm".ToCharArray(),
			"wwwwwwwwwwwwwwwwwwwTTTTTTTTTTTTTTTTmmmmmm".ToCharArray(),
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
