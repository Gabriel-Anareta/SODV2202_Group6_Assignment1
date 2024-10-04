using System;
using System.Collections.Generic;

namespace Assignment1
{
    // TODO Add supporting classes here

    public class Program
    {
        public static string ProcessCommand(string input)
        {
            try
            {
                // TODO Evaluate the expression and return the result

                ExpressionFormator expForm = new ExpressionFormator();
                ExpressionTree expTree = new ExpressionTree();

                expForm.InfixCheck(input);
                string postfix = expForm.InfixToPostfix(input);

                expTree.SetRoot(postfix);
                double eval = expTree.Evaluate(expTree.Root);
                eval = Math.Round(eval * 100) / 100;

                return eval.ToString();
            }
            catch (Exception e)
            {
                return "Error evaluating expression: " + e;
            }
        }

        static void Main(string[] args)
        {
            string input;
            while ((input = Console.ReadLine()) != "exit")
            {
                Console.WriteLine(ProcessCommand(input));
            }
        }
    }
}
