namespace Console_Monsters.Shops;

internal class TomsShop : ShopBase
{
	public override string Name => "Tom's Shop";

	public override List<(ItemBase Item, int Price, int Quantity)> Inventory => new()
	{
		(HealthPotionSmall.Instance, 10, 5),
		(HealthPotionMedium.Instance, 25, 3),
		(HealthPotionLarge.Instance, 50, 1),
		(MonsterBox.Instance, 20, 20),
	};

	public override CharacterBase? Character => OldMan.Instance;

	public static readonly TomsShop Instance = new();
}
