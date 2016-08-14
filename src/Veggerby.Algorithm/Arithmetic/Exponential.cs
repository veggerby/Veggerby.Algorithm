using System;

namespace Veggerby.Algorithm.Arithmetic
{
    public class Exponential : UnaryOperation
    {
        public Exponential(Operand inner) : base(inner)
        {
        }

        public override double Evaluate(OperationContext context)
        {
            var inner = Inner.Evaluate(context);
            return Math.Exp(inner);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"e^{Inner}";
        }
    }
}