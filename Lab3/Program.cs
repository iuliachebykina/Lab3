using System;
using System.Linq;

namespace Lab3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            for (int i = 1; i < 15; i++)
            {
                var m = new Matrix(i);
                var det = m.GetDeterminant();
                Console.WriteLine(Matrix.Count);
            }
        }

        public void client()
        {
            Console.WriteLine("Введите размер квадратной матрицы A: ");
            int n;
            try
            {
                n = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Размер матрицы должен быть целым положительным числом");
                throw;
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
                Console.WriteLine("Возможно, вы неправильно ввели матрицу");
                throw;
            }

            Console.WriteLine("Введите через пробел элементы вектора B");
            var b = (double[]) Console.ReadLine()?.Split(' ').Select(t => Convert.ToDouble(t));

            var a = new Matrix(data);
            var cramer = new CramersRule();
            cramer.FindSolution(a, b);
            if (cramer.IsSolution)
            {
                var result = cramer.Solution;
                Console.Write("X = ");
                Console.Write("(");
                foreach (var e in result)
                {
                    Console.Write(e + " ");
                }

                Console.Write(")");
            }
            else if (cramer.IsEndlessSolution)
            {
                Console.WriteLine("СЛАУ имеет бесконечно много решений");
            }
            else if (cramer.IsNoSolution)
            {
                Console.WriteLine("СЛАУ не имеет решений");
            }
        }
    }
}