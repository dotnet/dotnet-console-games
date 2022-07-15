namespace Console_Monsters.Maps;

public class House1SecondFloor : MapBase
{
	private readonly char[][] spriteSheet = new char[][]
		{
			"afffffffffffffffb".ToCharArray(),
			"hpq  uv   -     g".ToCharArray(),
			"hno          kemg".ToCharArray(),
			"h      yy   xkemg".ToCharArray(),
			"hrst       wzkijg".ToCharArray(),
			"cllllllllllllllld".ToCharArray(),
		};

	public override char[][] SpriteSheet => spriteSheet;

	public override string GetMapTileRender(int tileI, int tileJ)
	{
		if (tileJ < 0 || tileJ >= SpriteSheet.Length || tileI < 0 || tileI >= SpriteSheet[tileJ].Length)
		{
			return Sprites.Open;
		}
		return SpriteSheet[tileJ][tileI] switch
		{
			// actions
			'0' => Sprites.StairsLeft,
			// non actions
			'a' => Sprites.InteriorWallSEShort,
			'b' => Sprites.InteriorWallSWShort,
			'c' => Sprites.InteriorWallNEShort,
			'd' => Sprites.InteriorWallNWShort,
			'e' => Sprites.StairsLeft,
			'f' => Sprites.InteriorWallHorizontalBottmn,
			'g' => Sprites.InteriorWallNSLeft,
			'h' => Sprites.InteriorWallNSMid,
			'i' => Sprites.StairsLeft,
			'j' => Sprites.StairsRight,
			'k' => Sprites.InteriorWallNSRightRight,
			'l' => Sprites.InteriorWallHorizontalTop,
			'm' => Sprites.StairsRight,
			'n' => Sprites.TVDeskLeft,
			'o' => Sprites.TVDeskRight,
			'p' => Sprites.TVLeft,
			'q' => Sprites.TVRight,
			'r' => Sprites.PersonInBed1,
			's' => Sprites.PersonInBed2,
			't' => Sprites.PersonInBed3,
			'u' => Sprites.ChairLeft	,
			'v' => Sprites.Table,
			'w' => Sprites.Penguin,
			'x' => Sprites.Lamp,
			'y' => Sprites.Carpet,
			'z' => Sprites.Lamp2,
			'-' => Sprites.PotPlant2,
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
		return SpriteSheet[j][i] switch
		{
			'r' => true,
			'w' => true,
			_ => false,
		};
	}

	public override void InteractWithMapTile(int i, int j)
	{
		if (j >= 0 && j < SpriteSheet.Length && i >= 0 && i < SpriteSheet[j].Length)
		{
			switch (SpriteSheet[j][i])
			{
				case 'r':
					promptText = new string[]
					{
						"Mozin0:",
						"ZzzZzzZzz...",
						"MonsterBoz...",
						"ZzzZzzZzz...",
					};
					break;
				case 'w':
					promptText = new string[]
					{
						"Penguin:",
						"BrrrRRRrrr!",
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
			'm' => true,
			'n' => true,
			'o' => true,
			'e' => true,
			'y' => true,
			_ => false,
		};
	}

	public override void PerformTileAction(int i, int j)
	{
		switch (SpriteSheet[j][i])
		{
			case 'i':
				map = new House1();
				map.SpawnCharacterOn('1');
				break;
			case 'j':
				map = new House1();
				map.SpawnCharacterOn('2');
				break;
		}
	}
}
