using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Console = Website.Console<Website.Games.Minesweeper.Minesweeper>;

namespace Website.Games.Minesweeper;

public class Minesweeper
{
	public static async Task Run()
	{

const int mine = -1;
Random random = new();
(int Value, bool Visible)[,] board;

await Console.WriteLine("Minesweeper");
await Console.WriteLine();
int selectedWidth = await GetIntegerInput($"Enter board width (10-{Math.Min(Console.LargestWindowWidth, 50)}): ", 10, Math.Min(Console.LargestWindowWidth, 50));
int selectedHeight = await GetIntegerInput($"Enter board height (10-{Math.Min(Console.LargestWindowHeight, 50)}): ", 10, Math.Min(Console.LargestWindowHeight, 50));
double mineRatio = await GetMineRatio("Enter mine ratio (example: 0.1): ");
int mineCount = (int)(selectedWidth * selectedHeight * mineRatio);
if (OperatingSystem.IsWindows())
{
	Console.WindowHeight = selectedHeight;
	Console.WindowWidth = selectedWidth;
}
(int Column, int Row) = (selectedWidth / 2, selectedHeight / 2);
GenerateBoard();
await Console.Clear();
await RenderBoard();
int height = Console.WindowHeight;
int width = Console.WindowWidth;
while (true)
{
	if (Console.WindowHeight != height || Console.WindowWidth != width)
	{
		await RenderBoard();
	}
	await Console.SetCursorPosition(Column, Row);
	switch ((await Console.ReadKey(true)).Key)
	{
		case ConsoleKey.UpArrow:    Row = Math.Max(Row - 1, 0); break;
		case ConsoleKey.DownArrow:  Row = Math.Min(Row + 1, selectedHeight - 1); break;
		case ConsoleKey.LeftArrow:  Column = Math.Max(Column - 1, 0); break;
		case ConsoleKey.RightArrow: Column = Math.Min(Column + 1, selectedWidth - 1); break;
		case ConsoleKey.Enter:
			if (!board[Column, Row].Visible)
			{
				if (board[Column, Row].Value == mine)
				{
					for (int column = 0; column < selectedWidth; column++)
					{
						for (int row = 0; row < selectedHeight; row++)
						{
							board[column, row].Visible = true;
						}
					}
					await RenderBoard();
					await Console.SetCursorPosition(0, selectedHeight - 1);
					await Console.Write("You Lose. Press Enter To Exit...");
					await Console.ReadLine();
					await Console.Clear();
					return;
				}
				else if (board[Column, Row].Value == 0)
				{
					Reveal(Column, Row);
					await RenderBoard();
				}
				else
				{
					board[Column, Row].Visible = true;
					await RenderBoard();
				}
				int visibleCount = 0;
				for (int column = 0; column < selectedWidth; column++)
				{
					for (int row = 0; row < selectedHeight; row++)
					{
						if (board[column, row].Visible)
						{
							visibleCount++;
						}
					}
				}
				if (visibleCount == selectedWidth * selectedHeight - mineCount)
				{
					await Console.SetCursorPosition(0, selectedHeight - 1);
					await Console.Write("You Win. Press Enter To Exit...");
					await Console.ReadLine();
					await Console.Clear();
					await Console.Refresh();
					return;
				}
			}
			break;
		case ConsoleKey.Escape:
			await Console.Clear();
			await Console.Write("Minesweeper was closed.");
			await Console.Refresh();
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
	/* C */ if (row > 0 && column < selectedWidth - 1) yield return (row - 1, column + 1);
	/* D */ if (column > 0) yield return (row, column - 1);
	/* E */ if (column < selectedWidth - 1) yield return (row, column + 1);
	/* F */ if (row < selectedHeight - 1 && column > 0) yield return (row + 1, column - 1);
	/* G */ if (row < selectedHeight - 1) yield return (row + 1, column);
	/* H */ if (row < selectedHeight - 1 && column < selectedWidth - 1) yield return (row + 1, column + 1);
}

async Task<int> GetIntegerInput(string prompt, int min, int max)
{
	int inputValue;
	await Console.Write(prompt);
	while (!int.TryParse(await Console.ReadLine(), out inputValue) || inputValue < min || max < inputValue)
	{
		await Console.WriteLine("Invalid Input. Try Again...");
		await Console.Write(prompt);
	}
	return inputValue;
}

async Task<double> GetMineRatio(string prompt)
{
	double inputValue;
	await Console.Write(prompt);
	while (!double.TryParse(await Console.ReadLine(), out inputValue) || (mineCount = (int)(selectedWidth * selectedHeight * inputValue)) < 0 || mineCount > selectedHeight * selectedWidth)
	{
		await Console.WriteLine("Invalid Input. Try Again...");
		await Console.Write(prompt);
	}
	return inputValue;
}

void GenerateBoard()
{
	board = new (int Value, bool Visible)[selectedWidth, selectedHeight];
	var coordinates = new List<(int Row, int Column)>();
	for (int column = 0; column < selectedWidth; column++)
	{
		for (int row = 0; row < selectedHeight; row++)
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

async Task RenderBoard()
{
	for (int row = 0; row < selectedHeight; row++)
	{
		for (int column = 0; column < selectedWidth; column++)
		{
			await Console.SetCursorPosition(column, row);
			await Console.Write(board[column, row].Visible
				? Render(board[column, row].Value)
				: '█');
		}
	}
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
	}
}
