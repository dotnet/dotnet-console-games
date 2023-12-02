using System;
using System.Linq;
using static Website.Games.Console_Monsters.Statics;
//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;
using Website.Games.Console_Monsters.Characters;
using Website.Games.Console_Monsters.Utilities;
using Towel;
using static Towel.Statics;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters.Maps;

class PaletTown : MapBase
{
	public Scientist scientist;

	public PaletTown()
	{
		scientist = new();
	}

	private readonly char[][] spriteSheet =
		[
			"tttttgggggggfgggggf11fgggggfgggggttttt".ToCharArray(),
			"tttttggggffffffffff  ffffffffggggttttt".ToCharArray(),
			"tttttggggfg                gfggggttttt".ToCharArray(),
			"tttttggggfg  bbbb    cccc  gfggggttttt".ToCharArray(),
			"tttttggggfg  bbbb    cccc  gfggggttttt".ToCharArray(),
			"tttttggggfg sb2bb   scccc  gfggggttttt".ToCharArray(),
			"tttttggggfg  p             gfggggttttt".ToCharArray(),
			"tttttggggfg                gfggggttttt".ToCharArray(),
			"tttttggggfg        dddddd  gfggggttttt".ToCharArray(),
			"tttttggggfg  FFFš  dddddd  gfggggttttt".ToCharArray(),
			"tttttggggfg  gggg  dddddd  gfggggttttt".ToCharArray(),
			"tttttggggfg  gggg  d0dddd  gfggggttttt".ToCharArray(),
			"tttttggggfg           n    gfggggttttt".ToCharArray(),
			"tttttggggfg      X         gfggggttttt".ToCharArray(),
			"tttttggggfg  o     FFFśFF  gfggggttttt".ToCharArray(),
			"tttttggggfgggWWWW  gggggg  gfggggttttt".ToCharArray(),
			"tttttggggfgggWwwW  gggggg  gfggggttttt".ToCharArray(),
			"tttttggggfgggWwwW          gfggggttttt".ToCharArray(),
			"tttttggggffggWwwWffffffffffffggggttttt".ToCharArray(),
			"tttttggggffffWwwWffffffffffffggggttttt".ToCharArray(),
			"tttttggggggggWwwWggggggfgggggggggttttt".ToCharArray(),
			"tttttggggggggWwwWggggggfgggggggggttttt".ToCharArray(),
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
			// spawn
			'X' => Sprites.Open,
			// actions
			'0' => Sprites.Door,
			'1' => Sprites.ArrowHeavyUp,
			'2' => Sprites.Door,
			// Buildings
			'b' => Sprites.House3x4.Get(Subtract((i, j), FindTileInMap('b')!.Value).Reverse()),
			'c' => Sprites.House3x4.Get(Subtract((i, j), FindTileInMap('c')!.Value).Reverse()),
			'd' => Sprites.House4x6.Get(Subtract((i, j), FindTileInMap('d')!.Value).Reverse()),
			// Decor
			'š' => Sprites.SignARight,
			's' => Sprites.SignALeft,
			'ś' => Sprites.SignALeft,
			'f' => Sprites.Fence,
			'F' => Sprites.FenceLow,
			// Nature
			'w' => Sprites.Water,
			'g' => Sprites.GrassDec,
			'G' => Sprites.Grass,
			't' => Sprites.Tree,
			'T' => Sprites.Tree2,
			// NPCs
			'n' => Sprites.NPC1,
			'o' => scientist.Sprite,
			'p' => Sprites.NPC5,
			// Extra
			'W' => Sprites.Wall_0000,
			'z' => Sprites.Door,
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
			's' or 'a' => true,
			'ś' => true,
			'o' => true,
			_ => false,
		};
	}

	public override void InteractWithMapTile(int i, int j)
	{
		if (j >= 0 && j < SpriteSheet.Length && i >= 0 && i < SpriteSheet[j].Length)
		{
			switch (SpriteSheet[j][i])
			{
				case 's' or 'a':
					promptText =
					[
						"Sign Says:",
						"Hello! I am sign. :P",
					];
					break;
				case 'ś':
					promptText =
					[
						"Sign #2 Says:",
						"Hello! I am sign #2. :P",
					];
					break;
				case 'o':
					promptText = scientist.Dialogue;
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
			'1' => true,
			'2' => true,
			'X' => true,
			'g' => true,
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
				Map = new Center1();
				Map.SpawnCharacterOn('0');
				break;
			case '1':
				Map = new Route1();
				Map.SpawnCharacterOn('0');
				break;
			case '2':
				Map = new House1();
				Map.SpawnCharacterOn('0');
				break;
		}
	}
}
