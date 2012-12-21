using System;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public static class EuclideanRingExtensions
    {
        public static T Gcd<T>(this IEuclideanRing<T> ring, T first, T second)
        {
            T gcd;
            Tuple<T, T> coefficients = ExtendedEuclideanAlgorithm(ring, first, second, out gcd);

            return gcd;
        }

        public static Tuple<T, T> ExtendedEuclideanAlgorithm<T>(this IEuclideanRing<T> ring, T first, T second, out T gcd)
        {
            var dividend = first;
            var divisor = second;

            if (ring.Degree(dividend) < ring.Degree(divisor))
            {
                dividend = second;
                divisor = first;
            }

            Tuple<T, T> ordered = InnerExtendedEuclideanAlgorithm(ring, dividend, divisor, out gcd);

            if (ring.Degree(first) < ring.Degree(second))
            {
                return Tuple.Create(ordered.Item2, ordered.Item1);
            }

            return ordered;
        }

        /// <remarks>
        /// A_i * r_i + B_i * r_(i + 1) = d
        /// 
        /// a = q0 * b + r0
        /// b = q1 * r0 + r1
        /// r0 = q2 * r1 + r2
        /// r1 = q3 * r2 + r3
        /// ...
        /// r_(i - 1) = q_(i + 1) * r_i + r_(i + 1)
        /// ...
        /// r_k = q_(k + 2) * r_(k + 1) + d
        /// r_(k + 1) = r_(k + 3) * d 
        /// ----
        /// r_(i - 1) - q_(i + 1) * r_i = r_(i + 1)
        /// A_i * r_i + B_i * r_(i + 1) = d
        /// 
        /// A_i * r_i + B_i * ( r_(i - 1) - q_(i + 1) * r_i ) = d
        /// B_i * r_(i - 1) + ( A_i - B_i * q_(i + 1) ) * r_i = d
        /// 
        /// A_(i - 1) = B_i
        /// B_(i - 1) = A_i - B_i * q_(i + 1)
        /// q_(i + 1) =  r_(i - 1) / r_i
        /// </remarks>
        private static Tuple<T, T> InnerExtendedEuclideanAlgorithm<T>(IEuclideanRing<T> ring, T dividend, T divisor, out T gcd)
        {
            T remainder;
            T quotient = ring.Divide(dividend, divisor, out remainder);

            if (ring.Comparer.Equals(remainder, ring.Zero))
            {
                gcd = divisor;
                return Tuple.Create(ring.Zero, ring.Identity);
            }

            Tuple<T, T> recursiveResult = InnerExtendedEuclideanAlgorithm(ring, divisor, remainder, out gcd);
            
            return Tuple.Create(recursiveResult.Item2,
                                ring.Add(recursiveResult.Item1,
                                         ring.Negative(ring.Multiply(recursiveResult.Item2, quotient))));
        }
    }
}