using Lab_4.Models;
using System;
using System.Collections.Generic;

namespace Lab_4.Handlers
{
    public class BellmanFord
    {
        public List<Egde> Edge = new List<Egde>();
        public int V;   // number of vertex

        public class Egde
        {
            public int From, To, Cost;
            public char Name;
            public Egde(int from, int to, int cost, char t)
            {
                From = from;
                To = to;
                Cost = cost;
                t = Name;
            }
        }

        private int GetTotalPositiveCost()
        {
            int sum = 0;
            foreach (var e in Edge)
            {
                if (e.Cost > 0)
                    sum += e.Cost;
            }
            return sum;
        }

        private void generateV()
        {
            foreach (var e in Edge)
            {
                V = Math.Max(V, e.From);
                V = Math.Max(V, e.To);
            }
            V++;
        }

        /// <summary>
        ///  return shortestPath[V] represents distance from startIndex
        /// </summary>
        public int[] GetShortestPath(int startIndex, int vertex)
        {
           
            this.V = vertex;
            int[] shortestPath = new int[vertex];
            int INF = GetTotalPositiveCost() + 1;

            for (int i = 0; i < V; i++)
                shortestPath[i] = INF;
       
            shortestPath[startIndex] = 0;
            while (true)
            {
                bool update = false;
                foreach (Egde e in Edge)
                {
                    if (shortestPath[e.From] != INF && shortestPath[e.To] > shortestPath[e.From] + e.Cost)
                    {
                        shortestPath[e.To] = shortestPath[e.From] + e.Cost;
                        update = true;
                    }
                }
                if (!update)
                    break;
            }

            return shortestPath;
        }

        /// <summary>
        ///  return true if it has negative close loop
        /// </summary>
        public bool HasNegativeLoop()
        {
            int[] d = new int[V];
            for (int i = 0; i < V; i++)
            {
                foreach (Egde e in Edge)
                {
                    if (d[e.To] > d[e.From] + e.Cost)
                    {
                        d[e.To] = d[e.From] + e.Cost;
                        if (i == V - 1) return true;
                    }
                }
            }
            return false;
        }
    }
    public class ShortestPath
    {
        //Using a priority queue
        //https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
        public static List<char> Dijkstra_ShortestPath(Dictionary<char, Dictionary<char, int>> vertices, char start, char finish)
        {
            //*********************************************************
            // Initialization
            //  Creates population holders
            //*********************************************************
            Dictionary<char, char> previous = new Dictionary<char, char>();
            Dictionary<char, int> distances = new Dictionary<char, int>();
            List<char> nodes = new List<char>();
            //Return list
            List<char> path = null;

            //Instantiate our movement "Queue", just to know point of origin
            foreach (var vertex in vertices)
            {
                //Set the current position equal to zero if its start (source)
                distances[vertex.Key] = (vertex.Key == start) ? 0 : int.MaxValue;
                //Adds the movement nodes in to list of nodes 
                nodes.Add(vertex.Key);
            }

            // The main loop
            while (nodes.Count != 0)
            {
                //Sort all nodes by their distance from this point to next standing point
                nodes.Sort((x, y) => distances[x] - distances[y]);
                //Sets the next node 
                char smallest = nodes[0];
                // Remove current position so we don't move to this position
                nodes.Remove(smallest);
                //If we at the end of path
                if (smallest == finish)
                {
                    path = new List<char>();
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }
                    break;
                }

                //Checks if the current distance is equal to int max, if it is we have no path to move and therefor break
                if (distances[smallest] == int.MaxValue)
                    break;


                //ForEach neighbor in verices set the new distance for the next step from smallest  (uses the previous values)
                foreach (var neighbor in vertices[smallest])
                {
                    //adds the distance with neighbor value
                    int alt = distances[smallest] + neighbor.Value;
                    //
                    if (alt < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = alt;
                        previous[neighbor.Key] = smallest;
                    }
                }
            }
            //Reverse the list so it start from the first node
            if (path != null)
            {
                path.Reverse();
            }
            return path;
        }


        //http://www.geeksforgeeks.org/dynamic-programming-set-23-bellman-ford-algorithm/
        //https://en.wikipedia.org/wiki/Bellman%E2%80%93Ford_algorithm
        public static int[] BellmanFord(BellmanGraph graph, int src)
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
            for (int j = 0; j < Edges; j++)
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


