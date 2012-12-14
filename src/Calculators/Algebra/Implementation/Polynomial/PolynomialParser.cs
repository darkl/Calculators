using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Calculators.Algebra.Abstract;
using Mono.CSharp;
using Delegate = System.Delegate;

namespace Calculators.Algebra
{
    public class PolynomialParser
    {
        private readonly Evaluator mEvaluator;
        private readonly string mArgument;
        private readonly string mDeclarationFormat;
        private readonly Dictionary<string, object> mAliases;

        public PolynomialParser() : this(new PolynomialParserOptions())
        {
        }

        public PolynomialParser(PolynomialParserOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            mEvaluator = CreateEvaluator();

            mArgument = options.Argument ?? "x";

            mAliases = options.Aliases ?? new Dictionary<string, object>();

            const string declarationFormatFormat =
                "Func<{0}, dynamic> evaluator = ({1}) => ({{0}});";

            if (mAliases != null)
            {
                string parameterTypes =
                    string.Join(", ",
                                new[] {mArgument}.Concat(mAliases.Keys).Select(x => "dynamic"));

                string parameterNames =
                    string.Join(", ",
                                new[] {mArgument}.Concat(mAliases.Keys).Select(x => "dynamic " + x));

                mDeclarationFormat = string.Format(declarationFormatFormat, parameterTypes, parameterNames);
            }
        }

        private static Evaluator CreateEvaluator()
        {
            var result =
                new Evaluator(new CompilerSettings()
                                  {
                                      AssemblyReferences =
                                          new List<string>() {"System.Core"}
                                  },
                              new Report(new StreamReportPrinter(new StringWriter(new StringBuilder()))));

            result.ReferenceAssembly(typeof(OperatorSyntax<>).Assembly);

            result.Run("using System;");
            result.Run("using Calculators.Algebra;");

            return result;
        }

        public Polynomial<T> Parse<T>(string expression, IRing<T> ring)
        {
            string declaration =
                string.Format(mDeclarationFormat, expression);

            mEvaluator.Run(declaration);
            Delegate evaluator =
                mEvaluator.Evaluate("evaluator;")
                as Delegate;

            PolynomialRing<T> polynomialRing = new PolynomialRing<T>(ring);

            OperatorSyntax<Polynomial<T>> polynomialArgument = 
                new OperatorSyntax<Polynomial<T>>(ring.CreateMonomial(ring.Identity, 1), polynomialRing);

            object[] arguments = new object[] {polynomialArgument}.Concat(mAliases.Values).ToArray();

            OperatorSyntax<Polynomial<T>> evaluated =
                evaluator.DynamicInvoke(arguments) as OperatorSyntax<Polynomial<T>>;

            return evaluated.Value;
        }
    }
}