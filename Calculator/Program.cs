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

            int i = 0;
            string element = "";

            Stack<char> operators = new Stack<char>();

            //Parse
            while (i < input.Length)
            {
                //числа
                if ((input[i] >= '0' && input[i] <= '9') || input[i] == '.')
                {
                    element += input[i];
                    i++;
                    continue;
                }
                //операции
                else
                {
                    input_rpn = input_rpn + element + ' '; //очистка переменной числа
                    element = "";


                    if (input[i] == '(')
                    {
                        operators.Push(input[i]);

                        i++;

                        continue;
                    }

                    if (input[i] == ')')
                    {
                        while (operators.Count != 0 && operators.Peek() != '(')
                        {
                            input_rpn = input_rpn + operators.Pop() + ' ';
                        }

                        operators.Pop();

                        i++;

                        continue;//**************добавить проверку на ошибки несогласования скобок-------------------
                    }

                    if (input[i] == '*' || input[i] == '/')
                    {
                        while (operators.Count != 0 && (operators.Peek() == '/' || operators.Peek() == '*' || operators.Peek() == '^'))
                        {
                            input_rpn = input_rpn + operators.Pop() + ' ';
                        }

                        operators.Push(input[i]);

                        i++;

                        continue;
                    }

                    if (input[i] == '+' || input[i] == '-')
                    {
                        while (operators.Count != 0 && operators.Peek() != '(')
                        {
                            input_rpn = input_rpn + operators.Pop() + ' ';
                        }

                        operators.Push(input[i]);

                        i++;

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

            for (i = 0; i < elements.Length; i++)
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

                buffer.Push(double.Parse(elements[i]));

            }

            double result = buffer.Pop();

            Console.WriteLine(result);

        }
    }
}
