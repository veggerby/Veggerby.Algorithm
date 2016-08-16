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

        public override Operand GetDerivative(Variable variable)
        {
            var inner = Inner.GetDerivative(variable);

            if (inner == null)
            {
                return null;
            }

            // chain rule
            return -1 * inner * new Sine(Inner);
        }

        public override string ToString()
        {
            return $"cos({Inner})";
        }
    }
}