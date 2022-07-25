namespace Console_Monsters.Bases;

public abstract class ShopBase
{
	public abstract string Name { get; }

	public virtual CharacterBase? Character { get; }

	public abstract List<Row> Inventory { get; }

	public class Row
	{
		public ItemBase Item { get; private set; }
		public int Price { get; private set; }
		public int Quantity { get; set; }

		public Row(ItemBase item, int price, int quantity)
		{
			Item = item;
			Price = price;
			Quantity = quantity;
		}
	}
}
