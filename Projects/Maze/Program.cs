//#define MazeGenertorLoop // uncomment to run the generator in a loop
//#define DebugRandomMazeGeneration // uncomment me to watch the maze being built node-by-node
//#define UsePrims // uncomment me to use an alternate algorithm for maze generation

using System;
using System.Collections.Generic;
using System.Text;
using Towel.DataStructures;

class Program
{
	static void Main()
	{
		if (OperatingSystem.IsWindows())
		{
			Console.WindowHeight = 32;
		}
		const int rows = 8;
		const int columns = 20;
		static Maze.Tile[,] GenerateMaze() =>
#if UsePrims
			Maze.GeneratePrims(rows, columns);
#else
			Maze.Generate(rows, columns);
#endif
#if MazeGenertorLoop
		while (true)
		{
			Maze.Tile[,] maze = GenerateMaze();
			Console.Clear();
			Console.WriteLine(Maze.Render(maze));
			Console.WriteLine("Press Enter To Continue...");
			Console.ReadLine();
		}
#else
		Console.CursorVisible = true;
		Maze.Tile[,] maze = GenerateMaze();
		Console.Clear();
		Console.WriteLine(Maze.Render(maze));
		Console.WriteLine();
		Console.WriteLine("Maze");
		Console.WriteLine("Solve the maze by using the arrow keys.");
		Console.WriteLine("Press escape to quit.");
		int row = 0;
		int column = 0;
		while (row != rows - 1 || column != columns - 1)
		{
			Console.SetCursorPosition(column * 3 + 1, row * 3 + 1);
			switch (Console.ReadKey().Key)
			{
				case ConsoleKey.UpArrow:
					if (maze[row, column].HasFlag(Maze.Tile.Up))
						row--;
					break;
				case ConsoleKey.DownArrow:
					if (maze[row, column].HasFlag(Maze.Tile.Down))
						row++;
					break;
				case ConsoleKey.LeftArrow:
					if (maze[row, column].HasFlag(Maze.Tile.Left))
						column--;
					break;
				case ConsoleKey.RightArrow:
					if (maze[row, column].HasFlag(Maze.Tile.Right))
						column++;
					break;
				case ConsoleKey.Escape:
					Console.Clear();
					Console.Write("MMaze was closed.");
					return;
			}
		}
		Console.Clear();
		Console.Write("You Win.");
#endif
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

	#region Algorithm 1

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
		Random random = new();
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

			#region Optimizations

			// optimizations to prevent the algorithm from exploring unnecessary isolations
			// that will never reach the end of the maze. these currently depend on using the
			// default start/end locations, but they could be improved to help with custom
			// locations too. I am just lazy and didn't care enough to make a more general
			// purpose algorithm...

			bool DefaultLocations() =>
				start_row == 0 && start_column == 0 &&
				end_row == rows - 1 && end_column == columns - 1;
			bool UpOptimization(int column) => !(DefaultLocations() && column == columns - 1 || column == 0);
			bool LeftOptimization(int row) => !(DefaultLocations() && row == rows - 1 || row == 0);

			#endregion

			static bool NullOrEnd(Tile tile) => tile is Tile.Null || tile is Tile.End;

			bool MoveRandom()
			{
				Node node = stack.Peek();
				int i = 0;
				// populate possible moves
				if (node.Row != rows - 1 && NullOrEnd(maze[node.Row + 1, node.Column]) && !node.DownExplored)
					directionBuffer[i++] = (node.Row + 1, node.Column);
				if (node.Row != 0 && NullOrEnd(maze[node.Row - 1, node.Column]) && !node.UpExplored && UpOptimization(node.Column))
					directionBuffer[i++] = (node.Row - 1, node.Column);
				if (node.Column != 0 && NullOrEnd(maze[node.Row, node.Column - 1]) && !node.LeftExplored && LeftOptimization(node.Row))
					directionBuffer[i++] = (node.Row, node.Column - 1);
				if (node.Column != columns - 1 && NullOrEnd(maze[node.Row, node.Column + 1]) && !node.RightExplored)
					directionBuffer[i++] = (node.Row, node.Column + 1);
				// if no possibilities return false
				if (i is 0)
				{
					return false;
				}
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

#if DebugRandomMazeGeneration
				Console.Clear();
				Console.WriteLine(Render(maze));
				Console.WriteLine("Press Enter To Continue...");
				Console.ReadLine();
#endif
			}
		}

