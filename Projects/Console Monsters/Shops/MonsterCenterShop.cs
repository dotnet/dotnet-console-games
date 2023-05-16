namespace Console_Monsters.Shops;

internal class MonsterCenterShop : ShopBase
{
	public override string Name => "Monster Center Shop";

	private List<Row> InstanceInventory = new()
	{
		new(HealthPotionSmall.Instance, 25, 1),
		new(HealthPotionMedium.Instance, 50, 1),
		new(HealthPotionLarge.Instance, 75, 1),
		new(MonsterBox.Instance, 50, 1),
	};

	public override List<Row> Inventory => InstanceInventory;

	public override CharacterBase? Character => Scientist.Instance;

	public static readonly MonsterCenterShop Instance = new();
}
