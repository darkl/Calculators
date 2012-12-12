using System;

namespace Calculators.Algebra
{
    public class IntegersRing : SlowOperatorBasedEuclideanRing<int>
    {
        public override int Degree(int element)
        {
            return Math.Abs(element);
        }
    }
}