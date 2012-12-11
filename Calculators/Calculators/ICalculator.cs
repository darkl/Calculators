namespace Calculators
{
    public interface ICalculator
    {
        TResult Add<T1, T2, TResult>(T1 first, T2 second);
        TResult Subtract<T1, T2, TResult>(T1 first, T2 second);
        TResult Multiply<T1, T2, TResult>(T1 first, T2 second);
        TResult Divide<T1, T2, TResult>(T1 first, T2 second);
    }
}
