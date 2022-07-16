namespace Console_Monsters.Maps;

public class House1SecondFloor : MapBase
{
	private static readonly char[][] spriteSheet = new char[][]
		{
			"afffffffffffffffb".ToCharArray(),
			"hpq! uv   -     g".ToCharArray(),
			"hno          kemg".ToCharArray(),
			"h      yy   xkemg".ToCharArray(),
			"hrrr       wzkijg".ToCharArray(),
			"cllllllllllllllld".ToCharArray(),
		};

	public override char[][] SpriteSheet => spriteSheet;

	public override string GetMapTileRender(int i, int j)
	{
		if (j < 0 || j >= SpriteSheet.Length || i < 0 || i >= SpriteSheet[j].Length)
		{
			return Sprites.Open;
		}
		return SpriteSheet[j][i] switch
		{
			// actions
			'0' => Sprites.StairsLeft,
			// non actions
			// Walls&Stairs
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
			// Objects
			'n' => Sprites.TVDeskLeft,
			'o' => Sprites.TVDeskRight,
			'p' => Sprites.TVLeft,
			'q' => Sprites.TVRight,
			'r' => Sprites.Bed1x3.Get(Subtract((i, j), FindTileInMap('r')!.Value).Reverse()),
			'u' => Sprites.ChairLeft,
			'v' => Sprites.Table,
			'x' => Sprites.Lamp,
			'y' => Sprites.Carpet,
			'z' => Sprites.Lamp2,
			'-' => Sprites.PotPlant2,
			// Monsters
			'w' => Sprites.Penguin,
			// Items
			'!' => Sprites.MonsterBox,
			// Extras
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
			'!' => true,
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
						"MonsterBox...",
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
				case '!':
					promptText = new string[]
					{
						"You picked up a MonsterBox",
					};
					PlayerInventory.TryAdd(MonsterBox.Instance);
					spriteSheet[j][i] = ' ';
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
