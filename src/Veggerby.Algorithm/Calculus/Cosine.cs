using System;

namespace Veggerby.Algorithm.Calculus
{
    public class Cosine : UnaryOperation
    {
        private Cosine(Operand inner) : base(inner)
        {
        }

        public override double Evaluate(OperationContext context)
        {
            var inner = Inner.Evaluate(context);

            return Math.Cos(inner);
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