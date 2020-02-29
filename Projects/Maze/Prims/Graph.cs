using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
}
