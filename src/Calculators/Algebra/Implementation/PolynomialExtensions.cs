using System.Linq;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public static class PolynomialExtensions
    {
        public static Polynomial<T> CreateMonomial<T>(this IRing<T> coefficientsRing,
                                                   T leadingCoefficient,
                                                   int degree)
        {
            T[] coefficients = Enumerable.Repeat(coefficientsRing.Zero, degree + 1).ToArray();
            coefficients[degree] = leadingCoefficient;
            return new Polynomial<T>(coefficients, coefficientsRing);
        }

        public static T LeadingCoefficient<T>(this Polynomial<T> polynomial)
        {
            return polynomial[polynomial.Degree];
        }
    }
}