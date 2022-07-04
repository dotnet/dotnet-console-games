﻿namespace Console_Monsters;

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
					// Console.SetWindowPosition(0, 0);
				}
				catch
				{
					// empty on purpose
				}
			}

			#region DEBUGGING INVENTORY
			Inventory.AddToStorage(Items.MonsterBox);
			Inventory.AddToStorage(Items.XPBerries);
			Inventory.AddToStorage(Items.HealthPotionLarge);
			Inventory.AddToStorage(Items.HealthPotionMedium);
			Inventory.AddToStorage(Items.HealthPotionSmall);
			Inventory.AddToStorage(Items.PLACEHOLDER1);
			Inventory.AddToStorage(Items.PLACEHOLDER2);
			Inventory.AddToStorage(Items.PLACEHOLDER3);
			Inventory.AddToStorage(Items.PLACEHOLDER4);
			Inventory.AddToStorage(Items.PLACEHOLDER5);
			Inventory.AddToStorage(Items.PLACEHOLDER6);
			Inventory.AddToStorage(Items.PLACEHOLDER7);
			Inventory.AddToStorage(Items.PLACEHOLDER8);
			Inventory.AddToStorage(Items.PLACEHOLDER9);
			Inventory.AddToStorage(Items.PLACEHOLDER10);
			Inventory.AddToStorage(Items.PLACEHOLDER11);
			Inventory.AddToStorage(Items.PLACEHOLDER12);
			Inventory.AddToStorage(Items.PLACEHOLDER13);

			for (int i = 0; i < (maxPartySize - GameRandom.Next(0, 3)); i++)
			{
				activeMonsters.Add(MonsterBase.GetRandom());
			}
			#endregion


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
			case ConsoleKey.UpArrow or ConsoleKey.W:   arrowOption = Math.Max(1, arrowOption - 1); goto ReDraw;
			case ConsoleKey.DownArrow or ConsoleKey.S: arrowOption = Math.Min(3, arrowOption + 1); goto ReDraw;
			case ConsoleKey.Enter or ConsoleKey.E:
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
			case ConsoleKey.UpArrow or ConsoleKey.W:   arrowOption = Math.Max(1, arrowOption - 1); goto ReDraw;
			case ConsoleKey.DownArrow or ConsoleKey.S: arrowOption = Math.Min(4, arrowOption + 1); goto ReDraw;
			case ConsoleKey.Enter or ConsoleKey.E:
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
		if (character.Animation == Character.RunUp)    character.J--;
		if (character.Animation == Character.RunDown)  character.J++;
		if (character.Animation == Character.RunLeft)  character.I--;
		if (character.Animation == Character.RunRight) character.I++;

		character.AnimationFrame++;

		if ((character.Animation == Character.RunUp    && character.AnimationFrame >= Sprites.Height) ||
			(character.Animation == Character.RunDown  && character.AnimationFrame >= Sprites.Height) ||
			(character.Animation == Character.RunLeft  && character.AnimationFrame >= Sprites.Width) ||
			(character.Animation == Character.RunRight && character.AnimationFrame >= Sprites.Width))
		{
			map.PerformTileAction();
			character.Animation =
				character.Animation == Character.RunUp    ? Character.IdleUp    :
				character.Animation == Character.RunDown  ? Character.IdleDown  :
				character.Animation == Character.RunLeft  ? Character.IdleLeft  :
				character.Animation == Character.RunRight ? Character.IdleRight :
				throw new NotImplementedException();
			character.AnimationFrame = 0;
		}
		else if (character.IsIdle && character.AnimationFrame >= character.Animation.Length)
		{
			character.AnimationFrame = 0;
		}
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
					if (promptText is not null)
					{
						break;
					}
					if (character.IsIdle)
					{
						var (i, j) = MapBase.WorldToTile(character.I, character.J);
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
									case ConsoleKey.UpArrow    or ConsoleKey.W: character.J -= Sprites.Height; character.Animation = Character.IdleUp;    break;
									case ConsoleKey.DownArrow  or ConsoleKey.S: character.J += Sprites.Height; character.Animation = Character.IdleDown;  break;
									case ConsoleKey.LeftArrow  or ConsoleKey.A: character.I -= Sprites.Width;  character.Animation = Character.IdleLeft;  break;
									case ConsoleKey.RightArrow or ConsoleKey.D: character.I += Sprites.Width;  character.Animation = Character.IdleRight; break;
								}
								map.PerformTileAction();
							}
							else
							{
								switch (key)
								{
									case ConsoleKey.UpArrow    or ConsoleKey.W: character.AnimationFrame = 0; character.Animation = Character.RunUp;    break;
									case ConsoleKey.DownArrow  or ConsoleKey.S: character.AnimationFrame = 0; character.Animation = Character.RunDown;  break;
									case ConsoleKey.LeftArrow  or ConsoleKey.A: character.AnimationFrame = 0; character.Animation = Character.RunLeft;  break;
									case ConsoleKey.RightArrow or ConsoleKey.D: character.AnimationFrame = 0; character.Animation = Character.RunRight; break;
								}
							}
						}
						else
						{
							character.Animation = key switch
							{
								ConsoleKey.UpArrow    or ConsoleKey.W => Character.IdleUp,
								ConsoleKey.DownArrow  or ConsoleKey.S => Character.IdleDown,
								ConsoleKey.LeftArrow  or ConsoleKey.A => Character.IdleLeft,
								ConsoleKey.RightArrow or ConsoleKey.D => Character.IdleRight,
								_ => throw new Exception("bug"),
							};
						}
					}
					break;
				case ConsoleKey.B:
					if (promptText is not null)
					{
						break; 
					}

					inInventory = true;
					while (inInventory)
					{
						Renderer.RenderInventoryView();
					
						switch (Console.ReadKey(true).Key)
						{
							case ConsoleKey.UpArrow:
								if (Inventory.SelectedItem > 0)
								{
									Inventory.SelectedItem--;
								}
								else
								{
									Inventory.SelectedItem = Inventory.Count - 1;
								}
								break;
							case ConsoleKey.DownArrow:
								if (Inventory.SelectedItem < Inventory.Count - 1)
								{
									Inventory.SelectedItem++;
								}
								else
								{
									Inventory.SelectedItem = 0;
								}
								break;
							case ConsoleKey.Escape: inInventory = false; break;
						}
					}
					break;
				case ConsoleKey.Enter:
					promptText = null;
					break;
				case ConsoleKey.E:
					if (promptText is not null)
					{
						promptText = null;
						break;
					}
					{
						var (i, j) = MapBase.WorldToTile(character.I, character.J);;
						map.InteractWithMapTile(i, j);
						break;
					}
				case ConsoleKey.Escape:
					StartMenu();
					return;
			}
		}
	}
}
