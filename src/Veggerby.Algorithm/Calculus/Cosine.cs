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

        public override Operand GetDerivative(Variable variable)
        {
            var inner = Inner.GetDerivative(variable);

            if (inner == null)
            {
                return null;
            }

            // chain rule
            return Multiplication.Create(Negative.Create(inner), Sine.Create(Inner));
        }

        public static Operand Create(Operand inner)
        {
            return new Cosine(inner);
        }
    }
}