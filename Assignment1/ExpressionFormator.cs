﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    internal class ExpressionFormator
    {
        public class InvalidFormatException : Exception
        {
            public InvalidFormatException(string message) : base(message) { }
        }

        public string InfixToPostfix(string infix)
        {
            StringBuilder postfix = new StringBuilder();
            Stack<char> stack = new Stack<char>();
            List<char> operators = new List<char> { '*', '/', '+', '-' };

            bool prevIsOperator = false;    // distinguishes minus and negative operators

            for (int i = 0; i < infix.Length; i++)
            {
                if (infix[i] == ' ') continue;  // function ignores whitespaces in expression bewteen operators and operands

                bool isPositive = true;
                if (                                // checking for negatives
                    infix[i] == '-'
                    && Char.IsDigit(infix[i + 1])
                    && prevIsOperator
                )
                {
                    isPositive = false;
                }

                if (operators.Contains(infix[i]) && isPositive)     // checking for operators
                {
                    prevIsOperator = true;

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

                prevIsOperator = false;

                if (infix[i] == '(')
                {
                    stack.Push(infix[i]);
                    continue;
                }

                if (infix[i] == ')')
                {
                    while (stack.Peek() != '(')     // keep popping stack until space between () is empty
                    {
                        postfix.Append(stack.Pop() + " ");
                    }

                    stack.Pop();
                    continue;
                }

                int counter = 0;
                while (                                 // pushing number to postfix - makes exeptions for decimal places and negative values
                    Char.IsDigit(infix[i + counter])
                    || infix[i + counter] == '.'
                    || (infix[i + counter] == '-' && counter == 0)
                )
                {
                    postfix.Append(infix[i + counter++]);
                    if (i + counter == infix.Length) break;
                }

                postfix.Append(" ");

                i += counter - 1;
            }

            while (stack.Count > 0)   // after infix has been looped through, push all remaining operations in stack
            {
                postfix.Append(stack.Pop() + " ");
            }

            return postfix.ToString().Trim();
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

        public void InfixCheck(string infix)
        {
            List<char> operators = new List<char> { '*', '/', '+', '-' };

            bool prevIsOperator = false;    // distinguishes minus and negative operators
            bool prevIsNumber = false;      // records previous type

            for (int i = 0; i < infix.Length; i++)
            {
                if (infix[i] == ' ') continue;  // function ignores whitespaces in expression bewteen operators and operands

                bool isPositive = true;
                if (                                // checking for negatives
                    infix[i] == '-'
                    && Char.IsDigit(infix[i + 1])
                    && prevIsOperator
                )
                {
                    isPositive = false;
                }

                if (operators.Contains(infix[i]) && isPositive)     // checking for operators
                {
                    prevIsOperator = true;
                    prevIsNumber = false;

                    bool foundNextChar = true;
                    int nextCharCounter = 0;
                    while (foundNextChar)   // check for empty space in front of operators
                    {
                        if (infix[i + nextCharCounter] != ' ')
                        {
                            if (infix[i + nextCharCounter] == ')')
                                throw new InvalidFormatException("operations cannot be directly followed by )");
                            
                            foundNextChar = false;
                        }
                        else
                        {
                            continue;
                        }
                            
                        nextCharCounter++;
                        if (i + nextCharCounter == infix.Length)
                            throw new InvalidFormatException("operations must be followed by an operand");

                        i += nextCharCounter - 1;
                    }

                    int counter = 0;
                    while (true)
                    {
                        if (infix[i + counter] == ' ')
                            continue;

                        if (                                // checking for negatives
                            infix[i] == '-'
                            && Char.IsDigit(infix[i + counter + 1])
                        )
                        {
                            isPositive = false;
                        }

                        if (operators.Contains(infix[i + counter]) && !isPositive)
                            throw new InvalidFormatException("operators must be followed by an operand");

                        if (
                            infix[i + counter + 1] == '0'
                            && infix[i] == '/'
                        )
                        {
                            throw new InvalidFormatException("Cannot divide by zero");
                        }

                        counter++;
                        if (i + counter == infix.Length)
                            break;
                    }

                    i += counter - 1;
                    continue;
                }

                int parenthesisCounter = 0;
                if (infix[i] == '(')
                {
                    if (parenthesisCounter >= 0)
                        parenthesisCounter++;

                    if (prevIsNumber) 
                        throw new InvalidFormatException("( must follow an operator");
                }

                if (infix[i] == ')')
                {
                    parenthesisCounter--;

                    if (prevIsOperator)
                        throw new InvalidFormatException(") must follow a number");
                }

                if (parenthesisCounter < 0)
                {
                    throw new InvalidFormatException(") must be preceded by a (");
                }

                int indexCounter = 0;
                int decimalCounter = 0;
                while (                                 // checking number values
                    Char.IsDigit(infix[i + indexCounter])
                    || infix[i + indexCounter] == '.'
                    || (infix[i + indexCounter] == '-' && indexCounter == 0)
                )
                {
                    if (infix[i + indexCounter] == '.') 
                        decimalCounter += 1;

                    if (decimalCounter > 1)
                        throw new InvalidFormatException("Numbers can only have 1 decimal point");

                    if (i + indexCounter == infix.Length)
                        break;
                }

                i += indexCounter - 1;
                prevIsOperator = false;
                prevIsNumber = true;
            }
        }
    }
}
