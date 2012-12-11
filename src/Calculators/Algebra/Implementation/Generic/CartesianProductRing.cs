using System;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class CartesianProductRing<T1, T2> : IRing<Tuple<T1, T2>>
    {
        private readonly IRing<T1> mRing1;
        private readonly IRing<T2> mRing2;

        public CartesianProductRing(IRing<T1> ring1, IRing<T2> ring2)
        {
            mRing1 = ring1;
            mRing2 = ring2;
        }

        public Tuple<T1, T2> Add(Tuple<T1, T2> x, Tuple<T1, T2> y)
        {
            return Tuple.Create(mRing1.Add(x.Item1, y.Item1), mRing2.Add(x.Item2, y.Item2));
        }

        public Tuple<T1, T2> Negative(Tuple<T1, T2> x)
        {
            return Tuple.Create(mRing1.Negative(x.Item1), mRing2.Negative(x.Item2));
        }

        public Tuple<T1, T2> Zero
        {
            get
            {
                return Tuple.Create(mRing1.Zero, mRing2.Zero);
            }
        }

        // Only multiply methods are virtual since it is very uncommon to change the
        // add operation in cartesian products
        public virtual Tuple<T1, T2> Multiply(Tuple<T1, T2> x, Tuple<T1, T2> y)
        {
            return Tuple.Create(mRing1.Multiply(x.Item1, y.Item1),
                                mRing2.Multiply(x.Item2, x.Item2));
        }

        public virtual Tuple<T1, T2> Identity
        {
            get
            {
                return Tuple.Create(mRing1.Identity, mRing2.Identity);
            }
        }
    }
}
