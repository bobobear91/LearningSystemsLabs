using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4.Handlers
{
    class BellmanNode
    {
        public int src, dest, weight;
        char name;

        public BellmanNode(char name, int src, int dest, int weight)
        {
            this.src = src;
            this.dest = dest;
            this.weight = weight;
            this.name = name;
        }
        public BellmanNode(char name)
        {
            this.name = name;
        }
        public BellmanNode()
        {
            src =  dest=  weight = 0;
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
    class BellmanGraph
    {
        public int Vertex, Edges;
        public BellmanNode[] edge;

        // Creates a graph with V vertices and E edges
        public BellmanGraph(int v, int e)
        {
            Vertex = v;
            Edges = e;
            edge = new BellmanNode[e];
            for (int i = 0; i < e; ++i)
                edge[i] = new BellmanNode();
        }

        public int[] BellmanFord(BellmanGraph graph, int src)
        {
            //declare variables
            int V = graph.Vertex, E = graph.Edges;
            int[] dist = new int[V];

            // Step 1: Initialize distances from src to all other
            // vertices as INFINITE
            for (int i = 0; i < V; ++i)
                dist[i] = int.MaxValue;
            dist[src] = 0;

            // Step 2: Relax all edges |V| - 1 times. A simple
            // shortest path from src to any other vertex can
            // have at-most |V| - 1 edges
            for (int i = 1; i < V; ++i)
            {
                for (int j = 0; j < E; ++j)
                {
                    int u = graph.edge[j].src;
                    int v = graph.edge[j].dest;
                    int weight = graph.edge[j].weight;
                    if (dist[u] != int.MaxValue && dist[u] + weight < dist[v])
                        dist[v] = dist[u] + weight;
                }
            }

            // Step 3: check for negative-weight cycles.  The above
            // step guarantees shortest distances if graph doesn't
            // contain negative weight cycle. If we get a shorter
            //  path, then there is a cycle.
            for (int j = 0; j < E; ++j)
            {
                int u = graph.edge[j].src;
                int v = graph.edge[j].dest;
                int weight = graph.edge[j].weight;
                if (dist[u] != int.MaxValue && dist[u] + weight < dist[v])
                {
                    break;
                    //System.out.println("Graph contains negative weight cycle");
                }
            }
            return dist;
        }
    }
}
