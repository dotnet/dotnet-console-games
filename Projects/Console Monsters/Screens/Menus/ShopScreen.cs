namespace Console_Monsters.Screens.Menus;

public class ShopScreen
{
	public static void Render()
	{
		if (Shop is null) throw new Exception("attempting to render a null shop");
		if (PromptText is not null) throw new Exception("attempting to render shop when prompt is active");

		ItemBase.ItemCategory[] categories = Shop.Inventory.Select(row => row.Item.Category).Distinct().ToArray();
		int selection = 0;
		int category = 0;
		string[] TomsAsciiText = AsciiGenerator.ToAscii(Shop?.Name ?? "Shop");
		ShopBase.Row[] itemsInCategory = Shop.Inventory.Where(row => row.Item.Category == categories[category]).ToArray();

		while (Shop is not null)
		{
			List<string> shopText = new();

			// header
			string[]? characterSprite = Shop.Character?.Sprite.Split('\n');
			shopText.Add($"{characterSprite?[0]}");
			shopText.Add($"{characterSprite?[1]}   {TomsAsciiText[0]}");
			shopText.Add($"{characterSprite?[2]}   {TomsAsciiText[1]}");
			shopText.Add($"{characterSprite?[3]}   {TomsAsciiText[2]}");
			shopText.Add($"{characterSprite?[4]}");

			// tabs
			shopText.Add($"{new string('═', 100)}");
			shopText.Add(string.Join("    ", categories.Select(c => c == categories[category] ? (selection is 0 ? $">[{c}]<" : $"[{c}]") : c.ToString())));
			shopText.Add($"{new string('═', 100)}");

			// items
			int r = 0;
			foreach (ShopBase.Row row in itemsInCategory)
			{
				string[] sprite = row.Item.Sprite.Split('\n');

				shopText.Add($"{(r == selection - 1 ? ">" : " ")}{sprite[0]} {row.Item.Name}");
				shopText.Add($"{(r == selection - 1 ? ">" : " ")}{sprite[1]}");
				shopText.Add($"{(r == selection - 1 ? ">" : " ")}{sprite[2]} ${row.Price}");
				shopText.Add($"{(r == selection - 1 ? ">" : " ")}{sprite[3]} Stock Left: {row.Quantity}");
				shopText.Add($"{(r == selection - 1 ? ">" : " ")}{sprite[4]} {row.Item.Description}");
				shopText.Add("");
				r++;
			}

			ShopText = shopText.ToArray();
			MapScreen.Render();

			SleepAfterRender();

			while (Console.KeyAvailable)
			{
				ConsoleKey key = Console.ReadKey(true).Key;
				switch (keyMappings.GetValueOrDefault(key))
				{
					case UserKeyPress.Left:
						if (PromptShopText is not null)
						{
							break;
						}
						if (selection is 0)
						{
							category = Math.Max(0, category - 1);
							itemsInCategory = Shop.Inventory.Where(row => row.Item.Category == categories[category]).ToArray();
						}
						break;
					case UserKeyPress.Right:
						if (PromptShopText is not null)
						{
							break;
						}
						if (selection is 0)
						{
							category = Math.Min(categories.Length - 1, category + 1);
							itemsInCategory = Shop.Inventory.Where(row => row.Item.Category == categories[category]).ToArray();
						}
						break;
					case UserKeyPress.Up:
						if (PromptShopText is not null)
						{
							break;
						}
						selection = Math.Max(0, selection - 1);
						break;
					case UserKeyPress.Down:
						if (PromptShopText is not null)
						{
							break;
						}
						selection = Math.Min(itemsInCategory.Length, selection + 1);
						break;
					case UserKeyPress.Confirm:
						if (PromptShopText is not null)
						{
							PromptShopText = null;
							break;
						}
						if (selection > 0)
						{
							if (itemsInCategory[selection - 1].Quantity <= 0)
							{
								PromptShopText = new[]
								{
									"Item is out of stock."
								};
								break;
							}
							if (itemsInCategory[selection - 1].Price > player.Money)
							{
								PromptShopText = new[]
								{
									"Not enough money."
								};
								break;
							}
							player.Money -= itemsInCategory[selection - 1].Price;
							PlayerInventory.TryAdd(itemsInCategory[selection - 1].Item);
							itemsInCategory[selection - 1].Quantity--;
							PromptShopText = new[]
								{
									$"You purchased a {itemsInCategory[selection - 1].Item.Name}",
								};
							break;
						}
						break;
					case UserKeyPress.Escape:
						if (PromptShopText is not null)
						{
							PromptShopText = null;
							break;
						}
						Shop = null;
						ShopText = null;
						break;
				}
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
