using System;

namespace Veggerby.Algorithm.Arithmetic
{
    public class Tangent : UnaryOperation
    {
        public Tangent(Operand inner) : base(inner)
        {
        }

        public override double Evaluate(OperationContext context)
        {
            var inner = Inner.Evaluate(context);

            return Math.Tan(inner);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"tan({Inner})";
        }
    }
}