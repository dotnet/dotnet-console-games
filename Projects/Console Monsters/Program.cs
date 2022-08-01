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
				UpdatePlayer();
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

	static void UpdatePlayer()
	{
		if (player.Animation == Player.RunUp)    player.J--;
		if (player.Animation == Player.RunDown)  player.J++;
		if (player.Animation == Player.RunLeft)  player.I--;
		if (player.Animation == Player.RunRight) player.I++;

		player.AnimationFrame++;

		if ((player.Animation == Player.RunUp    && player.AnimationFrame >= Sprites.Height) ||
			(player.Animation == Player.RunDown  && player.AnimationFrame >= Sprites.Height) ||
			(player.Animation == Player.RunLeft  && player.AnimationFrame >= Sprites.Width) ||
			(player.Animation == Player.RunRight && player.AnimationFrame >= Sprites.Width))
		{
			var (i, j) = MapBase.WorldToTile(player.I, player.J);
			Map.PerformTileAction(i, j);
			player.Animation =
				player.Animation == Player.RunUp    ? Player.IdleUp    :
				player.Animation == Player.RunDown  ? Player.IdleDown  :
				player.Animation == Player.RunLeft  ? Player.IdleLeft  :
				player.Animation == Player.RunRight ? Player.IdleRight :
				throw new NotImplementedException();
			player.AnimationFrame = 0;
		}
		else if (player.IsIdle && player.AnimationFrame >= player.Animation.Length)
		{
			player.AnimationFrame = 0;
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
					if (player.IsIdle)
					{
						var (i, j) = MapBase.WorldToTile(player.I, player.J);
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
									case UserKeyPress.Up:    player.J -= Sprites.Height; player.Animation = Player.IdleUp;    break;
									case UserKeyPress.Down:  player.J += Sprites.Height; player.Animation = Player.IdleDown;  break;
									case UserKeyPress.Left:  player.I -= Sprites.Width;  player.Animation = Player.IdleLeft;  break;
									case UserKeyPress.Right: player.I += Sprites.Width;  player.Animation = Player.IdleRight; break;
								}
								var (i2, j2) = MapBase.WorldToTile(player.I, player.J);
								Map.PerformTileAction(i2, j2);
							}
							else
							{
								switch (input)
								{
									case UserKeyPress.Up:    player.AnimationFrame = 0; player.Animation = Player.RunUp;    break;
									case UserKeyPress.Down:  player.AnimationFrame = 0; player.Animation = Player.RunDown;  break;
									case UserKeyPress.Left:  player.AnimationFrame = 0; player.Animation = Player.RunLeft;  break;
									case UserKeyPress.Right: player.AnimationFrame = 0; player.Animation = Player.RunRight; break;
								}
							}
						}
						else
						{
							player.Animation = input switch
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
					if (player.IsIdle)
					{
						var (i, j) = player.InteractTile;
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
