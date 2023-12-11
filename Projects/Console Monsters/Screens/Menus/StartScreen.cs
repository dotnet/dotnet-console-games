namespace Console_Monsters.Screens.Menus;

public static class StartScreen
{
	public static void Show()
	{
		string[] bigHeader =
		[
			" ██████╗ ██████╗ ███╗   ██╗███████╗ ██████╗ ██╗     ███████╗    ███╗   ███╗ ██████╗ ███╗   ██╗███████╗████████╗███████╗██████╗ ███████╗",
			"██╔════╝██╔═══██╗████╗  ██║██╔════╝██╔═══██╗██║     ██╔════╝    ████╗ ████║██╔═══██╗████╗  ██║██╔════╝╚══██╔══╝██╔════╝██╔══██╗██╔════╝",
			"██║     ██║   ██║██╔██╗ ██║███████╗██║   ██║██║     █████╗      ██╔████╔██║██║   ██║██╔██╗ ██║███████╗   ██║   █████╗  ██████╔╝███████╗",
			"██║     ██║   ██║██║╚██╗██║╚════██║██║   ██║██║     ██╔══╝      ██║╚██╔╝██║██║   ██║██║╚██╗██║╚════██║   ██║   ██╔══╝  ██╔══██╗╚════██║",
			"╚██████╗╚██████╔╝██║ ╚████║███████║╚██████╔╝███████╗███████╗    ██║ ╚═╝ ██║╚██████╔╝██║ ╚████║███████║   ██║   ███████╗██║  ██║███████║",
			" ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚══════╝╚══════╝    ╚═╝     ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝╚══════╝",
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
						AsciiGenerator.ToAscii((selectedOption is 0 ? "■" : "□") + (FirstTimeLaunching ? " start" : " resume")),
						AsciiGenerator.ToAscii((selectedOption is 1 ? "■" : "□") + " options"),
						AsciiGenerator.ToAscii((selectedOption is 2 ? "■" : "□") + " exit"),
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
						$@"Console Monsters",
						$@"{(selectedOption is 0 ? ">" : " ")} {(FirstTimeLaunching ? "Start" : "Resume")}",
						$@"{(selectedOption is 1 ? ">" : " ")} Options",
						$@"{(selectedOption is 2 ? ">" : " ")} Exit",
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
					case UserKeyPress.Up:
						selectedOption = Math.Max(0, selectedOption - 1);
						needToRender = true;
						break;
					case UserKeyPress.Down:
						selectedOption = Math.Min(2, selectedOption + 1);
						needToRender = true;
						break;
					case UserKeyPress.Confirm:
						switch (selectedOption)
						{
							case 0:
								if (FirstTimeLaunching)
								{
									Map = new PaletTown();
									Map.SpawnCharacterOn('X');
								}
								FirstTimeLaunching = false;
								return;
							case 1:
								OptionsScreen.Show();
								Console.Clear();
								needToRender = true;
								break;
							case 2:
								GameRunning = false;
								return;
							default:
								throw new NotImplementedException();
						}
						break;
					case UserKeyPress.Escape:
						if (FirstTimeLaunching)
						{
							GameRunning = false;
						}
						return;
				}
			}
			// prevent CPU spiking
			Thread.Sleep(TimeSpan.FromMilliseconds(1));
		}
	}
}
