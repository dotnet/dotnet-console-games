namespace Console_Monsters.Screens;

public static class OptionsScreen
{
	public static void OptionsMenu()
	{
		StringBuilder sb = new();

		int arrowOption = 1;
		const int maxOption = 6;

		string optionIndent = new(' ', 50);
		string titleIndent = new(' ', 50);
		string newLineOptions = new('\n', 2);
		string newLineTitle = new('\n', 6);

		Console.Clear();
	ReDraw:
		sb.Clear();
		// TODO: Refactor the code here so this bug will go away
		sb.AppendLine(@$"{newLineTitle}");
		sb.AppendLine(@$"{titleIndent} ██████╗ ██████╗ ████████╗██╗ ██████╗ ███╗   ██╗███████╗");
		sb.AppendLine(@$"{titleIndent}██╔═══██╗██╔══██╗╚══██╔══╝██║██╔═══██╗████╗  ██║██╔════╝");
		sb.AppendLine(@$"{titleIndent}██║   ██║██████╔╝   ██║   ██║██║   ██║██╔██╗ ██║███████╗");
		sb.AppendLine(@$"{titleIndent}██║   ██║██╔═══╝    ██║   ██║██║   ██║██║╚██╗██║╚════██║");
		sb.AppendLine(@$"{titleIndent}╚██████╔╝██║        ██║   ██║╚██████╔╝██║ ╚████║███████║");
		sb.AppendLine(@$"{titleIndent} ╚═════╝ ╚═╝        ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent}{(DisableMovementAnimation ? "╔══╗" : "╔══╗")}                      {(arrowOption is 1 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableMovementAnimation ? "║  ║" : "║██║")}  Movement Animation  {(arrowOption is 1 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableMovementAnimation ? "╚══╝" : "╚══╝")}                      {(arrowOption is 1 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattleTransition ? "╔══╗" : "╔══╗")}                     {(arrowOption is 2 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattleTransition ? "║  ║" : "║██║")}  Battle Transition  {(arrowOption is 2 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattleTransition ? "╚══╝" : "╚══╝")}                     {(arrowOption is 2 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattle ? "╔══╗" : "╔══╗")}                      {(arrowOption is 3 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattle ? "║  ║" : "║██║")}  Battles (DEV TOOL)  {(arrowOption is 3 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattle ? "╚══╝" : "╚══╝")}                      {(arrowOption is 3 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{optionIndent} ▄▄▄  ▄▄▄  ▄     ▄▄▄  ▄▄▄     ▄▄▄ ▄▄▄▄ ▄▄▄▄▄ ▄▄▄▄▄ ▄ ▄   ▄  ▄▄▄   ▄▄▄   {(arrowOption is 4 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█    █   █ █    █   █ █▄▄▀   ▀■■▄ █▄▄    █     █   █ █▀▄ █ █  ▄▄ ▀■■▄   {(arrowOption is 4 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}▀▄▄▄ ▀▄▄▄▀ █▄▄▄ ▀▄▄▄▀ █  ▀▄  ■■■▀ █▄▄▄   █     █   █ █  ▀█ ▀▄▄▄▀ ■■■▀   {(arrowOption is 4 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{optionIndent}▄  ▄  ▄▄▄▄ ▄   ▄   ▄   ▄  ▄▄  ▄▄▄  ▄▄▄  ▄ ▄   ▄  ▄▄▄    {(arrowOption is 5 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█■█   █▄▄   ▀▄▀    █▀▄▀█ █▄▄█ █▄▄▀ █▄▄▀ █ █▀▄ █ █  ▄▄   {(arrowOption is 5 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█  ▀▄ █▄▄▄   █     █   █ █  █ █    █    █ █  ▀█ ▀▄▄▄▀   {(arrowOption is 5 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{newLineOptions}");
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
					case 1: DisableMovementAnimation = !DisableMovementAnimation; goto ReDraw;
					case 2: DisableBattleTransition = !DisableBattleTransition; goto ReDraw;
					case 3: DisableBattle = !DisableBattle; goto ReDraw;
					case 4: ConsoleColorSettingsScreen.ColorSchemeMenu(); break;
					case 5: KeyMappingScreen.KeyMappingMenu(); break;
					case maxOption: break;
				}
				break;
			case ConsoleKey.Escape: break;
			default: goto ReDraw;
		}
	}
}
