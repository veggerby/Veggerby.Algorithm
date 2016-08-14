using System;

namespace Veggerby.Algorithm.Arithmetic
{
    public class Cosine : UnaryOperation
    {
        public Cosine(Operand inner) : base(inner)
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

        public override string ToString()
        {
            return $"cos({Inner})";
        }
    }
}