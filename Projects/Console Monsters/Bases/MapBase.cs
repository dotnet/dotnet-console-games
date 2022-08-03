namespace Console_Monsters.Bases;

public abstract class MapBase
{
	/// <summary>Converts from world (character) coordinates to tile coordinates.</summary>
	public static (int I, int J) WorldToTile(int i, int j)
	{
		int tilei = i < 0 ? (i - (Sprites.Width - 1)) / Sprites.Width : i / Sprites.Width;
		int tilej = j < 0 ? (j - (Sprites.Height - 1)) / Sprites.Height : j / Sprites.Height;
		return (tilei, tilej);
	}

	/// <summary>Relocates the player to the top-left most occurence of a character in a <see cref="SpriteSheet"/>.</summary>
	public void SpawnPlayerOn(char c)
	{
		(int I, int J)? tile = FindTileInMap(c);
		if (tile is null)
		{
			throw new InvalidOperationException("Attempting to spawn the player on a non-existing tile.");
		}
		player.I = tile.Value.I * Sprites.Width;
		player.J = tile.Value.J * Sprites.Height;
	}

	/// <summary>Finds the top-left most occurence of a character in a <see cref="SpriteSheet"/>.</summary>
	public (int I, int J)? FindTileInMap(char c)
	{
		for (int j = 0; j < SpriteSheet.Length; j++)
		{
			for (int i = 0; i < SpriteSheet[j].Length; i++)
			{
				if (SpriteSheet[j][i] == c)
				{
					return (i, j);
				}
			}
		}
		return null;
	}

	/// <summary>The file name for the audio that will be played when this map is active.</summary>
	public virtual string? AudioFile => null;

	/// <summary>Gets the sprite for a given tile coordinate in the <see cref="MapBase"/>.</summary>
	public abstract string GetMapTileRender(int i, int j);

	/// <summary>Determines if the given tile coordinates are valid for the player to move to.</summary>
	public abstract bool IsValidCharacterMapTile(int i, int j);

	/// <summary>Interacts with the tile the player is facing.</summary>
	public abstract void InteractWithMapTile(int i, int j);

	/// <summary>Checks if the player can interact with the map tile they are facing.</summary>
	public abstract bool CanInteractWithMapTile(int i, int j);

	/// <summary>If necessary, performs an action when the player moves onto a tile.</summary>
	public abstract void PerformTileAction(int i, int j);

	public abstract char[][] SpriteSheet { get; }

	public string SpawnType { get; set; }
}
