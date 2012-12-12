using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class PrincipalIdeal<T> : IPrincipalIdeal<T>
    {
        private readonly T mGenerator;
        private readonly IEuclideanRing<T> mRing;

        public PrincipalIdeal(IEuclideanRing<T> ring, T generator)
        {
            mGenerator = generator;
            mRing = ring;
        }

        public IRing<T> Ring
        {
            get
            {
                return mRing;
            }
        }

        public T Modulo(T x)
        {
            T result;
            mRing.Divide(x, Generator, out result);
            return result;
        }

        public T Generator
        {
            get
            {
                return mGenerator;
            }
        }
    }
}