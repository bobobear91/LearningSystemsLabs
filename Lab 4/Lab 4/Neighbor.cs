using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab_4
{
    public class Neighbor
    {
        public Node node { get; set; }
        public int distance { get; set; }
    }
    public class Node
    {

        public string name { get; set; }
        public Dictionary<string, List<string>> distanceDict { get; set; }
        public bool visited { get; set; }
        public List<Neighbor> neighbors { get; set; }
    }
    public class ShortestPath
    {
        public void CreateGraph(string from, string dest, int cost)
        {

        }
        public static void CreateFile()
        {
            CityNodeCollection collection = new CityNodeCollection();
            collection.Add(new CityNode("A", "B", 2));
            collection.Add(new CityNode("A", "E", 2));
            collection.Add(new CityNode("A", "W", 1));
            collection.Add(new CityNode("B", "D", 5));
            collection.Add(new CityNode("B", "W", 4));
            collection.Add(new CityNode("B", "C", 2));
            collection.Add(new CityNode("B", "F", 3));
            collection.Add(new CityNode("C", "F", 7));
            collection.Add(new CityNode("C", "V", 9));
            collection.Add(new CityNode("D", "E", 1));
            collection.Add(new CityNode("D", "J", 7));
            collection.Add(new CityNode("E", "K", 3));
            collection.Add(new CityNode("F", "L", 2));
            collection.Add(new CityNode("F", "M", 7));
            collection.Add(new CityNode("F", "R", 3));
            collection.Add(new CityNode("F", "Y", 1));
            collection.Add(new CityNode("G", "K", 8));
            collection.Add(new CityNode("G", "J", 5));
            collection.Add(new CityNode("G", "H", 2));
            collection.Add(new CityNode("H", "I", 4));
            collection.Add(new CityNode("H", "P", 1));
            collection.Add(new CityNode("I", "K", 5));
            collection.Add(new CityNode("J", "O", 3));
            collection.Add(new CityNode("J", "L", 2));
            collection.Add(new CityNode("L", "N", 4));
            collection.Add(new CityNode("N", "O", 1));
            collection.Add(new CityNode("O", "P", 1));
            collection.Add(new CityNode("M", "N", 5));
            collection.Add(new CityNode("M", "Z", 3));
            collection.Add(new CityNode("M", "X", 1));
            collection.Add(new CityNode("Z", "N", 6));
            collection.Add(new CityNode("X", "Z", 2));
            collection.Add(new CityNode("Y", "X", 5));
            collection.Add(new CityNode("R", "S", 4));
            collection.Add(new CityNode("S", "V", 2));

            //Serilize it
            Data.XML.SerializeToFile(collection, "city 1.xml");
        }
    }
    [Serializable]
    public class CityNodeCollection : Collection<CityNode>
    {

    }

    [Serializable]
    public class CityNode
    {
        [XmlElement("From")]
        public string From { get; set; }
        [XmlElement("Dest")]
        public string Dest { get; set; }
        [XmlElement("Cost")]
        public int Cost { get; set; }

        public CityNode()
        {

        }

        public CityNode(string from, string dest, int cost)
        {
            this.From = from;
            this.Dest = dest;
            this.Cost = cost;
        }
    }
}
