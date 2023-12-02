using System;
using System.Text;
using static Website.Games.Console_Monsters.Statics;
//using Website.Games.Console_Monsters.Screens;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters.Screens.Menus;

public static class OptionsScreen
{
	public static async Task OptionsMenu()
	{
		StringBuilder sb = new();

		int arrowOption = 1;
		const int maxOption = 6;

		string optionIndent = new(' ', 50);
		string titleIndent = new(' ', 50);

		await Statics.Console.Clear();
	ReDraw:
		sb.Clear();

		sb.AppendLine();
		sb.AppendLine();
		sb.AppendLine();
		sb.AppendLine();
		sb.AppendLine();
		sb.AppendLine();
		sb.AppendLine(@$"{titleIndent} ██████╗ ██████╗ ████████╗██╗ ██████╗ ███╗   ██╗███████╗");
		sb.AppendLine(@$"{titleIndent}██╔═══██╗██╔══██╗╚══██╔══╝██║██╔═══██╗████╗  ██║██╔════╝");
		sb.AppendLine(@$"{titleIndent}██║   ██║██████╔╝   ██║   ██║██║   ██║██╔██╗ ██║███████╗");
		sb.AppendLine(@$"{titleIndent}██║   ██║██╔═══╝    ██║   ██║██║   ██║██║╚██╗██║╚════██║");
		sb.AppendLine(@$"{titleIndent}╚██████╔╝██║        ██║   ██║╚██████╔╝██║ ╚████║███████║");
		sb.AppendLine(@$"{titleIndent} ╚═════╝ ╚═╝        ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝");
		sb.AppendLine();
		sb.AppendLine();
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent}{(DisableMovementAnimation ? "╔══╗" : "╔══╗")}                       {(arrowOption is 1 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableMovementAnimation ? "║  ║" : "║██║")}  Movement Animation   {(arrowOption is 1 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableMovementAnimation ? "╚══╝" : "╚══╝")}                       {(arrowOption is 1 ? "╰───╯" : "     ")}");
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent}{(DisableBattleTransition ? "╔══╗" : "╔══╗")}                      {(arrowOption is 2 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattleTransition ? "║  ║" : "║██║")}  Battle Transition   {(arrowOption is 2 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattleTransition ? "╚══╝" : "╚══╝")}                      {(arrowOption is 2 ? "╰───╯" : "     ")}");
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent}{(DisableBattle ? "╔══╗" : "╔══╗")}                       {(arrowOption is 3 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattle ? "║  ║" : "║██║")}  Battles (DEV TOOL)   {(arrowOption is 3 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattle ? "╚══╝" : "╚══╝")}                       {(arrowOption is 3 ? "╰───╯" : "     ")}");
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent} ▄▄▄  ▄▄▄  ▄    ▄▄▄  ▄▄▄  ▄▄▄▄   {(arrowOption is 4 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█    █   █ █   █   █ █▄▄▀ █▄▄▄   {(arrowOption is 4 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}▀▄▄▄ ▀▄▄▄▀ █▄▄ ▀▄▄▄▀ █  █ ▄▄▄█   {(arrowOption is 4 ? "╰───╯" : "     ")}");
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent} ▄▄▄  ▄▄▄  ▄   ▄ ▄▄▄▄▄ ▄▄▄   ▄▄▄  ▄   ▄▄▄▄   {(arrowOption is 5 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█    █   █ █▀▄ █   █   █▄▄▀ █   █ █   █▄▄▄   {(arrowOption is 5 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}▀▄▄▄ ▀▄▄▄▀ █  ▀█   █   █  █ ▀▄▄▄▀ █▄▄ ▄▄▄█   {(arrowOption is 5 ? "╰───╯" : "     ")}");
		sb.AppendLine();
		sb.AppendLine(@$"{optionIndent}▄▄▄   ▄▄   ▄▄▄ ▄  ▄   {(arrowOption is maxOption ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█▄▄█ █▄▄█ █    █▄▀    {(arrowOption is maxOption ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█▄▄▀ █  █ ▀▄▄▄ █ ▀▄   {(arrowOption is maxOption ? "╰───╯" : "     ")}");

		await Statics.Console.SetCursorPosition(0, 0);
		await Statics.Console.WriteLine(sb);

		switch ((await Statics.Console.ReadKey(true)).Key)
		{
			case ConsoleKey.UpArrow or ConsoleKey.W: arrowOption = Math.Max(1, arrowOption - 1); goto ReDraw;
			case ConsoleKey.DownArrow or ConsoleKey.S: arrowOption = Math.Min(maxOption, arrowOption + 1); goto ReDraw;
			case ConsoleKey.Enter or ConsoleKey.E:
				switch (arrowOption)
				{
					case 1: DisableMovementAnimation = !DisableMovementAnimation; goto ReDraw;
					case 2: DisableBattleTransition = !DisableBattleTransition; goto ReDraw;
					case 3: DisableBattle = !DisableBattle; goto ReDraw;
					case 4: await ConsoleColorSettingsScreen.ColorSchemeMenu(); break;
					case 5: await ControlsScreen.ControlsMenu(); break;
					case maxOption: break;
				}
				break;
			case ConsoleKey.Escape: break;
			default: goto ReDraw;
		}
	}
}
