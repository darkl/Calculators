using System;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    /// <remarks>
    /// There is no easy way to ensure this is a field.
    /// </remarks>
    public class QuotientField<T> : QuotientRing<T>, IField<T>
    {
        private readonly IEuclideanRing<T> mRing;
        private readonly T mGenerator;
        private readonly IPrincipalIdeal<T> mIdeal;

        public QuotientField(IPrincipalIdeal<T> ideal) : base(ideal)
        {
            IEuclideanRing<T> ring = ideal.Ring as IEuclideanRing<T>;

            if (ring == null)
            {
                throw new ArgumentException("Expected an euclidean ring", "ideal");
            }

            mIdeal = ideal;
            mRing = ring;
            mGenerator = ideal.Generator;
        }

        public T Inverse(T x)
        {
            T gcd;
            Tuple<T, T> coefficients = mRing.ExtendedEuclideanAlgorithm(x, mGenerator, out gcd);

            // Sometimes the gcd isn't one, but a different unit, such as -1.
            T zero;
            T unit = mRing.Divide(mRing.Identity, gcd, out zero);

            if (!mRing.Comparer.Equals(zero, mRing.Zero))
            {
                throw new DivideByZeroException("The current element doesn't have an inverse, " +
                                                "this might mean a divison by zero was attempted, " +
                                                "or that the current ring isn't a field.");
            }

            return this.mIdeal.Modulo(this.mRing.Multiply(unit, coefficients.Item1));
        }
    }
}