		// Generate invalid paths (to fill in the rest of the maze)
		{
			var stack = new Stack<Node>();
			var invalidPath = new HashSet<(int Row, int Column)>();
			var previousMoves = new Stack<Tile>();

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
					maze[move.Row, move.Column] |= Tile.Up;
					previousMoves.Push(Tile.Up);
				}
				if (move.Row == node.Row - 1)
				{
					node.UpExplored = true;
					maze[node.Row, node.Column] |= Tile.Up;
					maze[move.Row, move.Column] |= Tile.Down;
					previousMoves.Push(Tile.Down);
				}
				if (move.Column == node.Column - 1)
				{
					node.LeftExplored = true;
					maze[node.Row, node.Column] |= Tile.Left;
					maze[move.Row, move.Column] |= Tile.Right;
					previousMoves.Push(Tile.Right);
				}
				if (move.Column == node.Column + 1)
				{
					node.RightExplored = true;
					maze[node.Row, node.Column] |= Tile.Right;
					maze[move.Row, move.Column] |= Tile.Left;
					previousMoves.Push(Tile.Left);
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
				previousMoves.Clear();
				previousMoves.Push(Tile.Null);
				while (maze[stack.Peek().Row, stack.Peek().Column] == previousMoves.Peek())
				{
					if (!MoveRandom())
					{
						Node move = stack.Pop();
						Node parent = stack.Peek();
						previousMoves.Pop();
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

#if DebugRandomMazeGeneration
					Console.Clear();
					Console.WriteLine(Render(maze));
					Console.WriteLine("Press Enter To Continue...");
					Console.ReadLine();
#endif
				}
			}
		}

