namespace ConsoleApplication4
{
    public interface IAlgorithm
    {
        int Multiply(int first, int second);
    }

    public class SimpleMultiplication : IAlgorithm
    {
        public int Multiply(int first, int second)
        {
            return first * second;
        }
    }

    public class KaratsubaMultiplication : IAlgorithm
    {
        public int Multiply(int first, int second)
        {
            return Karatsuba.Multiplication(first, second);
        }
    }
}