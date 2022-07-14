using Console_Monsters.Util;

namespace Console_Monsters.Screens;

public static class ConsoleColorSettingsScreen
{
	public static void ColorSchemeMenu()
	{
		StringBuilder sb = new();

		int arrowOption = 1;
		const int maxOption = 6;

		string optionIndent = new(' ', 50);
		string headerIndent = new(' ', 85);
		string titleIndent = new(' ', 40);

		var headerAsciiText = TextGenerator.GenerateAsciiText(@$"background {new(' ', 8)}|{new(' ', 8)} text");
		var blackAsciiText = TextGenerator.GenerateAsciiText("black");
		var whiteAsciiText = TextGenerator.GenerateAsciiText("white");
		var greenAsciiText = TextGenerator.GenerateAsciiText("green");
		var redAsciiText = TextGenerator.GenerateAsciiText("red");
		var blueAsciiText = TextGenerator.GenerateAsciiText("blue");
		var returnToOptionsText = TextGenerator.GenerateAsciiText("Back");
		
		Console.Clear();
	ReDraw:
		sb.Clear();

		// TODO: Fix this formatting later when the ability to select individual colors is available
		sb.Append('\n', 3); // SET TO 6 WHEN SCROLL
		sb.AppendLine(@$"{titleIndent}██████╗ ██████╗ ██╗      ██████╗ ██████╗     ███████╗ ██████╗██╗  ██╗███████╗███╗   ███╗███████╗");
		sb.AppendLine(@$"{titleIndent}██╔════╝██╔═══██╗██║     ██╔═══██╗██╔══██╗    ██╔════╝██╔════╝██║  ██║██╔════╝████╗ ████║██╔════╝");
		sb.AppendLine(@$"{titleIndent}██║     ██║   ██║██║     ██║   ██║██████╔╝    ███████╗██║     ███████║█████╗  ██╔████╔██║█████╗  ");
		sb.AppendLine(@$"{titleIndent}██║     ██║   ██║██║     ██║   ██║██╔══██╗    ╚════██║██║     ██╔══██║██╔══╝  ██║╚██╔╝██║██╔══╝  ");
		sb.AppendLine(@$"{titleIndent}╚██████╗╚██████╔╝███████╗╚██████╔╝██║  ██║    ███████║╚██████╗██║  ██║███████╗██║ ╚═╝ ██║███████╗");
		sb.AppendLine(@$"{titleIndent} ╚═════╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝  ╚═╝    ╚══════╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝     ╚═╝╚══════╝");
		sb.Append('\n', 3); // SET TO 6 WHEN SCROLL
		sb.AppendLine($@"{headerIndent}{headerAsciiText[0]}");
		sb.AppendLine(@$"{headerIndent}{headerAsciiText[1]}");
		sb.AppendLine(@$"{headerIndent}{headerAsciiText[2]}");
		sb.AppendLine();
		sb.AppendLine($@"{new(' ', 89)}{new(' ', 18)}");
		sb.AppendLine(@$"{optionIndent}{blackAsciiText[0]}{new(' ', 28)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 1 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{blackAsciiText[1]}{new(' ', 28)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 1 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{blackAsciiText[2]}{new(' ', 28)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 1 ? "╰───╯" : "     ")}");
		sb.AppendLine($@"{new(' ', 89)}{new(' ', 18)}");
		sb.AppendLine();
		sb.AppendLine($@"{new(' ', 89)}{new(' ', 18)}");
		sb.AppendLine(@$"{optionIndent}{whiteAsciiText[0]}{new(' ', 16)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 2 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{whiteAsciiText[1]}{new(' ', 16)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 2 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{whiteAsciiText[2]}{new(' ', 16)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 2 ? "╰───╯" : "     ")}");
		sb.AppendLine($@"{new(' ', 89)}{new(' ', 18)}");
		sb.AppendLine();
		sb.AppendLine($@"{new(' ', 89)}{new(' ', 18)}");
		sb.AppendLine(@$"{optionIndent}{greenAsciiText[0]}{new(' ', 18)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 3 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{greenAsciiText[1]}{new(' ', 18)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 3 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{greenAsciiText[2]}{new(' ', 18)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 3 ? "╰───╯" : "     ")}");
		sb.AppendLine($@"{new(' ', 89)}{new(' ', 18)}");
		sb.AppendLine();
		sb.AppendLine($@"{new(' ', 89)}{new(' ', 18)}");
		sb.AppendLine(@$"{optionIndent}{redAsciiText[0]}{new(' ', 14)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 4 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{redAsciiText[1]}{new(' ', 14)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 4 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{redAsciiText[2]}{new(' ', 14)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 4 ? "╰───╯" : "     ")}");
		sb.AppendLine($@"{new(' ', 89)}{new(' ', 18)}");
		sb.AppendLine();
		sb.AppendLine($@"{new(' ', 89)}{new(' ', 18)}");
		sb.AppendLine(@$"{optionIndent}{blueAsciiText[0]}{new(' ', 9)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 5 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{blueAsciiText[1]}{new(' ', 9)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 5 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{blueAsciiText[2]}{new(' ', 9)}{new(' ', 8)}█{new(' ', 11)}   {(arrowOption is 5 ? "╰───╯" : "     ")}");
		sb.AppendLine($@"{new(' ', 89)}{new(' ', 18)}");
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent}{returnToOptionsText[0]} {(arrowOption is maxOption ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{returnToOptionsText[1]} {(arrowOption is maxOption ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{returnToOptionsText[2]} {(arrowOption is maxOption ? "╰───╯" : "     ")}");

		Console.SetCursorPosition(0, 0);
		Console.WriteLine(sb);

		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.UpArrow or ConsoleKey.W: arrowOption = Math.Max(1, arrowOption - 1); goto ReDraw;
			case ConsoleKey.DownArrow or ConsoleKey.S: arrowOption = Math.Min(maxOption, arrowOption + 1); goto ReDraw;
			case ConsoleKey.Enter or ConsoleKey.E:
				switch (arrowOption)
				{ 
					// TODO: Add the ability to choose what background and foreground you want to use individually
					case 1: Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.White; goto ReDraw;
					case 2: Console.BackgroundColor = ConsoleColor.White;
						Console.ForegroundColor = ConsoleColor.Black; goto ReDraw;
					case 3: Console.BackgroundColor = ConsoleColor.Green;
						Console.ForegroundColor = ConsoleColor.Black; goto ReDraw;
					case 4: Console.BackgroundColor = ConsoleColor.Red;
						Console.ForegroundColor = ConsoleColor.Black; goto ReDraw;
					case 5: Console.BackgroundColor = ConsoleColor.Blue;
						Console.ForegroundColor = ConsoleColor.Black; goto ReDraw;
					case maxOption: break;
				}
				break;
			case ConsoleKey.Escape: OptionsScreen.OptionsMenu(); break;
			default: goto ReDraw;
		}
	}
}
