using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class SlowOperatorBasedField<T> : SlowOperatorBasedRing<T>, IField<T> where T : struct
    {
        public T Inverse(T x)
        {
            dynamic current = x;
            dynamic identity = this.Identity;

            return identity / current;
        }
    }
}