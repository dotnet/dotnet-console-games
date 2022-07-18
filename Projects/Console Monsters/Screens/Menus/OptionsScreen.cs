namespace Console_Monsters.Screens.Menus;

public static class OptionsScreen
{
	public static void Show()
	{
		string[] bigHeader = new[]
		{
			" ██████╗ ██████╗ ████████╗██╗ ██████╗ ███╗   ██╗███████╗",
			"██╔═══██╗██╔══██╗╚══██╔══╝██║██╔═══██╗████╗  ██║██╔════╝",
			"██║   ██║██████╔╝   ██║   ██║██║   ██║██╔██╗ ██║███████╗",
			"██║   ██║██╔═══╝    ██║   ██║██║   ██║██║╚██╗██║╚════██║",
			"╚██████╔╝██║        ██║   ██║╚██████╔╝██║ ╚████║███████║",
			" ╚═════╝ ╚═╝        ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝",
		};
		int bigHeaderWidth = bigHeader.Max(line => line.Length);
		const int bigHeaderPadding = 2;
		const int optionPadding = 1;
		var (consoleWidth, consoleHeight) = ConsoleHelpers.GetWidthAndHeight();
		Console.Clear();
		int selectedOption = 0;
		bool needToRender = true;
		Console.CursorVisible = false;

		string[] movementAnimation = new[]
		{
			"                        ",
			"   Movement Animation   ",
			"                        ",
		};

		string[] battleTransition = new[]
		{
			"                        ",
			"   Battle Transition    ",
			"                        ",
		};

		string[] battles = new[]
		{
			"                        ",
			"   Battles              ",
			"                        ",
		};

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
						AsciiGenerator.Concat(AsciiGenerator.ToAscii(selectedOption is 0 ? "■ " : "□ "), movementAnimation, AsciiGenerator.ToAscii(DisableMovementAnimation ? "○" : "●")),
						AsciiGenerator.Concat(AsciiGenerator.ToAscii(selectedOption is 1 ? "■ " : "□ "), battleTransition, AsciiGenerator.ToAscii(DisableBattleTransition ? "○" : "●")),
						AsciiGenerator.Concat(AsciiGenerator.ToAscii(selectedOption is 2 ? "■ " : "□ "), battles, AsciiGenerator.ToAscii(DisableBattle ? "○" : "●")),
						AsciiGenerator.Concat(AsciiGenerator.ToAscii((selectedOption is 3 ? "■" : "□") + " colors")),
						AsciiGenerator.Concat(AsciiGenerator.ToAscii((selectedOption is 4 ? "■" : "□") + " controls")),
						AsciiGenerator.Concat(AsciiGenerator.ToAscii((selectedOption is 5 ? "■" : "□") + " back")),
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
						$@"Options",
						$@"{(selectedOption is 0 ? ">" : " ")} Movement Animation {(DisableMovementAnimation ? "□" : "■")}",
						$@"{(selectedOption is 1 ? ">" : " ")} Battle Transition  {(DisableBattleTransition ? "□" : "■")}",
						$@"{(selectedOption is 2 ? ">" : " ")} Battles            {(DisableBattle ? "□" : "■")}",
						$@"{(selectedOption is 3 ? ">" : " ")} Colors",
						$@"{(selectedOption is 4 ? ">" : " ")} Controls",
						$@"{(selectedOption is 5 ? ">" : " ")} Exit",
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
					case UserKeyPress.Up:   selectedOption = Math.Max(0, selectedOption - 1); needToRender = true; break;
					case UserKeyPress.Down: selectedOption = Math.Min(5, selectedOption + 1); needToRender = true; break;
					case UserKeyPress.Confirm:
						switch (selectedOption)
						{
							case 0: DisableMovementAnimation = !DisableMovementAnimation; needToRender = true; break;
							case 1: DisableBattleTransition = !DisableBattleTransition; needToRender = true; break;
							case 2: DisableBattle = !DisableBattle; needToRender = true; break;
							case 3: ColorsScreen.Show(); needToRender = true; break;
							case 4: ControlsScreen.ControlsMenu(); needToRender = true; break;
							case 5: return;
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
