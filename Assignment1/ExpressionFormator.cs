using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    internal class ExpressionFormator
    {
        public string InfixToPostfix(string infix)
        {
            StringBuilder postfix = new StringBuilder();
            Stack<char> stack = new Stack<char>();
            List<char> operators = new List<char> { '*', '/', '+', '-' };

            for (int i = 0; i < infix.Length; i++)
            {
                if (infix[i] == ' ') continue;  // function ignores whitespaces in expression bewteen operators and operands

                bool isPositive = true;
                if (                                // checking for negatives
                    infix[i] == '-'
                    && Char.IsDigit(infix[i + 1])
                )
                {
                    isPositive = false;
                }

                if (operators.Contains(infix[i]) && isPositive)     // checking for operators
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(infix[i]);
                        continue;
                    }

                    while (SetPriority(stack.Peek()) >= SetPriority(infix[i]))  // keep popping stack until latest item in stack has a lower priority than checked item
                    {
                        char popped = stack.Pop();
                        postfix.Append(popped + " ");
                        if (stack.Count == 0) break;
                    }

                    stack.Push(infix[i]);
                    continue;
                }
            }

            return "";
        }

        public int SetPriority(char symbol)
        {
            switch (symbol)
            {
                case '^': return 3;
                case '*': return 2;
                case '/': return 2;
                case '+': return 1;
                case '-': return 1;
            }

            return 0;
        }
    }
}
