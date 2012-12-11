namespace Calculators.Algebra.Abstract
{
    /// <summary>
    /// Represents a group with additive syntax. See: http://en.wikipedia.org/wiki/Group_(mathematics)
    /// </summary>
    public interface IAdditiveGroup<TElement>
    {
        TElement Add(TElement x, TElement y);

        TElement Negative(TElement x);

        TElement Zero { get; }
    }
}