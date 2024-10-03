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
                else                                                // if a number is found, set as leaf node
                {
                    StringBuilder numValue = new StringBuilder();

                    int counter = 0;
                    while (                                 // pushing number to postfix - makes exeptions for decimal places and negative values
                        Char.IsDigit(postfix[i + counter])
                        || postfix[i + counter] == '.'
                        || (postfix[i + counter] == '-' && counter == 0)
                    )
                    {
                        numValue.Append(postfix[i + counter++]);
                        if (i + counter == postfix.Length) break;
                    }

                    i += counter - 1;
                    current = new Node(numValue.ToString());
                }

                stack.Push(current);
            }

            return stack.Pop(); // return root node;
        }
    }
}
