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
				// TODO: Make this dynamic at some point as it is causing a bug with our menus
				const int screenWidth = 150;
				const int screenHeight = 50;
				try
				{
					Console.SetWindowSize(screenWidth, screenHeight);
					Console.SetBufferSize(screenWidth, screenHeight);
				}
				catch
				{
					// empty on purpose
				}
			}

			StartScreen.StartMenu();
			while (gameRunning)
			{
				UpdateCharacter();
				HandleMapUserInput();
				if (gameRunning)
				{
					MapScreen.Render();
					SleepAfterRender();
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

	static void UpdateCharacter()
	{
		if (character.Animation == Player.RunUp)    character.J--;
		if (character.Animation == Player.RunDown)  character.J++;
		if (character.Animation == Player.RunLeft)  character.I--;
		if (character.Animation == Player.RunRight) character.I++;

		character.AnimationFrame++;

		if ((character.Animation == Player.RunUp    && character.AnimationFrame >= Sprites.Height) ||
			(character.Animation == Player.RunDown  && character.AnimationFrame >= Sprites.Height) ||
			(character.Animation == Player.RunLeft  && character.AnimationFrame >= Sprites.Width) ||
			(character.Animation == Player.RunRight && character.AnimationFrame >= Sprites.Width))
		{
			var (i, j) = MapBase.WorldToTile(character.I, character.J);
			map.PerformTileAction(i, j);
			character.Animation =
				character.Animation == Player.RunUp    ? Player.IdleUp    :
				character.Animation == Player.RunDown  ? Player.IdleDown  :
				character.Animation == Player.RunLeft  ? Player.IdleLeft  :
				character.Animation == Player.RunRight ? Player.IdleRight :
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
									case ConsoleKey.UpArrow    or ConsoleKey.W: character.J -= Sprites.Height; character.Animation = Player.IdleUp;    break;
									case ConsoleKey.DownArrow  or ConsoleKey.S: character.J += Sprites.Height; character.Animation = Player.IdleDown;  break;
									case ConsoleKey.LeftArrow  or ConsoleKey.A: character.I -= Sprites.Width;  character.Animation = Player.IdleLeft;  break;
									case ConsoleKey.RightArrow or ConsoleKey.D: character.I += Sprites.Width;  character.Animation = Player.IdleRight; break;
								}
								var (i2, j2) = MapBase.WorldToTile(character.I, character.J);
								map.PerformTileAction(i2, j2);
							}
							else
							{
								switch (key)
								{
									case ConsoleKey.UpArrow    or ConsoleKey.W: character.AnimationFrame = 0; character.Animation = Player.RunUp;    break;
									case ConsoleKey.DownArrow  or ConsoleKey.S: character.AnimationFrame = 0; character.Animation = Player.RunDown;  break;
									case ConsoleKey.LeftArrow  or ConsoleKey.A: character.AnimationFrame = 0; character.Animation = Player.RunLeft;  break;
									case ConsoleKey.RightArrow or ConsoleKey.D: character.AnimationFrame = 0; character.Animation = Player.RunRight; break;
								}
							}
						}
						else
						{
							character.Animation = key switch
							{
								ConsoleKey.UpArrow    or ConsoleKey.W => Player.IdleUp,
								ConsoleKey.DownArrow  or ConsoleKey.S => Player.IdleDown,
								ConsoleKey.LeftArrow  or ConsoleKey.A => Player.IdleLeft,
								ConsoleKey.RightArrow or ConsoleKey.D => Player.IdleRight,
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

					#warning TODO: this is temporary population of monsters during developemnt
					partyMonsters.Clear();
					Turtle turtle = new();
					for (int i = 0; i < (maxPartySize - GameRandom.Next(0, 3)); i++)
					{
						partyMonsters.Add(turtle);
					}

					inInventory = true;
					while (inInventory)
					{
						InventoryScreen.Render();
					
						switch (Console.ReadKey(true).Key)
						{
							case ConsoleKey.UpArrow:
								if (SelectedPlayerInventoryItem > 0)
								{
									SelectedPlayerInventoryItem--;
								}
								else
								{
									SelectedPlayerInventoryItem = PlayerInventory.Distinct().Count() - 1;
								}
								break;
							case ConsoleKey.DownArrow:
								if (SelectedPlayerInventoryItem < PlayerInventory.Distinct().Count() - 1)
								{
									SelectedPlayerInventoryItem++;
								}
								else
								{
									SelectedPlayerInventoryItem = 0;
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
						var (i, j) = character.InteractTile;
						map.InteractWithMapTile(i, j);
						break;
					}
				case ConsoleKey.Escape:
					StartScreen.StartMenu();
					return;
			}
		}
	}

	public static void SleepAfterRender()
	{
		// frame rate control targeting 30 frames per second
		DateTime now = DateTime.Now;
		TimeSpan sleep = TimeSpan.FromMilliseconds(33) - (now - previoiusRender);
		if (sleep > TimeSpan.Zero)
		{
			Thread.Sleep(sleep);
		}
		previoiusRender = DateTime.Now;
	}
}
