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
                T underlyingValue = PolynomialBuilder.BuildForRing(value, operatorSyntax.mRing);

                operatorSyntaxArgument = new OperatorSyntax<T>(underlyingValue, operatorSyntax.mRing);
            }

            return operation(operatorSyntaxArgument, operatorSyntax);
        }

        #endregion
    }
}