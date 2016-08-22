namespace Veggerby.Algorithm.Calculus
{
    public class Logarithm : UnaryOperation
    {
        private Logarithm(Operand inner) : base(inner)
        {
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public static Operand Create(Operand inner)
        {
            return new Logarithm(inner);
        }
    }
}