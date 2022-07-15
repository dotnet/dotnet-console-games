using System;
using System.Linq;
using System.Text;
using System.Threading;
using static Website.Games.Console_Monsters.Statics;
//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Items;
using Website.Games.Console_Monsters.Maps;
using Website.Games.Console_Monsters.Monsters;
using Website.Games.Console_Monsters.Bases;
using Website.Games.Console_Monsters.Characters;
using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Screens.Menus;
using Website.Games.Console_Monsters.Enums;
using Website.Games.Console_Monsters.Utilities;
using System.Collections.Generic;
using Towel;
using static Towel.Statics;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters.Bases;

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
	public void SpawnCharacterOn(char c)
	{
		(int I, int J)? tile = FindTileInMap(c);
		if (tile is null)
		{
			throw new InvalidOperationException("Attempting to spawn the player on a non-existing tile.");
		}
		character.I = tile.Value.I * Sprites.Width;
		character.J = tile.Value.J * Sprites.Height;
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

	/// <summary>Gets the sprite for a given tile coordinate in the <see cref="MapBase"/>.</summary>
	public abstract string GetMapTileRender(int i, int j);

	/// <summary>Determines if the given tile coordinates are valid for the player to move to.</summary>
	public abstract bool IsValidCharacterMapTile(int i, int j);

	/// <summary>Interacts with the tile the player is facing.</summary>
	public abstract void InteractWithMapTile(int i, int j);

	/// <summary>Checks if the player can interact with the map tile they are facing.</summary>
	public abstract bool CanInteractWithMapTile(int i, int j);

	/// <summary>If necessary, performs an action when the player moves onto a tile.</summary>
	public abstract Task PerformTileAction(int i, int j);

	public abstract char[][] SpriteSheet { get; }
}
