using System;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public static class FractionExtensions
    {
        public static Fraction<T> CreateFraction<T>(this IRing<T> ring,
                                                    T numerator,
                                                    T denominator)
        {
            if (ring.Comparer.Equals(denominator, ring.Zero))
            {
                throw new DivideByZeroException();
            }

            IEuclideanRing<T> euclideanRing = ring as IEuclideanRing<T>;

            if (euclideanRing != null)
            {
                T gcd = euclideanRing.Gcd(numerator, denominator);

                T zero;
                numerator = euclideanRing.Divide(numerator, gcd, out zero);

                denominator = euclideanRing.Divide(denominator, gcd, out zero);
            }

            return new Fraction<T>(numerator, denominator);
        }
    }
}