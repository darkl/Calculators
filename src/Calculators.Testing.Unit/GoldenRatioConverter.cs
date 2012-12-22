using System;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class GoldenRatioConverter : IBijection<Polynomial<double>, Tuple<int, int>>
    {
        private readonly IRing<double> mCoefficientsRing;

        public GoldenRatioConverter(IRing<double> coefficientsRing)
        {
            mCoefficientsRing = coefficientsRing;
        }

        public Tuple<int, int> ConvertFrom(Polynomial<double> value)
        {
            return new Tuple<int, int>((int) value[0], (int) value[1]);
        }

        public Polynomial<double> ConvertBack(Tuple<int, int> value)
        {
            return new Polynomial<double>(new double[] {value.Item1, value.Item2},
                                          mCoefficientsRing);
        }
    }
}