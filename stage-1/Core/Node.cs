using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    // The base building block of a circularly linked list
    public class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Up { get; set; }
        public Node Down { get; set; }
        public HeaderNode ColHeader { get; set; }

        public int RowNumberInMatrix { get; set; }

        public Node()
        {
            this.Left = this;
            this.Right = this;
            this.Up = this;
            this.Down = this;
        }
    }
}
