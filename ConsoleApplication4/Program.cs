using System;
using System.Diagnostics;

namespace ConsoleApplication4
{
    internal static class Program
    {
        private const int Degree = 1000;
        private const int MaxCoeff = 1000;

        private static readonly Polynomial First = GetRandomPolynomial();
        private static readonly Polynomial Second = GetRandomPolynomial();

        private static readonly IAlgorithm Simple = new SimpleMultiplication();
        private static readonly IAlgorithm Karatsuba = new KaratsubaMultiplication();

        public static void Main()
        {
            SimpleSequential();
            SimpleParallel();
            KaratsubaSequential();
            KaratsubaParallel();
        }

        private static Polynomial GetRandomPolynomial()
        {
            return Polynomial.CreateRandomPolynomial(Degree, MaxCoeff);
        }

        private static void SimpleSequential()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var simpleSequential = PolynomialCalculator.SequentialMultiply(Simple, First, Second);
            Console.WriteLine(simpleSequential);
            
            stopwatch.Stop();
            Console.WriteLine("Simple Sequential took {0} ms", stopwatch.ElapsedMilliseconds);
        }

        private static void SimpleParallel()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var simpleParallel = PolynomialCalculator.ParallelMultiply(Simple, First, Second);
            Console.WriteLine(simpleParallel);
            
            stopwatch.Stop();
            Console.WriteLine("Simple Parallel took {0} ms", stopwatch.ElapsedMilliseconds);
        }

        private static void KaratsubaSequential()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var karatsubaSequential = PolynomialCalculator.SequentialMultiply(Karatsuba, First, Second);
            Console.WriteLine(karatsubaSequential);
            
            stopwatch.Stop();
            Console.WriteLine("Karatsuba Sequential took {0} ms", stopwatch.ElapsedMilliseconds);
        }

        private static void KaratsubaParallel()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var karatsubaParallel = PolynomialCalculator.ParallelMultiply(Karatsuba, First, Second);
            Console.WriteLine(karatsubaParallel);
            
            stopwatch.Stop();
            Console.WriteLine("Karatsuba Parallel took {0} ms", stopwatch.ElapsedMilliseconds);
        }
    }
}