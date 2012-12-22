namespace Calculators.Algebra.Abstract
{
    public interface IModule<TScalar, TVector> : IAdditiveGroup<TVector>
    {
        IRing<TScalar> ScalarRing { get; }

        TVector Multiply(TScalar scalar, TVector vector);
    }
}