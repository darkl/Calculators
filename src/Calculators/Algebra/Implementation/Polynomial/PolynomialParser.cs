using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Calculators.Algebra.Abstract;
using Mono.CSharp;

namespace Calculators.Algebra
{
    public class PolynomialParser
    {
        private readonly Evaluator mEvaluator;

        public PolynomialParser()
        {
            mEvaluator = new Evaluator(new CompilerSettings(){AssemblyReferences = 
                new List<string>(){"System.Core"}}, 
                new Report(new StreamReportPrinter(new StringWriter(new StringBuilder()))));

            mEvaluator.ReferenceAssembly(typeof(PolynomialOperatorSyntax<>).Assembly);

            mEvaluator.Run("using System;");
            mEvaluator.Run("using System.Linq.Expressions;");
            mEvaluator.Run("using Calculators.Algebra;");
        }

        public Polynomial<T> Parse<T>(string expression, IRing<T> ring)
        {
            string declarationFormat = 
                "Func<PolynomialOperatorSyntax<{0}>, PolynomialOperatorSyntax<{0}>> evaluator = x => ({1});";

            string declaration =
                string.Format(declarationFormat, typeof (T).FullName, expression);

            mEvaluator.Run(declaration);
            Func<PolynomialOperatorSyntax<T>, PolynomialOperatorSyntax<T>> evaluator =
                mEvaluator.Evaluate("evaluator;")
                as Func<PolynomialOperatorSyntax<T>, PolynomialOperatorSyntax<T>>;

            PolynomialRing<T> polynomialRing = new PolynomialRing<T>(ring);
            
            PolynomialOperatorSyntax<T> evaluated =
                evaluator(new PolynomialOperatorSyntax<T>(ring.CreateMonomial(ring.Identity, 1), polynomialRing));

            return evaluated.Value;
        }
    }
}