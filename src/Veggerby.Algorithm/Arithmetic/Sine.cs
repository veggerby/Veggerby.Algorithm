using System;

namespace Veggerby.Algorithm.Arithmetic
{
    public class Sine : UnaryOperation
    {
        public Sine(Operand inner) : base(inner)
        {
        }

        public override double Evaluate(OperationContext context)
        {
            var inner = Inner.Evaluate(context);

            return Math.Sin(inner);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"sin({Inner})";
        }
    }
}