﻿using System;
using System.Collections.Generic;

class Program
{
	enum Direction { Up = 0, Down = 1, Left = 2, Right = 3, }
	enum Tile { Open = 0, Snake, Food, }

	static readonly char[] DirectionChars = { '^', 'v', '<', '>', };
	static readonly TimeSpan sleep = TimeSpan.FromMilliseconds(70);
	static readonly int width = Console.WindowWidth;
	static readonly int height = Console.WindowHeight;
	static readonly Random random = new Random();
	static readonly Tile[,] map = new Tile[width, height];
	static Direction? direction = null;
	static readonly Queue<(int X, int Y)> snake = new Queue<(int X, int Y)>();
	static (int X, int Y) snakePosition = (width / 2, height / 2);
	static bool closeRequested = false;

	static void Main()
	{
		Console.CursorVisible = false;
		Console.Clear();
		snake.Enqueue(snakePosition);
		map[snakePosition.X, snakePosition.Y] = Tile.Snake;
		PositionFood();
		Console.SetCursorPosition(snakePosition.X, snakePosition.Y);
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
				case Direction.Up: snakePosition.Y--; break;
				case Direction.Down: snakePosition.Y++; break;
				case Direction.Left: snakePosition.X--; break;
				case Direction.Right: snakePosition.X++; break;
			}
			if (snakePosition.X < 0 || snakePosition.X >= width ||
				snakePosition.Y < 0 || snakePosition.Y >= height ||
				map[snakePosition.X, snakePosition.Y] == Tile.Snake)
			{
				Console.Clear();
				Console.Write("Game Over. Score: " + (snake.Count - 1) + ".");
				return;
			}
			Console.SetCursorPosition(snakePosition.X, snakePosition.Y);
			Console.Write(DirectionChars[(int)direction]);
			snake.Enqueue((snakePosition.X, snakePosition.Y));
			if (map[snakePosition.X, snakePosition.Y] == Tile.Food)
			{
				PositionFood();
			}
			else
			{
				(int X, int Y) = snake.Dequeue();
				map[X, Y] = Tile.Open;
				Console.SetCursorPosition(X, Y);
				Console.Write(' ');
			}
			map[snakePosition.X, snakePosition.Y] = Tile.Snake;
			if (Console.KeyAvailable)
			{
				GetDirection();
			}
			System.Threading.Thread.Sleep(sleep);
		}
		Console.Clear();
		Console.Write("Snake game was closed.");
	}

	static void GetDirection()
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

	static void PositionFood()
	{
		List<(int X, int Y)> possibleCoordinates = new List<(int X, int Y)>();
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (map[i, j] == Tile.Open)
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
}