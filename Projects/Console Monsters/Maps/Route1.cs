﻿namespace Console_Monsters.Maps;

class Route1 : Map
{
	public override char[][] SpriteSheet()
	{
		return new char[][]
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
			"gggfgggggf00fgggggfg".ToCharArray(),
		};
	}

	public override void InteractWithMapTile(int tileI, int tileJ)
	{
		var s = map.SpriteSheet();

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
}
