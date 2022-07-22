namespace Console_Monsters.Maps;

class Western : MapBase
{
	private static readonly char[][] spriteSheet = new char[][]
		{
			"bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb".ToCharArray(),
			"bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb".ToCharArray(),
			"bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb".ToCharArray(),
			"bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb".ToCharArray(),
			"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa".ToCharArray(),
			"0aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa!".ToCharArray(),
			"0aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa!".ToCharArray(),
			"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa".ToCharArray(),
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
			'0' => Sprites.ArrowHeavyLeft,
			// no actions
			'a' => Sprites.DesertGround4x11.Get(Modulus(Subtract((i, j), FindTileInMap('a')!.Value).Reverse(), (null, 11))),
			'b' => Sprites.WesternBackground4x36.Get(Modulus(Subtract((i, j), FindTileInMap('b')!.Value).Reverse(), (null, 36))),
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
			//'<char>' => true,
			_ => false,
		};
	}

	public override void InteractWithMapTile(int i, int j)
	{
		if (j >= 0 && j < SpriteSheet.Length && i >= 0 && i < SpriteSheet[j].Length)
		{
			switch (SpriteSheet[j][i])
			{
				//case '<char>':
				//	break;
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
			'0' => true,
			'1' => true,
			' ' => true,
			'a' => true,
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
				Map = new Route2();
				Map.SpawnCharacterOn('1');
				break;
			case 'a':
				if (!DisableBattle && Random.Shared.Next(4) is 0) // BATTLE CHANCE
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
