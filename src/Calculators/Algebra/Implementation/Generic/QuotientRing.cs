using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class QuotientRing<T> : IRing<T>
    {
        private readonly IIdeal<T> mIdeal;
        private readonly IRing<T> mRing;

        public QuotientRing(IIdeal<T> ideal)
        {
            mRing = ideal.Ring;
            mIdeal = ideal;
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
    }
}