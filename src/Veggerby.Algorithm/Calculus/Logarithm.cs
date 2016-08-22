using System;

namespace Veggerby.Algorithm.Calculus
{
    public class Logarithm : UnaryOperation
    {
        private Logarithm(Operand inner) : base(inner)
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

        public override Operand GetDerivative(Variable variable)
        {
            var inner = Inner.GetDerivative(variable);

            if (inner == null)
            {
                return null;
            }

            // chain rule
            return Division.Create(inner, Inner);
        }

        public static Operand Create(Operand inner)
        {
            return new Logarithm(inner);
        }
    }
}