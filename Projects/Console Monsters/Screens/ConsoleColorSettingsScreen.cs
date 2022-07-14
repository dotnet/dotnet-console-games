namespace Console_Monsters.Screens;

public static class ConsoleColorSettingsScreen
{
	public static void ColorSchemeMenu()
	{
		StringBuilder sb = new();

		int arrowOption = 1;
		const int maxOption = 8;

		string optionIndent = new(' ', 60);
		string titleIndent = new(' ', 30);

		string[] blackAsciiText = TextGenerator.GenerateAsciiText("black");
		string[] greenAsciiText = TextGenerator.GenerateAsciiText("green");
		string[] redAsciiText = TextGenerator.GenerateAsciiText("red");
		string[] blueAsciiText = TextGenerator.GenerateAsciiText("blue");
		string[] yellowAsciiText = TextGenerator.GenerateAsciiText("yellow");
		string[] invertAsciiText = TextGenerator.GenerateAsciiText("invert");
		string[] resetAsciiText = TextGenerator.GenerateAsciiText("reset");
		string[] returnToOptionsText = TextGenerator.GenerateAsciiText("Back");
		
		Console.Clear();
	ReDraw:
		sb.Clear();

		sb.AppendLine();
		sb.AppendLine();
		sb.AppendLine();
		sb.AppendLine(@$"{titleIndent}██████╗ ██████╗  ██╗      ██████╗ ██████╗     ███████╗ ██████╗██╗  ██╗███████╗███╗   ███╗███████╗");
		sb.AppendLine(@$"{titleIndent}██╔════╝██╔═══██╗██║     ██╔═══██╗██╔══██╗    ██╔════╝██╔════╝██║  ██║██╔════╝████╗ ████║██╔════╝");
		sb.AppendLine(@$"{titleIndent}██║     ██║   ██║██║     ██║   ██║██████╔╝    ███████╗██║     ███████║█████╗  ██╔████╔██║█████╗  ");
		sb.AppendLine(@$"{titleIndent}██║     ██║   ██║██║     ██║   ██║██╔══██╗    ╚════██║██║     ██╔══██║██╔══╝  ██║╚██╔╝██║██╔══╝  ");
		sb.AppendLine(@$"{titleIndent}╚██████╗╚██████╔╝███████╗╚██████╔╝██║  ██║    ███████║╚██████╗██║  ██║███████╗██║ ╚═╝ ██║███████╗");
		sb.AppendLine(@$"{titleIndent} ╚═════╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝  ╚═╝    ╚══════╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝     ╚═╝╚══════╝");
		sb.AppendLine();
		sb.AppendLine();
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent}{blackAsciiText[0]}   {(arrowOption is 1 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{blackAsciiText[1]}   {(arrowOption is 1 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{blackAsciiText[2]}   {(arrowOption is 1 ? "╰───╯" : "     ")}");
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent}{greenAsciiText[0]}   {(arrowOption is 2 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{greenAsciiText[1]}   {(arrowOption is 2 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{greenAsciiText[2]}   {(arrowOption is 2 ? "╰───╯" : "     ")}");
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent}{redAsciiText[0]}   {(arrowOption is 3 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{redAsciiText[1]}   {(arrowOption is 3 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{redAsciiText[2]}   {(arrowOption is 3 ? "╰───╯" : "     ")}");
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent}{blueAsciiText[0]}   {(arrowOption is 4 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{blueAsciiText[1]}   {(arrowOption is 4 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{blueAsciiText[2]}   {(arrowOption is 4 ? "╰───╯" : "     ")}");
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent}{yellowAsciiText[0]}   {(arrowOption is 5 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{yellowAsciiText[1]}   {(arrowOption is 5 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{yellowAsciiText[2]}   {(arrowOption is 5 ? "╰───╯" : "     ")}");
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent}{invertAsciiText[0]}   {(arrowOption is 6 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{invertAsciiText[1]}   {(arrowOption is 6 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{invertAsciiText[2]}   {(arrowOption is 6 ? "╰───╯" : "     ")}");
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent}{resetAsciiText[0]}   {(arrowOption is 7 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{resetAsciiText[1]}   {(arrowOption is 7 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{resetAsciiText[2]}   {(arrowOption is 7 ? "╰───╯" : "     ")}");
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent}{returnToOptionsText[0]}   {(arrowOption is maxOption ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{returnToOptionsText[1]}   {(arrowOption is maxOption ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{returnToOptionsText[2]}   {(arrowOption is maxOption ? "╰───╯" : "     ")}");

		Console.SetCursorPosition(0, 0);
		Console.WriteLine(sb);

		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.UpArrow   or ConsoleKey.W: arrowOption = Math.Max(1, arrowOption - 1); goto ReDraw;
			case ConsoleKey.DownArrow or ConsoleKey.S: arrowOption = Math.Min(maxOption, arrowOption + 1); goto ReDraw;
			case ConsoleKey.Enter or ConsoleKey.E:
				switch (arrowOption)
				{
					case 1:
						Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.White;
						Console.Clear();
						goto ReDraw;
					case 2:
						Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.Green;
						Console.Clear();
						goto ReDraw;
					case 3:
						Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Clear();
						goto ReDraw;
					case 4:
						Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.Clear();
						goto ReDraw;
					case 5:
						Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.Clear();
						goto ReDraw;
					case 6:
						(Console.BackgroundColor, Console.ForegroundColor) = (Console.ForegroundColor, Console.BackgroundColor);
						Console.Clear();
						goto ReDraw;
					case 7:
						Console.ResetColor();
						Console.Clear();
						goto ReDraw;
					case maxOption:
						break;
				}
				break;
			case ConsoleKey.Escape: OptionsScreen.OptionsMenu(); break;
			default: goto ReDraw;
		}
	}
}
