using System;
using System.Collections.Generic;
using Calculators.Algebra;
using Calculators.Algebra.Abstract;
using NUnit.Framework;

namespace Calculators.Testing.Unit
{
    [TestFixture]
    public class PolynomialTests
    {
        [Test]
        public void TestParse()
        {
            string somePolynomial = "(x*x + 1)*(x*x - 1)";

            PolynomialParser parser = new PolynomialParser();

            IRing<double> ring = new SlowOperatorBasedRing<double>();
            
            Polynomial<double> parsed =
                parser.Parse(somePolynomial, ring);

            PolynomialRing<double> polynomialRing = new PolynomialRing<double>(ring);

            Assert.IsTrue(polynomialRing.Comparer.Equals(parsed,
                                                         new Polynomial<double>(new double[] {-1, 0, 0, 0, 1}, ring)));
        }

        [Test]
        public void TestComplicatedParse()
        {
            string somePolynomial = "(x*x + i)*(x*x - i)";

            SlowOperatorBasedField<double> realField = new SlowOperatorBasedField<double>();

            PolynomialOverFieldRing<double> realPolynomialRing = new PolynomialOverFieldRing<double>(realField);

            IRing<Polynomial<double>> complexRing =
                new QuotientRing<Polynomial<double>>
                    (new PrincipalIdeal<Polynomial<double>>
                         (realPolynomialRing,
                          new Polynomial<double>(new double[] {1, 0, 1}, realField)));

            var i = realField.CreateMonomial(1, 1);

            PolynomialParser parser = new PolynomialParser
                (new PolynomialParserOptions()
                     {
                         Aliases = new Dictionary<string, object>()
                                       {
                                           {"i", i}
                                       }
                     });

            Polynomial<Polynomial<double>> parsed =
                parser.Parse(somePolynomial, complexRing);

            PolynomialRing<Polynomial<double>> complexPolynomialRing =
                new PolynomialRing<Polynomial<double>>(complexRing);

            Polynomial<Polynomial<double>> expectedResult =
                complexPolynomialRing.Add(
                complexRing.CreateMonomial(complexRing.Identity, 4),
                complexPolynomialRing.Identity);

            Assert.IsTrue(complexPolynomialRing.Comparer.Equals(parsed, expectedResult));
        }

        [Test]
        public void TestComplicatedParseSqrt2()
        {
            string somePolynomial = "(x*x + sqrt2*x + 1)*(x*x - sqrt2*x + 1)";

            SlowOperatorBasedField<double> rationalField = new SlowOperatorBasedField<double>();

            PolynomialOverFieldRing<double> rationalPolynomialRing = new PolynomialOverFieldRing<double>(rationalField);

            IRing<Polynomial<double>> rationalWithSqrt2Ring =
                new QuotientRing<Polynomial<double>>
                    (new PrincipalIdeal<Polynomial<double>>
                         (rationalPolynomialRing,
                          new Polynomial<double>(new double[] { -2, 0, 1 }, rationalField)));

            var sqrt2 = rationalField.CreateMonomial(1, 1);

            PolynomialParser parser = new PolynomialParser
                (new PolynomialParserOptions()
                {
                    Aliases = new Dictionary<string, object>()
                                       {
                                           {"sqrt2", sqrt2}
                                       }
                });

            Polynomial<Polynomial<double>> parsed =
                parser.Parse(somePolynomial, rationalWithSqrt2Ring);

            PolynomialRing<Polynomial<double>> complexPolynomialRing =
                new PolynomialRing<Polynomial<double>>(rationalWithSqrt2Ring);

            Polynomial<Polynomial<double>> expectedResult =
                complexPolynomialRing.Add(
                rationalWithSqrt2Ring.CreateMonomial(rationalWithSqrt2Ring.Identity, 4),
                complexPolynomialRing.Identity);

            Assert.IsTrue(complexPolynomialRing.Comparer.Equals(parsed, expectedResult));
        }

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