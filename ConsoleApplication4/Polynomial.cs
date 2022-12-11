using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication4
{
    public class Polynomial
    {
        private readonly IList<int> _coefficients;
        private readonly int _degree;

        private static readonly Random Rand = new Random(DateTime.UtcNow.GetHashCode());

        public Polynomial(IList<int> coefficients)
        {
            _coefficients = coefficients;
            _degree = coefficients.Count - 1;
        }

        public IList<int> Coefficients() => _coefficients;
        public int Degree() => _degree;

        public static Polynomial CreateRandomPolynomial(int degree, int maxCoeff)
        {
            IList<int> coefficients = new List<int>();

            for (var _ = 0; _ <= degree; _++) // degree + 1 elements in poly
            {
                var coeff = Rand.Next(maxCoeff);
                coefficients.Add(coeff);
            }

            return new Polynomial(coefficients);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            for (var i = 0; i <= _degree; i++)
            {
                builder.Append(_coefficients[i]);

                if (i > 0)
                {
                    builder.Append("*X");
                    builder.Append("^");
                    builder.Append(i);
                }

                if (i < _degree)
                {
                    builder.Append(" + ");
                }
            }

            return builder.ToString();
        }
    }
}