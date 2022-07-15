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

namespace Website.Games.Console_Monsters.Maps;

public class Center1 : MapBase
{
	private readonly char[][] spriteSheet = new char[][]
		{
			"affffifffffjffffb".ToCharArray(),
			"go   gttktth   oh".ToCharArray(),
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
			'k' => Nurse.Idle1,
			'l' => Sprites.DeskMiddle,
			't' => Sprites.DeskBottom,
			'm' => Sprites.InteriorWallNLeft,
			'n' => Sprites.InteriorWallNRight,
			'o' => Sprites.PotPlant1,
			'p' => Sprites.DeskLeft,
			'q' => Sprites.NPC3,
			'r' => Sprites.DeskRight,
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
			_ => false,
		};
	}

	public override void InteractWithMapTile(int i, int j)
	{
		if (j >= 0 && j < SpriteSheet.Length && i >= 0 && i < SpriteSheet[j].Length)
		{
			if (SpriteSheet[j][i] is 'k')
			{
				promptText = new string[]
				{
					" Hello and welcome to the monster center.",
					" I will heal all your monsters.",
				};
				for(int p = 0; p < partyMonsters.Count; p++)
				{
					partyMonsters[p].CurrentHP = partyMonsters[p].MaximumHP;
				}
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
			'l' => true,
			'p' => true,
			'r' => true,
			_ => false,
		};
	}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
	public override async Task PerformTileAction(int i, int j)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
	{
		if (j < 0 || j >= SpriteSheet.Length || i < 0 || i >= SpriteSheet[j].Length)
		{
			return;
		}
		switch (SpriteSheet[j][i])
		{
			case '0':
				map = new PaletTown();
				map.SpawnCharacterOn('0');
				break;
		}
	}
}
