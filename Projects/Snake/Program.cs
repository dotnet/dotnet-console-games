using System;
using System.Collections.Generic;

char[] DirectionChars = { '^', 'v', '<', '>', };
TimeSpan sleep = TimeSpan.FromMilliseconds(70);
int width = Console.WindowWidth;
int height = Console.WindowHeight;
Random random = new Random();
Tile[,] map = new Tile[width, height];
Direction? direction = null;
Queue<(int X, int Y)> snake = new Queue<(int X, int Y)>();
(int X, int Y) = (width / 2, height / 2);
bool closeRequested = false;

try
{
	Console.CursorVisible = false;
	Console.Clear();
	snake.Enqueue((X, Y));
	map[X, Y] = Tile.Snake;
	PositionFood();
	Console.SetCursorPosition(X, Y);
	Console.Write('@');
	while (!direction.HasValue && !closeRequested)
	{
		GetDirection();
	}
	while (!closeRequested)
	{
		if (Console.WindowWidth != width || Console.WindowHeight != height)
		{
			Console.Clear();
			Console.Write("Console was resized. Snake game has ended.");
			return;
		}
		switch (direction)
		{
			case Direction.Up: Y--; break;
			case Direction.Down: Y++; break;
			case Direction.Left: X--; break;
			case Direction.Right: X++; break;
		}
		if (X < 0 || X >= width ||
			Y < 0 || Y >= height ||
			map[X, Y] is Tile.Snake)
		{
			Console.Clear();
			Console.Write("Game Over. Score: " + (snake.Count - 1) + ".");
			return;
		}
		Console.SetCursorPosition(X, Y);
		Console.Write(DirectionChars[(int)direction]);
		snake.Enqueue((X, Y));
		if (map[X, Y] == Tile.Food)
		{
			PositionFood();
		}
		else
		{
			(int x, int y) = snake.Dequeue();
			map[x, y] = Tile.Open;
			Console.SetCursorPosition(x, y);
			Console.Write(' ');
		}
		map[X, Y] = Tile.Snake;
		if (Console.KeyAvailable)
		{
			GetDirection();
		}
		System.Threading.Thread.Sleep(sleep);
	}
	Console.Clear();
}
finally
{
	Console.CursorVisible = true;
}

void GetDirection()
{
	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.UpArrow:    direction = Direction.Up; break;
		case ConsoleKey.DownArrow:  direction = Direction.Down; break;
		case ConsoleKey.LeftArrow:  direction = Direction.Left; break;
		case ConsoleKey.RightArrow: direction = Direction.Right; break;
		case ConsoleKey.Escape:     closeRequested = true; break;
	}
}

void PositionFood()
{
	List<(int X, int Y)> possibleCoordinates = new List<(int X, int Y)>();
	for (int i = 0; i < width; i++)
	{
		for (int j = 0; j < height; j++)
		{
			if (map[i, j] is Tile.Open)
			{
				possibleCoordinates.Add((i, j));
			}
		}
	}
	int index = random.Next(possibleCoordinates.Count);
	(int X, int Y) = possibleCoordinates[index];
	map[X, Y] = Tile.Food;
	Console.SetCursorPosition(X, Y);
	Console.Write('+');
}

enum Direction
{
	Up = 0,
	Down = 1,
	Left = 2,
	Right = 3,
}

enum Tile
{
	Open = 0,
	Snake,
	Food,
}
