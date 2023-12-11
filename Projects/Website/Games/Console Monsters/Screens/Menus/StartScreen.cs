using System;
using System.Text;
using static Website.Games.Console_Monsters.Statics;
//using Website.Games.Console_Monsters.Screens;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters.Screens.Menus;

public static class StartScreen
{
	public static async Task StartMenu()
	{
		await Statics.Console.Clear();
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

		await Statics.Console.SetCursorPosition(0, 0);
		await Statics.Console.WriteLine(sb);


		ConsoleKey key = (await Statics.Console.ReadKey(true)).Key;
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
						await OptionsScreen.OptionsMenu();
						await Statics.Console.Clear();
						goto ReDraw; // To not run "arrowOption" so it stays on "Options" after going back
					case 3:
						GameRunning = false;
						break;
				}
				break;
			case ConsoleKey.Escape: break;
			default: goto ReDraw;
		}
	}
}
