using System;
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
    }
}