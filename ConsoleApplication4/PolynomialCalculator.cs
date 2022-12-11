using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    public static class PolynomialCalculator
    {
        private static IList<int> PrepareResultPolynomialCoefficients(int numberOfTerms)
        {
            return Enumerable.Repeat(0, numberOfTerms).ToList();
        }

        public static Polynomial SequentialMultiply(IAlgorithm algo, Polynomial first, Polynomial second)
        {
            var firstCoefficients = first.Coefficients();
            var secondCoefficients = second.Coefficients();

            var resultPolynomialDegree = first.Degree() + second.Degree();
            var resultPolynomialCoefficients = PrepareResultPolynomialCoefficients(resultPolynomialDegree + 1);

            for (var i = 0; i <= first.Degree(); i++)
            {
                for (var j = 0; j <= second.Degree(); j++)
                {
                    resultPolynomialCoefficients[i + j] += algo.Multiply(firstCoefficients[i], secondCoefficients[j]);
                }
            }

            return new Polynomial(resultPolynomialCoefficients);
        }

        public static Polynomial ParallelMultiply(IAlgorithm algo, Polynomial first, Polynomial second)
        {
            var firstCoefficients = first.Coefficients();
            var secondCoefficients = second.Coefficients();

            var resultPolynomialDegree = first.Degree() + second.Degree();
            var resultPolynomialCoefficients = PrepareResultPolynomialCoefficients(resultPolynomialDegree + 1);

            var taskArray = new Task[firstCoefficients.Count];

            for (var i = 0; i <= first.Degree(); i++)
            {
                var finalIndex = i;
                var task = Task.Run(() =>
                {
                    for (var j = 0; j <= second.Degree(); j++)
                    {
                        resultPolynomialCoefficients[finalIndex + j] +=
                            algo.Multiply(firstCoefficients[finalIndex], secondCoefficients[j]);
                    }
                });
                taskArray[i] = task;
            }

            Task.WaitAll(taskArray);

            return new Polynomial(resultPolynomialCoefficients);
        }
    }
}