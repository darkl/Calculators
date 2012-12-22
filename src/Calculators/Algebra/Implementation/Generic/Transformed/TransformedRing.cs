using System.Collections.Generic;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class TransformedRing<TOriginal, TElement> : IRing<TElement>, IRingEmbedding<TOriginal, TElement>
    {
        private readonly IRing<TOriginal> mRing;
        private readonly IBijection<TOriginal, TElement> mConvert;
        private readonly ItemComparer mComparer;

        public TransformedRing(IRing<TOriginal> ring, IBijection<TOriginal, TElement> convert)
        {
            mRing = ring;
            mConvert = convert;
            mComparer = new ItemComparer(this);
        }

        public TElement Add(TElement x, TElement y)
        {
            return mConvert.ConvertFrom(mRing.Add(mConvert.ConvertBack(x), mConvert.ConvertBack(y)));
        }

        public TElement Negative(TElement x)
        {
            return mConvert.ConvertFrom(mRing.Negative(mConvert.ConvertBack(x)));
        }

        public TElement Zero
        {
            get
            {
                return mConvert.ConvertFrom(mRing.Zero);
            }
        }

        public TElement Multiply(TElement x, TElement y)
        {
            return mConvert.ConvertFrom(mRing.Multiply(mConvert.ConvertBack(x), mConvert.ConvertBack(y)));
        }

        public TElement Identity
        {
            get
            {
                return mConvert.ConvertFrom(mRing.Identity);
            }
        }

        public IEqualityComparer<TElement> Comparer
        {
            get
            {
                return mComparer;
            }            
        }

        #region Item Comparer

        private class ItemComparer : IEqualityComparer<TElement>
        {
            private readonly TransformedRing<TOriginal, TElement> mTransformedRing;

            public ItemComparer(TransformedRing<TOriginal, TElement> transformedRing)
            {
                mTransformedRing = transformedRing;
            }

            public bool Equals(TElement x, TElement y)
            {
                return mTransformedRing.mRing.Comparer.Equals
                    (mTransformedRing.mConvert.ConvertBack(x),
                     mTransformedRing.mConvert.ConvertBack(y));
            }

            public int GetHashCode(TElement obj)
            {
                return mTransformedRing.mRing.Comparer.GetHashCode
                    (mTransformedRing.mConvert.ConvertBack(obj));
            }
        }

        #endregion

        // TODO: think about this.
        public IRing<TOriginal> Subring
        {
            get
            {
                return mRing;
            }
        }

        public TElement Embed(TOriginal element)
        {
            return mConvert.ConvertFrom(element);
        }
    }
}