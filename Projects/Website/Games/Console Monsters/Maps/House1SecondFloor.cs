using System;
using System.Linq;
using static Website.Games.Console_Monsters.Statics;
//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;
using Website.Games.Console_Monsters.Utilities;
using Towel;
using static Towel.Statics;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters.Maps;

public class House1SecondFloor : MapBase
{
	private readonly char[][] spriteSheet =
		[
			"afffffffffffffffb".ToCharArray(),
			"hpq  uv   -     g".ToCharArray(),
			"hno          kemg".ToCharArray(),
			"h      yy   xkemg".ToCharArray(),
			"hrrr       wzkijg".ToCharArray(),
			"cllllllllllllllld".ToCharArray(),
		];

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
			'0' => Sprites.StairsLeft,
			// non actions
			'a' => Sprites.InteriorWallSEShort,
			'b' => Sprites.InteriorWallSWShort,
			'c' => Sprites.InteriorWallNEShort,
			'd' => Sprites.InteriorWallNWShort,
			'e' => Sprites.StairsLeft,
			'f' => Sprites.InteriorWallHorizontalBottmn,
			'g' => Sprites.InteriorWallNSLeft,
			'h' => Sprites.InteriorWallNSMid,
			'i' => Sprites.StairsLeft,
			'j' => Sprites.StairsRight,
			'k' => Sprites.InteriorWallNSRightRight,
			'l' => Sprites.InteriorWallHorizontalTop,
			'm' => Sprites.StairsRight,
			'n' => Sprites.TVDeskLeft,
			'o' => Sprites.TVDeskRight,
			'p' => Sprites.TVLeft,
			'q' => Sprites.TVRight,
			'r' => Sprites.Bed1x3.Get(Subtract((i, j), FindTileInMap('r')!.Value).Reverse()),
			'u' => Sprites.ChairLeft,
			'v' => Sprites.Table,
			'w' => Sprites.Penguin,
			'x' => Sprites.Lamp,
			'y' => Sprites.Carpet,
			'z' => Sprites.Lamp2,
			'-' => Sprites.PotPlant2,
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
			'r' => true,
			'w' => true,
			_ => false,
		};
	}

	public override void InteractWithMapTile(int i, int j)
	{
		if (j >= 0 && j < SpriteSheet.Length && i >= 0 && i < SpriteSheet[j].Length)
		{
			switch (SpriteSheet[j][i])
			{
				case 'r':
					promptText =
					[
						"Mozin0:",
						"ZzzZzzZzz...",
						"MonsterBoz...",
						"ZzzZzzZzz...",
					];
					break;
				case 'w':
					promptText =
					[
						"Penguin:",
						"BrrrRRRrrr!",
					];
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
			'm' => true,
			'n' => true,
			'o' => true,
			'e' => true,
			'y' => true,
			_ => false,
		};
	}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
	public override async Task PerformTileAction(int i, int j)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
	{
		switch (SpriteSheet[j][i])
		{
			case 'i':
				Map = new House1();
				Map.SpawnCharacterOn('1');
				break;
			case 'j':
				Map = new House1();
				Map.SpawnCharacterOn('2');
				break;
		}
	}
}
