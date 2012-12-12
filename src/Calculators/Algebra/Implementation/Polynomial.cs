using System.Linq;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public struct Polynomial<T>
    {
        private readonly IRing<T> mCoefficientsRing;
        private readonly T[] mCoefficients;

        public Polynomial(T[] coefficients, IRing<T> coefficientsRing)
        {
            mCoefficientsRing = coefficientsRing;

            // Take the items until the last item that is not zero.
            var lastCoefficient =
                coefficients.Select((x, i) => new
                                                  {
                                                      Coefficient = x,
                                                      Index = i
                                                  })
                            .LastOrDefault(x => !coefficientsRing.Comparer.Equals(x.Coefficient,
                                                                                  coefficientsRing.Zero));

            if (lastCoefficient == null)
            {
                mCoefficients = new T[0];
            }
            else
            {
                mCoefficients = coefficients.Take(lastCoefficient.Index + 1).ToArray();
            }
        }

        public T this[int index]
        {
            get
            {
                if (index <= Degree)
                {
                    return mCoefficients[index];    
                }

                return CoefficientsRing.Zero;
            }
        }

        public int Degree
        {
            get
            {
                return mCoefficients.Length - 1;
            }
        }

        public IRing<T> CoefficientsRing
        {
            get
            {
                return mCoefficientsRing;
            }
        }
    }
}