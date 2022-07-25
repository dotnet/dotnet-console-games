namespace Console_Monsters.Shops;

internal class TomsShop : ShopBase
{
	public override string Name => "Tom's Shop";

	private List<Row> InstanceInventory = new()
	{
		new(HealthPotionSmall.Instance, 10, 5),
		new(HealthPotionMedium.Instance, 25, 3),
		new(HealthPotionLarge.Instance, 50, 1),
		new(MonsterBox.Instance, 20, 20),
	};

	public override List<Row> Inventory => InstanceInventory;

	public override CharacterBase? Character => OldMan.Instance;

	public static readonly TomsShop Instance = new();
}
