using System;

namespace Calculators.Algebra.Abstract
{
    /// <summary>
    /// Represents a field. See: http://en.wikipedia.org/wiki/Field_(mathematics)
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public interface IField<TElement> : IRing<TElement>
    {
        /// <remarks>
        /// Should throw <see cref="DivideByZeroException"/> for <see cref="IAdditiveGroup{TElement}.Zero"/>
        /// </remarks>
        TElement Inverse(TElement x);
    }
}