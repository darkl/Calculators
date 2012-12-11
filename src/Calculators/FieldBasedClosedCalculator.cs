using Calculators.Algebra.Abstract;

namespace Calculators
{
    public class FieldBasedClosedCalculator<T> : IClosedCalculator<T>
    {
        private readonly IField<T> mField;

        public FieldBasedClosedCalculator(IField<T> field)
        {
            mField = field;
        }

        public T Add(T first, T second)
        {
            return mField.Add(first, second);
        }

        public T Subtract(T first, T second)
        {
            return mField.Add(first, mField.Negative(second));
        }

        public T Multiply(T first, T second)
        {
            return mField.Multiply(first, second);
        }

        public T Divide(T first, T second)
        {
            return mField.Multiply(first, mField.Inverse(second));
        }
    }
}