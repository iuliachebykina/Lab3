using System;
using System.Linq;
using System.Runtime.Remoting.Lifetime;

namespace Lab3
{
    internal class Program
    {
        public static void Main()
        {
            Client();
        }

        private static void Client()
        {
            Console.WriteLine("Решение системы линейных уравнений A*x=B методом Крамера");
            Console.WriteLine("Введите размер квадратной матрицы A: ");
            int n;
            try
            {
                n = Convert.ToInt32(Console.ReadLine());
                if (n <= 0)
                    throw new Exception();
            }
            catch (Exception)
            {
                Console.WriteLine("Размер матрицы должен быть целым положительным числом");
                Console.ReadLine();
                return;
            }

            var data = new double[n, n];
            Console.WriteLine("Вводите построчно элементы матрицы A через пробел: ");
            try
            {
                for (var i = 0; i < n; i++)
                {
                    var numbers = Console.ReadLine()?.Split(' ');
                    for (var j = 0; j < n; j++)
                    {
                        if (numbers != null) data[i, j] = Convert.ToDouble(numbers[j]);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Матрица была введена неправильно");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Введите через пробел элементы вектора B");
            double[] b;
            try
            {
                b = Console.ReadLine()?.Split(' ').Select(Convert.ToDouble).ToArray();
            }
            catch (Exception)
            {
                Console.WriteLine("Вектор B был введен неправильно");
                Console.ReadLine();
                return;
            }

            var a = new Matrix(data);
            var cramer = new CramersRule();
            cramer.FindSolution(a, b);
            if (cramer.IsSolution)
            {
                var result = cramer.Solution;
                Console.Write("X = ");
                Console.Write("(");

                for (var i = 0; i < result.Length; i++)
                {
                    Console.Write(result[i]);
                    if (i != result.Length - 1)
                        Console.Write(" ");
                }

                Console.WriteLine(")");
            }
            else if (cramer.IsEndlessSolution)
            {
                Console.WriteLine("СЛУ имеет бесконечно много решений");
            }
            else if (cramer.IsNoSolution)
            {
                Console.WriteLine("СЛУ не имеет решений");
            }

            Console.ReadLine();
        }
    }
}