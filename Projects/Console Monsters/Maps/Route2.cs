﻿namespace Console_Monsters.Maps;

class Route2 : MapBase
{
	private static readonly char[][] spriteSheet = new char[][]
		{
			"TTTTTTTTTTTT                                             ".ToCharArray(),
			"TTTTTTTTTTTT                                             ".ToCharArray(),
			"TTTTTTTTTTTT                                             ".ToCharArray(),
			"TTTTTaaaaaaafffffffffffffffffffffffffffffffffffffffffffff".ToCharArray(),
			"TTTTTaaaaaaaggggggggggggggggg    ggggggggg       GGGGGGGf".ToCharArray(),
			"TTTTTaaaaaaaagggggggggggggggg    ggggggggg       GGGGGGGf".ToCharArray(),
			"     !ggggggggggggggggggggggg    gggggggggTTTTTTTGGGGGGGf".ToCharArray(),
			"     fggggggggggggggggggggggg    gggggggggggggggT       f".ToCharArray(),
			"     fggggggggggggggggggggggg    gggggggggggggggT       f".ToCharArray(),
			"     fggggggggggggggTTTTTTTTTTTTTTggggggggggggggT       f".ToCharArray(),
			"     fgggggggggggggg             TTTTTTTTgggggggT       !".ToCharArray(),
			"     fgggggggggggggg             GGGGGGGGgggggggT       !".ToCharArray(),
			"     fggggggggggggggTTTTTTTTTs   GGGGGGGGgggggggTTTTTTTTf".ToCharArray(),
			"     fggggggggggggggggggggggT    TTTTTTTTgggggggggggggggf".ToCharArray(),
			"     fffffffffffffffffffffffff00fffffffffffffffffffffffff".ToCharArray(),
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
			// no actions
			's' => Sprites.SignARight,
			'f' => Sprites.Fence,
			'g' => Sprites.GrassDec,
			'G' => Sprites.Grass,
			'T' => Sprites.Tree2,
			'a' => Sprites.Camping3x8.Get(Subtract((i, j), FindTileInMap('a')!.Value).Reverse()),
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
			's' => true,
			_ => false,
		};
	}

	public override void InteractWithMapTile(int i, int j)
	{
		if (j >= 0 && j < SpriteSheet.Length && i >= 0 && i < SpriteSheet[j].Length)
		{
			switch (SpriteSheet[j][i])
			{
				case 's':
					PromptText = new string[]
					{
						"Sign Says:",
						"Vejle Town <----- -----> Aalborg City",
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
			'g' => true,
			'G' => true,
			_ => false,
		};
	}

	public override void PerformTileAction(int i, int j)
	{
		if (j < 0 || j >= SpriteSheet.Length || i < 0 || i >= SpriteSheet[j].Length)
		{
			return;
		}
		switch (SpriteSheet[j][i])
		{
			case '0':
				Map = new Route1();
				Map.SpawnCharacterOn('1');
				break;
			case 'G':
				if (!DisableBattle && Random.Shared.Next(2) is 0) // BATTLE CHANCE
				{
					Console.Clear();
					if (!DisableBattleTransition)
					{
						BattleTransition.Random();
					}
					BattleScreen.Render(partyMonsters[0], MonsterBase.GetRandom());
					//Battle();
					ConsoleHelper.PressToContinue();
				}
				break;
		}
	}
}
