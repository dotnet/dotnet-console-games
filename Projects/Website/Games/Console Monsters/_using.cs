using System;
using System.Linq;
using System.Text;
using System.Threading;
using static Website.Games.Console_Monsters._using;
using Website.Games.Console_Monsters.Maps;
using Website.Games.Console_Monsters.Monsters;
using Website.Games.Console_Monsters.Bases;
using Website.Games.Console_Monsters.NPCs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters;

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CA2211

public static class _using
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

	public static BlazorConsole Console;
	public static BlazorConsole OperatingSystem;

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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
		{ Items.HealthPotionLarge,  ("A Large Health Potion", "Used to restore hp to monsters", Sprites.HealthPotionLarge)},
		{ Items.HealthPotionMedium,  ("A Medium Health Potion", "Used to restore hp to monsters", Sprites.HealthPotionMedium)},
		{ Items.HealthPotionSmall,  ("A Small Health Potion", "Used to restore hp to monsters", Sprites.HealthPotionSmall)},
		{ Items.XPBerries,  ("Magical XP Berries", "Used to increase a monsters experience", Sprites.XPBerries)},
	};

#pragma warning disable CS8618

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

#pragma warning restore CS8618

	public static async Task PressEnterToContiue()
	{
		while ((await Console.ReadKey(true)).Key is not ConsoleKey.Enter)
		{
			// inentionally blank
		}
	}
}

#pragma warning restore CA2211
#pragma warning restore IDE1006 // Naming Styles
