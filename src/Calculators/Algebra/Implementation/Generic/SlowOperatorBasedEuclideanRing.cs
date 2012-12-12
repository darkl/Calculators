using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public abstract class SlowOperatorBasedEuclideanRing<T> : SlowOperatorBasedRing<T>, IEuclideanRing<T> 
        where T : struct
    {
        public virtual T Divide(T dividend, T divisor, out T remainder)
        {
            dynamic dynamicDividend = dividend;
            dynamic dynamicDivisor = divisor;

            // This a bit uglier than it is supposed to be, because the remainder must be
            // canonical, i.e: non-negative.
            remainder = (dynamicDividend + dynamicDivisor) % dynamicDivisor; 
            return (dynamicDividend / dynamicDivisor);
        }

        public abstract int Degree(T element);
    }
}