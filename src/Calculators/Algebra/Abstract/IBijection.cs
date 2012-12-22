namespace Calculators.Algebra.Abstract
{
    public interface IBijection<TSource, TTarget>
    {
        TTarget ConvertFrom(TSource value);
        TSource ConvertBack(TTarget value);
    }
}