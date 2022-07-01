namespace Console_Monsters.Maps;

class Route2 : Map
{
	public override char[][] SpriteSheet => new char[][]
		{
			"ffffffffffffffffffffffffffffffffffffffffffffffffffff".ToCharArray(),
			"fggggggggggggggggggggggg    ggggggggg       GGGGGGGf".ToCharArray(),
			" ggggggggggggggggggggggg    ggggggggg       GGGGGGGf".ToCharArray(),
			" ggggggggggggggggggggggg    gggggggggTTTTTTTGGGGGGGf".ToCharArray(),
			"fggggggggggggggggggggggg    gggggggggggggggT       f".ToCharArray(),
			"fggggggggggggggggggggggg    gggggggggggggggT       f".ToCharArray(),
			"fggggggggggggggTTTTTTTTTTTTTTggggggggggggggT       f".ToCharArray(),
			"fgggggggggggggg             TTTTTTTTgggggggT        ".ToCharArray(),
			"fgggggggggggggg             GGGGGGGGgggggggT        ".ToCharArray(),
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
					Console.Clear();
					Console.WriteLine();
					Console.WriteLine("Sign Says:");
					Console.WriteLine();
					Console.WriteLine("----->");
					Console.WriteLine("Aalborg City");
					Console.WriteLine("<-----");
					Console.WriteLine("Vejle Town");
					Console.WriteLine();
					Console.Write(" Press [enter] to continue...");
					PressEnterToContiue();
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

	public override void PerformTileAction()
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
					Console.Clear();
					if (!DisableBattleTransition)
					{
						Renderer.RenderBattleTransition();
					}
					Renderer.RenderBattleView();
					PressEnterToContiue();
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Clear();
				}
				break;
		}
	}
}
