using Calculators.Algebra;
using NUnit.Framework;

namespace Calculators.Testing.Unit
{
    [TestFixture]
    public class PolynomialTests
    {
        [Test]
        public void TestDivision()
        {
            PolynomialOverFieldRing<double> ring = new PolynomialOverFieldRing<double>(new SlowOperatorBasedField<double>());

            Polynomial<double> remainder;

            var quotient =
                ring.Divide(new Polynomial<double>(new double[] { -1, 0, 0, 1 }, ring.CoefficientsRing),
                            new Polynomial<double>(new double[] { -1, 1 }, ring.CoefficientsRing), out remainder);

            Assert.IsTrue(ring.Comparer.Equals(remainder, ring.Zero));

            Assert.IsTrue(ring.Comparer.Equals(quotient, new Polynomial<double>(new double[] {1, 1, 1}, ring.CoefficientsRing)));
        }
    }
}