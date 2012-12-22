using System;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public static class Bijection
    {
        public static IBijection<TSource, TTarget> Create<TSource, TTarget>
            (Func<TSource, TTarget> convertFrom, Func<TTarget, TSource> convertBack)
        {
            return new DelegateBijection<TSource, TTarget>(convertFrom, convertBack);
        }
    }
}