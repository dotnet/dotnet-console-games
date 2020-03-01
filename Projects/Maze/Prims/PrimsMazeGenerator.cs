// #define DebugMazeGeneration
using System;
using Towel.DataStructures;

namespace Prims
{
	public class PrimsMazeGenerator
	{
		public static Maze.Tile[,] Generate(
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
			
			#if DebugMazeGeneration
			
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
#if DebugMazeGeneration
			, int rows, int columns, Func<int, int, int> index, int start_row, int start_column, int end_row, int end_column
#endif

		)
		{
			var newGraph = new Graph(new Graph.Node[graph.Nodes.Length]);
			var nodes = graph.Nodes;
			var current = nodes[0];
			newGraph.Nodes[0] = new Graph.Node(0);

			var heap = new HeapArray<TwoWayConnection>();

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
				
#if DebugMazeGeneration
			
				Console.Clear();
				Console.WriteLine(Maze.Render(Graph.ConvertToGrid(newGraph, rows, columns, index, start_row, start_column, end_row, end_column)));
				Console.WriteLine("Press Enter To Continue...");
				Console.ReadLine();
			
#endif
			}
		}
	}
}