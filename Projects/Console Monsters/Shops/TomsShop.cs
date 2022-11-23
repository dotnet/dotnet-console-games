namespace Console_Monsters.Shops;

internal class TomsShop : ShopBase
{
	public override string Name => "Tom's Shop";

	private List<Row> InstanceInventory = new()
	{
		new(Mushroom.Instance, 25, 3),
		new(Leaf.Instance, 50, 1),
		new(Candle.Instance, 20, 20),
	};

	public override List<Row> Inventory => InstanceInventory;

	public override CharacterBase? Character => OldMan.Instance;

	public static readonly TomsShop Instance = new();
}
