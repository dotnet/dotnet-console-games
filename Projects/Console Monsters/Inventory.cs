namespace Console_Monsters;

static class Inventory
{
	private const short MAX_STACK = 99;

	public static int SelectedItem = 0;
	public static int[] ItemStorage = new int[(int)Items.Count];

	public static int Count
	{
		get { return GetListOfItems().Count; }
	}

	public static Items GetSelectedItem()
	{
		return GetListOfItems()[SelectedItem];
	}
	public static bool Contains(Items item)
	{
		return ItemStorage[(int)item] > 0;
	}
	public static int GetStorageCount(Items item)
	{
		return ItemStorage[(int)item];
	}
	public static bool AddToStorage(Items item)
	{
		int itemID = (int)item;
		if (ItemStorage[itemID] < MAX_STACK)
		{
			ItemStorage[itemID]++;
			return true;
		}
		return false;
	}
	public static bool RemoveFromStorage(Items item)
	{
		int itemID = (int)item;
		if (ItemStorage[itemID] > 0)
		{
			ItemStorage[itemID]--;
			return true;
		}
		return false;
	}
	public static List<Items> GetListOfItems()
	{
		List<Items> items = new();
		for (int i = 0; i < (int)Items.Count; i++)
		{
			if (ItemStorage[i] > 0)
			{
				items.Add((Items)i);
			}
		}
		return items;
	}
	public static bool IsEmpty() => ItemStorage.All(x => x is 0);
}
public enum Items // When adding Items ensure you add a description in _using.cs
{
	MonsterBox,
	HealthPotion,
	XPBerries,

	PLACEHOLDER1,
	PLACEHOLDER2,
	PLACEHOLDER3,
	PLACEHOLDER4,
	PLACEHOLDER5,
	PLACEHOLDER6,
	PLACEHOLDER7,
	PLACEHOLDER8,
	PLACEHOLDER9,
	PLACEHOLDER10,
	PLACEHOLDER11,
	PLACEHOLDER12,
	PLACEHOLDER13,

	Count, /* KEEP AT END */
}

