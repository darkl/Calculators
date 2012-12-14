using System;
using System.Dynamic;
using System.Linq.Expressions;
using Calculators.Algebra;
using Calculators.Algebra.Abstract;

namespace Calculators
{
    public class OperatorSyntax<T> 
    {
        private readonly IRing<T> mRing;
        private readonly T mValue;

        public OperatorSyntax(T value, IRing<T> ring)
        {
            mValue = value;
            mRing = ring;
        }

        public T Value
        {
            get
            {
                return mValue;
            }
        }

        public static OperatorSyntax<T> operator +(OperatorSyntax<T> first, OperatorSyntax<T> second)
        {
            if (first.mRing != second.mRing)
            {
                throw new ArgumentException("The arguments must have the same ring");
            }

            IRing<T> ring = first.mRing;

            return new OperatorSyntax<T>(ring.Add(first.Value, second.Value), ring);
        }

        public static OperatorSyntax<T> operator -(OperatorSyntax<T> element)
        {
            IRing<T> ring = element.mRing;

            return new OperatorSyntax<T>(ring.Negative(element.Value), ring);
        }

        public static OperatorSyntax<T> operator -(OperatorSyntax<T> first, OperatorSyntax<T> second)
        {
            if (first.mRing != second.mRing)
            {
                throw new ArgumentException("The arguments must have the same ring");
            }

            IRing<T> ring = first.mRing;

            return new OperatorSyntax<T>(ring.Add(first.Value, ring.Negative(second.Value)), ring);
        }

        public static OperatorSyntax<T> operator *(OperatorSyntax<T> first, OperatorSyntax<T> second)
        {
            if (first.mRing != second.mRing)
            {
                throw new ArgumentException("The arguments must have the same ring");
            }

            IRing<T> ring = first.mRing;

            return new OperatorSyntax<T>(ring.Multiply(first.Value, second.Value), ring);
        }

        public static OperatorSyntax<T> operator /(OperatorSyntax<T> first, OperatorSyntax<T> second)
        {
            if (first.mRing != second.mRing)
            {
                throw new ArgumentException("The arguments must have the same ring");
            }

            IEuclideanRing<T> ring = first.mRing as IEuclideanRing<T>;

            if (ring == null)
            {
                throw new ArgumentException("Ring must be euclidean in order to use divison operator");
            }

            T remainder;
            return new OperatorSyntax<T>(ring.Divide(first.Value, second.Value, out remainder), ring);
        }

        public static OperatorSyntax<T> operator %(OperatorSyntax<T> first, OperatorSyntax<T> second)
        {
            if (first.mRing != second.mRing)
            {
                throw new ArgumentException("The arguments must have the same ring");
            }

            IEuclideanRing<T> ring = first.mRing as IEuclideanRing<T>;

            if (ring == null)
            {
                throw new ArgumentException("Ring must be euclidean in order to use divison operator");
            }

            T remainder;
            
            ring.Divide(first.Value, second.Value, out remainder);
            
            return new OperatorSyntax<T>(remainder, ring);
        }

        #region Dynamic Operator Support

        public static OperatorSyntax<T> operator +(object first, OperatorSyntax<T> second)
        {
            return TryBinaryOperation(first, second, (x, y) => x + y);
        }

        public static OperatorSyntax<T> operator +(OperatorSyntax<T> first, object second)
        {
            return TryBinaryOperation(second, first, (x, y) => y + x);
        }

        public static OperatorSyntax<T> operator -(object first, OperatorSyntax<T> second)
        {
            return TryBinaryOperation(first, second, (x, y) => x - y);
        }

        public static OperatorSyntax<T> operator -(OperatorSyntax<T> first, object second)
        {
            return TryBinaryOperation(second, first, (x, y) => y - x);
        }

        public static OperatorSyntax<T> operator *(object first, OperatorSyntax<T> second)
        {
            return TryBinaryOperation(first, second, (x, y) => x * y);
        }

        public static OperatorSyntax<T> operator *(OperatorSyntax<T> first, object second)
        {
            return TryBinaryOperation(second, first, (x, y) => y * x);
        }

        public static OperatorSyntax<T> operator /(object first, OperatorSyntax<T> second)
        {
            return TryBinaryOperation(first, second, (x, y) => x / y);
        }

        public static OperatorSyntax<T> operator /(OperatorSyntax<T> first, object second)
        {
            return TryBinaryOperation(second, first, (x, y) => y / x);
        }

        public static OperatorSyntax<T> operator %(object first, OperatorSyntax<T> second)
        {
            return TryBinaryOperation(first, second, (x, y) => x % y);
        }

        public static OperatorSyntax<T> operator %(OperatorSyntax<T> first, object second)
        {
            return TryBinaryOperation(second, first, (x, y) => y % x);
        }

        private static OperatorSyntax<T> TryBinaryOperation(object value, OperatorSyntax<T> operatorSyntax, Func<OperatorSyntax<T>, OperatorSyntax<T>, OperatorSyntax<T>> operation)
        {
            OperatorSyntax<T> operatorSyntaxArgument = value as OperatorSyntax<T>;

            if (operatorSyntaxArgument == null)
            {
                T underlyingValue;

                if (!TryConvert(value, out underlyingValue))
                {
                    underlyingValue = Convert(((dynamic) operatorSyntax.mRing), (dynamic) value);
                }

                operatorSyntaxArgument = new OperatorSyntax<T>(underlyingValue, operatorSyntax.mRing);
            }

            return operation(operatorSyntaxArgument, operatorSyntax);
        }

        private static bool TryConvert<TCasted>(object arg, out TCasted result)
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
                    result = (TCasted) dynamicCasted;
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

            if (TryConvert(givenElement, out converted))
            {
                return embedding.Embed(converted);
            }

            return embedding.Embed(Convert((dynamic) embedding.Subring, (dynamic)givenElement));
        }

        private static dynamic Convert(object embedding, object givenElement)
        {
            throw new ArgumentException("Argument was not contained in any subring of the given ring.");
        }

        #endregion
    }
}