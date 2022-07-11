global using System;
global using System.Linq;
global using System.Text;
global using System.Threading;
global using static Console_Monsters._using;
global using static Console_Monsters.BattleSystem;
global using Console_Monsters.Items;
global using Console_Monsters.Maps;
global using Console_Monsters.Monsters;
global using Console_Monsters.Bases;
global using Console_Monsters.Characters;
global using Console_Monsters.Screens;
global using Console_Monsters.Enums;
global using System.Collections.Generic;

namespace Console_Monsters;

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CA2211

public static class _using
{
	#region Options
	public static bool DisableMovementAnimation = false;
	public static bool DisableBattle = false;
	public static bool DisableBattleTransition = false;
	public static bool FirstTimeLaunching = true;
	#endregion

	public static Random BattleTransitionRandom = new();
	public static Random GameRandom = new(7);
	public static Random BattleRandom = new(7);
	public static Player character = new();
	public static MapBase map = new PaletTown();
	public static DateTime previoiusRender = DateTime.Now;
	public static int maxPartySize = 6;
	public static bool gameRunning = true;
	public static bool startMenu = true;
	public static bool inInventory = false;
	public static List<MonsterBase> ownedMonsters = new();
	public static List<MonsterBase> partyMonsters = new();

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
		"[↑, W, ←, A, ↓, S, →, D]: Move Selection, [E]: Select, [Escape]: Back",
	};

	public static string[]? promptText = null;

	public static int SelectedPlayerInventoryItem = 0;
	public static readonly Towel.DataStructures.IBag<ItemBase> PlayerInventory = Towel.DataStructures.BagMap.New<ItemBase>();

	static _using()
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

	public static void PressEnterToContiue()
	{
		while (Console.ReadKey(true).Key is not ConsoleKey.Enter)
		{
			// inentionally blank
		}
	}
}

#pragma warning restore CA2211
#pragma warning restore IDE1006 // Naming Styles
