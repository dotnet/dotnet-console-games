using System;
using System.Collections.Generic;
using System.Text;

const int mine = -1;
int height = Console.WindowHeight;
int width = Console.WindowWidth;
Random random = new Random();
float mineRatio = .1f;
int mineCount = (int)(width * height * mineRatio);
(int Value, bool Visible)[,] board;
(int Column, int Row) = (width / 2, height / 2);

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
	Console.SetCursorPosition(Column, Row);
	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.UpArrow:    Row = Math.Max(Row - 1, 0); break;
		case ConsoleKey.DownArrow:  Row = Math.Min(Row + 1, height - 1); break;
		case ConsoleKey.LeftArrow:  Column = Math.Max(Column - 1, 0); break;
		case ConsoleKey.RightArrow: Column = Math.Min(Column + 1, width - 1); break;
		case ConsoleKey.Enter:
			if (!board[Column, Row].Visible)
			{
				if (board[Column, Row].Value == mine)
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
				else if (board[Column, Row].Value == 0)
				{
					Reveal(Column, Row);
					RenderBoard();
				}
				else
				{
					board[Column, Row].Visible = true;
					RenderBoard();
				}
				int visibleCount = 0;
				for (int column = 0; column < width; column++)
				{
					for (int row = 0; row < height; row++)
					{
						if (board[column, row].Visible)
						{
							visibleCount++;
						}
					}
				}
				if (visibleCount == width * height - mineCount)
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

IEnumerable<(int Row, int Column)> AdjacentTiles(int column, int row)
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

void GenerateBoard()
{
	board = new (int Value, bool Visible)[width, height];
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
		(int column, int row) = coordinates[randomIndex];
		coordinates.RemoveAt(randomIndex);
		board[column, row] = (mine, false);
		foreach (var tile in AdjacentTiles(column, row))
		{
			if (board[tile.Column, tile.Row].Value != mine)
			{
				board[tile.Column, tile.Row].Value++;
			}
		}
	}
}

char Render(int value) => value switch
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

void RenderBoard()
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
		//stringBuilder.AppendLine();
	}
	Console.CursorVisible = false;
	Console.SetCursorPosition(0, 0);
	Console.Write(stringBuilder.ToString());
	Console.CursorVisible = true;
}

void Reveal(int column, int row)
{
	board[column, row].Visible = true;
	if (board[column, row].Value == 0)
	{
		foreach (var (r, c) in AdjacentTiles(column, row))
		{
			if (!board[c, r].Visible)
			{
				Reveal(c, r);
			}
		}
	}
}
