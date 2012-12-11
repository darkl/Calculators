using System;
using System.Collections.Generic;
using Calculators.Algebra;
using NUnit.Framework;

namespace Calculators.Testing.Unit
{
    [TestFixture]
    public class GoldenRatioRingTests
    {
        [Test]
         public void Test_Multiply_FibbonacciSequence()
         {
             GoldenRatioRing ring = new GoldenRatioRing();

             Tuple<int, int> first = Tuple.Create(0, 1);

             Tuple<int, int> current = first;

             List<int> numbers = new List<int>();

             for (int i = 0; i < 8; i++)
             {
                 current = ring.Multiply(current, first);
                 numbers.Add(current.Item1);
             }

             CollectionAssert.AreEqual(numbers, new int[] {1, 1, 2, 3, 5, 8, 13, 21});
         }
    }
}