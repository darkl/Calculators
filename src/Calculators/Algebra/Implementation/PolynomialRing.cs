using System;
using System.Collections.Generic;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class PolynomialRing<T> : IRing<Polynomial<T>>
    {
        private readonly IRing<T> mCoefficientsRing;
        private readonly IEqualityComparer<Polynomial<T>> mComparer;

        public PolynomialRing(IRing<T> coefficientsRing)
        {
            mCoefficientsRing = coefficientsRing;
            mComparer = new ItemComparer(this);
        }

        public Polynomial<T> Add(Polynomial<T> x, Polynomial<T> y)
        {
            // This is reference equality until we think of a better way doing this.
            if ((x.CoefficientsRing != mCoefficientsRing) || (y.CoefficientsRing != mCoefficientsRing))
            {
                throw new ArgumentException("Polynomials must be over the current ring's coefficients ring");
            }

            IRing<T> ring = mCoefficientsRing;

            int suggestedDegree =
                Math.Max(x.Degree, y.Degree);

            T[] coefficients = new T[suggestedDegree + 1];

            for (int i = 0; i <= suggestedDegree; i++)
            {
                coefficients[i] = ring.Add(x[i], y[i]);
            }

            return new Polynomial<T>(coefficients, ring);
        }

        public Polynomial<T> Negative(Polynomial<T> x)
        {
            if (x.CoefficientsRing != mCoefficientsRing)
            {
                throw new ArgumentException("Polynomials must be over the current ring's coefficients ring");
            }

            IRing<T> ring = x.CoefficientsRing;

            T[] coefficients = new T[x.Degree + 1];

            for (int i = 0; i <= x.Degree; i++)
            {
                coefficients[i] = ring.Negative(x[i]);
            }

            return new Polynomial<T>(coefficients, ring);
        }

        public Polynomial<T> Zero
        {
            get
            {
                return new Polynomial<T>(new T[0], mCoefficientsRing);
            }
        }

        public Polynomial<T> Multiply(Polynomial<T> x, Polynomial<T> y)
        {
            // This is reference equality until we think of a better way doing this.
            if ((x.CoefficientsRing != mCoefficientsRing) || (y.CoefficientsRing != mCoefficientsRing))
            {
                throw new ArgumentException("Polynomials must be over the current ring's coefficients ring");
            }

            int suggestedDegree = x.Degree + y.Degree;

            T[] coefficients = new T[suggestedDegree + 1];

            for (int i = 0; i <= suggestedDegree; i++)
            {
                T currentCofficient = mCoefficientsRing.Zero;

                for (int j = 0; j <= i; j++)
                {
                    currentCofficient =
                        mCoefficientsRing.Add(currentCofficient,
                                              mCoefficientsRing.Multiply(x[j], y[i - j]));
                }

                coefficients[i] = currentCofficient;
            }

            return new Polynomial<T>(coefficients, mCoefficientsRing);
        }

        public Polynomial<T> Identity
        {
            get
            {
                return new Polynomial<T>(new[] {mCoefficientsRing.Identity}, mCoefficientsRing);
            }
        }

        public IEqualityComparer<Polynomial<T>> Comparer
        {
            get
            {
                return mComparer;
            }
        }

        #region Comparer Implementation

        private class ItemComparer : IEqualityComparer<Polynomial<T>>
        {
            private readonly PolynomialRing<T> mRing;

            public ItemComparer(PolynomialRing<T> ring)
            {
                mRing = ring;
            }

            public bool Equals(Polynomial<T> x, Polynomial<T> y)
            {
                if (x.Degree != y.Degree)
                {
                    return false;
                }

                for (int i = 0; i <= x.Degree; i++)
                {
                    if (!mRing.mCoefficientsRing.Comparer.Equals(x[i], y[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            public int GetHashCode(Polynomial<T> obj)
            {
                int result = 0;

                for (int i = 0; i <= obj.Degree; i++)
                {
                    result ^= mRing.mCoefficientsRing.Comparer.GetHashCode(obj[i]);
                }

                return result;
            }
        }

        #endregion
    }
}