using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4.Handlers
{
    public class Path
    {
        public char Start { get; set; }
        public char Finish { get; set; }
        public int Cost { get; set; }

        public List<char> PathChars { get; set; }
    }
}
