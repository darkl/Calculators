﻿using Calculators.Algebra;
using NUnit.Framework;

namespace Calculators.Testing.Unit
{
    [TestFixture]
    public class IntegersModuloRingTest
    {
        [Test]
        public void TestModulo()
        {
            IntegersModuloRing ring = new IntegersModuloRing(7);

            int one = ring.Multiply(3, 5);

            Assert.That(one, Is.EqualTo(1));
        }

        [Test]
        public void TestInverse()
        {
            IntegersModuloField ring = new IntegersModuloField(7);

            int five = ring.Inverse(3);

            Assert.That(five, Is.EqualTo(5));
        }

    }
}