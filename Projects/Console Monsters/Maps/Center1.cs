namespace Console_Monsters.Maps;

public class Center1 : Map
{
	public override char[][] SpriteSheet => new char[][]
		{
			"affffifffffjffffb".ToCharArray(),
			"go   g  k  h   oh".ToCharArray(),
			"g    mllpllnq   h".ToCharArray(),
			"g               h".ToCharArray(),
			"go             oh".ToCharArray(),
			"ceeeeee000eeeeeed".ToCharArray(),
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
			'0' => Sprites.ArrowDown,
			'p' => Sprites.Desk,
			// non actions
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
			'k' => Sprites.NPC2,
			'l' => Sprites.Desk,
			'm' => Sprites.InteriorWallNLeft,
			'n' => Sprites.InteriorWallNRight,
			'o' => Sprites.PotPlant1,
			'q' => Sprites.NPC3,
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
				if (s[j][i] is 'p')
				{
					Console.Clear();
					Console.WriteLine();
					Console.WriteLine(@" Hello and welcome to the monster center.");
					Console.WriteLine(@" I will heal all your monsters.");
					Console.WriteLine();
					Console.Write(" Press [enter] to continue...");
					PressEnterToContiue();
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
				SpawnCharacterOn('0');
				break;
		}
	}
}
