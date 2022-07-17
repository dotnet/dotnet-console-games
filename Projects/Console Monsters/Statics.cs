namespace Console_Monsters;

public static class Statics
{
	#region Options

	public static bool DisableMovementAnimation { get; set; } = false;
	public static bool DisableBattle { get; set; } = false;
	public static bool DisableBattleTransition { get; set; } = false;
	public static bool FirstTimeLaunching { get; set; } = true;

	#endregion

	public readonly static Random BattleTransitionRandom = new();
	public readonly static Random GameRandom = new(7);
	public readonly static Random BattleRandom = new(7);
	public readonly static Player character = new();
	public readonly static List<MonsterBase> ownedMonsters = new();
	public readonly static List<MonsterBase> partyMonsters = new();

	public static MapBase map = new PaletTown();
	public static DateTime previoiusRender = DateTime.Now;
	public static int maxPartySize = 6;
	public static bool gameRunning { get; set; } = true;
	public static bool startMenu { get; set; } = true;
	public static bool inInventory { get; set; } = false;

	public static readonly string[] defaultMaptext = new[]
	{
		"[↑, W, ←, A, ↓, S, →, D]: Move, [B]: Status, [Escape]: Menu",
	};

	public static readonly string[] defaultMaptextWithInteract = new[]
	{
		"[↑, W, ←, A, ↓, S, →, D]: Move, [B]: Status, [Escape]: Menu, [E]: Interact",
	};

	public static readonly string[] mapTextPressEnter = new string[]
	{
		"[E, Enter]: Continue, [Escape]: Menu",
	};

	public static string[] MapText
	{
		get
		{
			if (promptText is not null)
			{
				return mapTextPressEnter;
			}
			if (character.IsIdle)
			{
				var interactTile = character.InteractTile;
				if (map.CanInteractWithMapTile(interactTile.I, interactTile.J))
				{
					return defaultMaptextWithInteract;
				}
			}
			return defaultMaptext;
		}
	}

	public static readonly string[] battletext = new[]
	{
		//"[↑, W, ←, A, ↓, S, →, D]: Move Selection, [E]: Select, [Escape]: Back",
		"Battles are still in development.",
		"We are just showing two random monsters at the moment.",
		"[Enter]: exit battle"
	};

	public static string[]? promptText = null;

	public static int SelectedPlayerInventoryItem = 0;
	public static readonly Towel.DataStructures.IBag<ItemBase> PlayerInventory = Towel.DataStructures.BagMap.New<ItemBase>();

	static Statics()
	{
		character = new()
		{
			Animation = Player.IdleDown,
		};
		map = new PaletTown();
		map.SpawnCharacterOn('X');
		PlayerInventory.TryAdd(ExperienceBerries.Instance);
		PlayerInventory.TryAdd(HealthPotionLarge.Instance);
		PlayerInventory.TryAdd(HealthPotionMedium.Instance);
		PlayerInventory.TryAdd(HealthPotionSmall.Instance);
		PlayerInventory.TryAdd(MonsterBox.Instance);
		PlayerInventory.TryAdd(Mushroom.Instance);
		PlayerInventory.TryAdd(Leaf.Instance);
		PlayerInventory.TryAdd(Key.Instance);
		PlayerInventory.TryAdd(Candle.Instance);
	}

	[System.Diagnostics.DebuggerHidden]
	public static (int, int) Subtract((int, int) a, (int, int) b) => (a.Item1 - b.Item1, a.Item2 - b.Item2);
}
