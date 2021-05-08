using System;
using NUnit.Framework;

namespace Lab3
{
    public class CramerTests
    {
        [Test]
        public void TestCramersRuleWithSolution()
        {
            var data = new double[,] {{506, 66}, {66, 11}};
            var answer = new[] {2315.1, 392.3};
            var m = new Matrix(data);
            var cramer = new CramersRule();
            cramer.FindSolution(m, answer);
            Assert.False(!cramer.IsSolution);
            var x = cramer.Solution;
            
            for (var i = 0; i < m.N; i++)
            {
                var t = 0.0;
                for (var j = 0; j < m.N; j++)
                {
                    t += Math.Round(m[i, j] * x[j], 1);
                }

                Assert.AreEqual(t, answer[i]);
            }
        }

        [Test]
        public void TestCramersRuleWithoutSolution()
        {
            var data = new double[,] {{1, 1}, {1, 1}};
            var answer = new double[] {2, 465};
            var m = new Matrix(data);
            var cramer = new CramersRule();
            cramer.FindSolution(m, answer);
            Assert.True(cramer.IsNoSolution);
            Assert.True(cramer.Solution == null);
            
        }
        
        [Test]
        public void TestCramersRuleWithEndlessSolution()
        {
            var data = new double[,] {{1, 1}, {1, 1}};
            var answer = new double[] {2, 2};
            var m = new Matrix(data);
            var cramer = new CramersRule();
            cramer.FindSolution(m, answer);
            Assert.True(cramer.IsEndlessSolution);
            Assert.True(cramer.Solution == null);
            
        }
        
        [Test]
        public void TestCramersRuleWithNonSquaredMatrix()
        {
            var data = new double[,] {{1, 1}, {1, 1}, {2, 2}};
            var answer = new double[] {2, 2};
            var m = new Matrix(data);
            var exception = Assert.Throws<ArgumentException>(() => { new CramersRule().FindSolution(m, answer); });
            if (exception != null)
                Assert.AreEqual("Matrix A must be squared", exception.Message);
        }
        
        [Test]
        public void TestCramersRuleWithWrongBSize()
        {
            var data = new double[,] {{1, 1}, {1, 1}};
            var answer = new double[] {2, 2, 423, 34};
            var m = new Matrix(data);
            var exception = Assert.Throws<ArgumentException>(() => { new CramersRule().FindSolution(m, answer); });
            if (exception != null)
                Assert.AreEqual("B's size must be matrix's size", exception.Message);
        }
    }
}