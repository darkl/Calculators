namespace Calculators.Algebra.Abstract
{
    public interface IEuclideanRing<T> : IRing<T>
    {
        T Divide(T dividend, T divisor, out T remainder);

        int Degree(T element);
    }
}