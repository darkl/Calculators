using System.Collections.Generic;

namespace Calculators.Algebra.Abstract
{
    /// <summary>
    /// Represents a ring. See: http://en.wikipedia.org/wiki/Ring_(mathematics)
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public interface IRing<TElement> : IAdditiveGroup<TElement>, IMultiplicativeSemigroup<TElement>
    {
        /// <summary>
        /// A comparer that will be used to determinate whether two elements are equivalent.
        /// </summary>
        IEqualityComparer<TElement> Comparer { get; } 
    }
}