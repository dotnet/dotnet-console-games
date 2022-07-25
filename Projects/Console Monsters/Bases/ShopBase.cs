namespace Console_Monsters.Bases;

public abstract class ShopBase
{
	public abstract string Name { get; }

	public virtual CharacterBase? Character { get; }

	public abstract List<(ItemBase Item, int Price, int Quantity)> Inventory { get; }
}
