namespace Veggerby.Algorithm.Calculus
{
    public class Sine : UnaryOperation
    {
        private Sine(Operand inner) : base(inner)
        {
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public static Operand Create(Operand inner)
        {
            return new Sine(inner);
        }
    }
}