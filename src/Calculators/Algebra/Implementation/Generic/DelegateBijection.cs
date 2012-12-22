using System;
using Calculators.Algebra.Abstract;

namespace Calculators.Algebra
{
    public class DelegateBijection<TSource, TTarget> : IBijection<TSource, TTarget>
    {
        private readonly Func<TSource, TTarget> mConvertFrom;
        private readonly Func<TTarget, TSource> mConvertBack;

        public DelegateBijection(Func<TSource, TTarget> convertFrom,
                                 Func<TTarget, TSource> convertBack)
        {
            mConvertFrom = convertFrom;
            mConvertBack = convertBack;
        }

        public TTarget ConvertFrom(TSource value)
        {
            return mConvertFrom(value);
        }

        public TSource ConvertBack(TTarget value)
        {
            return mConvertBack(value);
        }
    }
}