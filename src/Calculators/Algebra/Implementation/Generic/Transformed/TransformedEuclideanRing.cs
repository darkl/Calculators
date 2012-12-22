using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class TransformedEuclideanRing<TOriginal, TElement> : 
        TransformedRing<TOriginal, TElement>,
        IEuclideanRing<TElement>
    {
        private readonly IEuclideanRing<TOriginal> mRing;
        private readonly IBijection<TOriginal, TElement> mConvert;

        public TransformedEuclideanRing(IEuclideanRing<TOriginal> ring, IBijection<TOriginal, TElement> convert) : 
            base(ring, convert)
        {
            mRing = ring;
            mConvert = convert;
        }

        public TElement Divide(TElement dividend, TElement divisor, out TElement remainder)
        {
            TOriginal originalRemainder;

            TOriginal originalQuotient =
                mRing.Divide(mConvert.ConvertBack(dividend),
                             mConvert.ConvertBack(divisor),
                             out originalRemainder);

            remainder = mConvert.ConvertFrom(originalRemainder);
            return mConvert.ConvertFrom(originalQuotient);
        }

        public int Degree(TElement element)
        {
            return mRing.Degree(mConvert.ConvertBack(element));
        }
    }
}