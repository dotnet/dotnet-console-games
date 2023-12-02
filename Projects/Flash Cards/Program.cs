using System;

(char Letter, string CodeWord)[] array = new[]
{
	( 'A', "Alpha"   ), ( 'B', "Bravo"    ), ( 'C', "Charlie" ), ( 'D', "Delta" ),
	( 'E', "Echo"    ), ( 'F', "Foxtrot"  ), ( 'G', "Golf"    ), ( 'H', "Hotel" ),
	( 'I', "India"   ), ( 'J', "Juliett"  ), ( 'K', "Kilo"    ), ( 'L', "Lima"  ),
	( 'M', "Mike"    ), ( 'N', "November" ), ( 'O', "Oscar"   ), ( 'P', "Papa"  ),
	( 'Q', "Quebec"  ), ( 'R', "Romeo"    ), ( 'S', "Sierra"  ), ( 'T', "Tango" ),
	( 'U', "Uniform" ), ( 'V', "Victor"   ), ( 'W', "Whiskey" ), ( 'X', "X-ray" ),
	( 'Y', "Yankee"  ), ( 'Z', "Zulu"     ),
};

while (true)
{
	Console.Clear();
	Console.Write("""

		  Flash Cards

		  In this game you will be doing flash card exercises
		  to help you memorize the NATO phonetic alphabet. The
		  NATO phonetic alphabet is commonly used during radio
		  communication in aviation. Each flash card will have
		  a letter from the English alphabet and you need to
		  provide the corresponding code word for that letter.

		  |  NATO phonetic alphabet code words
		  |
		  |  A -> Alpha      B -> Bravo       C -> Charlie    D -> Delta
		  |  E -> Echo       F -> Foxtrot     G -> Golf       H -> Hotel
		  |  I -> India      J -> Juliett     K -> Kilo       L -> Lima
		  |  M -> Mike       N -> November    O -> Oscar      P -> Papa
		  |  Q -> Quebec     R -> Romeo       S -> Sierra     T -> Tango
		  |  U -> Uniform    V -> Victor      W -> Whiskey    X -> X-ray
		  |  Y -> Yankee     Z -> Zulu");

		  Press [enter] to continue or [escape] to quit...
		""");
	while (true)
	{
		ConsoleKey key = Console.ReadKey(true).Key;
		if (key is ConsoleKey.Enter)
		{
			break;
		}
		if (key is ConsoleKey.Escape)
		{
			Console.Clear();
			Console.WriteLine("Flash Cards was closed.");
			return;
		}
	}
	bool returnToMainMenu = false;
	while (!returnToMainMenu)
	{
		int index = Random.Shared.Next(array.Length);
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine("  What is the NATO phonetic alphabet code word for...");
		Console.WriteLine();
		Console.Write($"  {array[index].Letter}? ");
		string input = Console.ReadLine()!;
		Console.WriteLine();
		if (input.Trim().ToUpper() == array[index].CodeWord.ToUpper())
		{
			Console.WriteLine("  Correct! :)");
		}
		else
		{
			Console.WriteLine($"  Incorrect. :( {array[index].Letter} -> {array[index].CodeWord}");
		}
		Console.Write("  Press [enter] to continue or [escape] to return to main menu...");
		while (true)
		{
			ConsoleKey key = Console.ReadKey(true).Key;
			if (key is ConsoleKey.Enter)
			{
				break;
			}
			if (key is ConsoleKey.Escape)
			{
				returnToMainMenu = true;
				break;
			}
		}
	}
}
