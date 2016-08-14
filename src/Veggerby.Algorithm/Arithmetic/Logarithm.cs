using System;

namespace Veggerby.Algorithm.Arithmetic
{
    public class Logarithm : UnaryOperation
    {
        public Logarithm(Operand inner) : base(inner)
        {
        }

        public override double Evaluate(OperationContext context)
        {
            var inner = Inner.Evaluate(context);
            return Math.Log(inner);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"ln({Inner})";
        }
    }
}