using System;
using System.Collections.Generic;
using System.Text;

class Program
{
	const int mine = -1;
	static readonly int height = Console.WindowHeight;
	static readonly int width = Console.WindowWidth;
	static readonly Random random = new Random();
	static readonly float mineRatio = .1f;
	static (int Value, bool Visible)[,] board;
	static (int Column, int Row) position = (width / 2, height / 2);

	static void Main()
	{
		GenerateBoard();
		RenderBoard();
		while (true)
		{
			if (Console.WindowHeight != height || Console.WindowWidth != width)
			{
				Console.Clear();
				Console.Write("Console resized. Minesweeper was closed.");
				return;
			}
			Console.SetCursorPosition(position.Column, position.Row);
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.UpArrow:
					position.Row = Math.Max(position.Row - 1, 0);
					break;
				case ConsoleKey.DownArrow:
					position.Row = Math.Min(position.Row + 1, height - 1);
					break;
				case ConsoleKey.LeftArrow:
					position.Column = Math.Max(position.Column - 1, 0);
					break;
				case ConsoleKey.RightArrow:
					position.Column = Math.Min(position.Column + 1, width - 1);
					break;
				case ConsoleKey.Enter:
					if (!board[position.Column, position.Row].Visible)
					{
						if (board[position.Column, position.Row].Value == mine)
						{
							for (int column = 0; column < width; column++)
							{
								for (int row = 0; row < height; row++)
								{
									board[column, row].Visible = true;
								}
							}
							RenderBoard();
							Console.SetCursorPosition(0, height - 1);
							Console.Write("You Lose. Press Enter To Exit...");
							Console.ReadLine();
							Console.Clear();
							return;
						}
						else if (board[position.Column, position.Row].Value == 0)
						{
							Reveal(position.Column, position.Row);
							RenderBoard();
						}
						else
						{
							board[position.Column, position.Row].Visible = true;
							RenderBoard();
						}
						bool allVisible = true;
						for (int column = 0; column < width && allVisible; column++)
						{
							for (int row = 0; row < height && allVisible; row++)
							{
								if (!board[column, row].Visible)
								{
									allVisible = false;
								}
							}
						}
						if (allVisible)
						{
							Console.SetCursorPosition(0, height - 1);
							Console.Write("You Win. Press Enter To Exit...");
							Console.ReadLine();
							Console.Clear();
							return;
						}
					}
					break;
				case ConsoleKey.Escape:
					Console.Clear();
					Console.Write("Minesweeper was closed.");
					return;
			}
		}
	}

	static IEnumerable<(int Row, int Column)> AdjacentTiles(int column, int row)
	{
		//    A B C
		//    D + E
		//    F G H
		
		/* A */ if (row > 0 && column > 0) yield return (row - 1, column - 1);
		/* B */ if (row > 0) yield return (row - 1, column);
		/* C */ if (row > 0 && column < width - 1) yield return (row - 1, column + 1);
		/* D */ if (column > 0) yield return (row, column - 1);
		/* E */ if (column < width - 1) yield return (row, column + 1);
		/* F */ if (row < height - 1 && column > 0) yield return (row + 1, column - 1);
		/* G */ if (row < height - 1) yield return (row + 1, column);
		/* H */ if (row < height - 1 && column < width - 1) yield return (row + 1, column + 1);
	}

	static void GenerateBoard()
	{
		board = new (int Value, bool Visible)[width, height];
		int mineCount = (int)(width * height * mineRatio);
		var coordinates = new List<(int Row, int Column)>();
		for (int column = 0; column < width; column++)
		{
			for (int row = 0; row < height; row++)
			{
				coordinates.Add((column, row));
			}
		}
		for (int i = 0; i < mineCount; i++)
		{
			int randomIndex = random.Next(0, coordinates.Count);
			(int Column, int Row) = coordinates[randomIndex];
			coordinates.RemoveAt(randomIndex);
			board[Column, Row] = (mine, false);
			foreach (var tile in AdjacentTiles(Column, Row))
			{
				if (board[tile.Column, tile.Row].Value != mine)
				{
					board[tile.Column, tile.Row].Value++;
				}
			}
		}
	}

	static char Render(int value) => value switch
	{
		mine => '@',
		0 => ' ',
		1 => '1',
		2 => '2',
		3 => '3',
		4 => '4',
		5 => '5',
		6 => '6',
		7 => '7',
		8 => '8',
		_ => throw new NotImplementedException(),
	};

	static void RenderBoard()
	{
		StringBuilder stringBuilder = new StringBuilder(width * height);
		for (int row = 0; row < height; row++)
		{
			for (int column = 0; column < width; column++)
			{
				stringBuilder.Append(
					board[column, row].Visible
					? Render(board[column, row].Value)
					: '█');
			}
			stringBuilder.AppendLine();
		}
		Console.CursorVisible = false;
		Console.SetCursorPosition(0, 0);
		Console.Write(stringBuilder.ToString());
		Console.CursorVisible = true;
	}

	static void Reveal(int column, int row)
	{
		board[column, row].Visible = true;
		if (board[column, row].Value == 0)
		{
			foreach (var (Row, Column) in AdjacentTiles(column, row))
			{
				if (!board[Column, Row].Visible)
				{
					Reveal(Column, Row);
				}
			}
		}
	}
}
