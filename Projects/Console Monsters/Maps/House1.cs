namespace Console_Monsters.Maps;

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

	public override string GetMapTileRender(int i, int j)
	{
		if (j < 0 || j >= SpriteSheet.Length || i < 0 || i >= SpriteSheet[j].Length)
		{
			return Sprites.Open;
		}
		return SpriteSheet[j][i] switch
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

	public override bool CanInteractWithMapTile(int i, int j)
	{
		if (j < 0 || j >= SpriteSheet.Length || i < 0 || i >= SpriteSheet[j].Length)
		{
			return false;
		}

		return false;
	}

	public override void InteractWithMapTile(int i, int j)
	{
		if (j >= 0 && j < SpriteSheet.Length && i >= 0 && i < SpriteSheet[j].Length)
		{
			switch (SpriteSheet[j][i])
			{
				case 'q':
					promptText = new string[]
					{
						"Mozin0's Mum:",
						"Welcome to my house, My son always gifts guests.",
						"He's Upstairs, go talk to him to recieve your gift.",
					};
					break;
			}
		}
	}

	public override bool IsValidCharacterMapTile(int i, int j)
	{
		if (j < 0 || j >= SpriteSheet.Length || i < 0 || i >= SpriteSheet[j].Length)
		{
			return false;
		}
		char c = SpriteSheet[j][i];
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

	public override void PerformTileAction(int i, int j)
	{
		if (j < 0 || j >= SpriteSheet.Length || i < 0 || i >= SpriteSheet[j].Length)
		{
			return;
		}
		switch (SpriteSheet[j][i])
		{
			case '0':
				map = new PaletTown();
				map.SpawnCharacterOn('2');
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
