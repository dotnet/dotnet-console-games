global using System;
global using System.Linq;
global using System.Text;
global using System.Threading;
global using static Console_Monsters._using;
global using Console_Monsters.Maps;
global using Console_Monsters.Monsters;
global using Console_Monsters.Bases;
global using Console_Monsters.NPCs;
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

	public static Random GameRandom = new(7);
	public static Character character = new();
	public static MapBase map = new PaletTown();
	public static DateTime previoiusRender = DateTime.Now;
	public static int maxPartySize = 6;
	public static bool gameRunning = true;
	public static bool startMenu = true;
	public static bool inventoryOpen = false;
	public static List<MonsterBase> ownedMonsters = new();
	public static List<MonsterBase> activeMonsters = new();

	public static readonly string[] defaultMaptext = new[]
	{
		"[↑, W, ←, A, ↓, S, →, D]: Move, [E]: Interact, [B]: Status, [Escape]: Menu",
	};

	public static readonly string[] mapTextPressEnter = new string[]
	{
		"[E, Enter]: Continue, [Escape]: Menu",
	};

	public static string[] MapText => promptText is null
		? defaultMaptext
		: mapTextPressEnter;

	//public static string[] mapText = defaultMaptext;

	public static readonly string[] battletext = new[]
	{
		"Battles are still in development.",
		"Press [enter] to continue...",
	};

	public static string[]? promptText = null;

	public static readonly Dictionary<Items, (string Name, string Description, string Sprite)> ItemDetails = new()
	{
		{ Items.MonsterBox, ("A Monster Box", "Used to trap and store monsters", Sprites.MonsterBox)},
		{ Items.HealthPotion,  ("A Health Potion", "Used to restore hp to monsters", Sprites.HealthPotion)},
		{ Items.XPBerries,  ("Magical XP Berries", "Used to increase a monsters experience", Sprites.XPBerries)},
	};

	static _using()
	{
		map = new PaletTown();
		var (i, j) = MapBase.FindTileInMap(map, 'X')!.Value;
		character = new()
		{
			I = i * Sprites.Width,
			J = j * Sprites.Height,
			Animation = Character.IdleDown,
		};
	}

	public static void PressEnterToContiue()
	{
	GetInput:
		ConsoleKey key = Console.ReadKey(true).Key;
		if(key == ConsoleKey.Enter)
			return;
		goto GetInput;
	}
}

#pragma warning restore CA2211
#pragma warning restore IDE1006 // Naming Styles
