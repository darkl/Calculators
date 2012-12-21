namespace Calculators.Algebra
{
    public class IntegersModuloRing : QuotientRing<int>
    {
        public IntegersModuloRing(int number) : 
            base(new PrincipalIdeal<int>(new IntegersRing(), number))
        {
        }
    }

    public class IntegersModuloField : QuotientField<int>
    {
        public IntegersModuloField(int number) :
            base(new PrincipalIdeal<int>(new IntegersRing(), number))
        {
        }
    }
}