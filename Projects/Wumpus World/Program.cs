using System;
using System.Collections.Generic;
using System.Linq;
using Towel;

(int X, int Y) PlayerLocation;
Tile[,] Map;

bool InvalidInput = false;
Home:
Console.Clear();
Console.WriteLine("Wumpus World...");
Console.WriteLine();
Console.WriteLine("Home:");
Console.WriteLine();
Console.WriteLine(
	"Rumors say that the Wumpus has a stash of gold " +
	"inside his cave, but few who enter ever return. " +
	"Will you seek the gold?");
Console.WriteLine();
Console.WriteLine("yes:  enter the cave of the Wumpus");
Console.WriteLine("quit: exit Wumpus World");
Console.WriteLine("info: view info");
Console.WriteLine();
if (InvalidInput)
{
	Console.WriteLine("Invalid Input. Try again...");
	InvalidInput = false;
}
Console.Write(">");
switch (Console.ReadLine())
{
	case "quit": Quit(); goto Home;
	case "info": Info(); goto Home;
	case "yes": GenerateCave(); Play(); goto Home;
	default: InvalidInput = true; goto Home;
};

void Info()
{
	Console.Clear();
	Console.WriteLine("Wumpus World...");
	Console.WriteLine();
	Console.WriteLine("Rules:");
	Console.WriteLine();
	Console.WriteLine(
		"The Wumpus's cave is a 4x4 grid. It is dark, and you " +
		"can only see the reach of your hands. The Wumpus will " +
		"eat you if you disturb him, but you can smell him " +
		"when he is close. The Wumpus's cave is also full of pits " +
		"he uses to trap his prey. Be careful not to fall in one; " +
		"you can feel a breeze when a pit is near. If you find the " +
		"gold, you may exit the way you came.");
	Console.WriteLine();
	Console.WriteLine("Press Enter To Return...");
	Console.ReadLine();
}

void Quit()
{
	bool InvalidInput = false;
Quit:
	Console.Clear();
	Console.WriteLine("Wumpus World...");
	Console.WriteLine();
	Console.WriteLine("Quit:");
	Console.WriteLine();
	Console.WriteLine("Are you sure you want to quit?");
	Console.WriteLine();
	Console.WriteLine("yes: quit");
	Console.WriteLine("no:  return");
	Console.WriteLine();
	if (InvalidInput)
	{
		Console.WriteLine("Invalid Input. Try again...");
	}
	Console.Write(">");
	switch (Console.ReadLine())
	{
		case "yes": Console.Clear(); Environment.Exit(0); return;
		case "no": return;
		default: InvalidInput = true; goto Quit;
	};
}

void Play()
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
	Console.Clear();
	Console.WriteLine("Wumpus World...");
	Console.WriteLine();
	Console.WriteLine("Play:");
	Console.WriteLine();
	if (move is not null)
	{
		Console.WriteLine(move);
		Console.WriteLine();
	}
	Console.Write("You are inside the cave of the Wumpus.");
	if (AdjacentToWumpus())
	{
		Console.Write(" You smell a foul odor from something nearby.");
	}
	if (AdjacentToPit())
	{
		Console.Write(" You feel a breeze. Watch your step.");
	}
	Console.WriteLine();
	Console.WriteLine();
	Console.WriteLine("up:    move up");
	Console.WriteLine("down:  move down");
	Console.WriteLine("left:  move left");
	Console.WriteLine("right: move right");
	Console.WriteLine("quit:  exit Wumpus World");
	Console.WriteLine("info:  view info");
	Console.WriteLine();
	if (InvalidInput)
	{
		Console.WriteLine("Invalid Input. Try again...");
		InvalidInput = false;
	}
	Console.Write(">");
	Direction movement;
	switch (Console.ReadLine())
	{
		case "quit": Quit(); goto Play;
		case "info": Info(); goto Play;
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
			case Tile.Gold: Console.Clear(); Console.WriteLine("You found the gold and left the cave the way you entered. You win."); Console.ReadLine(); return;
			case Tile.Wumpus: Console.Clear(); Console.WriteLine("You got eaten by the Wumpus. You lose."); Console.ReadLine(); return;
			case Tile.Pit: Console.Clear(); Console.WriteLine("You fell into one of the Wumpus's pits. You lose."); Console.ReadLine(); return;
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
	(int X, int Y)[] randomCoordinates = Random.Shared.NextUnique(5, 1, (width * height)).Select(i => (i / width, i % width)).ToArray();
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
