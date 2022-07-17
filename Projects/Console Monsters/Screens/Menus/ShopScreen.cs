using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Monsters.Screens.Menus;

public class ShopScreen
{
	public static void Render()
	{
		Player player = new();
		string MonsterBoxString =
		   @"╭───╮" +
		  @" ╞═●═╡"+ 
		   @"╰───╯";


		string SpaceIndent = new(' ', 50);
		string SpaceIndent2 = new(' ', 25);
		string titleIndent = new(' ', 50);

		int arrowOption = 1;
		const int maxOption = 6;

		StringBuilder sb = new StringBuilder();

		List<(ItemBase items, int Price)> Shop = new();
		Shop.Add((HealthPotionSmall.Instance, 0));
		Shop.Add((HealthPotionMedium.Instance, 25));
		Shop.Add((HealthPotionLarge.Instance, 50));

		var itemSprite = Shop.First().items.Sprite;
		var itemName = Shop.First().items.Name;
		var itemDesc = Shop.First().items.Description;
		var itemSPrite2 = Shop[1].items.Sprite;
		var itemName2 = Shop[1].items.Name;
		var itemDesc2 = Shop[1].items.Description;
		var itemSprite3 = Shop[2].items.Sprite;
		var itemName3 = Shop[2].items.Name;
		var itemDesc3 = Shop[2].items.Description;

		string[] ShopAsciiText = AsciiGenerator.ToAscii("Shop");
		string[] ItemsAsciiText = AsciiGenerator.ToAscii("Items");
		string[] InAsciiText = AsciiGenerator.ToAscii("In");
		string[] StockAsciiText = AsciiGenerator.ToAscii("Stock");
		string[] ColonAsciiText = AsciiGenerator.ToAscii(":");
		string[] PotionsAsciiText = AsciiGenerator.ToAscii("Potions");
		string[] MonsterAsciiText = AsciiGenerator.ToAscii("Monster");
		string[] MiscAsciiText = AsciiGenerator.ToAscii("Misc");
		string[] PriceAsciiText = AsciiGenerator.ToAscii("Price");
		string[] QuantityAsciiText = AsciiGenerator.ToAscii("Quantity");
		string[] ZeroAsciiText = AsciiGenerator.ToAscii("0");

		var textAtBottom = shopTextPressEnter;

		foreach (var item in Shop)
		{
			var itemNames = item.items.Name;
		}
		Console.CursorVisible = false;

		Console.Clear();

		while (shopMenu)
		{
			ReDraw:
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine($"{titleIndent}{titleIndent}{ShopAsciiText[0]}");
			sb.AppendLine($"{titleIndent}{titleIndent}{ShopAsciiText[1]}");
			sb.AppendLine($"{titleIndent}{titleIndent}{ShopAsciiText[2]}");
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine($"  {ItemsAsciiText[0]}    {InAsciiText[0]}    {StockAsciiText[0]}  {ColonAsciiText[0]}");
			sb.AppendLine($"  {ItemsAsciiText[1]}    {InAsciiText[1]}    {StockAsciiText[1]}  {ColonAsciiText[1]}");
			sb.AppendLine($"  {ItemsAsciiText[2]}    {InAsciiText[2]}    {StockAsciiText[2]}  {ColonAsciiText[2]}");
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine();
			//sb.AppendLine($"  {PotionsAsciiText[0]}	   {MonsterAsciiText[0]}     {MiscAsciiText[0]}");
			//sb.AppendLine($"  {PotionsAsciiText[1]}    {MonsterAsciiText[1]}     {MiscAsciiText[1]}");
			//sb.AppendLine($"  {PotionsAsciiText[2]}    {MonsterAsciiText[2]}     {MiscAsciiText[2]}");
			sb.AppendLine($"{SpaceIndent2}{ItemsAsciiText[0]} {SpaceIndent}{PriceAsciiText[0]}{SpaceIndent2}  {QuantityAsciiText[0]}");
			sb.AppendLine($"{SpaceIndent2}{ItemsAsciiText[1]} {SpaceIndent}{PriceAsciiText[1]}{SpaceIndent2}  {QuantityAsciiText[1]}");
			sb.AppendLine($"{SpaceIndent2}{ItemsAsciiText[2]} {SpaceIndent}{PriceAsciiText[2]}{SpaceIndent2}  {QuantityAsciiText[2]}");
			sb.AppendLine();
			sb.AppendLine($"{itemSprite.IndentLines().IndentLines()}");
			sb.AppendLine($"");
			//sb.AppendLine($"{(arrowOption is 1 ? : "     ")}");
			sb.AppendLine($"{itemSPrite2} {itemSprite}");
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine($"{itemSprite3}");
			sb.AppendLine();
			sb.AppendLine();
			sb.AppendLine();

			Console.SetCursorPosition(0, 0);
			//sb.AppendLine($"{textAtBottom}");
			Console.WriteLine(sb);
			var keyPressed = Console.ReadKey(true).Key;
			switch (keyPressed)
			{
				case ConsoleKey.UpArrow or ConsoleKey.W: arrowOption = Math.Max(1, arrowOption - 1); goto ReDraw;
				case ConsoleKey.DownArrow or ConsoleKey.S: arrowOption = Math.Min(maxOption, arrowOption + 1); goto ReDraw;
			    case ConsoleKey.Enter:
					foreach (var item in Shop)
					{
						if (player.currentMoney >= item.Price)
						{
							player.currentMoney = player.currentMoney - item.Price;
							sb.AppendLine($"You have Purchased this item");
						}
						else
						{
							sb.AppendLine($"Sorry, You do not have enough Money to buy this");
						}
					}
					switch(arrowOption)
					{
						case 1:
							PlayerInventory.TryAdd(HealthPotionSmall.Instance);
							break;
						case 2:
							PlayerInventory.TryAdd(HealthPotionMedium.Instance);
							break;
						case 3:
							PlayerInventory.TryAdd(HealthPotionLarge.Instance);
							break;
					}
					break;
			}
			if (keyPressed == ConsoleKey.Escape)
			{
				Console.Clear();
				MapScreen.Render();
				break;
			}
		}

		
	}
}
