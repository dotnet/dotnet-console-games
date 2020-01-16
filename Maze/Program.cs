using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

class Program
{
	static void Main()
	{
		Maze.Tile[,] maze = Maze.Generate(10, 10);
		Console.WindowHeight = 35;
		Console.Clear();
		Console.WriteLine("This game is still in development, but the random maze generator works. :)");
		Console.WriteLine();
		Console.WriteLine(Maze.Render(maze));
		Console.ReadLine();
	}
}

public static class Maze
{
	[Flags]
	public enum Tile
	{
		Null = 0,
		Up = 1,
		Down = 2,
		Left = 4,
		Right = 8,
		Start = 16,
		End = 32,
	}

	internal class Node
	{
		internal int Row;
		internal int Column;
		internal bool UpExplored;
		internal bool DownExplored;
		internal bool LeftExplored;
		internal bool RightExplored;
	}

	public static Tile[,] Generate(
		int rows, int columns,
		int? start_row = null, int? start_column = null,
		int? end_row = null, int? end_column = null)
	{
#pragma warning disable IDE0042 // Deconstruct variable declaration

		// parameter defaults
		start_row ??= 0;
		start_column ??= 0;
		end_row ??= rows - 1;
		end_column ??= columns - 1;

		#region Exceptions
		if (rows <= 1)
			throw new ArgumentOutOfRangeException(nameof(rows));
		if (columns <= 1)
			throw new ArgumentOutOfRangeException(nameof(columns));
		if (start_row < 0 || rows < start_row)
			throw new ArgumentOutOfRangeException(nameof(start_row));
		if (end_row < 0 || rows < end_row || start_row == end_row)
			throw new ArgumentOutOfRangeException(nameof(end_row));
		if (start_column < 0 || columns < start_column)
			throw new ArgumentOutOfRangeException(nameof(start_column));
		if (end_column < 0 || columns < end_column || start_column == end_column)
			throw new ArgumentOutOfRangeException(nameof(end_column));
		#endregion

		Tile[,] maze = new Tile[rows, columns];
		Random random = new Random();
		var directionBuffer = new (int Row, int Column)[4];

		maze[start_row.Value, start_column.Value] = Tile.Start;
		maze[end_row.Value, end_column.Value] = Tile.End;

		// generate a valid path (so the maze is guaranteed to be solve-able)
		{
			var stack = new Stack<Node>();
			stack.Push(new Node()
			{
				Row = start_row.Value,
				Column = start_column.Value,
			});

			bool NullOrEnd(Tile tile) => tile is Tile.Null || tile is Tile.End;

			bool MoveRandom()
			{
				Node node = stack.Peek();
				int i = 0;
				// populate possible moves
				if (node.Row != rows - 1 && NullOrEnd(maze[node.Row + 1, node.Column]) && !node.DownExplored)
					directionBuffer[i++] = (node.Row + 1, node.Column);
				if (node.Row != 0 && NullOrEnd(maze[node.Row - 1, node.Column]) && !node.UpExplored)
					directionBuffer[i++] = (node.Row - 1, node.Column);
				if (node.Column != 0 && NullOrEnd(maze[node.Row, node.Column - 1]) && !node.LeftExplored)
					directionBuffer[i++] = (node.Row, node.Column - 1);
				if (node.Column != columns - 1 && NullOrEnd(maze[node.Row, node.Column + 1]) && !node.RightExplored)
					directionBuffer[i++] = (node.Row, node.Column + 1);
				// if no possibilities return false
				if (i is 0)
					return false;
				// get a random move from the possibilities
				var move = directionBuffer[random.Next(0, i)];
				// mark the move as explored
				if (move.Row == node.Row + 1)
				{
					node.DownExplored = true;
					maze[node.Row, node.Column] |= Tile.Down;
					maze[move.Row, move.Column] |= Tile.Up;
				}
				if (move.Row == node.Row - 1)
				{
					node.UpExplored = true;
					maze[node.Row, node.Column] |= Tile.Up;
					maze[move.Row, move.Column] |= Tile.Down;
				}
				if (move.Column == node.Column - 1)
				{
					node.LeftExplored = true;
					maze[node.Row, node.Column] |= Tile.Left;
					maze[move.Row, move.Column] |= Tile.Right;
				}
				if (move.Column == node.Column + 1)
				{
					node.RightExplored = true;
					maze[node.Row, node.Column] |= Tile.Right;
					maze[move.Row, move.Column] |= Tile.Left;
				}
				stack.Push(new Node()
				{
					Row = move.Row,
					Column = move.Column,
				});
				// return the move
				return true;
			}

			while (stack.Peek().Row != end_row || stack.Peek().Column != end_column)
			{
				if (!MoveRandom())
				{
					Node move = stack.Pop();
					maze[move.Row, move.Column] = Tile.Null;
					Node parent = stack.Peek();
					if (move.Row == parent.Row - 1) maze[parent.Row, parent.Column] &= ~Tile.Up;
					if (move.Row == parent.Row + 1) maze[parent.Row, parent.Column] &= ~Tile.Down;
					if (move.Column == parent.Column + 1) maze[parent.Row, parent.Column] &= ~Tile.Right;
					if (move.Column == parent.Column - 1) maze[parent.Row, parent.Column] &= ~Tile.Left;
				}

				//// Debugging Write
				//Console.Clear();
				//Console.WriteLine(Render(maze));
			}
		}

		// Generate invalid paths (to fill in the rest of the maze)
		{
			var stack = new Stack<Node>();
			var invalidPath = new HashSet<(int Row, int Column)>();
			Tile previousMove;

			int CountNulls()
			{
				int count = 0;
				for (int row = 0; row < rows; row++)
					for (int column = 0; column < columns; column++)
						if (maze[row, column] is Tile.Null)
							count++;
				return count;
			}

			(int Row, int Column)? GetRandomNull(int? nullCount = null)
			{
				int nullCountInt = nullCount ?? CountNulls();
				// if no Tile.Null's, return null
				if (nullCountInt <= 0) return null;
				// nulls exist, get a random one
				int index = random.Next(0, nullCountInt + 1);
				(int, int) @null = default;
				for (int row = 0; row < rows && index > 0; row++)
					for (int column = 0; column < columns && index > 0; column++)
						if (maze[row, column] is Tile.Null && --index == 0)
							@null = (row, column);
				return @null;
			}

			bool MoveRandom()
			{
				Node node = stack.Peek();
				int i = 0;
				if (node.Row != rows - 1 && !invalidPath.Contains((node.Row + 1, node.Column)) && !node.DownExplored)
					directionBuffer[i++] = (node.Row + 1, node.Column);
				if (node.Row != 0 && !invalidPath.Contains((node.Row - 1, node.Column)) && !node.UpExplored)
					directionBuffer[i++] = (node.Row - 1, node.Column);
				if (node.Column != 0 && !invalidPath.Contains((node.Row, node.Column - 1)) && !node.LeftExplored)
					directionBuffer[i++] = (node.Row, node.Column - 1);
				if (node.Column != columns - 1 && !invalidPath.Contains((node.Row, node.Column + 1)) && !node.RightExplored)
					directionBuffer[i++] = (node.Row, node.Column + 1);
				if (i is 0)
					return false;
				var move = directionBuffer[random.Next(0, i)];
				if (move.Row == node.Row + 1)
				{
					node.DownExplored = true;
					maze[node.Row, node.Column] |= Tile.Down;
					maze[move.Row, move.Column] |= previousMove = Tile.Up;
				}
				if (move.Row == node.Row - 1)
				{
					node.UpExplored = true;
					maze[node.Row, node.Column] |= Tile.Up;
					maze[move.Row, move.Column] |= previousMove = Tile.Down;
				}
				if (move.Column == node.Column - 1)
				{
					node.LeftExplored = true;
					maze[node.Row, node.Column] |= Tile.Left;
					maze[move.Row, move.Column] |= previousMove = Tile.Right;
				}
				if (move.Column == node.Column + 1)
				{
					node.RightExplored = true;
					maze[node.Row, node.Column] |= Tile.Right;
					maze[move.Row, move.Column] |= previousMove = Tile.Left;
				}
				stack.Push(new Node()
				{
					Row = move.Row,
					Column = move.Column,
				});
				invalidPath.Add((node.Row, node.Column));
				return true;
			}

			(int Row, int Column)? nullStart;
			while ((nullStart = GetRandomNull()).HasValue)
			{
				stack.Clear();
				invalidPath.Clear();
				stack.Push(new Node()
				{
					Row = nullStart.Value.Row,
					Column = nullStart.Value.Column,
				});
				invalidPath.Add((nullStart.Value.Row, nullStart.Value.Column));
				previousMove = Tile.Null;
				while (maze[stack.Peek().Row, stack.Peek().Column] == previousMove)
				{
					if (!MoveRandom())
					{
						Node move = stack.Pop();
						Node parent = stack.Peek();
						if (move.Row == parent.Row - 1)
						{
							maze[move.Row, move.Column] &= ~Tile.Down;
							maze[parent.Row, parent.Column] &= ~Tile.Up;
						}
						if (move.Row == parent.Row + 1)
						{
							maze[move.Row, move.Column] &= ~Tile.Up;
							maze[parent.Row, parent.Column] &= ~Tile.Down;
						}
						if (move.Column == parent.Column + 1)
						{
							maze[move.Row, move.Column] &= ~Tile.Left;
							maze[parent.Row, parent.Column] &= ~Tile.Right;
						}
						if (move.Column == parent.Column - 1)
						{
							maze[move.Row, move.Column] &= ~Tile.Right;
							maze[parent.Row, parent.Column] &= ~Tile.Left;
						}
					}

					//// Debugging Write
					//Console.Clear();
					//Console.WriteLine(Render(maze));
				}
			}
		}

		return maze;
#pragma warning restore IDE0042 // Deconstruct variable declaration
	}

