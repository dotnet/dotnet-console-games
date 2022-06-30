namespace Console_Monsters.Maps;

public abstract class Map
{
	internal static (int I, int J) ScreenToTile(int i, int j)
	{
		int tilei = i < 0 ? (i - 6) / 7 : i / 7;
		int tilej = j < 0 ? (j - 4) / 5 : j / 5;
		return (tilei, tilej);
	}

	internal static void TransitionMapToPaletTown()
	{
		map = new PaletTown();
		var (i, j) = Map.FindTileInMap(map, '1')!.Value;
		character.I = i * 7;
		character.J = j * 5;
	}

	internal static void TransitionMapToRoute1()
	{
		map = new Route1();
		var (i, j) = Map.FindTileInMap(map, '0')!.Value;
		character.I = i * 7;
		character.J = j * 5;
	}

	internal static void TransitionMapToRoute2()
	{
		map = new Route2();
		var (i, j) = Map.FindTileInMap(map, '2')!.Value;
		character.I = i * 7;
		character.J = j * 5;
	}

	internal static (int I, int J)? FindTileInMap(Map map, char c)
	{
		var s = map.SpriteSheet();
		for (int j = 0; j < s.Length; j++)
		{
			for (int i = 0; i < s[j].Length; i++)
			{
				if (s[j][i] == c)
				{
					return (i, j);
				}
			}
		}
		return null;
	}

	internal virtual string GetMapTileRender(int tileI, int tileJ)
	{
		var s = map.SpriteSheet();
		if (tileJ < 0 || tileJ >= s.Length || tileI < 0 || tileI >= s[tileJ].Length)
		{
			return Sprites.Open;
		}
		return s[tileJ][tileI] switch
		{
			//Game 
			'X' => Sprites.Open,
			'0' => Sprites.ArrowDown,
			'1' => Sprites.ArrowUp,
			'2' => Sprites.ArrowDown,
			'3' => Sprites.ArrowUp,

			//Buildings
			'b' => Sprites.BuildingSmall,
			'v' => Sprites.VetSmall,
			'S' => Sprites.Store,
			'd' => Sprites.Door,
			'o' => Sprites.LowWindowSideLeft,
			'l' => Sprites.LowWindow,
			'h' => Sprites.BuildingLeft,
			'u' => Sprites.BuildingBaseLeft,
			'y' => Sprites.BuildingRight,
			'U' => Sprites.BuildingBaseRight,
			'M' => Sprites.MiddleRoof,
			'R' => Sprites.TopRoofLeft,
			'j' => Sprites.TopRoofRight,
			'k' => Sprites.MiddleWindow,

			//Decor
			's' => Sprites.Sign,
			'ś' => Sprites.Sign,
			'f' => Sprites.Fence,
			'F' => Sprites.FenceLow,

			//Nature
			'w' => Sprites.Water,
			'g' => Sprites.GrassDec,
			'G' => Sprites.Grass,
			't' => Sprites.Tree,
			'T' => Sprites.Tree2,
			'r' => Sprites.HalfRock,
			'Ş' => Sprites.HalfRockStairs,
			'ş' => Sprites.HalfRockStairsGrass,
			'ŕ' => Sprites.HalfRockGrass,
			'm' => Sprites.Mountain,
			'p' => Sprites.Mountain2,

			//Extra
			'W' => Sprites.Wall_0000,
			'x' => Sprites.Open,
			' ' => Sprites.Open,
			_ => Sprites.Error,
		};
	}
	internal virtual bool IsValidCharacterMapTile(int tileI, int tileJ)
	{
		char[][] s = map.SpriteSheet();
		if (tileJ < 0 || tileJ >= s.Length || tileI < 0 || tileI >= s[tileJ].Length)
		{
			return false;
		}
		char c = s[tileJ][tileI];
		return c switch
		{
			' ' => true,
			'v' => true,
			'c' => true,
			'e' => true,
			'3' => true,
			'2' => true,
			'1' => true,
			'0' => true,
			'o' => true,
			'g' => true,
			'X' => true,
			'Ş' => true,
			'ş' => true,
			'G' => true,
			'd' => true,
			_ => false,
		};
	}

	public abstract void InteractWithMapTile(int tileI, int tileJ);

	public abstract char[][] SpriteSheet();
}
