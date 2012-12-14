namespace Calculators.Algebra.Abstract
{
    public interface IRingEmbedding<in TOriginalElement, TElement> : IRing<TElement>
    {
        IRing<TElement> ExtendedRing { get; } 

        TElement Embed(TOriginalElement element);
    }
}