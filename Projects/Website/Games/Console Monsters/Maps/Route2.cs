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

class Route2 : MapBase
{
	public override char[][] SpriteSheet => new char[][]
		{
			"ffffffffffffffffffffffffffffffffffffffffffffffffffff".ToCharArray(),
			"fggggggggggggggggggggggg    ggggggggg       GGGGGGGf".ToCharArray(),
			"!ggggggggggggggggggggggg    ggggggggg       GGGGGGGf".ToCharArray(),
			"!ggggggggggggggggggggggg    gggggggggTTTTTTTGGGGGGGf".ToCharArray(),
			"fggggggggggggggggggggggg    gggggggggggggggT       f".ToCharArray(),
			"fggggggggggggggggggggggg    gggggggggggggggT       f".ToCharArray(),
			"fggggggggggggggTTTTTTTTTTTTTTggggggggggggggT       f".ToCharArray(),
			"fgggggggggggggg             TTTTTTTTgggggggT       !".ToCharArray(),
			"fgggggggggggggg             GGGGGGGGgggggggT       !".ToCharArray(),
			"fggggggggggggggTTTTTTTTTs   GGGGGGGGgggggggTTTTTTTTf".ToCharArray(),
			"fggggggggggggggggggggggT    TTTTTTTTgggggggggggggggf".ToCharArray(),
			"fffffffffffffffffffffffff00fffffffffffffffffffffffff".ToCharArray(),
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
			// no actions
			's' => Sprites.Sign,
			'f' => Sprites.Fence,
			'g' => Sprites.GrassDec,
			'G' => Sprites.Grass,
			'T' => Sprites.Tree2,
			' ' => Sprites.Open,
			_ => Sprites.Error,
		};
	}

	public override async Task InteractWithMapTile(int tileI, int tileJ)
	{
		char[][] s = map.SpriteSheet;

		await Interact(tileI, tileJ + 1);
		await Interact(tileI, tileJ - 1);
		await Interact(tileI - 1, tileJ);
		await Interact(tileI + 1, tileJ);

		async Task Interact(int i, int j)
		{
			if (j >= 0 && j < s.Length && i >= 0 && i < s[j].Length)
			{
				if (s[j][i] == 's')
				{
					await _using.Console.Clear();
					await _using.Console.WriteLine();
					await _using.Console.WriteLine("Sign Says:");
					await _using.Console.WriteLine();
					await _using.Console.WriteLine("----->");
					await _using.Console.WriteLine("Aalborg City");
					await _using.Console.WriteLine("<-----");
					await _using.Console.WriteLine("Vejle Town");
					await _using.Console.WriteLine();
					await _using.Console.Write(" Press [enter] to continue...");
					await PressEnterToContiue();
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
			'g' => true,
			'G' => true,
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
				map = new Route1();
				SpawnCharacterOn('1');
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
