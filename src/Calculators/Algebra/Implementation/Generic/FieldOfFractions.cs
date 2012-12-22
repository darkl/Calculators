using System;
using System.Collections.Generic;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class FieldOfFractions<T> : IField<Fraction<T>>, IRingEmbedding<T, Fraction<T>>
    {
        private readonly IEqualityComparer<Fraction<T>> mComparer;
        private readonly IRing<T> mRing;

        public FieldOfFractions(IRing<T> ring)
        {
            mRing = ring;
            mComparer = new ItemComparer(this);
        }

        public Fraction<T> Add(Fraction<T> x, Fraction<T> y)
        {
            T numerator =
                mRing.Add(mRing.Multiply(x.Numerator, y.Denominator),
                          mRing.Multiply(y.Numerator, x.Denominator));

            T denominator = mRing.Multiply(x.Denominator, y.Denominator);

            return mRing.CreateFraction(numerator, denominator);
        }

        public Fraction<T> Negative(Fraction<T> x)
        {
            return mRing.CreateFraction(mRing.Negative(x.Numerator),
                                        x.Denominator);
        }

        public Fraction<T> Zero
        {
            get { return mRing.CreateFraction(mRing.Zero, mRing.Identity); }
        }

        public Fraction<T> Multiply(Fraction<T> x, Fraction<T> y)
        {
            T numerator = mRing.Multiply(x.Numerator, y.Numerator);
            T denominator = mRing.Multiply(x.Denominator, y.Denominator);

            return mRing.CreateFraction(numerator, denominator);
        }

        public Fraction<T> Identity
        {
            get { return mRing.CreateFraction(mRing.Identity, mRing.Identity); }
        }

        public Fraction<T> Inverse(Fraction<T> x)
        {
            if (mRing.Comparer.Equals(x.Numerator, mRing.Zero))
            {
                throw new DivideByZeroException();
            }

            T numerator = x.Denominator;
            T denominator = x.Numerator;

            return mRing.CreateFraction(numerator, denominator);
        }

        public IEqualityComparer<Fraction<T>> Comparer
        {
            get
            {
                return mComparer;
            }
        }

        #region Item Comparer

        private class ItemComparer : IEqualityComparer<Fraction<T>>
        {
            private readonly FieldOfFractions<T> mField;

            public ItemComparer(FieldOfFractions<T> field)
            {
                mField = field;
            }

            public bool Equals(Fraction<T> x, Fraction<T> y)
            {
                Fraction<T> normalizedX =
                    mField.mRing.CreateFraction(x.Numerator, x.Denominator);

                Fraction<T> normalizedY =
                    mField.mRing.CreateFraction(y.Numerator, y.Denominator);

                return
                    (mField.mRing.Comparer.Equals(normalizedX.Numerator, normalizedY.Numerator) &&
                     mField.mRing.Comparer.Equals(normalizedX.Denominator, normalizedY.Denominator)) ||
                    // If the ring isn't euclidean, then we use an alternative defintion
                    // of equality.
                    mField.mRing.Comparer.Equals
                        (mField.mRing.Multiply(normalizedX.Numerator, normalizedY.Denominator),
                         mField.mRing.Multiply(normalizedY.Numerator, normalizedX.Denominator));
            }

            public int GetHashCode(Fraction<T> obj)
            {
                Fraction<T> normalizedX =
                    mField.mRing.CreateFraction(obj.Numerator, obj.Denominator);

                return mField.mRing.Comparer.GetHashCode(normalizedX.Numerator) ^
                       mField.mRing.Comparer.GetHashCode(normalizedX.Denominator);
            }
        }

        #endregion

        public IRing<T> Subring
        {
            get
            {
                return mRing;
            }
        }

        public Fraction<T> Embed(T element)
        {
            return mRing.CreateFraction(element, mRing.Identity);
        }
    }
}