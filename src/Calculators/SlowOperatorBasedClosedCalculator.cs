using Calculators.Algebra;

namespace Calculators
{
    public class SlowOperatorBasedClosedCalculator<T> : FieldBasedClosedCalculator<T>
        where T : struct 
    {
        public SlowOperatorBasedClosedCalculator() : base(new SlowOperatorBasedField<T>())
        {
        }
    }
}