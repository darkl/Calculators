namespace Calculators
{
    public interface ICalculator<in T1, in T2, out TResult>
    {
        TResult Add(T1 first, T2 second);
        TResult Subtract(T1 first, T2 second);
        TResult Multiply(T1 first, T2 second);
        TResult Divide(T1 first, T2 second);
    }
}
