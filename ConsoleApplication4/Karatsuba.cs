using System;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    public static class Karatsuba
    {
        public static int Multiplication(int firstNumber, int secondNumber)
        {
            var maximumDigits = GetMaximumDigits(firstNumber, secondNumber); // 2

            if (maximumDigits == 1)
            {
                return firstNumber * secondNumber;
            }

            var order = GetKaratsubaOrder(maximumDigits); // 10
            return KaratsubaFormula(firstNumber, secondNumber, order);
        }

        /**
         * precondition: order = 10 ^ ( n / 2 ), where n = maximum number of digits between first and second
         */
        private static int KaratsubaFormula(int firstNumber, int secondNumber, int order)
        {
            var tasks = new Task<int>[2];

            tasks[0] = Task.Run(() => GetKaratsubaTerm(firstNumber, order));
            tasks[1] = Task.Run(() => GetKaratsubaTerm(secondNumber, order));
            
            var firstTerm = tasks[0].Result;
            var secondTerm = tasks[1].Result;

            return firstTerm * secondTerm;
        }

        private static int GetKaratsubaTerm(int number, int order)
        {
            var upperPart = GetUpperPart(number, order);
            var lowerPart = GetLowerPart(number, order);

            var lowerMultiplication = GetKaratsubaSubTermMultiplication(upperPart, order);

            return lowerMultiplication + lowerPart;
        }

        private static int GetKaratsubaSubTermMultiplication(int upperPart, int order)
        {
            var upperPartDigits = GetDigitNumber(upperPart);
            if (upperPartDigits > 1)
            {
                return Multiplication(upperPart, order);
            }

            return upperPart * order;
        }

        private static int GetMaximumDigits(int firstNumber, int secondNumber)
        {
            var firstDigits = GetDigitNumber(firstNumber);
            var secondDigits = GetDigitNumber(secondNumber);

            return Maximum(firstDigits, secondDigits);
        }

        private static int GetKaratsubaOrder(int numberOfDigits)
        {
            var half = numberOfDigits / 2;
            return PowerOfTen(half);
        }

        private static int GetUpperPart(int number, int order)
        {
            return number / order;
        }

        private static int GetLowerPart(int number, int order)
        {
            return number % order;
        }

        private static int PowerOfTen(int power)
        {
            var i = 1;
            for (var _ = 0; _ < power; _++)
            {
                i *= 10;
            }

            return i;
        }

        private static int Maximum(int first, int second)
        {
            return first >= second ? first : second;
        }

        private static int GetDigitNumber(int number)
        {
            var absoluteNumber = Math.Abs(number);

            var stringNumber = absoluteNumber.ToString();
            return stringNumber.Length;
        }
    }
}