using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class HeaderNode : Node
    {
        public string Name { get; set; }

        

        // Used by S-heuristics
        public int SatisfactionsCount { get; set; }
    }
}
