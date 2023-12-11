namespace Console_Monsters.Screens.Menus;

public static class ColorsScreen
{
	public static void Show()
	{
		string[] bigHeader =
		[
			" ██████╗ ██████╗ ██╗      ██████╗ ██████╗     ███████╗ ██████╗██╗  ██╗███████╗███╗   ███╗███████╗",
			"██╔════╝██╔═══██╗██║     ██╔═══██╗██╔══██╗    ██╔════╝██╔════╝██║  ██║██╔════╝████╗ ████║██╔════╝",
			"██║     ██║   ██║██║     ██║   ██║██████╔╝    ███████╗██║     ███████║█████╗  ██╔████╔██║█████╗  ",
			"██║     ██║   ██║██║     ██║   ██║██╔══██╗    ╚════██║██║     ██╔══██║██╔══╝  ██║╚██╔╝██║██╔══╝  ",
			"╚██████╗╚██████╔╝███████╗╚██████╔╝██║  ██║    ███████║╚██████╗██║  ██║███████╗██║ ╚═╝ ██║███████╗",
			" ╚═════╝ ╚═════╝ ╚══════╝ ╚═════╝ ╚═╝  ╚═╝    ╚══════╝ ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝     ╚═╝╚══════╝",
		];
		int bigHeaderWidth = bigHeader.Max(line => line.Length);
		const int bigHeaderPadding = 2;
		const int optionPadding = 1;
		var (consoleWidth, consoleHeight) = ConsoleHelpers.GetWidthAndHeight();
		Console.Clear();
		int selectedOption = 0;
		bool needToRender = true;
		Console.CursorVisible = false;
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
					string[][] options =
					[
						AsciiGenerator.ToAscii((selectedOption is 0 ? "■" : "□") + " black"),
						AsciiGenerator.ToAscii((selectedOption is 1 ? "■" : "□") + " green"),
						AsciiGenerator.ToAscii((selectedOption is 2 ? "■" : "□") + " red"),
						AsciiGenerator.ToAscii((selectedOption is 3 ? "■" : "□") + " blue"),
						AsciiGenerator.ToAscii((selectedOption is 4 ? "■" : "□") + " yellow"),
						AsciiGenerator.ToAscii((selectedOption is 5 ? "■" : "□") + " invert"),
						AsciiGenerator.ToAscii((selectedOption is 6 ? "■" : "□") + " reset"),
						AsciiGenerator.ToAscii((selectedOption is 7 ? "■" : "□") + " back"),
					];
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
					string[] render =
					[
						$@"Color Scheme",
						$@"{(selectedOption is 0 ? ">" : " ")} Black",
						$@"{(selectedOption is 1 ? ">" : " ")} Green",
						$@"{(selectedOption is 2 ? ">" : " ")} Red",
						$@"{(selectedOption is 3 ? ">" : " ")} Blue",
						$@"{(selectedOption is 4 ? ">" : " ")} Yellow",
						$@"{(selectedOption is 5 ? ">" : " ")} Invert",
						$@"{(selectedOption is 6 ? ">" : " ")} Reset",
						$@"{(selectedOption is 7 ? ">" : " ")} Back",
					];
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
					case UserKeyPress.Up:   selectedOption = Math.Max(0, selectedOption - 1); needToRender = true; break;
					case UserKeyPress.Down: selectedOption = Math.Min(7, selectedOption + 1); needToRender = true; break;
					case UserKeyPress.Confirm:
						switch (selectedOption)
						{
							case 0:
								Console.BackgroundColor = ConsoleColor.Black;
								Console.ForegroundColor = ConsoleColor.White;
								Console.Clear();
								needToRender = true;
								break;
							case 1:
								Console.BackgroundColor = ConsoleColor.Black;
								Console.ForegroundColor = ConsoleColor.Green;
								Console.Clear();
								needToRender = true;
								break;
							case 2:
								Console.BackgroundColor = ConsoleColor.Black;
								Console.ForegroundColor = ConsoleColor.Red;
								Console.Clear();
								needToRender = true;
								break;
							case 3:
								Console.BackgroundColor = ConsoleColor.Black;
								Console.ForegroundColor = ConsoleColor.Blue;
								Console.Clear();
								needToRender = true;
								break;
							case 4:
								Console.BackgroundColor = ConsoleColor.Black;
								Console.ForegroundColor = ConsoleColor.Yellow;
								Console.Clear();
								needToRender = true;
								break;
							case 5:
								(Console.BackgroundColor, Console.ForegroundColor) = (Console.ForegroundColor, Console.BackgroundColor);
								Console.Clear();
								needToRender = true;
								break;
							case 6:
								Console.ResetColor();
								Console.Clear();
								needToRender = true;
								break;
							case 7:
								return;
							default:
								throw new NotImplementedException();
						}
						break;
				}
			}
			// prevent CPU spiking
			Thread.Sleep(TimeSpan.FromMilliseconds(1));
		}
	}
}
