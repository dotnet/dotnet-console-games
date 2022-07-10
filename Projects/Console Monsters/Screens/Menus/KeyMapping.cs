namespace Console_Monsters.Screens.Menus;

public static class KeyMapping
{
	public static void KeyMappingMenu()
	{
		StringBuilder sb = new();

		int arrowOption = 1;
		const int maxOption = 6;

		string optionIndent = new(' ', 50);
		string headerIndent = new(' ', 85);
		string titleIndent = new(' ', 40);

		string[] currentUp = Sprites.W;
		string[] currentDown = Sprites.S;
		string[] currentLeft = Sprites.A;
		string[] currentRight = Sprites.D;

		string[] currentUpAlt = Sprites.UpArrow;
		string[] currentDownAlt = Sprites.DownArrow;
		string[] currentLeftAlt = Sprites.LeftArrow;
		string[] currentRightAlt = Sprites.RightArrow;

		string[] currentInteract = Sprites.E;
		string[] currentInteractAlt = Sprites.Enter;

		string boxTop = Sprites.BoxTop;
		string boxSide = Sprites.BoxSide;
		string boxBottom = Sprites.BoxBottom;
		string boxEmpty = new(' ', 9);

		int upOption = 2;

		Console.Clear();
		ReDraw:
		sb.Clear();

		sb.Append('\n', 3); // SET TO 6 WHEN SCROLL
		sb.AppendLine(@$"{titleIndent}██╗  ██╗███████╗██╗   ██╗    ███╗   ███╗ █████╗ ██████╗ ██████╗ ██╗███╗   ██╗ ██████╗ ");
		sb.AppendLine(@$"{titleIndent}██║ ██╔╝██╔════╝╚██╗ ██╔╝    ████╗ ████║██╔══██╗██╔══██╗██╔══██╗██║████╗  ██║██╔════╝ ");
		sb.AppendLine(@$"{titleIndent}█████╔╝ █████╗   ╚████╔╝     ██╔████╔██║███████║██████╔╝██████╔╝██║██╔██╗ ██║██║  ███╗");
		sb.AppendLine(@$"{titleIndent}██╔═██╗ ██╔══╝    ╚██╔╝      ██║╚██╔╝██║██╔══██║██╔═══╝ ██╔═══╝ ██║██║╚██╗██║██║   ██║");
		sb.AppendLine(@$"{titleIndent}██║  ██╗███████╗   ██║       ██║ ╚═╝ ██║██║  ██║██║     ██║     ██║██║ ╚████║╚██████╔╝");
		sb.AppendLine(@$"{titleIndent}╚═╝  ╚═╝╚══════╝   ╚═╝       ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝     ╚═╝╚═╝  ╚═══╝ ╚═════╝ ");
		sb.Append('\n', 3); // SET TO 6 WHEN SCROLL
		sb.AppendLine($@"{headerIndent}▄   ▄  ▄▄  ▄ ▄   ▄   █    ▄▄  ▄   ▄▄▄▄▄");
		sb.AppendLine(@$"{headerIndent}█▀▄▀█ █▄▄█ █ █▀▄ █   █   █▄▄█ █     █  ");
		sb.AppendLine(@$"{headerIndent}█   █ █  █ █ █  ▀█   █   █  █ █▄▄   █  ");
		sb.AppendLine();
		sb.AppendLine($@"{new(' ', 89)}{(upOption == 1 ? boxTop : boxEmpty)}{new(' ', 18)}{boxTop}");
		sb.AppendLine(@$"{optionIndent}▄  ▄ ▄▄▄   {new(' ', 28)}{boxSide} {currentUp[0]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentUpAlt[0]}   {(arrowOption is 1 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█  █ █▄▄▀ ▀{new(' ', 28)}{boxSide} {currentUp[1]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentUpAlt[1]}   {(arrowOption is 1 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}▀▄▄▀ █    ▄{new(' ', 28)}{boxSide} {currentUp[2]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentUpAlt[2]}   {(arrowOption is 1 ? "╰───╯" : "     ")}");
		sb.AppendLine($@"{new(' ', 89)}{boxBottom}{new(' ', 18)}{boxBottom}");
		sb.AppendLine();
		sb.AppendLine($@"{new(' ', 89)}{boxTop}{new(' ', 18)}{boxTop}");
		sb.AppendLine(@$"{optionIndent}▄▄▄   ▄▄  ▄   ▄ ▄   ▄  {new(' ', 16)}{boxSide} {currentDown[0]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentDownAlt[0]}   {(arrowOption is 2 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█  █ █  █ █ ▄ █ █▀▄ █ ▀{new(' ', 16)}{boxSide} {currentDown[1]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentDownAlt[1]}   {(arrowOption is 2 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█▄▄▀ ▀▄▄▀ █▀ ▀█ █  ▀█ ▄{new(' ', 16)}{boxSide} {currentDown[2]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentDownAlt[2]}   {(arrowOption is 2 ? "╰───╯" : "     ")}");
		sb.AppendLine($@"{new(' ', 89)}{boxBottom}{new(' ', 18)}{boxBottom}");
		sb.AppendLine();
		sb.AppendLine($@"{new(' ', 89)}{boxTop}{new(' ', 18)}{boxTop}");
		sb.AppendLine(@$"{optionIndent}▄   ▄▄▄▄ ▄▄▄▄ ▄▄▄▄▄  {new(' ', 18)}{boxSide} {currentLeft[0]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentLeftAlt[0]}   {(arrowOption is 3 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█   █▄▄  █▄▄    █   ▀{new(' ', 18)}{boxSide} {currentLeft[1]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentLeftAlt[1]}   {(arrowOption is 3 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█▄▄ █▄▄▄ █      █   ▄{new(' ', 18)}{boxSide} {currentLeft[2]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentLeftAlt[2]}   {(arrowOption is 3 ? "╰───╯" : "     ")}");
		sb.AppendLine($@"{new(' ', 89)}{boxBottom}{new(' ', 18)}{boxBottom}");
		sb.AppendLine();
		sb.AppendLine($@"{new(' ', 89)}{boxTop}{new(' ', 18)}{boxTop}");
		sb.AppendLine(@$"{optionIndent}▄▄▄  ▄  ▄▄▄  ▄  ▄ ▄▄▄▄▄  {new(' ', 14)}{boxSide} {currentRight[0]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentRightAlt[0]}   {(arrowOption is 4 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█▄▄▀ █ █  ▄▄ █▄▄█   █   ▀{new(' ', 14)}{boxSide} {currentRight[1]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentRightAlt[1]}   {(arrowOption is 4 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█  █ █ ▀▄▄▄▀ █  █   █   ▄{new(' ', 14)}{boxSide} {currentRight[2]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentRightAlt[2]}   {(arrowOption is 4 ? "╰───╯" : "     ")}");
		sb.AppendLine($@"{new(' ', 89)}{boxBottom}{new(' ', 18)}{boxBottom}");
		sb.AppendLine();
		sb.AppendLine($@"{new(' ', 89)}{boxTop}{new(' ', 18)}{boxTop}");
		sb.AppendLine(@$"{optionIndent} ▄▄   ▄▄▄ ▄▄▄▄▄ ▄  ▄▄  ▄   ▄  {new(' ', 9)}{boxSide} {currentInteract[0]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentInteractAlt[0]}   {(arrowOption is 5 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█▄▄█ █      █   █ █  █ █▀▄ █ ▀{new(' ', 9)}{boxSide} {currentInteract[1]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentInteractAlt[1]}   {(arrowOption is 5 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█  █ ▀▄▄▄   █   █ ▀▄▄▀ █  ▀█ ▄{new(' ', 9)}{boxSide} {currentInteract[2]} {boxSide}{new(' ', 8)}█{new(' ', 11)}{currentInteractAlt[2]}   {(arrowOption is 5 ? "╰───╯" : "     ")}");
		sb.AppendLine($@"{new(' ', 89)}{boxBottom}{new(' ', 18)}{boxBottom}");
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent}█▀▀▄  ▄▄   ▄▄▄ ▄  ▄   {(arrowOption is maxOption ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█■■█ █▄▄█ █    █■█    {(arrowOption is maxOption ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█▄▄▀ █  █ ▀▄▄▄ █  ▀▄  {(arrowOption is maxOption ? "╰───╯" : "     ")}");

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
					case 4: goto ReDraw;
					case maxOption: break;
				}
				break;
			case ConsoleKey.Escape: Options.OptionsMenu(); break;
			default: goto ReDraw;
		}
	}
}
