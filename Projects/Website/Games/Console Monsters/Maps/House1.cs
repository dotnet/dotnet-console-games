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

public class House1 : MapBase
{
	private readonly char[][] spriteSheet = new char[][]
		{
			"afffffffffffffffb".ToCharArray(),
			"hpmn      wvwkijg".ToCharArray(),
			"hmq          kijg".ToCharArray(),
			"h            k12g".ToCharArray(),
			"hssss  uuu      g".ToCharArray(),
			"cllllll000lllllld".ToCharArray(),
		};

	public override char[][] SpriteSheet => spriteSheet;

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
			'1' => Sprites.StairsLeft,
			'2' => Sprites.StairsRight,
			// non actions
			'a' => Sprites.InteriorWallSEShort,
			'b' => Sprites.InteriorWallSWShort,
			'c' => Sprites.InteriorWallNEShort,
			'd' => Sprites.InteriorWallNWShort,
			'e' => Sprites.InteriorWallEWLow,
			'f' => Sprites.InteriorWallHorizontalBottmn,
			'g' => Sprites.InteriorWallNSLeft,
			'h' => Sprites.InteriorWallNSMid,
			'i' => Sprites.StairsLeft,
			'j' => Sprites.StairsRight,
			'k' => Sprites.InteriorWallNSRightRight,
			'l' => Sprites.InteriorWallHorizontalTop,
			'm' => Sprites.Fridge,
			'n' => Sprites.LowerCabnetWithDraws,
			'o' => Sprites.PotPlant1,
			'p' => Sprites.MicroWave,
			'q' => Sprites.NPC11,
			's' => Sprites.DiningSet.Get(Subtract((i, j), FindTileInMap('s')!.Value).Reverse()),
			'u' => Sprites.Carpet,
			'v' => Sprites.WeirdMonster,
			'w' => Sprites.PotPlant1,
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
				case 'q':
					promptText = new string[]
					{
						"Mozin0's Mum:",
						"Welcome to my house, My son always gifts guests.",
						"He's Upstairs, go talk to him to recieve your gift.",
					};
					break;
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
			'i' => true,
			'j' => true,
			'1' => true,
			'2' => true,
			'u' => true,
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
				map.SpawnCharacterOn('2');
				break;
			case 'i':
				map = new House1SecondFloor();
				map.SpawnCharacterOn('i');
				break;
			case 'j':
				map = new House1SecondFloor();
				map.SpawnCharacterOn('j');
				break;

		}
	}
}
