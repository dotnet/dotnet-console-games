namespace Console_Monsters.Maps;

public class House1 : MapBase
{
	public override char[][] SpriteSheet => new char[][]
		{
			"afffffffffffffffb".ToCharArray(),
			"go             oh".ToCharArray(),
			"g               h".ToCharArray(),
			"g               h".ToCharArray(),
			"go             oh".ToCharArray(),
			"ceeeeee000eeeeeed".ToCharArray(),
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
			// non actions
			'a' => Sprites.InteriorWallSE,
			'b' => Sprites.InteriorWallSW,
			'c' => Sprites.InteriorWallNE,
			'd' => Sprites.InteriorWallNW,
			'e' => Sprites.InteriorWallEWLow,
			'f' => Sprites.InteriorWallEWHigh,
			'g' => Sprites.InteriorWallNSLeft,
			'h' => Sprites.InteriorWallNSRight,
			'o' => Sprites.PotPlant1,
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
			// currently no interactables
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
		}
	}
}
