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

	public readonly static Dictionary<ConsoleKey, UserKeyPress> keyMappings = new();
	public readonly static Dictionary<UserKeyPress, (ConsoleKey Main, ConsoleKey? Alternate)> reverseKeyMappings = new()
	{
		// input                main                    alternate
		{ UserKeyPress.Up,      (ConsoleKey.UpArrow,    ConsoleKey.W) },
		{ UserKeyPress.Down,    (ConsoleKey.DownArrow,  ConsoleKey.S) },
		{ UserKeyPress.Left,    (ConsoleKey.LeftArrow,  ConsoleKey.A) },
		{ UserKeyPress.Right,   (ConsoleKey.RightArrow, ConsoleKey.D) },
		{ UserKeyPress.Confirm, (ConsoleKey.Enter,      null) },
		{ UserKeyPress.Action,  (ConsoleKey.E,          null) },
		{ UserKeyPress.Status,  (ConsoleKey.B,          null) },
		{ UserKeyPress.Escape,  (ConsoleKey.Escape,     null) },
	};

	public static MapBase Map { get; set; } = new PaletTown();
	public static DateTime PrevioiusRender { get; set; } = DateTime.Now;
	public const int MaxPartySize = 6;
	public static bool GameRunning { get; set; } = true;
	public static bool StartMenu { get; set; } = true;
	public static bool InInventory { get; set; } = false;
    public static MapBase map = new PaletTown();
	public static DateTime previoiusRender = DateTime.Now;
	public static int maxPartySize = 6;
	public static bool gameRunning { get; set; } = true;
	public static bool startMenu { get; set; } = true;
	public static bool inInventory { get; set; } = false;
	public static bool shopMenu { get; set; } = true;


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

	public static readonly string[] shopTextPressEnter = new string[]
	{
		"[Escape]: Exit Shop",
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
				if (Map.CanInteractWithMapTile(interactTile.I, interactTile.J))
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
		Map = new PaletTown();
		Map.SpawnCharacterOn('X');
		PlayerInventory.TryAdd(ExperienceBerries.Instance);
		PlayerInventory.TryAdd(HealthPotionLarge.Instance);
		PlayerInventory.TryAdd(HealthPotionMedium.Instance);
		PlayerInventory.TryAdd(HealthPotionSmall.Instance);
		PlayerInventory.TryAdd(MonsterBox.Instance);
		PlayerInventory.TryAdd(Mushroom.Instance);
		PlayerInventory.TryAdd(Leaf.Instance);
		PlayerInventory.TryAdd(Key.Instance);
		PlayerInventory.TryAdd(Candle.Instance);
		ApplyKeyMappings();
	}

	[System.Diagnostics.DebuggerHidden]
	public static (int, int) Subtract((int, int) a, (int, int) b) => (a.Item1 - b.Item1, a.Item2 - b.Item2);

	public static void ApplyKeyMappings()
	{
		keyMappings.Clear();
		foreach (var pair in reverseKeyMappings)
		{
			keyMappings.Add(pair.Value.Main, pair.Key);
			if (pair.Value.Alternate is not null)
			{
				keyMappings.Add(pair.Value.Alternate.Value, pair.Key);
			}
		}
	}
}
