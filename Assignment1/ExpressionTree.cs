using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    internal class ExpressionTree
    {
        public Node Root { get; set; }

        public void SetRoot(string postfix)
        {
            Root = null;
        }
    }
}
