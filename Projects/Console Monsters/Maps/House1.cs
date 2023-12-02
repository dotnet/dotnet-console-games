namespace Console_Monsters.Maps;

public class House1 : MapBase
{
	public override string? AudioFile => AudioController.CoDA_Lullaby;

	private static readonly char[][] spriteSheet =
		[
			"afffffffffffffffb".ToCharArray(),
			"hpmn      wvwkijg".ToCharArray(),
			"hmq          kijg".ToCharArray(),
			"h            k12g".ToCharArray(),
			"hssss  uuu      g".ToCharArray(),
			"cllllll000lllllld".ToCharArray(),
		];

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
			'0' => Sprites.ArrowHeavyDown,
			'1' => Sprites.StairsLeft,
			'2' => Sprites.StairsRight,
			// non actions
			// Walls&Stairs
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
			// Objects
			'm' => Sprites.Fridge,
			'n' => Sprites.LowerCabnetWithDraws,
			'o' => Sprites.PotPlant1,
			'p' => Sprites.MicroWave,
			's' => Sprites.DiningSet1x4.Get(Subtract((i, j), FindTileInMap('s')!.Value).Reverse()),
			'u' => Sprites.Carpet,
			'v' => Sprites.WeirdMonster,
			'w' => Sprites.PotPlant1,
			//NPC's
			'q' => Sprites.NPC11,
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
			'q' => true,
			'v' => true,
			_ => false,
		};
	}

	public override void InteractWithMapTile(int i, int j)
	{
		if (j >= 0 && j < SpriteSheet.Length && i >= 0 && i < SpriteSheet[j].Length)
		{
			switch (SpriteSheet[j][i])
			{
				case 'q':
					PromptText =
					[
						"Mozin0's Mum:",
						"Welcome to my house, My son always gifts guests.",
						"He's Upstairs, go talk to him to recieve your gift.",
					];
					break;
				case 'v':
					PromptText =
					[
						"Funky:",
						"Slurp Slurp",
					];
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
				Map = new PaletTown();
				Map.SpawnCharacterOn('2');
				break;
			case 'i':
				Map = new House1SecondFloor();
				Map.SpawnCharacterOn('i');
				break;
			case 'j':
				Map = new House1SecondFloor();
				Map.SpawnCharacterOn('j');
				break;

		}
	}
}
