namespace Console_Monsters.Screens.Menus;

public static class ControlsScreen
{
	public static void ControlsMenu()
	{
		string[] bigHeader = new[]
		{
			"██╗  ██╗███████╗██╗   ██╗    ███╗   ███╗ █████╗ ██████╗ ██████╗ ██╗███╗   ██╗ ██████╗ ",
			"██║ ██╔╝██╔════╝╚██╗ ██╔╝    ████╗ ████║██╔══██╗██╔══██╗██╔══██╗██║████╗  ██║██╔════╝ ",
			"█████╔╝ █████╗   ╚████╔╝     ██╔████╔██║███████║██████╔╝██████╔╝██║██╔██╗ ██║██║  ███╗",
			"██╔═██╗ ██╔══╝    ╚██╔╝      ██║╚██╔╝██║██╔══██║██╔═══╝ ██╔═══╝ ██║██║╚██╗██║██║   ██║",
			"██║  ██╗███████╗   ██║       ██║ ╚═╝ ██║██║  ██║██║     ██║     ██║██║ ╚████║╚██████╔╝",
			"╚═╝  ╚═╝╚══════╝   ╚═╝       ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝     ╚═╝╚═╝  ╚═══╝ ╚═════╝ ",
		};
		int bigHeaderWidth = bigHeader.Max(line => line.Length);
		const int bigHeaderPadding = 2;
		const int optionPadding = 1;
		var (consoleWidth, consoleHeight) = ConsoleHelpers.GetWidthAndHeight();
		Console.Clear();
		int selectedOption = 0;
		bool needToRender = true;
		string[] spacingSmall = {"\n"};
		string[] spacingLarge = {"\n","\n"};
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
					string[][] options = new[]
					{
						AsciiGenerator.ToAscii((selectedOption is 0 ? "■" : "□") + " up:       "+ reverseKeyMappings[UserKeyPress.Up].ToDisplayString()     ), spacingSmall,
						AsciiGenerator.ToAscii((selectedOption is 1 ? "■" : "□") + " down:     "+ reverseKeyMappings[UserKeyPress.Down].ToDisplayString()   ), spacingSmall,
						AsciiGenerator.ToAscii((selectedOption is 2 ? "■" : "□") + " left:     "+ reverseKeyMappings[UserKeyPress.Left].ToDisplayString()   ), spacingSmall,
						AsciiGenerator.ToAscii((selectedOption is 3 ? "■" : "□") + " right:    "+ reverseKeyMappings[UserKeyPress.Right].ToDisplayString()  ), spacingSmall,
						AsciiGenerator.ToAscii((selectedOption is 4 ? "■" : "□") + " action:   "+ reverseKeyMappings[UserKeyPress.Action].ToDisplayString() ), spacingSmall,
						AsciiGenerator.ToAscii((selectedOption is 5 ? "■" : "□") + " confirm:  "+ reverseKeyMappings[UserKeyPress.Confirm].ToDisplayString()), spacingSmall,
						AsciiGenerator.ToAscii((selectedOption is 6 ? "■" : "□") + " status:   "+ reverseKeyMappings[UserKeyPress.Status].ToDisplayString() ), spacingSmall,
						AsciiGenerator.ToAscii((selectedOption is 7 ? "■" : "□") + " pause:    "+ reverseKeyMappings[UserKeyPress.Escape].ToDisplayString() ), 
						spacingLarge, //NEEDS FULL SCREEN TO SHOW ALL
						AsciiGenerator.ToAscii((selectedOption is 8 ? "■" : "□") + " restore defaults"),
						AsciiGenerator.ToAscii((selectedOption is 9 ? "■" : "□") + " back"),
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
						$@"Key Mapping",
						$@"{(selectedOption is 0 ? ">" : " ")} Up:       {reverseKeyMappings[UserKeyPress.Up].ToDisplayString()     }           ",
						$@"{(selectedOption is 1 ? ">" : " ")} Down:     {reverseKeyMappings[UserKeyPress.Down].ToDisplayString()   }           ",
						$@"{(selectedOption is 2 ? ">" : " ")} Left:     {reverseKeyMappings[UserKeyPress.Left].ToDisplayString()   }           ",
						$@"{(selectedOption is 3 ? ">" : " ")} Right:    {reverseKeyMappings[UserKeyPress.Right].ToDisplayString()  }           ",
						$@"{(selectedOption is 4 ? ">" : " ")} Action:   {reverseKeyMappings[UserKeyPress.Action].ToDisplayString() }           ",
						$@"{(selectedOption is 5 ? ">" : " ")} Confirm:  {reverseKeyMappings[UserKeyPress.Confirm].ToDisplayString()}           ",
						$@"{(selectedOption is 6 ? ">" : " ")} Status:   {reverseKeyMappings[UserKeyPress.Status].ToDisplayString() }           ",
						$@"{(selectedOption is 7 ? ">" : " ")} Pause:    {reverseKeyMappings[UserKeyPress.Escape].ToDisplayString() }           ",
						$@"{(selectedOption is 8 ? ">" : " ")} Restore Defaults",
						$@"{(selectedOption is 9 ? ">" : " ")} Back",
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
					case UserKeyPress.Up:
						selectedOption = Math.Max(0, selectedOption - 1);
						needToRender = true;
						break;
					case UserKeyPress.Down:
						selectedOption = Math.Min(9, selectedOption + 1);
						needToRender = true;
						break;
					case UserKeyPress.Confirm:
						switch (selectedOption)
						{
							case 0: PerformKeyMap(UserKeyPress.Up);      needToRender = true; break;
							case 1: PerformKeyMap(UserKeyPress.Down);    needToRender = true; break;
							case 2: PerformKeyMap(UserKeyPress.Left);    needToRender = true; break;
							case 3: PerformKeyMap(UserKeyPress.Right);   needToRender = true; break;
							case 4: PerformKeyMap(UserKeyPress.Action);  needToRender = true; break;
							case 5: PerformKeyMap(UserKeyPress.Confirm); needToRender = true; break;
							case 6: PerformKeyMap(UserKeyPress.Status);  needToRender = true; break;
							case 7: PerformKeyMap(UserKeyPress.Escape);  needToRender = true; break;
							case 8: DefaultKeyMappings(); needToRender = true; break;
							case 9: return;
							default: throw new NotImplementedException();
						}
						break;
					case UserKeyPress.Escape: return;
				}
			}
			// prevent CPU spiking
			Thread.Sleep(TimeSpan.FromMilliseconds(1));
		}
	}

	private static void PerformKeyMap(UserKeyPress userInput)
	{
		Console.Clear();
		Console.Write($"Press a key to use for the Main {userInput} input...");
		ConsoleKey main = Console.ReadKey(true).Key;
		if (keyMappings.ContainsKey(main) && keyMappings[main] is UserKeyPress.Escape)
		{
			return;
		}
		Console.Clear();
		Console.Write($"Press a key to use for the Alternate {userInput} input or [{reverseKeyMappings[UserKeyPress.Escape].ToDisplayString()}] to leave empty...");
		ConsoleKey? alternate = Console.ReadKey(true).Key;
		if (keyMappings.ContainsKey(alternate.Value) && keyMappings[alternate.Value] is UserKeyPress.Escape)
		{
			alternate = null;
		}
		bool valid_main = !keyMappings.ContainsKey(main) || keyMappings[main] == userInput;
		bool valid_alternate = alternate is null || !keyMappings.ContainsKey(alternate.Value) || keyMappings[alternate.Value] == userInput;
		if (valid_main && valid_alternate)
		{
			reverseKeyMappings[userInput] = (main, alternate);
			ApplyKeyMappings();
		}
		else
		{
			Console.Clear();
			Console.Write($"Keys were already in use. Setting could not be applied. Press {reverseKeyMappings[UserKeyPress.Confirm].ToDisplayString()} to continue...");
			while (true)
			{
				ConsoleKey key = Console.ReadKey(true).Key;
				if (keyMappings.ContainsKey(key) && keyMappings[key] is UserKeyPress.Confirm)
				{
					break;
				}
			}
		}
	}
}
