namespace Calculators.Algebra
{
    public class IntegersModuloRing : QuotientRing<int>
    {
        public IntegersModuloRing(int number) : 
            base(new PrincipalIdeal<int>(new IntegersRing(), number))
        {
        }
    }
}