global using System;
global using System.Linq;
global using System.Text;
global using static Animal_Trainer._using;
global using Animal_Trainer.Maps;
global using Animal_Trainer.Animals;
global using System.Collections.Generic;
global using System.Threading.Tasks;

namespace Animal_Trainer;

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CA2211

public static class _using
{
	public static Character character = new();
	public static Map map = new PaletTown();
	public static DateTime previoiusRender = DateTime.Now;
	public static bool gameRunning = true;
	public static List<Animal> ownedAnimals = new();

	public static readonly string[] maptext = new[]
	{
		"Move: ← ↑ → ↓ / W A S D",
		"Interact: E",
		"Animal Status: [Enter]",
		"Inventory: [Backspace]",
		"Quit: [Escape]",
	};

	static _using()
	{
		map = new PaletTown();
		var (i, j) = Map.FindTileInMap(map, 'X')!.Value;
		character = new()
		{
			I = i * Sprites.Width,
			J = j * Sprites.Height,
			Animation = Sprites.IdlePlayer,
		};
	}

	public static void PressEnterToContiue()
	{
	GetInput:
		ConsoleKey key = Console.ReadKey(true).Key;
		switch (key)
		{
			case ConsoleKey.Enter: return;
			case ConsoleKey.Escape: gameRunning = false; return;
			default: goto GetInput;
		}
	}
}

#pragma warning restore CA2211
#pragma warning restore IDE1006 // Naming Styles
