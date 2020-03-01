using System;
using Towel.DataStructures;

namespace Prims
{
	public static class PrimsAlgorithm
	{
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

		public static Graph SimplePrims(Graph graph)
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
			}
		}
	}
}