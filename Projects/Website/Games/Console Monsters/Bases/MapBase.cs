using System;
using System.Linq;
using System.Text;
using System.Threading;
using static Website.Games.Console_Monsters._using;
using Website.Games.Console_Monsters.Maps;
using Website.Games.Console_Monsters.Monsters;
using Website.Games.Console_Monsters.Bases;
using Website.Games.Console_Monsters.NPCs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters.Bases;

public abstract class MapBase
{
	/// <summary>Converts from world (character) coordinates to tile coordinates.</summary>
	public static (int I, int J) WorldToTile(int i, int j)
	{
		int tilei = i < 0 ? (i - 6) / 7 : i / 7;
		int tilej = j < 0 ? (j - 4) / 5 : j / 5;
		return (tilei, tilej);
	}

	/// <summary>Relocates the player to the top-left most occurence of a character in a <see cref="SpriteSheet"/>.</summary>
	public static void SpawnCharacterOn(char c)
	{
		var (i, j) = FindTileInMap(map, c)!.Value;
		character.I = i * 7;
		character.J = j * 5;
	}

	/// <summary>Finds the top-left most occurence of a character in a <see cref="SpriteSheet"/>.</summary>
	public static (int I, int J)? FindTileInMap(MapBase map, char c)
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

	/// <summary>Gets the sprite for a given tile coordinate in the <see cref="MapBase"/>.</summary>
	public abstract string GetMapTileRender(int tileI, int tileJ);

	/// <summary>Determines if the given tile coordinates are valid for the player to move to.</summary>
	public abstract bool IsValidCharacterMapTile(int tileI, int tileJ);

	/// <summary>Interacts with adjacent tiles if there is anything to interact with.</summary>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
	public virtual async Task InteractWithMapTile(int tileI, int tileJ) { throw new NotImplementedException(); }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

	/// <summary>If necessary, performs an action when the player moves onto a tile.</summary>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
	public virtual async Task PerformTileAction() { throw new NotImplementedException(); }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

	public abstract char[][] SpriteSheet { get; }
}
