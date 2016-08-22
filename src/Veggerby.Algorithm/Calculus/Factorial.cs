using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Factorial : UnaryOperation
    {
        private Factorial(Operand inner) : base(inner)
        {
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public static Operand Create(Operand inner)
        {
            return new Factorial(inner);
        }
    }
}