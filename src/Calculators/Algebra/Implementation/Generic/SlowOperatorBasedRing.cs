using System;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class SlowOperatorBasedRing<T> : IRing<T> where T : struct
    {
        public T Add(T x, T y)
        {
            dynamic first = x;
            dynamic second = y;

            return (first + second);
        }

        public T Negative(T x)
        {
            dynamic first = x;

            return (-first);
        }

        public T Zero
        {
            get
            {
                // The convention is that the default value of these things is zero.
                return default(T);
            }
        }

        public T Multiply(T x, T y)
        {
            dynamic first = x;
            dynamic second = y;

            return (first * second);
        }

        public T Identity
        {
            get
            {
                // Didn't find a better way to do it.
                if (typeof (IConvertible).IsAssignableFrom(typeof (T)))
                {
                    return (T)Convert.ChangeType(1, typeof(T));
                }

                return (T) Activator.CreateInstance(typeof (T), 1);
            }
        }
    }
}