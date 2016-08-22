using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Cosine : UnaryOperation
    {
        private Cosine(Operand inner) : base(inner)
        {
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public static Operand Create(Operand inner)
        {
            return new Cosine(inner);
        }
    }
}