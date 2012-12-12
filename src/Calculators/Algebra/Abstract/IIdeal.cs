namespace Calculators.Algebra.Abstract
{
    public interface IIdeal<T>
    {
        IRing<T> Ring { get; }
        
        /// <summary>
        /// Gets an item that represents the equivalence class of (x + I) where
        /// I is the current ideal.
        /// </summary>
        T Modulo(T x);
    }
}