namespace Console_Monsters;

public static class Statics
{
	#region Options

	public static bool DisableMovementAnimation { get; set; } = false;
	public static bool DisableBattle { get; set; } = false;
	public static bool DisableBattleTransition { get; set; } = false;
	public static bool FirstTimeLaunching { get; set; } = true;
	public static bool AudioEnabled { get; set; } = false;

	#endregion

	public readonly static Random BattleTransitionRandom = new();
	public readonly static Random GameRandom = new(7);
	public readonly static Random BattleRandom = new(7);
	public readonly static Player player = new();
	public readonly static List<MonsterBase> ownedMonsters = new();
	public readonly static List<MonsterBase> partyMonsters = new();
	public readonly static Dictionary<ConsoleKey, UserKeyPress> keyMappings = new();
	public readonly static Dictionary<UserKeyPress, (ConsoleKey Main, ConsoleKey? Alternate)> reverseKeyMappings = new();

	private static MapBase map = null!;

	public static MapBase Map
	{
		get => map;
		set
		{
			map = value;
			if (map is not null && map.AudioFile is not null)
			{
				AudioController.PlaySound(map.AudioFile);
			}
			else
			{
				AudioController.StopSound();
			}
		}
	}
	public static DateTime PrevioiusRender { get; set; } = DateTime.Now;
	public const int MaxPartySize = 6;
	public static bool GameRunning { get; set; } = true;
	public static bool StartMenu { get; set; } = true;
	public static bool InInventory { get; set; } = false;

	public static ShopBase? Shop { get; set; } = null;

	public static string[] DefaultMaptext => new[]
	{
		$" [{reverseKeyMappings[UserKeyPress.Up].ToDisplayString()}]: Up" +
		$" [{reverseKeyMappings[UserKeyPress.Left].ToDisplayString()}]: Left" +
		$" [{reverseKeyMappings[UserKeyPress.Down].ToDisplayString()}]: Down" +
		$" [{reverseKeyMappings[UserKeyPress.Right].ToDisplayString()}]: Right" +
		$" [{reverseKeyMappings[UserKeyPress.Status].ToDisplayString()}]: Status" +
		$" [{reverseKeyMappings[UserKeyPress.Escape].ToDisplayString()}]: Menu",
	};

	public static string[] DefaultMaptextWithInteract => new[]
	{
		$" [{reverseKeyMappings[UserKeyPress.Up].ToDisplayString()}]: Up" +
		$" [{reverseKeyMappings[UserKeyPress.Left].ToDisplayString()}]: Left" +
		$" [{reverseKeyMappings[UserKeyPress.Down].ToDisplayString()}]: Down" +
		$" [{reverseKeyMappings[UserKeyPress.Right].ToDisplayString()}]: Right" +
		$" [{reverseKeyMappings[UserKeyPress.Status].ToDisplayString()}]: Status" +
		$" [{reverseKeyMappings[UserKeyPress.Escape].ToDisplayString()}]: Menu" +
		$" [{reverseKeyMappings[UserKeyPress.Action].ToDisplayString()}]: Action",
	};

	public static string[] MapTextPressEnter => new string[]
	{
		$" [{reverseKeyMappings[UserKeyPress.Confirm].ToDisplayString()}]: Confirm" +
		$" [{reverseKeyMappings[UserKeyPress.Action].ToDisplayString()}]: Exit Dialogue" +
		$" [{reverseKeyMappings[UserKeyPress.Escape].ToDisplayString()}]: Menu",
	};

	public static readonly string[] ShopTextPressEnter = new string[]
	{
		"[Escape]: Exit Shop, [Enter]: Buy Item",
	};

	public static string[] MapText
	{
		get
		{
			if (PromptText is not null)
			{
				return MapTextPressEnter;
			}
			if (ShopText is not null)
			{
				return ShopTextPressEnter;
			}
			if (PromptShopText is not null)
			{
				return ShopTextPressEnter;
			}
			if (player.IsIdle)
			{
				var interactTile = player.InteractTile;
				if (Map.CanInteractWithMapTile(interactTile.I, interactTile.J))
				{
					return DefaultMaptextWithInteract;
				}
			}
			return DefaultMaptext;
		}
	}

