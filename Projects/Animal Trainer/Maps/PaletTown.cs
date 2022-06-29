using System;
using System.Collections.Generic;
namespace Animal_Trainer.Maps;

class PaletTown : Map
{
	public override char[][] SpriteSheet()
	{
		return new char[][]
		{
			"ffffffffff11ffffffff".ToCharArray(),
			"fg         X      gf".ToCharArray(),
			"fg  RMMj    RMMj  gf".ToCharArray(),
			"fg  hkky    hkky  gf".ToCharArray(),
			"fg sudlU   sudlU  gf".ToCharArray(),
			"fg                gf".ToCharArray(),
			"fg                gf".ToCharArray(),
			"fg        RMMMMj  gf".ToCharArray(),
			"fg  FFFs  hkkkky  gf".ToCharArray(),
			"fg  gggg  hkkkky  gf".ToCharArray(),
			"fg  gggg  uldllU  gf".ToCharArray(),
			"fg                gf".ToCharArray(),
			"fg        FFFsFF  gf".ToCharArray(),
			"fgggWWWW  gggggg  gf".ToCharArray(),
			"fgggWwwW  gggggg  gf".ToCharArray(),
			"fgggWwwW          gf".ToCharArray(),
			"ffggWwwWffffffffffff".ToCharArray(),
			"ffffWwwWffffffffffff".ToCharArray(),
		};
	}

	public static string closestSign()
	{
		var (i, j) = Map.ScreenToTile(character.I, character.J);
		var s = map.SpriteSheet();

		if (s[j - 1][i] == s[5][4] || s[j + 1][i] == s[5][4] || s[j][i - 1] == s[5][4] || s[j][i + 1] == s[5][4])
		{
			return "Top Left Sign";
		} 
		else if (s[j - 1][i] == s[9][8] || s[j + 1][i] == s[9][8] || s[j][i - 1] == s[9][8] || s[j][i + 1] == s[9][8])
		{
			return "Middle Left";
		}
		else if (s[j - 1][i] == s[5][12] || s[j + 1][i] == s[5][12] || s[j][i - 1] == s[5][12] || s[j][i + 1] == s[5][12])
		{
			return "Top Right";
		}
		else if (s[j - 1][i] == s[13][14] || s[j + 1][i] == s[13][14] || s[j][i - 1] == s[13][14] || s[j][i + 1] == s[13][14])
		{
			return "Bottom Right";
		}
		return "test";
	}
}
