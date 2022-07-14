namespace Console_Monsters.Screens;

public static class StartScreen
{
	public static void StartMenu()
	{
		Console.Clear();
		StringBuilder sb = new();

		int arrowOption = 1;

		string optionIndent = new(' ', 60);
		string titleIndent = new(' ', 10);
		string newLineOptions = new('\n', 2);
		string newLineTitle = new('\n', 6);

	ReDraw:
		sb.Clear();

		sb.AppendLine($"{newLineTitle}");
		sb.AppendLine(@$"{titleIndent} ██████╗ ██████╗ ███╗   ██╗███████╗ ██████╗ ██╗     ███████╗    ███╗   ███╗ ██████╗ ███╗   ██╗███████╗████████╗███████╗██████╗ ███████╗");
		sb.AppendLine(@$"{titleIndent}██╔════╝██╔═══██╗████╗  ██║██╔════╝██╔═══██╗██║     ██╔════╝    ████╗ ████║██╔═══██╗████╗  ██║██╔════╝╚══██╔══╝██╔════╝██╔══██╗██╔════╝");
		sb.AppendLine(@$"{titleIndent}██║     ██║   ██║██╔██╗ ██║███████╗██║   ██║██║     █████╗      ██╔████╔██║██║   ██║██╔██╗ ██║███████╗   ██║   █████╗  ██████╔╝███████╗");
		sb.AppendLine(@$"{titleIndent}██║     ██║   ██║██║╚██╗██║╚════██║██║   ██║██║     ██╔══╝      ██║╚██╔╝██║██║   ██║██║╚██╗██║╚════██║   ██║   ██╔══╝  ██╔══██╗╚════██║");
		sb.AppendLine(@$"{titleIndent}╚██████╗╚██████╔╝██║ ╚████║███████║╚██████╔╝███████╗███████╗    ██║ ╚═╝ ██║╚██████╔╝██║ ╚████║███████║   ██║   ███████╗██║  ██║███████║");
		sb.AppendLine(@$"{titleIndent} ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚══════╝╚══════╝    ╚═╝     ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝╚══════╝");
		sb.AppendLine(@$"{newLineTitle}");

		sb.AppendLine(@$"{optionIndent} {(FirstTimeLaunching ? "  ▄▄▄▄ ▄▄▄▄▄  ▄▄  ▄▄▄  ▄▄▄▄▄" : "▄▄▄  ▄▄▄▄ ▄▄▄▄ ▄   ▄ ▄   ▄ ▄▄▄▄")}   {(arrowOption is 1 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent} {(FirstTimeLaunching ? "  █▄▄▄   █   █▄▄█ █▄▄▀   █  " : "█▄▄▀ █▄▄  █▄▄▄ █   █ █▀▄▀█ █▄▄ ")}   {(arrowOption is 1 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent} {(FirstTimeLaunching ? "  ▄▄▄█   █   █  █ █  █   █  " : "█  █ █▄▄▄ ▄▄▄█ ▀▄▄▄▀ █   █ █▄▄▄")}   {(arrowOption is 1 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent} ▄▄▄  ▄▄▄  ▄▄▄▄▄ ▄  ▄▄▄  ▄   ▄ ▄▄▄▄   {(arrowOption is 2 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█   █ █▄▄▀   █   █ █   █ █▀▄ █ █▄▄▄   {(arrowOption is 2 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}▀▄▄▄▀ █      █   █ ▀▄▄▄▀ █  ▀█ ▄▄▄█   {(arrowOption is 2 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent}        ▄▄▄▄ ▄   ▄ ▄ ▄▄▄▄▄   {(arrowOption is 3 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}        █▄▄   ▀▄▀  █   █     {(arrowOption is 3 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}        █▄▄▄ ▄▀ ▀▄ █   █     {(arrowOption is 3 ? "╰───╯" : "     ")}");

		Console.SetCursorPosition(0, 0);
		Console.WriteLine(sb);


		ConsoleKey key = Console.ReadKey(true).Key;
		switch (key)
		{
			case ConsoleKey.UpArrow or ConsoleKey.W: arrowOption = Math.Max(1, arrowOption - 1); goto ReDraw;
			case ConsoleKey.DownArrow or ConsoleKey.S: arrowOption = Math.Min(3, arrowOption + 1); goto ReDraw;
			case ConsoleKey.Enter or ConsoleKey.E:
				switch (arrowOption)
				{
					case 1:
						FirstTimeLaunching = false;
						break;
					case 2:
						OptionsScreen.OptionsMenu();
						Console.Clear();
						goto ReDraw; // To not run "arrowOption" so it stays on "Options" after going back
					case 3:
						gameRunning = false;
						break;
				}
				break;
			case ConsoleKey.Escape: break;
			default: goto ReDraw;
		}
	}
}
