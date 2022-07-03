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

namespace Website.Games.Console_Monsters.Maps;

class Route1 : MapBase
{
	public override char[][] SpriteSheet => new char[][]
		{
			"gggfgggggf11fgggggfg".ToCharArray(),
			"gggfgggggf  fgggggfg".ToCharArray(),
			"gggfgggggf  fgggggfg".ToCharArray(),
			"gggfgggggf  fgggggfg".ToCharArray(),
			"gggfgggggf  fgggggfg".ToCharArray(),
			"gggfffffff  fffffffg".ToCharArray(),
			"gggfgggggg  ggggggfg".ToCharArray(),
			"gggfgggggg  ggggggfg".ToCharArray(),
			"gggfgggggT        fg".ToCharArray(),
			"gggfŕŕŕŕŕTrrrr    fg".ToCharArray(),
			"gggTgggggTGGGGGGGGfg".ToCharArray(),
			"gggTgggggTGGGGGGGGfg".ToCharArray(),
			"gggTgggggTGGGGGGGGfg".ToCharArray(),
			"gggTŕŕŕŕŕTGGGGGGGGfg".ToCharArray(),
			"gggTgggggggg      fg".ToCharArray(),
			"gggTgggggggg      fg".ToCharArray(),
			"gggTgggggggg  GGGGfg".ToCharArray(),
			"gggTTTŕŕŕŕTTTTGGGGfg".ToCharArray(),
			"gggfgggggggg  GGGGfg".ToCharArray(),
			"gggfgggggggg  GGGGfg".ToCharArray(),
			"gggfgg        ggggfg".ToCharArray(),
			"gggfgg        ggggfg".ToCharArray(),
			"gggfgg  ggggggggggfg".ToCharArray(),
			"gggfŕşrrŕşŕŕŕŕŕŕŕŕfg".ToCharArray(),
			"gggfgg            fg".ToCharArray(),
			"gggfgg            fg".ToCharArray(),
			"gggfgg      GGGG  fg".ToCharArray(),
			"gggfTTTTTTTTGGGGrrfg".ToCharArray(),
			"gggfggggggggGGGG  fg".ToCharArray(),
			"gggfggggggggGGGG  fg".ToCharArray(),
			"gggf              fg".ToCharArray(),
			"gggTrr   srrrrrrrrTg".ToCharArray(),
			"gggTggGGGG  ggGGGGTg".ToCharArray(),
			"gggTggGGGG  ggGGGGTg".ToCharArray(),
			"gggTggGGGG  GGGGggTg".ToCharArray(),
			"gggTGGGGgg  GGGGggTg".ToCharArray(),
			"gggTffffffGGffffffTg".ToCharArray(),
			"gggTgggggfGGfgggggTg".ToCharArray(),
			"gggTgggggfGGfgggggTg".ToCharArray(),
			"gggfgggggf00fgggggfg".ToCharArray(),
		};

	public override string GetMapTileRender(int tileI, int tileJ)
	{
		char[][] s = map.SpriteSheet;
		if (tileJ < 0 || tileJ >= s.Length || tileI < 0 || tileI >= s[tileJ].Length)
		{
			return Sprites.Open;
		}
		return s[tileJ][tileI] switch
		{
			// actions
			'0' => Sprites.ArrowDown,
			'1' => Sprites.ArrowUp,
			// Decor
			's' => Sprites.Sign,
			'f' => Sprites.Fence,
			// Nature
			'g' => Sprites.GrassDec,
			'G' => Sprites.Grass,
			'T' => Sprites.Tree2,
			'r' => Sprites.HalfRock,
			'ŕ' => Sprites.HalfRockGrass,
			'ş' => Sprites.HalfRockStairsGrass,
			// Extra
			' ' => Sprites.Open,
			_ => Sprites.Error,
		};
	}

	public override void InteractWithMapTile(int tileI, int tileJ)
	{
		char[][] s = map.SpriteSheet;

		Interact(tileI, tileJ + 1);
		Interact(tileI, tileJ - 1);
		Interact(tileI - 1, tileJ);
		Interact(tileI + 1, tileJ);

		void Interact(int i, int j)
		{
			if (j >= 0 && j < s.Length && i >= 0 && i < s[j].Length)
			{
				if (s[j][i] == 's')
				{
					promptText = new string[]
						{
							"Sign Says:",
							"Hello! I am a sign. :P",
						};
				}
			}
		}
	}

	public override bool IsValidCharacterMapTile(int tileI, int tileJ)
	{
		char[][] s = map.SpriteSheet;
		if (tileJ < 0 || tileJ >= s.Length || tileI < 0 || tileI >= s[tileJ].Length)
		{
			return false;
		}
		char c = s[tileJ][tileI];
		return c switch
		{
			' ' => true,
			'0' => true,
			'1' => true,
			'g' => true,
			'G' => true,
			'ş' => true,
			_ => false,
		};
	}

	public override async Task PerformTileAction()
	{
		var (i, j) = WorldToTile(character.I, character.J);
		char[][] s = map.SpriteSheet;
		switch (s[j][i])
		{
			case '0':
				map = new PaletTown();
				SpawnCharacterOn('1');
				break;
			case '1':
				map = new Route2();
				SpawnCharacterOn('0');
				break;
			case 'G':
				if (!DisableBattle && Random.Shared.Next(2) is 0) // BATTLE CHANCE
				{
					await _using.Console.Clear();
					if (!DisableBattleTransition)
					{
						await Renderer.RenderBattleTransition();
					}
					await Renderer.RenderBattleView();
					await PressEnterToContiue();
					_using.Console.BackgroundColor = ConsoleColor.Black;
					_using.Console.ForegroundColor = ConsoleColor.White;
					await _using.Console.Clear();
				}
				break;
		}
	}
}
