using Lab_4.Models;
using System;
using System.Collections.Generic;

namespace Lab_4.Handlers
{
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

            foreach (var vertex in vertices)
            {
                //Distance
                if (vertex.Key == start)
                {
                    distances[vertex.Key] = 0;
                }
                else
                {
                    distances[vertex.Key] = int.MaxValue;
                }

                nodes.Add(vertex.Key);
            }

            // The main loop
            while (nodes.Count != 0)
            {
                //Sort all nodes by their distance
                nodes.Sort((x, y) => distances[x] - distances[y]);
                //Sets the next node 
                var smallest = nodes[0];
                // Remove and return best vertex
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

                //Checks if the smallest path is equal to int max
                if (distances[smallest] == int.MaxValue)
                    break;

                //ForEach neighbor in verices set the new distance for the next step from smallest 
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
            path.Reverse();
            return path;
        }

        public static int Dijkstra_CostForPath(Dictionary<char, Dictionary<char, int>> vertices, List<char> path, char start, char answer)
        {
            if (start == answer)
                return 0;

            Queue<char> pathQueue = new Queue<char>(path);
            char temp = pathQueue.Dequeue();
            //A -> B
            int cost = vertices[start][temp];
            while (path.Count != 0)
            {
                if (temp == answer)
                    break;
                cost += vertices[temp][pathQueue.Peek()];
                temp = pathQueue.Dequeue();
   
            }
            return cost;
        }

        public static Tuple<int, List<char>> Dijkstra_ShortestPathAndCost(Dictionary<char, Dictionary<char, int>> vertices, char start, char finish)
        {
            List<char> path = Dijkstra_ShortestPath(vertices, start, finish);

            return new Tuple<int, List<char>>(Dijkstra_CostForPath(vertices, path, start, finish),path);
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
            collection.Add(new CityNode('A', 'B', 2));
            collection.Add(new CityNode('A', 'E', 2));
            collection.Add(new CityNode('A', 'W', 1));
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
        }
    }

}
