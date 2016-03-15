using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4.Models
{
    class Graph
    {
        #region Variables & Properties
        Dictionary<char, Dictionary<char, int>> vertices = new Dictionary<char, Dictionary<char, int>>();
        public Dictionary<char, Dictionary<char, int>> Vertices
        {
            get { return vertices; }
        }
        #endregion

        #region Public Methods
        public void AddNewVertices(char name, Dictionary<char, int> edges)
        {
            vertices[name] = edges;
        }
        #endregion

        #region Constructor
        public Graph()
        {

        }
        public Graph(Dictionary<char, Dictionary<char, int>> vertices)
        {
            this.vertices = vertices;
        }
        #endregion
    }

}
