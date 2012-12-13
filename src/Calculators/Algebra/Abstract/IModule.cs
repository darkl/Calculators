namespace Calculators.Algebra.Abstract
{
    public interface IModule<TRingElement, TElement> : IAdditiveGroup<TElement>
    {
        IRing<TRingElement> Ring { get; }

        TElement Multiply(TRingElement scalar, TElement vector);
    }
}