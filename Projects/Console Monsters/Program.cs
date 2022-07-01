namespace Console_Monsters;

public partial class Program
{
	public static void Main()
	{
		Encoding encoding = Console.OutputEncoding;
		try
		{
			Console.CursorVisible = false;
			Console.OutputEncoding = Encoding.UTF8;
			if (OperatingSystem.IsWindows())
			{
				const int screenWidth = 150;
				const int screenHeight = 50;
				try
				{
					Console.SetWindowSize(screenWidth, screenHeight);
					Console.SetBufferSize(screenWidth, screenHeight);
					Console.SetWindowPosition(0, 0);
				}
				catch { } // Left Blank on Purpose
			}

			StartMenu();
			while (gameRunning)
			{
				UpdateCharacter();
				HandleMapUserInput();
				if (gameRunning)
				{
					Renderer.RenderWorldMapView();
				}
			}
		}
		finally
		{
			Console.OutputEncoding = encoding;
			Console.Clear();
			Console.WriteLine("Console Monsters was closed.");
			Console.CursorVisible = true;
		}
	}

	static void StartMenu()
	{
		Console.Clear();
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

		sb.AppendLine(@$"{optionIndent} {(FirstTimeLaunching ? "  ▄▄▄▄▄ ▄▄▄▄▄  ▄▄  ▄▄▄  ▄▄▄▄▄" : "▄▄▄  ▄▄▄▄ ▄▄▄▄▄ ▄  ▄ ▄   ▄ ▄▄▄▄")}  {(arrowOption is 1 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent} {(FirstTimeLaunching ? "  █▄▄▄▄   █   █▄▄█ █▄▄▀   █  " : "█▄▄▀ █▄▄  █▄▄▄▄ █  █ █▀▄▀█ █▄▄ ")}  {(arrowOption is 1 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent} {(FirstTimeLaunching ? "  ▄▄▄▄█   █   █  █ █  █   █  " : "█  █ █▄▄▄ ▄▄▄▄█ ▀▄▄▀ █   █ █▄▄▄")}  {(arrowOption is 1 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent} ▄▄  ▄▄▄  ▄▄▄▄▄ ▄  ▄▄  ▄   ▄ ▄▄▄▄▄  {(arrowOption is 2 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█  █ █▄▄▀   █   █ █  █ █▀▄ █ █▄▄▄▄  {(arrowOption is 2 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}▀▄▄▀ █      █   █ ▀▄▄▀ █  ▀█ ▄▄▄▄█  {(arrowOption is 2 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent}        ▄▄▄▄ ▄   ▄ ▄ ▄▄▄▄▄  {(arrowOption is 3 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}        █▄▄   ▀▄▀  █   █    {(arrowOption is 3 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}        █▄▄▄ ▄▀ ▀▄ █   █    {(arrowOption is 3 ? "╰───╯" : "     ")}");

		Console.SetCursorPosition(0, 0);
		Console.WriteLine(sb);


		ConsoleKey key = Console.ReadKey(true).Key;
		switch (key)
		{
			case ConsoleKey.UpArrow:   arrowOption = Math.Max(1, arrowOption - 1); goto ReDraw;
			case ConsoleKey.DownArrow: arrowOption = Math.Min(3, arrowOption + 1); goto ReDraw;
			case ConsoleKey.Enter:
				switch (arrowOption)
				{
					case 1:
						FirstTimeLaunching = false;
						break;
					case 2:
						Options();
						Console.Clear();
						goto ReDraw; // To not run "arrowOption" so it stays on "Options" after going back
					case 3:
						gameRunning = false;
						break;
				}
				break;
			case ConsoleKey.Escape: break;
			default: goto ReDraw;
		}
	}

