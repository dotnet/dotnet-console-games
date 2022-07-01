namespace Console_Monsters;

static class Inventory
{
	private const short MAX_STACK = 99;

	public static int[] ItemStorage = new int[(int)Items.Count];

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
	CaptureDevice,
	HealthPotion,
	Count, /* KEEP AT END */
}

