using System;
using System.CodeDom;
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
            Root = CreateRoot(postfix);
        }

        public Node CreateRoot(string postfix)
        {
            Stack<Node> stack = new Stack<Node>();
            Node current;
            List<char> operators = new List<char> { '*', '/', '+', '-' };

            for (int i = 0; i < postfix.Length; i++)
            {
                bool isPositive = true;
                if (i + 1 != postfix.Length)    // checking for index bounds
                {
                    if (postfix[i] == '-' && Char.IsDigit(postfix[i + 1]))  // checking for negatives
                    {
                        isPositive = false;
                    }
                }

                if (operators.Contains(postfix[i]) && isPositive)   // if an operator is found, set node that points to other expressions
                {
                    current = new Node(postfix[i]);
                    current.Left = stack.Pop();
                    current.Right = stack.Pop();
                }
            }

            return null;
        }
    }
}
