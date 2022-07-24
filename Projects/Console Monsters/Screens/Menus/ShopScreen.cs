using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Monsters.Screens.Menus;

public class ShopScreen
{
	 static int quantity1 { get; set; } = Random.Shared.Next(1, 10);
	 static int quantity2 { get; set; } = Random.Shared.Next(1, 10);
	 static int quantity3 { get; set; } = Random.Shared.Next(1, 10);
	public static void Render()
	{
		string SpaceIndent = new(' ', 50);
		string SpaceIndent3 = new(' ', 20);
		string SpaceIndent4 = new(' ', 15);
		string SpaceIndent2 = new(' ', 25);
		string titleIndent = new(' ', 50);

		int arrowOption = 1;

		List<(ItemBase items, int Price)> Shop = new();
		Shop.Add((HealthPotionSmall.Instance, 10));
		Shop.Add((HealthPotionMedium.Instance, 25));
		Shop.Add((HealthPotionLarge.Instance, 50));

		var itemSprite = Shop.First().items.Sprite.Split('\n');
		var itemName = Shop.First().items.Name;
		var itemDesc = Shop.First().items.Description;
		var itemSprite2 = Shop[1].items.Sprite.Split('\n');
		var itemName2 = Shop[1].items.Name;
		var itemDesc2 = Shop[1].items.Description;
		var itemSprite3 = Shop[2].items.Sprite.Split('\n');
		var itemName3 = Shop[2].items.Name;
		var itemDesc3 = Shop[2].items.Description;

		string[] ShopAsciiText = AsciiGenerator.ToAscii("Shop");

	Redraw:

		ShopText = new string[]
		{
			$"{SpaceIndent2}     {ShopAsciiText[0]}",
			$"{SpaceIndent2}     {ShopAsciiText[1]}",
			$"{SpaceIndent2}     {ShopAsciiText[2]}",
			"",
			"",
			$"{itemSprite[0]} {itemName}",
			$"{itemSprite[1]}",
			$"{itemSprite[2]} ${Shop[0].Price} {(arrowOption is 1 ? $"{SpaceIndent}<<Current" : "         ")}",
			$"{itemSprite[3]} Stock Left: {quantity1}",
			$"{itemDesc}",
			"",
			$"{itemSprite2[0]}{itemName2}",
			$"{itemSprite2[1]}",
			$"{itemSprite2[2]} ${Shop[1].Price}{(arrowOption is 2 ? $"{SpaceIndent}<<Current" : "         ")}",
			$"{itemSprite2[3]} Stock Left: {quantity2}",
			$" {itemDesc2}",
			"",
			$"{itemSprite3[0]}{itemName3}",
			$"{itemSprite3[1]}",
			$"{itemSprite3[2]} ${Shop[2].Price}{(arrowOption is 3 ? $"{SpaceIndent}<<Current" : "         ")}",
			$"{itemSprite3[3]} Stock Left: {quantity3}",
			$"{itemSprite3[4]}",
			$" {itemDesc3}"
		};
		MapScreen.Render();
		PromptText = null;

		var keyPressed = Console.ReadKey(true).Key;

		switch (keyMappings.GetValueOrDefault(keyPressed))
		{
			case UserKeyPress.Up: arrowOption = Math.Min(3, arrowOption - 1); goto Redraw;
			case UserKeyPress.Down: arrowOption = Math.Max(1, arrowOption + 1); goto Redraw;
			case UserKeyPress.Confirm:
				switch (arrowOption)
				{
					case 1:
						if (Player.currentMoney >= Shop[0].Price && quantity1 > 0)
						{
							quantity1--;

							Player.currentMoney -= Shop[0].Price;

							PlayerInventory.TryAdd(HealthPotionSmall.Instance);
							PromptText = new string[]
							{
							"Purchase Complete"
							};
						}
						else if (Player.currentMoney <= Shop[0].Price)
						{
							PromptText = new string[]
							{
							"Sorry Not enough money"
							};
						}
						else if (quantity1 < 0)
						{
							PromptText = new string[]
							{
							"Item Out of Stock"
							};
						}
						goto Redraw;
					case 2:
						if (Player.currentMoney >= Shop[1].Price && quantity2 > 0)
						{
							quantity2--;
							Player.currentMoney -= Shop[1].Price;

							PlayerInventory.TryAdd(HealthPotionSmall.Instance);
							PromptText = new string[]
							{
							"Purchase Complete"
							};
						}
						else if (Player.currentMoney <= Shop[1].Price)
						{
							PromptText = new string[]
							{
							"Sorry Not enough money"
							};
						}
						else if (quantity2 <= 0)
						{
							PromptText = new string[]
							{
							"Item Out of Stock"
							};
						}
						goto Redraw;
					case 3:
						if (Player.currentMoney >= Shop[2].Price && quantity3 > 0)
						{
							quantity3--;
							Player.currentMoney -= Shop[2].Price;

							PlayerInventory.TryAdd(HealthPotionSmall.Instance);
							PromptText = new string[]
							{
							"Purchase Complete"
							};
						}
						else if (Player.currentMoney <= Shop[2].Price)
						{
							PromptText = new string[]
							{
							"Sorry Not enough money"
							};
						}
						else if (quantity3 <= 0)
						{
							PromptText = new string[]
							{
							"Item Out of Stock"
							};
						}
						goto Redraw;
				}
				goto Redraw;
			case UserKeyPress.Escape: ShopText = null; break;
			case UserKeyPress.Action: goto Redraw;
		}
	}
}
