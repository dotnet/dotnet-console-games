﻿namespace Animal_Trainer;

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
			while (!gameRunning)
			{
				StartMenu();
			}
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
			Console.WriteLine("Animal Trainer was closed.");
			Console.CursorVisible = true;
		}
	}

	static void StartMenu()
	{
		StringBuilder sb = new StringBuilder();

		int arrowOption = 1;

		string optionIndent = new string(' ', 60);
		string titleIndent = new string(' ', 40);
		string newLineOptions = new string('\n', 2);
		string newLineTitle = new string('\n', 6);
		ReDraw:
		sb.AppendLine($"{newLineTitle}");
		sb.AppendLine(@$"{titleIndent}  _____                      _        __  __                 _                ");
		sb.AppendLine(@$"{titleIndent} / ____|                    | |      |  \/  |               | |               ");
		sb.AppendLine(@$"{titleIndent}| |     ___  _ __  ___  ___ | | ___  | \  / | ___  _ __  ___| |_ ___ _ __ ___ ");
		sb.AppendLine(@$"{titleIndent}| |    / _ \| '_ \/ __|/ _ \| |/ _ \ | |\/| |/ _ \| '_ \/ __| __/ _ \ '__/ __|");
		sb.AppendLine(@$"{titleIndent}| |___| (_) | | | \__ \ (_) | |  __/ | |  | | (_) | | | \__ \ ||  __/ |  \__ \");
		sb.AppendLine(@$"{titleIndent} \_____\___/|_| |_|___/\___/|_|\___| |_|  |_|\___/|_| |_|___/\__\___|_|  |___/");

		sb.AppendLine(@$"{newLineTitle}");
		sb.AppendLine(@$"{optionIndent}   ▄▄▄▄▄ ▄▄▄▄▄  ▄▄  ▄▄▄  ▄▄▄▄▄ {(arrowOption is 1 ? "  ▄▀" : null)}");
		sb.AppendLine(@$"{optionIndent}   █▄▄▄▄   █   █▄▄█ █▄▄▀   █   {(arrowOption is 1 ? "■█  " : null)}");
		sb.AppendLine(@$"{optionIndent}   ▄▄▄▄█   █   █  █ █  █   █   {(arrowOption is 1 ? "  ▀▄" : null)}");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent} ▄▄  ▄▄▄  ▄▄▄▄▄ ▄  ▄▄  ▄   ▄ ▄▄▄▄▄ {(arrowOption is 2 ? "  ▄▀" : null)}");
		sb.AppendLine(@$"{optionIndent}█  █ █▄▄▀   █   █ █  █ █▀▄ █ █▄▄▄▄ {(arrowOption is 2 ? "■█  " : null)}");
		sb.AppendLine(@$"{optionIndent}▀▄▄▀ █      █   █ ▀▄▄▀ █  ▀█ ▄▄▄▄█ {(arrowOption is 2 ? "  ▀▄" : null)}");
		sb.AppendLine(@$"{newLineOptions}");
		sb.AppendLine(@$"{optionIndent}        ▄▄▄▄ ▄   ▄ ▄ ▄▄▄▄▄ {(arrowOption is 3 ? "  ▄▀" : null)}");
		sb.AppendLine(@$"{optionIndent}        █▄▄   ▀▄▀  █   █   {(arrowOption is 3 ? "■█  " : null)}");
		sb.AppendLine(@$"{optionIndent}        █▄▄▄ ▄▀ ▀▄ █   █   {(arrowOption is 3 ? "  ▀▄" : null)}");

		Console.Clear();
		Console.WriteLine(sb);


		ConsoleKey key = Console.ReadKey(true).Key;
		switch (key)
		{
			case ConsoleKey.UpArrow:
				if (arrowOption != 1)
					arrowOption--;
					goto ReDraw;
			case ConsoleKey.DownArrow:
				if (arrowOption != 3)
					arrowOption++;
				goto ReDraw;
			case ConsoleKey.Enter:
				switch (arrowOption)
				{
					case 1:
						gameRunning = true;
						break;
					case 2:
						Options();
						break;
					case 3:
						break;
				}
				break;
		}
	}

	static void Options()
	{
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine(" Options");
		Console.WriteLine();
		Console.WriteLine();
		Console.Write(" Press [enter] to continue...");
		PressEnterToContiue();
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
			var (i, j) = Map.ScreenToTile(character.I, character.J);
			var s = map.SpriteSheet();
			switch (s[j][i])
			{
				case 'v': EnterVet(); break;
				case '0': Map.TransitionMapToTown(); break;
				case '1': Map.TransitionMapToField(); break;
				case 'G':
					if (Random.Shared.Next(3) is 0)
					{
						Console.Clear();
						Renderer.RenderBattleTransition();
						Renderer.RenderBattleView();
						Console.ReadKey(true);
						Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.Gray;
						Console.Clear();
					}
					break;
			}

			character.Animation = Sprites.IdlePlayer;
			character.AnimationFrame = 0;
		}
		else if (character.Animation == Sprites.IdlePlayer && character.AnimationFrame >= character.Animation.Length)
		{
			character.AnimationFrame = 0;
		}
	}

	static void RenderStatusString()
	{
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine(" Animal Status");
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
		for (int i = 0; i < ownedAnimals.Count; i++)
		{
			ownedAnimals[i].CurrentHP = ownedAnimals[i].MaximumHP;
		}
		Console.WriteLine(" All your animals are healed.");
		Console.WriteLine();
		Console.Write(" Press [enter] to continue...");
		PressEnterToContiue();
	}

	static void HandleMapUserInput()
	{
		var (i, j) = Map.ScreenToTile(character.I, character.J);

		while (Console.KeyAvailable)
		{
			ConsoleKey key = Console.ReadKey(true).Key;
			switch (key)
			{
				case
					ConsoleKey.UpArrow or ConsoleKey.W or
					ConsoleKey.DownArrow or ConsoleKey.S or
					ConsoleKey.LeftArrow or ConsoleKey.A or
					ConsoleKey.RightArrow or ConsoleKey.D:
					if (character.Animation == Sprites.IdlePlayer)
					{
						(i, j) = key switch
						{
							ConsoleKey.UpArrow or ConsoleKey.W => (i, j - 1),
							ConsoleKey.DownArrow or ConsoleKey.S => (i, j + 1),
							ConsoleKey.LeftArrow or ConsoleKey.A => (i - 1, j),
							ConsoleKey.RightArrow or ConsoleKey.D => (i + 1, j),
							_ => throw new Exception("bug"),
						};
						if (map.IsValidCharacterMapTile(i, j))
						{
							switch (key)
							{
								case ConsoleKey.UpArrow or ConsoleKey.W: character.AnimationFrame = 0; character.Animation = Sprites.RunUp; break;
								case ConsoleKey.DownArrow or ConsoleKey.S: character.AnimationFrame = 0; character.Animation = Sprites.RunDown; break;
								case ConsoleKey.LeftArrow or ConsoleKey.A: character.AnimationFrame = 0; character.Animation = Sprites.RunLeft; break;
								case ConsoleKey.RightArrow or ConsoleKey.D: character.AnimationFrame = 0; character.Animation = Sprites.RunRight; break;
							}
						}
					}
					break;
				case ConsoleKey.Enter: RenderStatusString(); break;
				case ConsoleKey.Backspace: break;
				case ConsoleKey.E: map.InteractWithMapTile(i, j); break;
				case ConsoleKey.Escape: gameRunning = false; return;
			}
		}
	}
}
