namespace Calculators.Algebra.Abstract
{
    public interface IRingEmbedding<TOriginalElement, TElement> : IRing<TElement>
    {
        IRing<TOriginalElement> Subring { get; } 

        TElement Embed(TOriginalElement element);
    }
}