namespace Calculators.Algebra.Abstract
{
    public interface IModule<TRingElement, TElement> : IAdditiveGroup<TElement>
    {
        IRing<TRingElement> ScalarRing { get; }

        TElement Multiply(TRingElement scalar, TElement vector);
    }
}