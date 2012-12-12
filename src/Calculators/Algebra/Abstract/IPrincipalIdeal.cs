namespace Calculators.Algebra.Abstract
{
    public interface IPrincipalIdeal<T> : IIdeal<T>
    {
        T Generator { get; }
    }
}