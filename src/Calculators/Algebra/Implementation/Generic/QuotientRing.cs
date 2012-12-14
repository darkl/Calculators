using System.Collections.Generic;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class QuotientRing<T> : IRing<T>, IRingEmbedding<T,T>
    {
        private readonly IIdeal<T> mIdeal;
        private readonly IRing<T> mRing;
        private readonly IEqualityComparer<T> mComparer;

        public QuotientRing(IIdeal<T> ideal)
        {
            mRing = ideal.Ring;
            mIdeal = ideal;
            mComparer = new QuotientRing<T>.ItemComparer(this);
        }

        public T Add(T x, T y)
        {
            return mIdeal.Modulo(mRing.Add(x, y));
        }

        public T Negative(T x)
        {
            return mIdeal.Modulo(mRing.Negative(x));
        }

        public T Zero
        {
            get
            {
                return mIdeal.Modulo(mRing.Zero);
            }
        }

        public T Multiply(T x, T y)
        {
            return mIdeal.Modulo(mRing.Multiply(x, y));
        }

        public T Identity
        {
            get
            {
                return mIdeal.Modulo(mRing.Identity);
            }
        }

        public IEqualityComparer<T> Comparer
        {
            get
            {
                return mComparer;
            }
        }

        #region Comparer Implementation

        private class ItemComparer : IEqualityComparer<T>
        {
            private readonly QuotientRing<T> mRing;

            public ItemComparer(QuotientRing<T> ring)
            {
                mRing = ring;
            }

            public bool Equals(T x, T y)
            {
                return mRing.mRing.Comparer.Equals
                    (mRing.mIdeal.Modulo(x),
                     mRing.mIdeal.Modulo(y));
            }

            public int GetHashCode(T obj)
            {
                return mRing.mRing.Comparer.GetHashCode(mRing.mIdeal.Modulo(obj));
            }
        }

        #endregion

        // This is really wrong, since a quotient ring is almost never a super ring
        // of its creating ring, but I need to think about how to treat this right
        // so parse will work.
        public IRing<T> Subring
        {
            get
            {
                return mRing;
            }
        }

        public T Embed(T element)
        {
            return mIdeal.Modulo(element);
        }
    }
}