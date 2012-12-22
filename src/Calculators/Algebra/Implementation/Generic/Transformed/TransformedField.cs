using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class TransformedField<TOriginal, TElement> :
        TransformedRing<TOriginal, TElement>, IField<TElement>
    {
        private readonly IField<TOriginal> mField;
        private readonly IBijection<TOriginal, TElement> mConvert;

        public TransformedField(IField<TOriginal> field, IBijection<TOriginal, TElement> convert) : 
            base(field, convert)
        {
            mField = field;
            mConvert = convert;
        }

        public TElement Inverse(TElement x)
        {
            return mConvert.ConvertFrom(mField.Inverse(mConvert.ConvertBack(x)));
        }
    }
}