	public static string[] BattleText => new[]
	{
		$"Battles are still in development.",
		$"Let's just pretend you won this battle. :D",
		$"[{reverseKeyMappings[UserKeyPress.Confirm].ToDisplayString()}]: exit battle"
	};

	public static string[]? PromptText { get; set; } = null;
	public static string[]? ShopText { get; set; } = null;
	public static string[]? PromptShopText { get; set; } = null;


	public static int ItemDescriptionScrollFrame { get; set; } = 0;
	public static int SelectedPlayerInventoryItem { get; set; } = 0;
	public static readonly Towel.DataStructures.IBag<ItemBase> PlayerInventory = Towel.DataStructures.BagMap.New<ItemBase>();

	static Statics()
	{
		player = new()
		{
			Animation = Player.IdleDown,
		};
		PlayerInventory.TryAdd(ExperienceBerries.Instance);
		PlayerInventory.TryAdd(HealthPotionLarge.Instance);
		PlayerInventory.TryAdd(HealthPotionMedium.Instance);
		PlayerInventory.TryAdd(HealthPotionSmall.Instance);
		PlayerInventory.TryAdd(MonsterBox.Instance);
		PlayerInventory.TryAdd(Mushroom.Instance);
		PlayerInventory.TryAdd(Leaf.Instance);
		PlayerInventory.TryAdd(Key.Instance);
		PlayerInventory.TryAdd(Candle.Instance);
		DefaultKeyMappings();
		partyMonsters.Clear();
		partyMonsters.Add(new Turtle());
	}

	[System.Diagnostics.DebuggerHidden]
	public static (int, int) Subtract((int, int) a, (int, int) b) => (a.Item1 - b.Item1, a.Item2 - b.Item2);

	[System.Diagnostics.DebuggerHidden]
	public static (int, int) Modulus((int, int) a, (int?, int?) b) => (b.Item1 is null ? a.Item1 : a.Item1 % b.Item1.Value, b.Item2 is null ? a.Item2 : a.Item2 % b.Item2.Value);

	public static MonsterBase GetFirstAvailableMonster()
	{
		for(int i = 0; i < partyMonsters.Count; i++)
		{
			if(partyMonsters[i].CurrentHP > 0)
			{
				return partyMonsters[i];
			}
		}
		return new _ErrorMonster(); //TEMP: returns the error monster in case all party monsters are dead
	}

	public static bool IsAnyAvailableMonster()
	{
		for (int i = 0; i < partyMonsters.Count; i++)
		{
			if (partyMonsters[i].CurrentHP > 0)
			{
				return true;
			}
		}

		return false;
	}

	public static void DefaultKeyMappings()
	{
		reverseKeyMappings.Clear();
		reverseKeyMappings.Add(UserKeyPress.Up,      (ConsoleKey.UpArrow,    ConsoleKey.W));
		reverseKeyMappings.Add(UserKeyPress.Down,    (ConsoleKey.DownArrow,  ConsoleKey.S));
		reverseKeyMappings.Add(UserKeyPress.Left,    (ConsoleKey.LeftArrow,  ConsoleKey.A));
		reverseKeyMappings.Add(UserKeyPress.Right,   (ConsoleKey.RightArrow, ConsoleKey.D));
		reverseKeyMappings.Add(UserKeyPress.Confirm, (ConsoleKey.Enter,      null));
		reverseKeyMappings.Add(UserKeyPress.Action,  (ConsoleKey.E,          null));
		reverseKeyMappings.Add(UserKeyPress.Status,  (ConsoleKey.B,          null));
		reverseKeyMappings.Add(UserKeyPress.Escape,  (ConsoleKey.Escape,     null));
		ApplyKeyMappings();
	}

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
