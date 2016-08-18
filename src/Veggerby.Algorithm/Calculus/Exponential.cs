using System;

namespace Veggerby.Algorithm.Calculus
{
    public class Exponential : UnaryOperation
    {
        private Exponential(Operand inner) : base(inner)
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

        public override Operand GetDerivative(Variable variable)
        {
            var inner = Inner.GetDerivative(variable);

            if (inner == null)
            {
                return null;
            }

            // chain rule
            return Multiplication.Create(inner, new Exponential(Inner));
        }

        public override string ToString()
        {
            return $"exp({Inner})";
        }

        public static Operand Create(Operand inner)
        {
            return new Exponential(inner);
        }
    }
}