using System;
using System.Linq;
using System.Text;
using System.Threading;
using static Website.Games.Console_Monsters.Statics;
//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Items;
using Website.Games.Console_Monsters.Maps;
using Website.Games.Console_Monsters.Monsters;
using Website.Games.Console_Monsters.Bases;
using Website.Games.Console_Monsters.Characters;
using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Screens.Menus;
using Website.Games.Console_Monsters.Enums;
using Website.Games.Console_Monsters.Utilities;
using System.Collections.Generic;
using Towel;
using static Towel.Statics;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters.Screens;

public static class ConsoleColorSettingsScreen
{
	public static async Task ColorSchemeMenu()
	{
		StringBuilder sb = new();

		int arrowOption = 1;
		const int maxOption = 8;

		string optionIndent = new(' ', 60);
		string titleIndent = new(' ', 30);

		string[] blackAsciiText = AsciiGenerator.ToAscii("black");
		string[] greenAsciiText = AsciiGenerator.ToAscii("green");
		string[] redAsciiText = AsciiGenerator.ToAscii("red");
		string[] blueAsciiText = AsciiGenerator.ToAscii("blue");
		string[] yellowAsciiText = AsciiGenerator.ToAscii("yellow");
		string[] invertAsciiText = AsciiGenerator.ToAscii("invert");
		string[] resetAsciiText = AsciiGenerator.ToAscii("reset");
		string[] returnToOptionsText = AsciiGenerator.ToAscii("Back");

		await Statics.Console.Clear();
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

		await Statics.Console.SetCursorPosition(0, 0);
		await Statics.Console.WriteLine(sb);

		switch ((await Statics.Console.ReadKey(true)).Key)
		{
			case ConsoleKey.UpArrow   or ConsoleKey.W: arrowOption = Math.Max(1, arrowOption - 1); goto ReDraw;
			case ConsoleKey.DownArrow or ConsoleKey.S: arrowOption = Math.Min(maxOption, arrowOption + 1); goto ReDraw;
			case ConsoleKey.Enter or ConsoleKey.E:
				switch (arrowOption)
				{
					case 1:
						Statics.Console.BackgroundColor = ConsoleColor.Black;
						Statics.Console.ForegroundColor = ConsoleColor.White;
						await Statics.Console.Clear();
						goto ReDraw;
					case 2:
						Statics.Console.BackgroundColor = ConsoleColor.Black;
						Statics.Console.ForegroundColor = ConsoleColor.Green;
						await Statics.Console.Clear();
						goto ReDraw;
					case 3:
						Statics.Console.BackgroundColor = ConsoleColor.Black;
						Statics.Console.ForegroundColor = ConsoleColor.Red;
						await Statics.Console.Clear();
						goto ReDraw;
					case 4:
						Statics.Console.BackgroundColor = ConsoleColor.Black;
						Statics.Console.ForegroundColor = ConsoleColor.Blue;
						await Statics.Console.Clear();
						goto ReDraw;
					case 5:
						Statics.Console.BackgroundColor = ConsoleColor.Black;
						Statics.Console.ForegroundColor = ConsoleColor.Yellow;
						await Statics.Console.Clear();
						goto ReDraw;
					case 6:
						(Statics.Console.BackgroundColor, Statics.Console.ForegroundColor) = (Statics.Console.ForegroundColor, Statics.Console.BackgroundColor);
						await Statics.Console.Clear();
						goto ReDraw;
					case 7:
						Statics.Console.ResetColor();
						await Statics.Console.Clear();
						goto ReDraw;
					case maxOption:
						break;
				}
				break;
			case ConsoleKey.Escape: await OptionsScreen.OptionsMenu(); break;
			default: goto ReDraw;
		}
	}
}
