using System.Collections.Generic;

namespace Calculators.Algebra
{
    public class PolynomialParserOptions
    {
        public string Argument { get; set; }

        public Dictionary<string, object> Aliases { get; set; }
    }
}