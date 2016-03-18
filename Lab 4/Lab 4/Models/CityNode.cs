using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Lab_4.Models
{
    [Serializable]
    public class CityNode
    {
        [XmlElement("From")]
        public char From { get; set; }
        [XmlElement("Dest")]
        public char Dest { get; set; }
        [XmlElement("Cost")]
        public int Cost { get; set; }

        [XmlElement("BFrom")]
        public int BFrom { get; set; }
        [XmlElement("BDest")]
        public int BDest { get; set; }


        public CityNode()
        {

        }

        public CityNode(char from, char dest, int cost)
        {
            this.From = from;
            this.Dest = dest;
            this.Cost = cost;
            this.BFrom = from - 65;
            this.BDest = dest - 65;
        }

        public CityNode(char from,int f, char dest,int d, int cost)
        {
            this.From = from;
            this.Dest = dest;
            this.Cost = cost;
        }
    }

    [Serializable]
    public class CityNodeCollection : Collection<CityNode>
    {

    }
}