	static void Options()
	{
		StringBuilder sb = new();

		int arrowOption = 1;

		string optionIndent = new(' ', 60);
		string titleIndent = new(' ', 50);
		string newLineOptions = new('\n', 2);
		string newLineTitle = new('\n', 6);

		Console.Clear();
		ReDraw:
		sb.Clear();

		sb.AppendLine(@$"{newLineTitle}");
		sb.AppendLine(@$"{titleIndent} ██████╗ ██████╗ ████████╗██╗ ██████╗ ███╗   ██╗███████╗");
		sb.AppendLine(@$"{titleIndent}██╔═══██╗██╔══██╗╚══██╔══╝██║██╔═══██╗████╗  ██║██╔════╝");
		sb.AppendLine(@$"{titleIndent}██║   ██║██████╔╝   ██║   ██║██║   ██║██╔██╗ ██║███████╗");
		sb.AppendLine(@$"{titleIndent}██║   ██║██╔═══╝    ██║   ██║██║   ██║██║╚██╗██║╚════██║");
		sb.AppendLine(@$"{titleIndent}╚██████╔╝██║        ██║   ██║╚██████╔╝██║ ╚████║███████║");
		sb.AppendLine(@$"{titleIndent} ╚═════╝ ╚═╝        ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝");
		sb.AppendLine(@$"{newLineTitle}");

		sb.AppendLine(@$"{optionIndent}{(DisableMovementAnimation ? "╔══╗" : "╔══╗")}                      {(arrowOption is 1 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableMovementAnimation ? "║  ║" : "║██║")}  Movement Animation  {(arrowOption is 1 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableMovementAnimation ? "╚══╝" : "╚══╝")}                      {(arrowOption is 1 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattleTransition ? "╔══╗" : "╔══╗")}                     {(arrowOption is 2 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattleTransition ? "║  ║" : "║██║")}  Battle Transition  {(arrowOption is 2 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattleTransition ? "╚══╝" : "╚══╝")}                     {(arrowOption is 2 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattle ? "╔══╗" : "╔══╗")}                      {(arrowOption is 3 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattle ? "║  ║" : "║██║")}  Battles (DEV TOOL)  {(arrowOption is 3 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}{(DisableBattle ? "╚══╝" : "╚══╝")}                      {(arrowOption is 3 ? "╰───╯" : "     ")}");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent}█▀▀▄  ▄▄   ▄▄▄ ▄  ▄   {(arrowOption is 4 ? "╭───╮" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█■■█ █▄▄█ █    █■█    {(arrowOption is 4 ? "╞═●═╡" : "     ")}");
		sb.AppendLine(@$"{optionIndent}█▄▄▀ █  █ ▀▄▄▄ █  ▀▄  {(arrowOption is 4 ? "╰───╯" : "     ")}");

		Console.SetCursorPosition(0, 0);
		Console.WriteLine(sb);

		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.UpArrow:   arrowOption = Math.Max(1, arrowOption - 1); goto ReDraw;
			case ConsoleKey.DownArrow: arrowOption = Math.Min(4, arrowOption + 1); goto ReDraw;
			case ConsoleKey.Enter:
				switch (arrowOption)
				{
					case 1: DisableMovementAnimation = !DisableMovementAnimation; goto ReDraw;
					case 2: DisableBattleTransition = !DisableBattleTransition; goto ReDraw;
					case 3: DisableBattle = !DisableBattle; goto ReDraw;
					case 4: break;
				}
				break;
			case ConsoleKey.Escape: break;
			default: goto ReDraw;
		}
	}

	static void UpdateCharacter()
	{
		if (character.Animation == Sprites.RunUp) character.J--;
		if (character.Animation == Sprites.RunDown) character.J++;
		if (character.Animation == Sprites.RunLeft) character.I--;
		if (character.Animation == Sprites.RunRight) character.I++;

		character.AnimationFrame++;

		if ((character.Animation == Sprites.RunUp && character.AnimationFrame >= Sprites.Height) ||
			(character.Animation == Sprites.RunDown && character.AnimationFrame >= Sprites.Height) ||
			(character.Animation == Sprites.RunLeft && character.AnimationFrame >= Sprites.Width) ||
			(character.Animation == Sprites.RunRight && character.AnimationFrame >= Sprites.Width))
		{
			CheckTileForAction();
			character.Animation = Sprites.IdlePlayer;
			character.AnimationFrame = 0;
		}
		else if (character.Animation == Sprites.IdlePlayer && character.AnimationFrame >= character.Animation.Length)
		{
			character.AnimationFrame = 0;
		}
	}

