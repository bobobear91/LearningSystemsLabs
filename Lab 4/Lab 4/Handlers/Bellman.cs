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
    class BellmanGraph
    {
        public int Vertex, Edges;
        public BellmanNode[] edge;
        //http://www.geeksforgeeks.org/dynamic-programming-set-23-bellman-ford-algorithm/
        //https://en.wikipedia.org/wiki/Bellman%E2%80%93Ford_algorithm
        // Creates a graph with V vertices and E edges
        public BellmanGraph(int v, int e)
        {
            Vertex = v;
            Edges = e;
            edge = new BellmanNode[e];
            for (int i = 0; i < e; ++i)
                edge[i] = new BellmanNode();
        }


        //// Step 3: check for negative-weight cycles
        //for each edge (u, v) with weight w in edges:
        //    if distance[u] + w<distance[v]:
        //        error "Graph contains a negative-weight cycle"
        //return distance[], predecessor[]

        public int[] BellmanFord(BellmanGraph graph, int src)
        {
            //declare variables
            int Vertices = graph.Vertex, Edges = graph.Edges;
            int[] dist = new int[Vertices];
            int[] predecessor = new int[Vertices];

            // Step 1: Initialize distances from src to all other
            // vertices as INFINITE
            for (int i = 0; i < Vertices; i++)
            {
                dist[i] = int.MaxValue;
                predecessor[i] = int.MaxValue;
            }
            dist[src] = 0;
            // Step 2: Relax all edges |V| - 1 times. A simple
            // shortest path from src to any other vertex can
            // have at-most |V| - 1 edges
            for (int i = 1; i < Vertices; i++)
            {
                for (int j = 0; j < Edges; j++)
                {
                    //int u = graph.edge[j].src;
                    //int v = graph.edge[j].dest;
                    int weight = graph.edge[j].weight;
                    if (dist[graph.edge[j].src] != int.MaxValue && dist[graph.edge[j].src] + weight < dist[graph.edge[j].dest])
                    {
                        dist[graph.edge[j].dest] = dist[graph.edge[j].src] + weight;
                        predecessor[graph.edge[j].dest] = graph.edge[j].src;
                    }
                }
            }

            // Step 3: check for negative-weight cycles.  The above
            // step guarantees shortest distances if graph doesn't
            // contain negative weight cycle. If we get a shorter
            //  path, then there is a cycle.
            for (int j = 0; j < Edges;j++)
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
