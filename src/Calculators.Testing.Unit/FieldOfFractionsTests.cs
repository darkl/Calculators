using Calculators.Algebra;
using NUnit.Framework;

namespace Calculators.Testing.Unit
{
    [TestFixture]
    public class FieldOfFractionsTests
    {
        [Test]
        public void TestEquals()
        {
            IntegersRing ring = new IntegersRing();

            FieldOfFractions<int> fractions = new FieldOfFractions<int>(ring);

            Fraction<int> fraction = new Fraction<int>(2, 4);
            Fraction<int> fraction2 = new Fraction<int>(-2, -4);

            Assert.IsTrue(fractions.Comparer.Equals(fraction, fraction2));
        }
    }
}