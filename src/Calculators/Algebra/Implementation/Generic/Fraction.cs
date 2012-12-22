namespace Calculators.Algebra
{
    public struct Fraction<T>
    {
        private readonly T mNumerator;
        private readonly T mDenominator;

        public Fraction(T numerator, T denominator)
        {
            mNumerator = numerator;
            mDenominator = denominator;
        }

        public T Numerator
        {
            get
            {
                return mNumerator;
            }
        }

        public T Denominator
        {
            get
            {
                return mDenominator;
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}] / [{1}]", Numerator, Denominator);
        }
    }
}