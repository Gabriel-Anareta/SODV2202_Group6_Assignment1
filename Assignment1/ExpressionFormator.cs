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
                bool isPositive = true;
                if (                                // checking for negatives
                    infix[i] == '-'
                    && Char.IsDigit(infix[i + 1])
                )
                {
                    isPositive = false;
                }
            }

            return "";
        }
    }
}
