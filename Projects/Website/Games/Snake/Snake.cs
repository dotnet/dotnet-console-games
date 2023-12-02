using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Website.Games.Snake;

public class Snake
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		Exception? exception = null;
		int speedInput;
		string prompt = $"Select speed [1], [2] (default), or [3]: ";
		string? input;
		await Console.Write(prompt);
		while (!int.TryParse(input = await Console.ReadLine(), out speedInput) || speedInput < 1 || 3 < speedInput)
		{
			if (string.IsNullOrWhiteSpace(input))
			{
				speedInput = 2;
				break;
			}
			else
			{
				await Console.WriteLine("Invalid Input. Try Again...");
				await Console.Write(prompt);
			}
		}
		int[] velocities = { 50, 35, 20 };
		int velocity = velocities[speedInput - 1];
		char[] DirectionChars = { '^', 'v', '<', '>', };
		TimeSpan sleep = TimeSpan.FromMilliseconds(velocity);
		int width = Console.WindowWidth;
		int height = Console.WindowHeight;
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
				if (map[X, Y] is Tile.Food)
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
				await Console.RefreshAndDelay(sleep);
			}
		}
		catch (Exception e)
		{
			exception = e;
			throw;
		}
		finally
		{
			Console.CursorVisible = true;
			await Console.Clear();
			await Console.WriteLine(exception?.ToString() ?? "Snake was closed.");
			await Console.Refresh();
		}

		async Task GetDirection()
		{
			switch ((await Console.ReadKey(true)).Key)
			{
				case ConsoleKey.UpArrow: direction = Direction.Up; break;
				case ConsoleKey.DownArrow: direction = Direction.Down; break;
				case ConsoleKey.LeftArrow: direction = Direction.Left; break;
				case ConsoleKey.RightArrow: direction = Direction.Right; break;
				case ConsoleKey.Escape: closeRequested = true; break;
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
			int index = Random.Shared.Next(possibleCoordinates.Count);
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
