namespace Console_Monsters.Menus;

public class KeyMapping
{
	public static void KeyMappingMenu()
	{
		StringBuilder sb = new();

		int arrowOption = 1;
		const int maxOption = 6;

		string optionIndent = new(' ', 50);
		string headerIndent = new(' ', 85);
		string titleIndent = new(' ', 40);
		string newLineOptions = new('\n', 2);
		string newLineTitle = new('\n', 3); // DEFAULT 6


		string[] currentUp = Sprites.W;
		string[] currentDown = Sprites.S;
		string[] currentLeft = Sprites.A;
		string[] currentRight = Sprites.D;

		string[] currentUpAlt = Sprites.UpArrow;
		string[] currentDownAlt = Sprites.DownArrow;
		string[] currentLeftAlt = Sprites.LeftArrow;
		string[] currentRightAlt = Sprites.RightArrow;

		Console.Clear();
		ReDraw:
		sb.Clear();

		sb.AppendLine(@$"{newLineTitle}");
		sb.AppendLine(@$"{titleIndent}██╗  ██╗███████╗██╗   ██╗    ███╗   ███╗ █████╗ ██████╗ ██████╗ ██╗███╗   ██╗ ██████╗ ");
		sb.AppendLine(@$"{titleIndent}██║ ██╔╝██╔════╝╚██╗ ██╔╝    ████╗ ████║██╔══██╗██╔══██╗██╔══██╗██║████╗  ██║██╔════╝ ");
		sb.AppendLine(@$"{titleIndent}█████╔╝ █████╗   ╚████╔╝     ██╔████╔██║███████║██████╔╝██████╔╝██║██╔██╗ ██║██║  ███╗");
		sb.AppendLine(@$"{titleIndent}██╔═██╗ ██╔══╝    ╚██╔╝      ██║╚██╔╝██║██╔══██║██╔═══╝ ██╔═══╝ ██║██║╚██╗██║██║   ██║");
		sb.AppendLine(@$"{titleIndent}██║  ██╗███████╗   ██║       ██║ ╚═╝ ██║██║  ██║██║     ██║     ██║██║ ╚████║╚██████╔╝");
		sb.AppendLine(@$"{titleIndent}╚═╝  ╚═╝╚══════╝   ╚═╝       ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝     ╚═╝╚═╝  ╚═══╝ ╚═════╝ ");
		sb.AppendLine(@$"{newLineTitle}");
		sb.AppendLine($@"{headerIndent}▄   ▄  ▄▄  ▄ ▄   ▄   █    ▄▄  ▄   ▄▄▄▄▄");
		sb.AppendLine(@$"{headerIndent}█▀▄▀█ █▄▄█ █ █▀▄ █   █   █▄▄█ █     █  ");
		sb.AppendLine(@$"{headerIndent}█   █ █  █ █ █  ▀█   █   █  █ █▄▄   █  ");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent}▄  ▄ ▄▄▄   {new(' ', 30)}{currentUp[0]}{new(' ', 10)}█{new(' ', 11)}{currentUpAlt[0]}   {(arrowOption is 1 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█  █ █▄▄▀ ▀{new(' ', 30)}{currentUp[1]}{new(' ', 10)}█{new(' ', 11)}{currentUpAlt[1]}   {(arrowOption is 1 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}▀▄▄▀ █    ▄{new(' ', 30)}{currentUp[2]}{new(' ', 10)}█{new(' ', 11)}{currentUpAlt[2]}   {(arrowOption is 1 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent}▄▄▄   ▄▄  ▄   ▄ ▄   ▄  {new(' ', 18)}{currentDown[0]}{new(' ', 12 + currentDown[0].Length)}█{new(' ', 16)}{currentDownAlt[0]}   {(arrowOption is 2 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█  █ █  █ █ ▄ █ █▀▄ █ ▀{new(' ', 18)}{currentDown[1]}{new(' ', 12 + currentDown[1].Length)}█{new(' ', 16)}{currentDownAlt[1]}   {(arrowOption is 2 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█▄▄▀ ▀▄▄▀ █▀ ▀█ █  ▀█ ▄{new(' ', 18)}{currentDown[2]}{new(' ', 12 + currentDown[2].Length)}█{new(' ', 16)}{currentDownAlt[2]}   {(arrowOption is 2 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent}▄   ▄▄▄▄ ▄▄▄▄ ▄▄▄▄▄            {currentLeft[0]}{new(' ', 12 + currentLeft[0].Length)}█{new(' ', 11)}{currentLeftAlt[0]}   {(arrowOption is 3 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█   █▄▄  █▄▄    █   ▀          {currentLeft[1]}{new(' ', 12 + currentLeft[1].Length)}█{new(' ', 11)}{currentLeftAlt[1]}   {(arrowOption is 3 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█▄▄ █▄▄▄ █      █   ▄          {currentLeft[2]}{new(' ', 12 + currentLeft[2].Length)}█{new(' ', 11)}{currentLeftAlt[2]}   {(arrowOption is 3 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent}▄▄▄  ▄  ▄▄▄  ▄  ▄ ▄▄▄▄▄        {currentRight[0]}{new(' ', 12 + currentRight[0].Length)}█{new(' ', 11)}{currentRightAlt[0]}   {(arrowOption is 4 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█▄▄▀ █ █  ▄▄ █▄▄█   █   ▀      {currentRight[1]}{new(' ', 12 + currentRight[1].Length)}█{new(' ', 11)}{currentRightAlt[1]}   {(arrowOption is 4 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█  █ █ ▀▄▄▄▀ █  █   █   ▄      {currentRight[2]}{new(' ', 12 + currentRight[2].Length)}█{new(' ', 11)}{currentRightAlt[2]}   {(arrowOption is 4 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent} ▄▄   ▄▄▄ ▄▄▄▄▄ ▄  ▄▄  ▄   ▄   {currentRight[0]}{new(' ', 12 + currentRight[0].Length)}█{new(' ', 11)}{currentRightAlt[0]}   {(arrowOption is 5 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█▄▄█ █      █   █ █  █ █▀▄ █ ▀ {currentRight[1]}{new(' ', 12 + currentRight[1].Length)}█{new(' ', 11)}{currentRightAlt[1]}   {(arrowOption is 5 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█  █ ▀▄▄▄   █   █ ▀▄▄▀ █  ▀█ ▄ {currentRight[2]}{new(' ', 12 + currentRight[2].Length)}█{new(' ', 11)}{currentRightAlt[2]}   {(arrowOption is 5 ? "╰───╯" : "     ")}");

		Console.SetCursorPosition(0, 0);
		Console.WriteLine(sb);

		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.UpArrow or ConsoleKey.W: arrowOption = Math.Max(1, arrowOption - 1); goto ReDraw;
			case ConsoleKey.DownArrow or ConsoleKey.S: arrowOption = Math.Min(maxOption, arrowOption + 1); goto ReDraw;
			case ConsoleKey.Enter or ConsoleKey.E:
				switch (arrowOption)
				{
					case 1: goto ReDraw;
					case 2: goto ReDraw;
					case 3: goto ReDraw;
					case 4: break;
					case maxOption: break;
				}
				break;
			case ConsoleKey.Escape: Options.OptionsMenu(); break;
			default: goto ReDraw;
		}
	}
}
