namespace Calculators.Algebra.Abstract
{
    /// <summary>
    /// Represents a group with multiplicative syntax. See: http://en.wikipedia.org/wiki/Semigroup_(mathematics)
    /// </summary>
    public interface IMultiplicativeSemigroup<TElement>
    {
        TElement Multiply(TElement x, TElement y);

        TElement Identity { get; }
    }
}