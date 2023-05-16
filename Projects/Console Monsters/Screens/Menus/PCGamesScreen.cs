namespace Console_Monsters.Screens.Menus;
public class PCGamesScreen
{
	public static void Render()
	{
		Console.CursorVisible = false;

		string[] bigHeader = new[]
		{
			"██████╗  ██████╗   ██████╗  █████╗ ███╗   ███╗███████╗███████╗",
			"██╔══██╗██╔════╝  ██╔════╝ ██╔══██╗████╗ ████║██╔════╝██╔════╝",
			"██████╔╝██║       ██║  ███╗███████║██╔████╔██║█████╗  ███████╗",
			"██╔═══╝ ██║       ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  ╚════██║",
			"██║     ╚██████╗  ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗███████║",
			"╚═╝      ╚═════╝   ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝╚══════╝",
		};
		int bigHeaderWidth = bigHeader.Max(line => line.Length);
		const int bigHeaderPadding = 2;
		const int optionPadding = 1;
		var (consoleWidth, consoleHeight) = ConsoleHelpers.GetWidthAndHeight();
		Console.Clear();
		int selectedOption = 0;
		bool needToRender = true;

		int heightCutOff = consoleHeight - MapText.Length - 3;
		int midWidth = consoleWidth / 2;
		int midHeight = heightCutOff / 2;

		while (true)
		{
			if (ConsoleHelpers.ClearIfConsoleResized(ref consoleWidth, ref consoleHeight))
			{
				needToRender = true;
				Console.CursorVisible = false;
			}
			if (needToRender)
			{
				StringBuilder? buffer = null;
				if (consoleWidth - 1 >= bigHeaderWidth)
				{
					string[][] options = new[]
					{
						AsciiGenerator.Concat(AsciiGenerator.ToAscii((selectedOption is 0 ? "■" : "□") + " 2048")),
						AsciiGenerator.Concat(AsciiGenerator.ToAscii((selectedOption is 1 ? "■" : "□") + " Tents")),
						AsciiGenerator.Concat(AsciiGenerator.ToAscii((selectedOption is 2 ? "■" : "□") + " Game3")),
						AsciiGenerator.Concat(AsciiGenerator.ToAscii((selectedOption is 3 ? "■" : "□") + " Game4")),
						AsciiGenerator.Concat(AsciiGenerator.ToAscii((selectedOption is 4 ? "■" : "□") + " Game5")),
						AsciiGenerator.Concat(AsciiGenerator.ToAscii((selectedOption is 5 ? "■" : "□") + " Game6")),
						AsciiGenerator.Concat(AsciiGenerator.ToAscii((selectedOption is 6 ? "■" : "□") + " Game7")),
					};
					int optionsWidth = options.Max(o => o.Max(l => l.Length));
					int bigRenderHeight = bigHeader.Length + options.Sum(o => o.Length) + bigHeaderPadding + optionPadding * options.Length;
					if (consoleHeight - 1 >= bigRenderHeight && consoleWidth - 1 >= optionsWidth)
					{
						int indentSize = Math.Max(0, (bigHeaderWidth - optionsWidth) / 2);
						string indent = new(' ', indentSize);
						string[] render = new string[bigRenderHeight];
						int i = 0;
						foreach (string line in bigHeader)
						{
							render[i++] = line;
						}
						i += bigHeaderPadding;
						foreach (string[] option in options)
						{
							i += optionPadding;
							foreach (string line in option)
							{
								render[i++] = indent + line;
							}
						}
						buffer = ScreenHelpers.Center(render, (consoleHeight - 1, consoleWidth - 1));
					}
				}
				if (buffer is null)
				{
					string[] render = new[]
					{
						$@"PC Games",
						$@"{(selectedOption is 0 ? ">" : " ")} 2048",
						$@"{(selectedOption is 1 ? ">" : " ")} Tents",
						$@"{(selectedOption is 2 ? ">" : " ")} Game 3",
						$@"{(selectedOption is 3 ? ">" : " ")} Game 4",
						$@"{(selectedOption is 4 ? ">" : " ")} Game 5",
						$@"{(selectedOption is 5 ? ">" : " ")} Game 6",
						$@"{(selectedOption is 6 ? ">" : " ")} Game 7",
					};
					buffer = ScreenHelpers.Center(render, (consoleHeight - 1, consoleWidth - 1));
				}
				Console.SetCursorPosition(0, 0);
				Console.Write(buffer);
				needToRender = false;
			}
			while (Console.KeyAvailable)
			{
				switch (keyMappings.GetValueOrDefault(Console.ReadKey(true).Key))
				{
					case UserKeyPress.Escape: return;
					case UserKeyPress.Up: selectedOption = Math.Max(0, selectedOption - 1); needToRender = true; break;
					case UserKeyPress.Down: selectedOption = Math.Min(6, selectedOption + 1); needToRender = true; break;
					case UserKeyPress.Confirm:
						switch (selectedOption)
						{
							case 0: _2048.Run(); break;
							case 1: Tents.Run(); break;
							case 2: break;
							case 3: break;
							case 4: break;
							case 5: break;
							case 6: break;
							default: throw new NotImplementedException();
						}
						break;
				}
			}
			// prevent CPU spiking
			Thread.Sleep(TimeSpan.FromMilliseconds(1));
		}
	}
}