	public static string Render(Tile[,] maze)
	{
		static char Center(Tile tile) =>
			tile.HasFlag(Tile.Start) ? 'S' :
			tile.HasFlag(Tile.End) ? 'E' :
			/* default */ ' ';
		static char Side(Tile tile, Tile flag) =>
			tile.HasFlag(flag) ? ' ' : '█';
		char[,] RenderTile(Tile tile) => new char[,]
		{
			{ '█', Side(tile, Tile.Up), '█' },
			{ Side(tile, Tile.Left), Center(tile), Side(tile, Tile.Right) },
			{ '█', Side(tile, Tile.Down), '█' },
		};
		int rows = maze.GetLength(0);
		int columns = maze.GetLength(1);
		char[,][,] rendered = new char[rows, columns][,];
		for (int row = 0; row < rows; row++)
		{
			for (int column = 0; column < columns; column++)
			{
				rendered[row, column] = RenderTile(maze[row, column]);
			}
		}
		int rowsX3 = rows * 3;
		int columnsX3 = columns * 3;
		StringBuilder stringBuilder = new StringBuilder();
		for (int row = 0; row < rowsX3; row++)
		{
			for (int column = 0; column < columnsX3; column++)
			{
				int tileRow = row / 3;
				int tileColumn = column / 3;
				int renderRow = row % 3;
				int renderColumn = column % 3;
				stringBuilder.Append(rendered[tileRow, tileColumn][renderRow, renderColumn]);
			}
			stringBuilder.AppendLine();
		}
		string render = stringBuilder.ToString();
		return render;
	}
}