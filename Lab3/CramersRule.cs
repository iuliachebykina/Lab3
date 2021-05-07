using System;

namespace Lab3
{
    public class CramersRule
    {
        public bool IsEndlessSolution { get; private set; }
        public bool IsNoSolution { get; private set; }
        public bool IsSolution { get; private set; }
        public double[] Solution;

        public void FindSolution(Matrix a, double[] b)
        {
            IsEndlessSolution = false;
            IsNoSolution = false;
            IsSolution = true;
            Solution = new double[a.N];
            if (!a.IsSquare)
            {
                throw new ArgumentException("Matrix A must be squared");
            }

            if (b.Length != a.N)
            {
                throw new ArgumentException("B's size must be matrix's size");
            }

            var det = a.GetDeterminant();
            if (det == 0)
            {
                IsSolution = false;
            }

            var temp = a.Copy();
            for (var j = 0; j < a.N; j++)
            {
                for (var i = 0; i < a.N; i++)
                    temp[i, j] = b[i];
                Solution[j] = temp.GetDeterminant();
                if (det == 0)
                {
                    if (Solution[j] == 0)
                    {
                        IsEndlessSolution = true;
                        IsNoSolution = false;
                    }
                    else
                    {
                        Solution[j] /= det;
                        IsEndlessSolution = false;
                        IsNoSolution = true;
                    }
                }
                else
                {
                    Solution[j] /= det;
                }

                for (var i = 0; i < a.N; i++)
                    temp[i, j] = a[i, j];
            }
        }
    }
}