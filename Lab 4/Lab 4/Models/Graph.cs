using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4.Models
{
    class DijkstrasGraph
    {
        #region Variables & Properties
        private Dictionary<char, Dictionary<char, int>> graph = new Dictionary<char, Dictionary<char, int>>();
        public Dictionary<char, Dictionary<char, int>> Graph
        {
            get { return graph; }
        }


        private int edges = 0;
        public int Edges
        {
            get;
        }

        public int Vertex
        {
            get { return graph.Count; }
        }
        #endregion

        #region Public Methods
        public void AddNewVertices(char name, Dictionary<char, int> edges)
        {
            graph[name] = edges;
            this.edges += edges.Count;
        }
        #endregion

        #region Constructor
        public DijkstrasGraph()
        {

        }
        public DijkstrasGraph(Dictionary<char, Dictionary<char, int>> vertices)
        {
            this.graph = vertices;
        }
        #endregion
    }

}
