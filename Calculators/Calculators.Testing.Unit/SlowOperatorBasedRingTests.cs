using System;
using Calculators.Algebra;
using NUnit.Framework;

namespace Calculators.Testing.Unit
{
    [TestFixture]
    public class SlowOperatorBasedRingTests
    {
        [Test]
        public void Test_Add()
        {
            SlowOperatorBasedRing<int> ring = new SlowOperatorBasedRing<int>();

            Assert.That(ring.Add(3, 4), Is.EqualTo(7));
        }

        [Test]
        public void Test_Negative()
        {
            SlowOperatorBasedRing<int> ring = new SlowOperatorBasedRing<int>();

            Assert.That(ring.Negative(3), Is.EqualTo(-3));
        }

        [Test]
        public void Test_Multiply()
        {
            SlowOperatorBasedRing<int> ring = new SlowOperatorBasedRing<int>();

            Assert.That(ring.Multiply(3, 13), Is.EqualTo(39));
        }

        [Test]
        public void Test_Zero()
        {
            SlowOperatorBasedRing<int> ring = new SlowOperatorBasedRing<int>();

            Assert.That(ring.Zero, Is.EqualTo(0));
        }

        [Test]
        public void Test_Identity()
        {
            SlowOperatorBasedRing<int> ring = new SlowOperatorBasedRing<int>();

            Assert.That(ring.Identity, Is.EqualTo(1));
        }    
    }
}
