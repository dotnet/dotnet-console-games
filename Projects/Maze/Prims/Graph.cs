using System;
using System.Collections.Generic;

namespace Prims
{
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
}
