﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal_Trainer.Maps;

class PaletTown : Map
{
	public override char[][] SpriteSheet()
	{
		return new char[][]
		{
			"ffffffffff11ffffffff".ToCharArray(),
			"fg                gf".ToCharArray(),
			"fg                gf".ToCharArray(),
			"fg                gf".ToCharArray(),
			"fg                gf".ToCharArray(),
			"fg                gf".ToCharArray(),
			"fg                gf".ToCharArray(),
			"fg        RMMMMj  gf".ToCharArray(),
			"fg  FFFs  uxxxxy  gf".ToCharArray(),
			"fg  gggg  ukkkky  gf".ToCharArray(),
			"fg  gggg  hldll;  gf".ToCharArray(),
			"fg          X     gf".ToCharArray(),
			"fg        FFFsFF  gf".ToCharArray(),
			"fgggWWWW  gggggg  gf".ToCharArray(),
			"fgggWwwW  gggggg  gf".ToCharArray(),
			"fgggWwwW          gf".ToCharArray(),
			"ffggWwwWffffffffffff".ToCharArray(),
			"ffffWwwWffffffffffff".ToCharArray(),
		};
	}
}
