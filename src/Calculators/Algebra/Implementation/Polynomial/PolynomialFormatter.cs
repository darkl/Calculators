using System.Collections.Generic;

namespace Calculators.Algebra
{
    public class PolynomialFormatter
    {
        private readonly IDictionary<string, object> mAliases;

        public PolynomialFormatter(IDictionary<string, object> aliases)
        {
            mAliases = aliases;
        }
    }
}