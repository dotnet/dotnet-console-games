﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Towel;

namespace Website.Games.Wumpus_World;

public class Wumpus_World
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		bool escape = false;

		(int X, int Y) PlayerLocation;
		Tile[,] Map;

		bool InvalidInput = false;
		Home:
		if (escape)
		{
			await Console.WriteLine("Wumpus World was closed.");
			await Console.Refresh();
			return;
		}
		await Console.Clear();
		await Console.WriteLine("Wumpus World...");
		await Console.WriteLine();
		await Console.WriteLine("Home:");
		await Console.WriteLine();
		await Console.WriteLine(
			"Rumors say that the Wumpus has a stash of gold " +
			"inside his cave, but few who enter ever return. " +
			"Will you seek the gold?");
		await Console.WriteLine();
		await Console.WriteLine("yes:  enter the cave of the Wumpus");
		await Console.WriteLine("quit: exit Wumpus World");
		await Console.WriteLine("info: view info");
		await Console.WriteLine();
		if (InvalidInput)
		{
			await Console.WriteLine("Invalid Input. Try again...");
			InvalidInput = false;
		}
		await Console.Write(">");
		switch (await Console.ReadLine())
		{
			case "quit": await Quit(); goto Home;
			case "info": await Info(); goto Home;
			case "yes": GenerateCave(); await Play(); goto Home;
			default: InvalidInput = true; goto Home;
		};

		async Task Info()
		{
			await Console.Clear();
			await Console.WriteLine("Wumpus World...");
			await Console.WriteLine();
			await Console.WriteLine("Rules:");
			await Console.WriteLine();
			await Console.WriteLine(
				"The Wumpus's cave is a 4x4 grid. It is dark, and you " +
				"can only see the reach of your hands. The Wumpus will " +
				"eat you if you disturb him, but you can smell him " +
				"when he is close. The Wumpus's cave is also full of pits " +
				"he uses to trap his prey. Be careful not to fall in one; " +
				"you can feel a breeze when a pit is near. If you find the " +
				"gold, you may exit the way you came.");
			await Console.WriteLine();
			await Console.WriteLine("Press Enter To Return...");
			await Console.ReadLine();
		}

		async Task Quit()
		{
			bool InvalidInput = false;
		Quit:
			await Console.Clear();
			await Console.WriteLine("Wumpus World...");
			await Console.WriteLine();
			await Console.WriteLine("Quit:");
			await Console.WriteLine();
			await Console.WriteLine("Are you sure you want to quit?");
			await Console.WriteLine();
			await Console.WriteLine("yes: quit");
			await Console.WriteLine("no:  return");
			await Console.WriteLine();
			if (InvalidInput)
			{
				await Console.WriteLine("Invalid Input. Try again...");
			}
			await Console.Write(">");
			switch (await Console.ReadLine())
			{
				case "yes": await Console.Clear(); escape = true; return;
				case "no": return;
				default: InvalidInput = true; goto Quit;
			};
		}

		async Task Play()
		{
			IEnumerable<Tile> AdjacentTiles()
			{
				int x = PlayerLocation.X;
				int y = PlayerLocation.Y;
				if (x > 0) yield return Map[x - 1, y];
				if (x < Map.GetLength(0) - 1) yield return Map[x + 1, y];
				if (y > 0) yield return Map[x, y - 1];
				if (y < Map.GetLength(1) - 1) yield return Map[x, y + 1];
			}

			bool AdjacentToWumpus() => AdjacentTiles().Contains(Tile.Wumpus);
			bool AdjacentToPit() => AdjacentTiles().Contains(Tile.Pit);

			bool InvalidInput = false;
			string? move = null;
		Play:
			if (escape)
			{
				return;
			}
			await Console.Clear();
			await Console.WriteLine("Wumpus World...");
			await Console.WriteLine();
			await Console.WriteLine("Play:");
			await Console.WriteLine();
			if (move is not null)
			{
				await Console.WriteLine(move);
				await Console.WriteLine();
			}
			await Console.Write("You are inside the cave of the Wumpus.");
			if (AdjacentToWumpus())
			{
				await Console.Write(" You smell a foul odor from something nearby.");
			}
			if (AdjacentToPit())
			{
				await Console.Write(" You feel a breeze. Watch your step.");
			}
			await Console.WriteLine();
			await Console.WriteLine();
			await Console.WriteLine("up:    move up");
			await Console.WriteLine("down:  move down");
			await Console.WriteLine("left:  move left");
			await Console.WriteLine("right: move right");
			await Console.WriteLine("quit:  exit Wumpus World");
			await Console.WriteLine("info:  view info");
			await Console.WriteLine();
			if (InvalidInput)
			{
				await Console.WriteLine("Invalid Input. Try again...");
				InvalidInput = false;
			}
			await Console.Write(">");
			Direction movement;
			switch (await Console.ReadLine())
			{
				case "quit": await Quit(); goto Play;
				case "info": await Info(); goto Play;
				case "up": movement = Direction.Up; break;
				case "down": movement = Direction.Down; break;
				case "left": movement = Direction.Left; break;
				case "right": movement = Direction.Right; break;
				default: InvalidInput = true; goto Play;
			};
			bool insideMap = movement switch
			{
				Direction.Up => PlayerLocation.Y < Map.GetLength(1) - 1,
				Direction.Down => PlayerLocation.Y > 0,
				Direction.Left => PlayerLocation.X > 0,
				Direction.Right => PlayerLocation.X < Map.GetLength(0) - 1,
				_ => throw new Exception(),
			};
			move = insideMap
				? "You moved " + movement.ToString().ToLower() + "."
				: "You tried to move " + movement.ToString().ToLower() + " but you ran into a wall.";
			if (insideMap)
			{
				PlayerLocation = movement switch
				{
					Direction.Up => (PlayerLocation.X, PlayerLocation.Y + 1),
					Direction.Down => (PlayerLocation.X, PlayerLocation.Y - 1),
					Direction.Left => (PlayerLocation.X - 1, PlayerLocation.Y),
					Direction.Right => (PlayerLocation.X + 1, PlayerLocation.Y),
					_ => throw new Exception(),
				};
				switch (Map[PlayerLocation.X, PlayerLocation.Y])
				{
					case Tile.Gold: await Console.Clear(); await Console.WriteLine("You found the gold and left the cave the way you entered. You win."); await Console.ReadLine(); return;
					case Tile.Wumpus: await Console.Clear(); await Console.WriteLine("You got eaten by the Wumpus. You lose."); await Console.ReadLine(); return;
					case Tile.Pit: await Console.Clear(); await Console.WriteLine("You fell into one of the Wumpus's pits. You lose."); await Console.ReadLine(); return;
				}
			}
			goto Play;
		}

		void GenerateCave()
		{
			const int width = 4;
			const int height = 4;
			Map = new Tile[width, height];
			// Get Random Locations
			Random random = new();
			(int X, int Y)[] randomCoordinates = random.NextUnique(5, 1, (width * height)).Select(i => (i / width, i % width)).ToArray();
			var wumpusLocation = randomCoordinates[0];
			var goldLocation = randomCoordinates[1];
			var pitLocations = randomCoordinates[2..^0];
			// Place Randomized Locations On Map
			Map[wumpusLocation.X, wumpusLocation.Y] = Tile.Wumpus;
			Map[goldLocation.X, goldLocation.Y] = Tile.Gold;
			Array.ForEach(pitLocations, pit => Map[pit.X, pit.Y] = Tile.Pit);
			// Default Player Location
			PlayerLocation = default;
		}
	}

	enum Direction
	{
		Up,
		Down,
		Left,
		Right,
	}

	enum Tile
	{
		Normal = 0,
		Wumpus,
		Gold,
		Pit,
	}
}
