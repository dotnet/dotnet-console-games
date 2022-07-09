﻿namespace Console_Monsters.Maps;

public class House1 : MapBase
{
	public override char[][] SpriteSheet => new char[][]
		{
			"afffffffffffffffb".ToCharArray(),
		    "hpmnh     wvwkijg".ToCharArray(),
	        "hmq          kijg".ToCharArray(),
	        "hlll         k12g".ToCharArray(),
	        "hstr   uuu      g".ToCharArray(),
			"cllllll000lllllld".ToCharArray(),
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
			// actions
			'0' => Sprites.ArrowHeavyDown,
			'1' => Sprites.StairsLeft,
			'2' => Sprites.StairsRight,
			// non actions
			'a' => Sprites.InteriorWallSEShort,
			'b' => Sprites.InteriorWallSWShort,
			'c' => Sprites.InteriorWallNEShort,
			'd' => Sprites.InteriorWallNWShort,
			'e' => Sprites.InteriorWallEWLow,
			'f' => Sprites.InteriorWallHorizontalBottmn,
			'g' => Sprites.InteriorWallNSLeft,
			'h' => Sprites.InteriorWallNSMid,
			'i' => Sprites.StairsLeft,
			'j' => Sprites.StairsRight,
			'k' => Sprites.InteriorWallNSRightRight,
			'l' => Sprites.InteriorWallHorizontalTop,
			'm' => Sprites.Fridge,
			'n' => Sprites.LowerCabnetWithDraws,
			'o' => Sprites.PotPlant1,
			'p' => Sprites.MicroWave,
			'q' => Sprites.NPC11,
			'r' => Sprites.ChairRight,
			's' => Sprites.TVLeft,
			't' => Sprites.TVRight,
			'u' => Sprites.Carpet,
			'v' => Sprites.WeirdMonster,
			'w' => Sprites.PotPlant1,
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
				if (s[j][i] is 'q')
				{
					promptText = new string[]
						{
							"Mozin0's Mum:",
							"Welcome to my house, My son always gifts guests.",
							"He's Upstairs, go talk to him to recieve your gift.",
						};
				}
				if (s[j][i] is 'r')
				{
					promptText = new string[]
						{
							
						};
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
			'0' => true,
			'i' => true,
			'j' => true,
			'1' => true,
			'2' => true,
			'u' => true,
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
				SpawnCharacterOn('2');
				break;
			case 'i':
				map = new House1SecondFloor();
				SpawnCharacterOn('i');
				break;
			case 'j':
				map = new House1SecondFloor();
				SpawnCharacterOn('j');
				break;

		}
	}
}