        public static int Dijkstra_CostForPath(Dictionary<char, Dictionary<char, int>> vertices, List<char> path, char start, char answer)
        {
            //Checks if we are already at the end
            if (start == answer || path == null)
                return 0;
            //Creates a queue
            Queue<char> pathQueue = new Queue<char>(path);
            //First character for the movement (start= A, temp = B then A->B) 
            char temp = pathQueue.Dequeue();
            int cost = vertices[start][temp];

            //If there still movement in the queue, iterate through it 
            while (path.Count != 0)
            {
                //Checks if we are already at the end
                if (temp == answer)
                    break;

                //Sums the cost
                cost += vertices[temp][pathQueue.Peek()];
                //Move forward
                temp = pathQueue.Dequeue();
   
            }

            return cost;
        }

        public static Path PathShortest(Dictionary<char, Dictionary<char, int>> vertices, char start, char finish)
        {
            Path path = new Path();
            path.Start = start;
            path.Finish = finish;
            path.PathChars = Dijkstra_ShortestPath(vertices, start, finish);
            path.Cost = Dijkstra_CostForPath(vertices, path.PathChars, start, finish);
            return path;
        }

        public static void CreateFile()
        {
            CityNodeCollection collection = new CityNodeCollection();
            collection.Add(new CityNode('A', 'B', 2)); //0
            collection.Add(new CityNode('A', 'E', 2)); //0
            collection.Add(new CityNode('A', 'W', 1)); //0
            collection.Add(new CityNode('B', 'D', 5)); 
            collection.Add(new CityNode('B', 'W', 4));
            collection.Add(new CityNode('B', 'C', 2));
            collection.Add(new CityNode('B', 'F', 3));
            collection.Add(new CityNode('C', 'F', 7));
            collection.Add(new CityNode('C', 'V', 9));
            collection.Add(new CityNode('D', 'E', 1));
            collection.Add(new CityNode('D', 'J', 7));
            collection.Add(new CityNode('E', 'K', 3));
            collection.Add(new CityNode('F', 'L', 2));
            collection.Add(new CityNode('F', 'M', 7));
            collection.Add(new CityNode('F', 'R', 3));
            collection.Add(new CityNode('F', 'Y', 1));
            collection.Add(new CityNode('G', 'K', 8));
            collection.Add(new CityNode('G', 'J', 5));
            collection.Add(new CityNode('G', 'H', 2));
            collection.Add(new CityNode('H', 'I', 4));
            collection.Add(new CityNode('H', 'P', 1));
            collection.Add(new CityNode('I', 'K', 5));
            collection.Add(new CityNode('J', 'O', 3));
            collection.Add(new CityNode('J', 'L', 2));
            collection.Add(new CityNode('L', 'N', 4));
            collection.Add(new CityNode('N', 'O', 1));
            collection.Add(new CityNode('O', 'P', 1));
            collection.Add(new CityNode('M', 'N', 5));
            collection.Add(new CityNode('M', 'Z', 3));
            collection.Add(new CityNode('M', 'X', 1));
            collection.Add(new CityNode('Z', 'N', 6));
            collection.Add(new CityNode('X', 'Z', 2));
            collection.Add(new CityNode('Y', 'X', 5));
            collection.Add(new CityNode('R', 'S', 4));
            collection.Add(new CityNode('S', 'V', 2));

            //Serilize it
            Data.XML.SerializeToFile(collection, "city 1.xml");

            CityNodeCollection collection2 = new CityNodeCollection();
            collection2.Add(new CityNode('A', 'B', 2)); //1
            collection2.Add(new CityNode('A', 'C', 3)); //2
            collection2.Add(new CityNode('A', 'D', 4)); //3
            collection2.Add(new CityNode('B', 'C', 2)); //4
            collection2.Add(new CityNode('B', 'E', 1)); //5
            collection2.Add(new CityNode('C', 'E', 7)); //6
            collection2.Add(new CityNode('D', 'E', 8)); //7
            collection2.Add(new CityNode('D', 'C', 3)); //8
            Data.XML.SerializeToFile(collection2, "1.xml");

        }
    }

}
