using System;
using Calculators.Algebra.Abstract;

namespace Calculators
{
    public class PolynomialBuilder
    {
        public static T BuildForRing<T>(object value, IRing<T> ring)
        {
            T underlyingValue;

            if (!TryPrimitiveConvert(value, out underlyingValue))
            {
                underlyingValue = Convert(((dynamic) ring), (dynamic) value);
            }

            return underlyingValue;
        }

        private static bool TryPrimitiveConvert<TCasted>(object arg, out TCasted result)
        {
            result = default(TCasted);

            if (arg is TCasted)
            {
                result = (TCasted) arg;
                return true;
            }

            if (arg is IConvertible && typeof (IConvertible).IsAssignableFrom(typeof (TCasted)))
            {
                try
                {
                    dynamic dynamicCasted = arg;
                    result = dynamicCasted; // implicit cast
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        private static dynamic Convert<TOriginalElement, TElement, TGivenElement>(
            IRingEmbedding<TOriginalElement, TElement> embedding, TGivenElement givenElement)
        {
            TOriginalElement converted;

            if (TryPrimitiveConvert(givenElement, out converted))
            {
                return embedding.Embed(converted);
            }

            return embedding.Embed(Convert((dynamic) embedding.Subring, (dynamic)givenElement));
        }

        private static dynamic Convert(object embedding, object givenElement)
        {
            throw new ArgumentException("Argument was not contained in any subring of the given ring.");
        }
    }
}