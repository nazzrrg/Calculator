using System;
using System.Collections.Generic;

namespace Calculator
{
    class Program
    {
        static void Main()
        {
            string input = Console.ReadLine(); //input
            input = input.Replace(" ", "");//привод строки к виду

            string input_rpn = "";

            string element = "";

            Stack<char> operators = new Stack<char>();

            //Parse
            for (int i = 0; i < input.Length; i++)
            {
                //числа
                if ((input[i] >= '0' && input[i] <= '9') || input[i] == '.')
                {
                    element += input[i];
                    
                    continue;
                }
                //операции
                else
                {
                    input_rpn = input_rpn + element + ' '; //очистка переменной числа
                    element = "";

                    if (input[i] == '-' && (i == 0 || input[i - 1] == '('))
                    {
                        input_rpn += "0 ";

                        while (operators.Count != 0 && operators.Peek() != '(')
                        {
                            input_rpn = input_rpn + operators.Pop() + ' ';
                        }

                        operators.Push(input[i]);

                        continue;
                    }

                    if (input[i] == '(')
                    {
                        operators.Push(input[i]);

                        continue;
                    }

                    if (input[i] == ')')
                    {
                        while (operators.Count != 0 && operators.Peek() != '(')
                        {
                            input_rpn = input_rpn + operators.Pop() + ' ';
                        }

                        operators.Pop();

                        continue;//**************добавить проверку на ошибки несогласования скобок-------------------
                    }

                    if (input[i] == '*' || input[i] == '/')
                    {
                        while (operators.Count != 0 && (operators.Peek() == '/' || operators.Peek() == '*' || operators.Peek() == '^'))
                        {
                            input_rpn = input_rpn + operators.Pop() + ' ';
                        }

                        operators.Push(input[i]);

                        continue;
                    }

                    if (input[i] == '+' || input[i] == '-')
                    {
                        while (operators.Count != 0 && operators.Peek() != '(')
                        {
                            input_rpn = input_rpn + operators.Pop() + ' ';
                        }

                        operators.Push(input[i]);

                        continue;
                    }

                    if (input[i] == '^')
                    {
                        /*
                        while (operators.Count != 0 && (operators.Peek() == префиксные функции))
                        {
                            input_rpn = input_rpn + operators.Pop() + ' ';
                        }
                        */
                        operators.Push(input[i]);

                        continue;
                    }

                }



            }

            input_rpn = input_rpn + element + ' ';

            while (operators.Count != 0)
            {
                input_rpn = input_rpn + operators.Peek() + ' ';
                operators.Pop();
            }



            //Calculate:

            Stack<double> buffer = new Stack<double>();

            string[] elements = input_rpn.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < elements.Length; i++)
            {
                if (buffer.Count >= 2 && elements[i] == "+")
                {
                    double b = buffer.Pop();
                    double a = buffer.Pop();

                    buffer.Push(a + b);

                    continue;
                }

                if (buffer.Count >= 2 && elements[i] == "-")
                {
                    double b = buffer.Pop();
                    double a = buffer.Pop();

                    buffer.Push(a - b);

                    continue;
                }

                if (buffer.Count >= 2 && elements[i] == "*")
                {
                    double b = buffer.Pop();
                    double a = buffer.Pop();

                    buffer.Push(a * b);

                    continue;
                }

                if (buffer.Count >= 2 && elements[i] == "/")
                {
                    double b = buffer.Pop();
                    double a = buffer.Pop();

                    buffer.Push(a / b);

                    continue;
                }

                if (buffer.Count >= 2 && elements[i] == "^")
                {
                    double b = buffer.Pop();
                    double a = buffer.Pop();

                    buffer.Push(Math.Pow(a, b));

                    continue;
                }

                buffer.Push(double.Parse(elements[i]));

            }

            double result = buffer.Pop();

            Console.WriteLine(result);

        }
    }
}
