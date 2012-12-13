using System;

namespace Calculators.Algebra
{
    /// <remarks>
    /// Copied from <see cref="OperatorSyntax{T}"/>, I don't have a good idea how to
    /// reuse that code.
    /// </remarks>
    public class PolynomialOperatorSyntax<T>
    {
        private readonly PolynomialRing<T> mRing;
        private readonly Polynomial<T> mValue;

        public PolynomialOperatorSyntax(Polynomial<T> value, PolynomialRing<T> ring)
        {
            mValue = value;
            mRing = ring;
        }

        public Polynomial<T> Value
        {
            get { return mValue; }
        }

        public static PolynomialOperatorSyntax<T> operator +(PolynomialOperatorSyntax<T> first, PolynomialOperatorSyntax<T> second)
        {
            if (first.mRing != second.mRing)
            {
                throw new ArgumentException("The arguments must have the same ring");
            }

            PolynomialRing<T> ring = first.mRing;

            return new PolynomialOperatorSyntax<T>(ring.Add(first.Value, second.Value), ring);
        }

        public static PolynomialOperatorSyntax<T> operator -(PolynomialOperatorSyntax<T> element)
        {
            PolynomialRing<T> ring = element.mRing;

            return new PolynomialOperatorSyntax<T>(ring.Negative(element.Value), ring);
        }

        public static PolynomialOperatorSyntax<T> operator -(PolynomialOperatorSyntax<T> first, PolynomialOperatorSyntax<T> second)
        {
            if (first.mRing != second.mRing)
            {
                throw new ArgumentException("The arguments must have the same ring");
            }

            PolynomialRing<T> ring = first.mRing;

            return new PolynomialOperatorSyntax<T>(ring.Add(first.Value, ring.Negative(second.Value)), ring);
        }

        public static PolynomialOperatorSyntax<T> operator *(T scalar, PolynomialOperatorSyntax<T> vector)
        {
            PolynomialRing<T> ring = vector.mRing;

            return new PolynomialOperatorSyntax<T>(ring.Multiply(scalar, vector.Value), ring);
        }

        public static PolynomialOperatorSyntax<T> operator *(PolynomialOperatorSyntax<T> vector, T scalar)
        {
            return (scalar * vector);
        }

        public static PolynomialOperatorSyntax<T> operator +(T scalar, PolynomialOperatorSyntax<T> vector)
        {
            PolynomialRing<T> ring = vector.mRing;

            return new PolynomialOperatorSyntax<T>(ring.Add(new Polynomial<T>(new T[] {scalar}, ring.CoefficientsRing),
                                                            vector.Value),
                                                   ring);
        }

        public static PolynomialOperatorSyntax<T> operator +(PolynomialOperatorSyntax<T> vector, T scalar)
        {
            return (scalar + vector);
        }

        public static PolynomialOperatorSyntax<T> operator -(PolynomialOperatorSyntax<T> vector, T scalar)
        {
            return (vector + vector.mRing.CoefficientsRing.Negative(scalar));
        }

        public static PolynomialOperatorSyntax<T> operator -(T scalar, PolynomialOperatorSyntax<T> vector)
        {
            return (-vector + scalar);
        }

        public static PolynomialOperatorSyntax<T> operator *(PolynomialOperatorSyntax<T> first, PolynomialOperatorSyntax<T> second)
        {
            if (first.mRing != second.mRing)
            {
                throw new ArgumentException("The arguments must have the same ring");
            }

            PolynomialRing<T> ring = first.mRing;

            return new PolynomialOperatorSyntax<T>(ring.Multiply(first.Value, second.Value), ring);
        }

        public static PolynomialOperatorSyntax<T> operator /(PolynomialOperatorSyntax<T> first, PolynomialOperatorSyntax<T> second)
        {
            if (first.mRing != second.mRing)
            {
                throw new ArgumentException("The arguments must have the same ring");
            }

            PolynomialOverFieldRing<T> ring = first.mRing as PolynomialOverFieldRing<T>;

            if (ring == null)
            {
                throw new ArgumentException("Ring must be euclidean in order to use divison operator");
            }

            Polynomial<T> remainder;
            return new PolynomialOperatorSyntax<T>(ring.Divide(first.Value, second.Value, out remainder), ring);
        }

        public static PolynomialOperatorSyntax<T> operator %(PolynomialOperatorSyntax<T> first, PolynomialOperatorSyntax<T> second)
        {
            if (first.mRing != second.mRing)
            {
                throw new ArgumentException("The arguments must have the same ring");
            }

            PolynomialOverFieldRing<T> ring = first.mRing as PolynomialOverFieldRing<T>;

            if (ring == null)
            {
                throw new ArgumentException("Ring must be euclidean in order to use divison operator");
            }

            Polynomial<T> remainder;

            ring.Divide(first.Value, second.Value, out remainder);

            return new PolynomialOperatorSyntax<T>(remainder, ring);
        }
    }
}