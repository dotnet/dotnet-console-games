﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Console = Website.Console<Website.Games.Snake.Snake>;

namespace Website.Games.Snake;

public class Snake
{
	public static async Task Run()
	{
char[] DirectionChars = { '^', 'v', '<', '>', };
TimeSpan sleep = TimeSpan.FromMilliseconds(70);
int width = Console.WindowWidth;
int height = Console.WindowHeight;
Random random = new();
Tile[,] map = new Tile[width, height];
Direction? direction = null;
Queue<(int X, int Y)> snake = new();
(int X, int Y) = (width / 2, height / 2);
bool closeRequested = false;

try
{
	Console.CursorVisible = false;
	await Console.Clear();
	snake.Enqueue((X, Y));
	map[X, Y] = Tile.Snake;
	await PositionFood();
	await Console.SetCursorPosition(X, Y);
	await Console.Write('@');
	while (!direction.HasValue && !closeRequested)
	{
		await GetDirection();
	}
	while (!closeRequested)
	{
		if (Console.WindowWidth != width || Console.WindowHeight != height)
		{
			await Console.Clear();
			await Console.Write("Console was resized. Snake game has ended.");
			await Console.Refresh();
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
			await Console.Clear();
			await Console.Write("Game Over. Score: " + (snake.Count - 1) + ".");
			await Console.Refresh();
			return;
		}
		await Console.SetCursorPosition(X, Y);
		await Console.Write(DirectionChars[(int)direction!]);
		snake.Enqueue((X, Y));
		if (map[X, Y] == Tile.Food)
		{
			await PositionFood();
		}
		else
		{
			(int x, int y) = snake.Dequeue();
			map[x, y] = Tile.Open;
			await Console.SetCursorPosition(x, y);
			await Console.Write(' ');
		}
		map[X, Y] = Tile.Snake;
		if (await Console.KeyAvailable())
		{
			await GetDirection();
		}
		System.Threading.Thread.Sleep(sleep);
	}
	await Console.Clear();
	await Console.Refresh();
}
finally
{
	Console.CursorVisible = true;
	await Console.Refresh();
}

async Task GetDirection()
{
	switch ((await Console.ReadKey(true)).Key)
	{
		case ConsoleKey.UpArrow:    direction = Direction.Up; break;
		case ConsoleKey.DownArrow:  direction = Direction.Down; break;
		case ConsoleKey.LeftArrow:  direction = Direction.Left; break;
		case ConsoleKey.RightArrow: direction = Direction.Right; break;
		case ConsoleKey.Escape:     closeRequested = true; break;
	}
}

async Task PositionFood()
{
	List<(int X, int Y)> possibleCoordinates = new();
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
	await Console.SetCursorPosition(X, Y);
	await Console.Write('+');
}
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
}
