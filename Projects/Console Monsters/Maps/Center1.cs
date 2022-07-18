namespace Console_Monsters.Maps;

public class Center1 : MapBase
{
	public override string? AudioFile => AudioController.CoDA_Lullaby;

	private static readonly char[][] spriteSheet = new char[][]
		{
			"affffifffffjffffb".ToCharArray(),
			"go   gttktths  oh".ToCharArray(),
			"g    mplllrnq   h".ToCharArray(),
			"g               h".ToCharArray(),
			"go             oh".ToCharArray(),
			"ceeeeee000eeeeeed".ToCharArray(),
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
			'0' => Sprites.ArrowHeavyDown,
			// non actions
			//Walls
			'a' => Sprites.InteriorWallSE,
			'b' => Sprites.InteriorWallSW,
			'c' => Sprites.InteriorWallNE,
			'd' => Sprites.InteriorWallNW,
			'e' => Sprites.InteriorWallEWLow,
			'f' => Sprites.InteriorWallEWHigh,
			'g' => Sprites.InteriorWallNSLeft,
			'h' => Sprites.InteriorWallNSRight,
			'i' => Sprites.InteriorWallSWEHighLeft,
			'j' => Sprites.InteriorWallSWEHighRight,
			'm' => Sprites.InteriorWallNLeft,
			'n' => Sprites.InteriorWallNRight,
			// Objects
			'l' => Sprites.DeskMiddle,
			't' => Sprites.DeskBottom,
			'o' => Sprites.PotPlant1,
			'p' => Sprites.DeskLeft,
			'r' => Sprites.DeskRight,
			// NPC's
			'k' => Nurse.Idle1,
			'q' => Sprites.NPC3,
			// Items
			's' => Sprites.MonsterBox,
			// Extra
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
		return SpriteSheet[j][i] switch
		{
			'k' => true,
			's' => true,
			'q' => true,
			_ => false,
		};
	}

	public override void InteractWithMapTile(int i, int j)
	{
		if (j >= 0 && j < SpriteSheet.Length && i >= 0 && i < SpriteSheet[j].Length)
		{
			switch (SpriteSheet[j][i])
			{
				case 'k':
					PromptText = new string[]
					{
					" Hello and welcome to the monster center.",
					" I will heal all your monsters.",
					};
					for (int p = 0; p < partyMonsters.Count; p++)
					{
						partyMonsters[p].CurrentHP = partyMonsters[p].MaximumHP;
					}
					break;
				case 's':
					PromptText = new string[]
					{
						"You picked up a MonsterBox",
					};
					PlayerInventory.TryAdd(MonsterBox.Instance);
					spriteSheet[j][i] = ' ';
					break;
				case 'q':
					PromptText = new string[]
					{
						"...",
					};
					break;
			}	}
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
			'l' => true,
			'p' => true,
			'r' => true,
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
				Map.SpawnCharacterOn('0');
				break;
		}
	}
}
