namespace Calculators.Algebra.Abstract
{
    /// <summary>
    /// Represents a group with multiplicative syntax. See: http://en.wikipedia.org/wiki/Group_(mathematics)
    /// </summary>
    public interface IMultiplicativeGroup<TElement> : IMultiplicativeSemigroup<TElement>
    {
        TElement Inverse(TElement x);
    }
}