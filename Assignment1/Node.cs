using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    internal class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public string Value { get; set; }

        public Node(char value)
        {
            Left = null;
            Right = null;
            Value = value.ToString();
        }

        public Node(string value)
        {
            Left = null;
            Right = null;
            Value = value;
        }
    }
}
