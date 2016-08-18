using System;

namespace Veggerby.Algorithm.Calculus
{
    public class Tangent : UnaryOperation
    {
        private Tangent(Operand inner) : base(inner)
        {
        }

        public override double Evaluate(OperationContext context)
        {
            var inner = Inner.Evaluate(context);

            return Math.Tan(inner);
        }

        public override Operand GetDerivative(Variable variable)
        {
            throw new NotImplementedException();
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"tan({Inner})";
        }

        public static Operand Create(Operand inner)
        {
            return new Tangent(inner);
        }
    }
}