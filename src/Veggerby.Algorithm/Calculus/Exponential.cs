using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Exponential : UnaryOperation
    {
        private Exponential(Operand inner) : base(inner)
        {
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public static Operand Create(Operand inner)
        {
            return new Exponential(inner);
        }
    }
}