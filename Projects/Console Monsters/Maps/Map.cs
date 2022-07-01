namespace Console_Monsters.Maps;

public abstract class Map
{
	public static (int I, int J) WorldToTile(int i, int j)
	{
		int tilei = i < 0 ? (i - 6) / 7 : i / 7;
		int tilej = j < 0 ? (j - 4) / 5 : j / 5;
		return (tilei, tilej);
	}

	public static void SpawnCharacterOn(char c)
	{
		var (i, j) = FindTileInMap(map, c)!.Value;
		character.I = i * 7;
		character.J = j * 5;
	}

	public static (int I, int J)? FindTileInMap(Map map, char c)
	{
		char[][] s = map.SpriteSheet;
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

	public abstract string GetMapTileRender(int tileI, int tileJ);

	public abstract bool IsValidCharacterMapTile(int tileI, int tileJ);

	public abstract void InteractWithMapTile(int tileI, int tileJ);

	public abstract void PerformTileAction();

	public abstract char[][] SpriteSheet { get; }
}
