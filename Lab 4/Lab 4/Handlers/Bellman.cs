using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4.Handlers
{
    public class BellmanNode
    {
        public int src, dest, weight;
        char name;

        public BellmanNode(char name, int src, int dest, int weight)
        {
            this.name = name;
            this.src = src;
            this.dest = dest;
            this.weight = weight;
        }
        public BellmanNode(char name)
        {
            this.name = name;
            src = dest = weight = 0;
        }
        public BellmanNode()
        {
            src = dest = weight = 0;
            name = 'U';
        }

        public char Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

    }
    public class BellmanGraph
    {
        public int Vertex, Edges;
        public BellmanNode[] edge;

        // Creates a graph with V vertices and E edges
        public BellmanGraph(int vertices, int edges)
        {
            Vertex = vertices;
            Edges = edges;
            edge = new BellmanNode[edges];
            for (int i = 0; i < edges; ++i)
                edge[i] = new BellmanNode();
        }

        }
}
