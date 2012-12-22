using System;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class GoldenRatioRing : 
        TransformedRing<Polynomial<double>, Tuple<int, int>>
    {
        private static readonly IRing<Polynomial<double>> mOriginalRing =
            BuildRing();

        private static GoldenRatioConverter mGoldenRatioConverter;

        private static IRing<Polynomial<double>> BuildRing()
        {
            PolynomialOverFieldRing<double> polynomialRing =
                new PolynomialOverFieldRing<double>(new SlowOperatorBasedField<double>());

            mGoldenRatioConverter = new GoldenRatioConverter(polynomialRing.CoefficientsRing);

            Polynomial<double> phiPolynomial = new Polynomial<double>(new double[] {-1, -1, 1},
                                                                      polynomialRing.CoefficientsRing);

            QuotientRing<Polynomial<double>> quotientRing = new QuotientRing<Polynomial<double>>
                (new PrincipalIdeal<Polynomial<double>>(polynomialRing, phiPolynomial));

            return quotientRing;
        }

        public GoldenRatioRing()
            : base(mOriginalRing, mGoldenRatioConverter)
        {
        }
    }
}