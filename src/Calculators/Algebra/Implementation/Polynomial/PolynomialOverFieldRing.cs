using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class PolynomialOverFieldRing<T> : PolynomialRing<T>, IEuclideanRing<Polynomial<T>>
    {
        private readonly IField<T> mCoefficientsField;

        public PolynomialOverFieldRing(IField<T> coefficientsField) : base(coefficientsField)
        {
            mCoefficientsField = coefficientsField;
        }

        public Polynomial<T> Divide(Polynomial<T> dividend, Polynomial<T> divisor, out Polynomial<T> remainder)
        {
            Polynomial<T> quotient = this.Zero;

            remainder = dividend;

            T divisorLeadingCoefficient = divisor.LeadingCoefficient();
            T inverseOfCoefficient = mCoefficientsField.Inverse(divisorLeadingCoefficient);

            while (divisor.Degree <= remainder.Degree)
            {
                int degree = remainder.Degree - divisor.Degree;

                Polynomial<T> currentPart =
                    mCoefficientsField.CreateMonomial(
                        mCoefficientsField.Multiply(remainder.LeadingCoefficient(),
                                                    inverseOfCoefficient),
                        degree);

                remainder = this.Add(remainder, this.Negative(this.Multiply(currentPart, divisor)));
                quotient = this.Add(quotient, currentPart);
            }

            return quotient;
        }

        public int Degree(Polynomial<T> element)
        {
            return element.Degree;
        }
    }
}