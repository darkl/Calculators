using System;

namespace Calculators.Algebra
{
    public class GoldenRatioRing : CartesianProductRing<int, int>
    {
        private static readonly SlowOperatorBasedRing<int> mRing = new SlowOperatorBasedRing<int>();

        public GoldenRatioRing()
            : base(mRing, mRing)
        {
        }

        public override Tuple<int, int> Multiply(Tuple<int, int> x, Tuple<int, int> y)
        {
            return Tuple.Create(
                mRing.Add(mRing.Multiply(x.Item1, y.Item1), mRing.Multiply(x.Item2, y.Item2)),
                mRing.Add(mRing.Add(mRing.Multiply(x.Item1, y.Item2), mRing.Multiply(x.Item2, y.Item1)),
                          mRing.Multiply(x.Item2, y.Item2)));
        }

        public override Tuple<int, int> Identity
        {
            get
            {
                return Tuple.Create(mRing.Identity, mRing.Zero);
            }
        }
    }
}