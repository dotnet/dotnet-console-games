namespace Console_Monsters;

public partial class Program
{
	public static void Main()
	{
		Exception? exception = null;
		Encoding encoding = Console.OutputEncoding;
		try
		{
			Console.CursorVisible = false;
			Console.OutputEncoding = Encoding.UTF8;

			//// disabled because this breaks on Windows Terminal
			//if (OperatingSystem.IsWindows())
			//{
			//	const int screenWidth = 150;
			//	const int screenHeight = 50;
			//	try
			//	{
			//		Console.SetWindowSize(screenWidth, screenHeight);
			//		Console.SetBufferSize(screenWidth, screenHeight);
			//	}
			//	catch
			//	{
			//		// empty on purpose
			//	}
			//}

			StartScreen.Show();
			while (GameRunning)
			{
				UpdateCharacter();
				HandleMapUserInput();
				if (GameRunning)
				{
					MapScreen.Render();
					SleepAfterRender();
				}
			}
		}
		catch (Exception e)
		{
			exception = e;
			throw;
		}
		finally
		{
			Console.OutputEncoding = encoding;
			Console.ResetColor();
			Console.Clear();
			Console.WriteLine(exception?.ToString() ?? "Console Monsters was closed.");
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
			Map.PerformTileAction(i, j);
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
			UserKeyPress input = keyMappings.GetValueOrDefault(key);
			switch (input)
			{
				case
					UserKeyPress.Up or
					UserKeyPress.Down or
					UserKeyPress.Left or
					UserKeyPress.Right:
					if (PromptText is not null)
					{
						break;
					}
					if (ShopText is not null)
					{
						break;
					}
					if (PromptShopText is not null)
					{
						break;
					}
					if (character.IsIdle)
					{
						var (i, j) = MapBase.WorldToTile(character.I, character.J);
						(i, j) = input switch
						{
							UserKeyPress.Up    => (i, j - 1),
							UserKeyPress.Down  => (i, j + 1),
							UserKeyPress.Left  => (i - 1, j),
							UserKeyPress.Right => (i + 1, j),
							_ => throw new Exception("bug"),
						};
						if (Map.IsValidCharacterMapTile(i, j))
						{
							if (DisableMovementAnimation)
							{
								switch (input)
								{
									case UserKeyPress.Up:    character.J -= Sprites.Height; character.Animation = Player.IdleUp;    break;
									case UserKeyPress.Down:  character.J += Sprites.Height; character.Animation = Player.IdleDown;  break;
									case UserKeyPress.Left:  character.I -= Sprites.Width;  character.Animation = Player.IdleLeft;  break;
									case UserKeyPress.Right: character.I += Sprites.Width;  character.Animation = Player.IdleRight; break;
								}
								var (i2, j2) = MapBase.WorldToTile(character.I, character.J);
								Map.PerformTileAction(i2, j2);
							}
							else
							{
								switch (input)
								{
									case UserKeyPress.Up:    character.AnimationFrame = 0; character.Animation = Player.RunUp;    break;
									case UserKeyPress.Down:  character.AnimationFrame = 0; character.Animation = Player.RunDown;  break;
									case UserKeyPress.Left:  character.AnimationFrame = 0; character.Animation = Player.RunLeft;  break;
									case UserKeyPress.Right: character.AnimationFrame = 0; character.Animation = Player.RunRight; break;
								}
							}
						}
						else
						{
							character.Animation = input switch
							{
								UserKeyPress.Up    => Player.IdleUp,
								UserKeyPress.Down  => Player.IdleDown,
								UserKeyPress.Left  => Player.IdleLeft,
								UserKeyPress.Right => Player.IdleRight,
								_ => throw new Exception("bug"),
							};
						}
					}
					break;
				case UserKeyPress.Status:
					if (PromptText is not null)
					{
						break; 
					}
					if (ShopText is not null)
					{
						break;
					}
					if (PromptShopText is not null)
					{
						break;
					}
					InInventory = true;
					while (InInventory)
					{
						InventoryScreen.Render();
					
						switch (keyMappings.GetValueOrDefault(Console.ReadKey(true).Key))
						{
							case UserKeyPress.Up:
								if (SelectedPlayerInventoryItem > 0)
								{
									SelectedPlayerInventoryItem--;
								}
								else
								{
									SelectedPlayerInventoryItem = PlayerInventory.Distinct().Count() - 1;
								}
								break;
							case UserKeyPress.Down:
								if (SelectedPlayerInventoryItem < PlayerInventory.Distinct().Count() - 1)
								{
									SelectedPlayerInventoryItem++;
								}
								else
								{
									SelectedPlayerInventoryItem = 0;
								}
								break;
							case UserKeyPress.Escape: InInventory = false; break;
						}
					}
					break;
				case UserKeyPress.Confirm:
					PromptText = null;
					ShopText = null;
					PromptText = null;

					break;
				case UserKeyPress.Action:
					if (PromptText is not null)
					{
						PromptText = null;
						break;
					}
					if (ShopText is not null)
					{
						ShopText = null;
						break;
					}
					if (PromptShopText is not null)
					{
						PromptShopText = null;
						break;
					}
					if (character.IsIdle)
					{
						var (i, j) = character.InteractTile;
						Map.InteractWithMapTile(i, j);
					}
					break;

				case UserKeyPress.Escape:
					StartScreen.Show();
					return;
			}
		}
	}

	public static void SleepAfterRender()
	{
		// frame rate control targeting 30 frames per second
		DateTime now = DateTime.Now;
		TimeSpan sleep = TimeSpan.FromMilliseconds(33) - (now - PrevioiusRender);
		if (sleep > TimeSpan.Zero)
		{
			Thread.Sleep(sleep);
		}
		PrevioiusRender = DateTime.Now;
	}
}
