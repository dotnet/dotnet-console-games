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
				catch
				{
					// Left Blank on Purpose
				}
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

	static void UpdateCharacter()
	{
		if (character.Animation == Sprites.RunUp)    character.J--;
		if (character.Animation == Sprites.RunDown)  character.J++;
		if (character.Animation == Sprites.RunLeft)  character.I--;
		if (character.Animation == Sprites.RunRight) character.I++;

		character.AnimationFrame++;

		if ((character.Animation == Sprites.RunUp      && character.AnimationFrame >= Sprites.Height) ||
			(character.Animation == Sprites.RunDown    && character.AnimationFrame >= Sprites.Height) ||
			(character.Animation == Sprites.RunLeft    && character.AnimationFrame >= Sprites.Width) ||
			(character.Animation == Sprites.RunRight   && character.AnimationFrame >= Sprites.Width))
		{
			var (i, j) = Maps.ScreenToTile(character.I, character.J);
			switch (map[j][i])
			{
				case 'v': EnterVet(); break;
				case '0': Maps.TransitionMapToTown(); break;
				case '1': Maps.TransitionMapToField(); break;
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

	static void PressEnterToContiue()
	{
	GetInput:
		ConsoleKey key = Console.ReadKey(true).Key;
		switch (key)
		{
			case ConsoleKey.Enter: return;
			case ConsoleKey.Escape: gameRunning = false; return;
			default: goto GetInput;
		}
	}

	static void EnterVet()
	{
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine(" You enter the vet.");
		Console.WriteLine();
		Console.WriteLine(" All your animals are healed.");
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
						var (i, j) = Maps.ScreenToTile(character.I, character.J);
						(i, j) = key switch
						{
							ConsoleKey.UpArrow    or ConsoleKey.W => (i, j - 1),
							ConsoleKey.DownArrow  or ConsoleKey.S => (i, j + 1),
							ConsoleKey.LeftArrow  or ConsoleKey.A => (i - 1, j),
							ConsoleKey.RightArrow or ConsoleKey.D => (i + 1, j),
							_ => throw new Exception("bug"),
						};
						if (Maps.IsValidCharacterMapTile(map, i, j))
						{
							switch (key)
							{
								case ConsoleKey.UpArrow    or ConsoleKey.W: character.AnimationFrame = 0; character.Animation = Sprites.RunUp;    break;
								case ConsoleKey.DownArrow  or ConsoleKey.S: character.AnimationFrame = 0; character.Animation = Sprites.RunDown;  break;
								case ConsoleKey.LeftArrow  or ConsoleKey.A: character.AnimationFrame = 0; character.Animation = Sprites.RunLeft;  break;
								case ConsoleKey.RightArrow or ConsoleKey.D: character.AnimationFrame = 0; character.Animation = Sprites.RunRight; break;
							}
						}
					}
					break;
				case ConsoleKey.Enter: RenderStatusString(); break;
				case ConsoleKey.Backspace: break;
				case ConsoleKey.Escape: gameRunning = false; return;
			}
		}
	}
}
