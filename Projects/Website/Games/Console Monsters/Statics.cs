using System;
//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Items;
using Website.Games.Console_Monsters.Maps;
using Website.Games.Console_Monsters.Bases;
using System.Collections.Generic;

namespace Website.Games.Console_Monsters;

#pragma warning disable CA2211 // Non-constant fields should not be visible

public static class Statics
{

	public static BlazorConsole Console = null!;
	public static BlazorConsole OperatingSystem = null!;

	#region Options

	public static bool DisableMovementAnimation { get; set; } = false;
	public static bool DisableBattle { get; set; } = false;
	public static bool DisableBattleTransition { get; set; } = true;
	public static bool FirstTimeLaunching { get; set; } = true;

	#endregion

	public readonly static Random BattleTransitionRandom = new();
	public readonly static Random GameRandom = new(7);
	public readonly static Random BattleRandom = new(7);
	public readonly static Player character = new();
	public readonly static List<MonsterBase> ownedMonsters = new();
	public readonly static List<MonsterBase> partyMonsters = new();

	public static MapBase Map = new PaletTown();
	public static DateTime PrevioiusRender = DateTime.Now;
	public static int MaxPartySize = 6;
	public static bool GameRunning { get; set; } = true;
	public static bool StartMenu { get; set; } = true;
	public static bool InInventory { get; set; } = false;

	public static readonly string[] defaultMaptext =
	[
		"[↑, W, ←, A, ↓, S, →, D]: Move, [B]: Status, [Escape]: Menu",
	];

	public static readonly string[] defaultMaptextWithInteract =
	[
		"[↑, W, ←, A, ↓, S, →, D]: Move, [B]: Status, [Escape]: Menu, [E]: Interact",
	];

	public static readonly string[] mapTextPressEnter =
	[
		"[E, Enter]: Continue, [Escape]: Menu",
	];

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

	public static readonly string[] battletext =
	[
		//"[↑, W, ←, A, ↓, S, →, D]: Move Selection, [E]: Select, [Escape]: Back",
		"Battles are still in development.",
		"We are just showing two random monsters at the moment.",
		"[Enter]: exit battle"
	];

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
	}

	[System.Diagnostics.DebuggerHidden]
	public static (int, int) Subtract((int, int) a, (int, int) b) => (a.Item1 - b.Item1, a.Item2 - b.Item2);
}

#pragma warning restore CA2211 // Non-constant fields should not be visible
