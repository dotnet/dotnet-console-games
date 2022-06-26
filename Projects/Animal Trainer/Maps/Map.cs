﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal_Trainer.Maps;

public abstract class Map
{
	internal static (int I, int J) ScreenToTile(int i, int j)
	{
		int tilei = i < 0 ? (i - 6) / 7 : i / 7;
		int tilej = j < 0 ? (j - 4) / 5 : j / 5;
		return (tilei, tilej);
	}

	internal static void TransitionMapToTown()
	{
		map = new PaletTown();
		var (i, j) = Map.FindTileInMap(map, '1')!.Value;
		character.I = i * 7;
		character.J = j * 5;
	}

	internal static void TransitionMapToField()
	{
		map = new Route1();
		var (i, j) = Map.FindTileInMap(map, '0')!.Value;
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
			'f' => Sprites.Fence,
			'F' => Sprites.FenceLow,

			//Nature
			'w' => Sprites.Water,
			'g' => Sprites.GrassDec,
			'G' => Sprites.Grass,
			't' => Sprites.Tree,
			'T' => Sprites.Tree2,
			'r' => Sprites.HalfRock,
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
		var s = map.SpriteSheet();

		if (tileJ < 0 || tileJ >= s.Length || tileI < 0 || tileI >= s[tileJ].Length)
		{
			return false;
		}
		return s[tileJ][tileI] switch
		{
			' ' => true,
			'v' => true,
			'c' => true,
			'e' => true,
			'1' => true,
			'0' => true,
			'o' => true,
			'g' => true,
			'2' => true,
			'X' => true,
			'G' => true,
			'd' => true,
			_ => false,
		};
	}

	public virtual char[][] SpriteSheet()
	{
		throw new NotImplementedException();
	}
}
