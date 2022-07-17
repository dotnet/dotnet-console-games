namespace Console_Monsters.Screens.Menus;

public static class StartScreen
{
	public static void Show()
	{
		string[] bigHeader = new[]
		{
			" ██████╗ ██████╗ ███╗   ██╗███████╗ ██████╗ ██╗     ███████╗    ███╗   ███╗ ██████╗ ███╗   ██╗███████╗████████╗███████╗██████╗ ███████╗",
			"██╔════╝██╔═══██╗████╗  ██║██╔════╝██╔═══██╗██║     ██╔════╝    ████╗ ████║██╔═══██╗████╗  ██║██╔════╝╚══██╔══╝██╔════╝██╔══██╗██╔════╝",
			"██║     ██║   ██║██╔██╗ ██║███████╗██║   ██║██║     █████╗      ██╔████╔██║██║   ██║██╔██╗ ██║███████╗   ██║   █████╗  ██████╔╝███████╗",
			"██║     ██║   ██║██║╚██╗██║╚════██║██║   ██║██║     ██╔══╝      ██║╚██╔╝██║██║   ██║██║╚██╗██║╚════██║   ██║   ██╔══╝  ██╔══██╗╚════██║",
			"╚██████╗╚██████╔╝██║ ╚████║███████║╚██████╔╝███████╗███████╗    ██║ ╚═╝ ██║╚██████╔╝██║ ╚████║███████║   ██║   ███████╗██║  ██║███████║",
			" ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚══════╝╚══════╝    ╚═╝     ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝╚══════╝",
		};
		int bigHeaderWidth = bigHeader.Max(line => line.Length);
		const int bigHeaderPadding = 2;
		const int optionPadding = 1;
		MonsterBase monserA = MonsterBase.GetRandom();
		MonsterBase monserB = MonsterBase.GetRandom();
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
				if (consoleWidth >= bigHeaderWidth)
				{
					string[][] options = new[]
					{
						AsciiGenerator.ToAscii((selectedOption is 0 ? "■" : "□") + (FirstTimeLaunching ? "start" : "resume")),
						AsciiGenerator.ToAscii((selectedOption is 1 ? "■" : "□") + "options"),
						AsciiGenerator.ToAscii((selectedOption is 2 ? "■" : "□") + "exit"),
					};
					int optionsWidth = options.Max(o => o.Max(l => l.Length));
					int bigRenderHeight = bigHeader.Length + options.Sum(o => o.Length) + bigHeaderPadding + optionPadding * options.Length;
					if (consoleHeight >= bigRenderHeight && consoleWidth >= optionsWidth)
					{
						int indentSize = Math.Max(0, (bigHeaderWidth - optionsWidth) / 2);
						string indent = new(' ', indentSize);
						string[] render = new string[bigRenderHeight];
						int i = 0;
						foreach (string line in bigHeader)
						{
							render[i++] = line;
						}
						for (int j = 0; j < bigHeaderPadding; j++)
						{
							render[i++] = "";
						}
						foreach (string[] option in options)
						{
							for (int j = 0; j < optionPadding; j++)
							{
								render[i++] = "";
							}
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
						$@"Console Monsters",
						$@"{(selectedOption is 0 ? "> " : "  ")} {(FirstTimeLaunching ? "Start" : "Resume")}",
						$@"{(selectedOption is 1 ? "> " : "  ")} Options",
						$@"{(selectedOption is 2 ? "> " : "  ")} Exit",
					};
					buffer = ScreenHelpers.Center(render, (consoleHeight - 1, consoleWidth - 1));
				}
				Console.SetCursorPosition(0, 0);
				Console.Write(buffer);
				needToRender = false;
			}
			if (Console.KeyAvailable)
			{
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.UpArrow or ConsoleKey.W:
						selectedOption = Math.Max(0, selectedOption - 1);
						needToRender = true;
						break;
					case ConsoleKey.DownArrow or ConsoleKey.S:
						selectedOption = Math.Min(2, selectedOption + 1);
						needToRender = true;
						break;
					case ConsoleKey.Enter or ConsoleKey.E:
						switch (selectedOption)
						{
							case 0:
								FirstTimeLaunching = false;
								return;
							case 1:
								OptionsScreen.OptionsMenu();
								Console.Clear();
								needToRender = true;
								break;
							case 2:
								gameRunning = false;
								return;
						}
						break;
					case ConsoleKey.Escape:
						if (FirstTimeLaunching)
						{
							gameRunning = false;
						}
						return;
				}
			}
			Thread.Sleep(TimeSpan.FromTicks(1));
		}
	}
}