	static void CheckTileForAction()
	{
		var (i, j) = Map.ScreenToTile(character.I, character.J);
		var s = map.SpriteSheet();
		switch (s[j][i])
		{
			case 'v': EnterVet(); break;
			case '0': Map.TransitionMapToPaletTown(); break;
			case '1': Map.TransitionMapToRoute1(); break;
			case '3': Map.TransitionMapToRoute2(); break;
			case 'G':
				if (!DisableBattle && Random.Shared.Next(2) is 0) // BATTLE CHANCE
				{
					Console.Clear();
					if(!DisableBattleTransition)
						Renderer.RenderBattleTransition();
					Renderer.RenderBattleView();
					PressEnterToContiue();
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Clear();
				}
				break;
		}
	}

	static void RenderStatusString()
	{
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine(" Monsters Status");
		Console.WriteLine();
		Console.WriteLine();
		Console.Write(" Press [enter] to continue...");
		PressEnterToContiue();
	}

	static void EnterVet()
	{
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine(" You enter the vet.");
		Console.WriteLine();
		for (int i = 0; i < ownedMonsters.Count; i++)
		{
			ownedMonsters[i].CurrentHP = ownedMonsters[i].MaximumHP;
		}
		Console.WriteLine(" All your monsters are healed.");
		Console.WriteLine();
		Console.Write(" Press [enter] to continue...");
		PressEnterToContiue();
	}

	static void HandleMapUserInput()
	{
		while (Console.KeyAvailable)
		{
			ConsoleKey key = Console.ReadKey(true).Key;
			switch (key)
			{
				case
					ConsoleKey.UpArrow    or ConsoleKey.W or
					ConsoleKey.DownArrow  or ConsoleKey.S or
					ConsoleKey.LeftArrow  or ConsoleKey.A or
					ConsoleKey.RightArrow or ConsoleKey.D:
					if (character.Animation == Sprites.IdlePlayer)
					{
						var (i, j) = Map.ScreenToTile(character.I, character.J);
						(i, j) = key switch
						{
							ConsoleKey.UpArrow    or ConsoleKey.W => (i, j - 1),
							ConsoleKey.DownArrow  or ConsoleKey.S => (i, j + 1),
							ConsoleKey.LeftArrow  or ConsoleKey.A => (i - 1, j),
							ConsoleKey.RightArrow or ConsoleKey.D => (i + 1, j),
							_ => throw new Exception("bug"),
						};
						if (map.IsValidCharacterMapTile(i, j))
						{
							if (DisableMovementAnimation)
							{
								switch (key)
								{
									case ConsoleKey.UpArrow    or ConsoleKey.W: character.J -= Sprites.Height; break;
									case ConsoleKey.DownArrow  or ConsoleKey.S: character.J += Sprites.Height; break;
									case ConsoleKey.LeftArrow  or ConsoleKey.A: character.I -= Sprites.Width; break;
									case ConsoleKey.RightArrow or ConsoleKey.D: character.I += Sprites.Width; break;
								}
								CheckTileForAction();
							}
							else
							{
								switch (key)
								{
									case ConsoleKey.UpArrow    or ConsoleKey.W: character.AnimationFrame = 0; character.Animation = Sprites.RunUp; break;
									case ConsoleKey.DownArrow  or ConsoleKey.S: character.AnimationFrame = 0; character.Animation = Sprites.RunDown; break;
									case ConsoleKey.LeftArrow  or ConsoleKey.A: character.AnimationFrame = 0; character.Animation = Sprites.RunLeft; break;
									case ConsoleKey.RightArrow or ConsoleKey.D: character.AnimationFrame = 0; character.Animation = Sprites.RunRight; break;
								}
							}
						}
					}
					break;
				case ConsoleKey.Enter: RenderStatusString(); break;
				case ConsoleKey.Backspace: break;
				case ConsoleKey.E:
					{
						var (i, j) = Map.ScreenToTile(character.I, character.J);
						map.InteractWithMapTile(i, j);
						break;
					}
				case ConsoleKey.Escape: StartMenu(); return;
			}
		}
	}
}
