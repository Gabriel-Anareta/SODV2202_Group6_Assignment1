using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class DivideByZeroException : Exception
    {
        public DivideByZeroException(string message) : base(message) { }
    }

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
                if (postfix[i] == ' ') 
                    continue;

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
                        if (i + counter == postfix.Length) 
                            break;
                    }

                    i += counter - 1;
                    current = new Node(numValue.ToString());
                }

                stack.Push(current);
            }

            return stack.Pop(); // return root node;
        }

        public double Evaluate(Node root)
        {
            if (root == null) 
                return 0;

            List<string> operands = new List<string> { "*", "/", "+", "-" };

            if (operands.Contains(root.Value))   // if operator is found, execute this block
            {
                double left = Evaluate(root.Left);      // evaluate left node
                double right = Evaluate(root.Right);    // evaluate right node

                switch (new string(root.Value.ToArray()))   // perform operation on left and right node - Displays actions taken in order
                {
                    case "*":
                        Console.WriteLine(right + " * " + left + " = " + (right * left));
                        return right * left;
                    case "/":
                        if (left == 0)
                            throw new DivideByZeroException("evaluation found a divide by zero -> " + right + " / " + left);

                        Console.WriteLine(right + " / " + left + " = " + (right / left));
                        return right / left;
                    case "+":
                        Console.WriteLine(right + " + " + left + " = " + (right + left));
                        return right + left;
                    case "-":
                        Console.WriteLine(right + " - " + left + " = " + (right - left));
                        return right - left;
                }
            }

            return double.Parse(new string(root.Value.ToArray()));  // if node is a leaf node return number found at node
        }

        public override string ToString()   // For debugging of tree
        {
            return CreateTreeString(Root);
        }

        public string CreateTreeString(Node root)   // Can create tree string dynamically based on given node of tree
        {
            if (root == null) 
                return "";

            if (root.Left == null && root.Right == null)
                return root.Value;

            string Right = CreateTreeString(root.Right);
            string Left = CreateTreeString(root.Left);

            string TreeString = Right + " " + root.Value + " " + Left;

            if (root.Value == "*" || root.Value == "/" || root == Root)
                return TreeString;

            return "(" + TreeString + ")";
        }
    }
}
