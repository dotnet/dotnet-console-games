namespace Console_Monsters.Maps;

class Route2 : Map
{
	public override char[][] SpriteSheet()
	{
		return new char[][]
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
			"fffffffffffffffffffffffff22fffffffffffffffffffffffff".ToCharArray(),
		};
	}

	public override void InteractWithMapTile(int tileI, int tileJ)
	{
		var s = map.SpriteSheet();

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
}
