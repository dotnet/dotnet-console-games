namespace Console_Monsters.Maps;

public class House1SecondFloor : MapBase
{
	public override char[][] SpriteSheet => new char[][]
		{
			"afffffffffffffffb".ToCharArray(),
		    "hpq  uv   -     g".ToCharArray(),
			"hno          kemg".ToCharArray(),
	        "h      yy   xkemg".ToCharArray(),
	        "hrst       wzkijg".ToCharArray(),
			"cllllllllllllllld".ToCharArray(),
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
				if (s[j][i] is 'r')
				{
					promptText = new string[]
						{
							"Mozin0:",
							"ZzzZzzZzz...",
							"MonsterBoz...",
							"ZzzZzzZzz...",
						};
				}
				if (s[j][i] is 'w')
				{
					promptText = new string[]
						{
							"Penguin:",
							"BrrrRRRrrr!",
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
			'm' => true,
			'e' => true,
			'y' => true,
			_ => false,
		};
	}

	public override void PerformTileAction()
	{
		var (i, j) = WorldToTile(character.I, character.J);
		char[][] s = map.SpriteSheet;
		switch (s[j][i])
		{
			case 'i':
				map = new House1();
				SpawnCharacterOn('1');
				break;
			case 'j':
				map = new House1();
				SpawnCharacterOn('2');
				break;
		}
	}
}