		return maze;
	}

	#endregion

	#region Algorithm 2 (Prims)

	public class Graph
	{
		public class Node
		{
			public int OwnIndex { get; }
			public List<int> Connections { get; }
			public List<double> Costs { get; }

			public void Add(int other, double cost)
			{
				Connections.Add(other);
				Costs.Add(cost);
			}

			public Node(int ownIndex)
			{
				OwnIndex = ownIndex;
				Connections = new List<int>();
				Costs = new List<double>();
			}
		}

		public Node[] Nodes { get; }

		public Graph(Node[] nodes)
		{
			Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
		}

		public static Maze.Tile[,] ConvertToGrid(Graph graph, int rows, int columns, Func<int, int, int> index, int start_row, int start_column, int end_row, int end_column)
		{
			var tiles = new Maze.Tile[rows, columns];

			foreach (var node in graph.Nodes)
			{
				if (node == null)
					continue;

				(int, int) Unpack(int i) => (i % rows, i / rows);

				var (row, col) = Unpack(node.OwnIndex);

				// directional
				if (node.Connections.Contains(index(row - 1, col)))
				{
					tiles[row, col] |= Maze.Tile.Up;
					tiles[row - 1, col] |= Maze.Tile.Down;
				}
				if (node.Connections.Contains(index(row + 1, col)))
				{
					tiles[row, col] |= Maze.Tile.Down;
					tiles[row + 1, col] |= Maze.Tile.Up;
				}
				if (node.Connections.Contains(index(row, col - 1)))
				{
					tiles[row, col] |= Maze.Tile.Left;
					tiles[row, col - 1] |= Maze.Tile.Right;
				}
				if (node.Connections.Contains(index(row, col + 1)))
				{
					tiles[row, col] |= Maze.Tile.Right;
					tiles[row, col + 1] |= Maze.Tile.Left;
				}

				// start/end
				if (row == start_row && col == start_column)
				{
					tiles[row, col] |= Maze.Tile.Start;
				}
				if (row == end_row && col == end_column)
				{
					tiles[row, col] |= Maze.Tile.End;
				}
			}
			return tiles;
		}
	}

	public static Tile[,] GeneratePrims(
		int rows, int columns,
		int? start_row = null, int? start_column = null,
		int? end_row = null, int? end_column = null)
	{
		start_row ??= 0;
		start_column ??= 0;
		end_row ??= rows - 1;
		end_column ??= columns - 1;

		var grid = new Graph.Node[rows * columns];

		int Index(int row, int col) => row + rows * col;

		var random = new Random();

		for (int row = 0; row < rows; row++)
		{
			for (int col = 0; col < columns; col++)
			{
				var n = new Graph.Node(Index(row, col));
				if (row + 1 < rows)
				{
					n.Add(Index(row + 1, col), random.NextDouble());
				}
				if (row - 1 >= 0)
				{
					n.Add(Index(row - 1, col), random.NextDouble());
				}
				if (col + 1 < columns)
				{
					n.Add(Index(row, col + 1), random.NextDouble());
				}
				if (col - 1 >= 0)
				{
					n.Add(Index(row, col - 1), random.NextDouble());
				}
				grid[Index(row, col)] = n;
			}
		}

		var graph = new Graph(grid);

#if DebugRandomMazeGeneration
		Console.Clear();
		Console.WriteLine(Maze.Render(Graph.ConvertToGrid(graph, rows, columns, Index, start_row.Value, start_column.Value, end_row.Value, end_column.Value)));
		Console.WriteLine("Press Enter To Continue...");
		Console.ReadLine();
		var res = SimplePrims(graph, rows, columns, Index, start_row.Value, start_column.Value, end_row.Value, end_column.Value);
#else
		var res = SimplePrims(graph);
#endif

		return Graph.ConvertToGrid(res, rows, columns, Index, start_row.Value, start_column.Value, end_row.Value, end_column.Value);
	}

	private readonly struct TwoWayConnection : IComparable<TwoWayConnection>
	{
		public readonly int IndexA;
		public readonly int IndexB;
		public readonly double Cost;

		public TwoWayConnection(int indexA, int indexB, double cost)
		{
			IndexA = indexA;
			IndexB = indexB;
			Cost = cost;
		}

		public int CompareTo(TwoWayConnection other) => other.Cost.CompareTo(Cost); // inversed because of how the heap works
	}

	public static Graph SimplePrims(Graph graph
#if DebugRandomMazeGeneration
		, int rows, int columns, Func<int, int, int> index, int start_row, int start_column, int end_row, int end_column
#endif
		)
	{
		var newGraph = new Graph(new Graph.Node[graph.Nodes.Length]);
		var nodes = graph.Nodes;
		var current = nodes[0];
		newGraph.Nodes[0] = new Graph.Node(0);

		var heap = HeapArray.New<TwoWayConnection>();

		while (true)
		{
			for (int i = 0; i < current.Connections.Count; i++)
			{
				heap.Enqueue(new TwoWayConnection(current.OwnIndex, current.Connections[i], current.Costs[i]));
			}

			TwoWayConnection c;
			do
			{
				if (heap.Count == 0)
				{
					return newGraph;
				}
				c = heap.Dequeue();
			}
			while (newGraph.Nodes[c.IndexB] != null);

			newGraph.Nodes[c.IndexA].Add(c.IndexB, c.Cost);

			newGraph.Nodes[c.IndexB] = new Graph.Node(c.IndexB);
			current = graph.Nodes[c.IndexB];
			newGraph.Nodes[c.IndexB].Add(c.IndexA, c.Cost);

#if DebugRandomMazeGeneration
			Console.Clear();
			Console.WriteLine(Maze.Render(Graph.ConvertToGrid(newGraph, rows, columns, index, start_row, start_column, end_row, end_column)));
			Console.WriteLine("Press Enter To Continue...");
			Console.ReadLine();
#endif
		}
	}

	#endregion

	public static string Render(Tile[,] maze)
	{
		static char Center(Tile tile) =>
			tile.HasFlag(Tile.Start) ? 'S' :
			tile.HasFlag(Tile.End) ? 'E' :
			/* default */ ' ';

		static char Side(Tile tile, Tile flag) =>
			tile.HasFlag(flag) ? ' ' : '█';

		static char[,] RenderTile(Tile tile) => new char[,]
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
		StringBuilder stringBuilder = new();
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
