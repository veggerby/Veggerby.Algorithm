using System;

namespace Veggerby.Algorithm.Calculus
{
    public class Sine : UnaryOperation
    {
        private Sine(Operand inner) : base(inner)
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

        public override Operand GetDerivative(Variable variable)
        {
            var inner = Inner.GetDerivative(variable);

            if (inner == null)
            {
                return null;
            }

            // chain rule
            return Multiplication.Create(inner, Cosine.Create(Inner));
        }

        public override string ToString()
        {
            return $"sin({Inner})";
        }

        public static Operand Create(Operand inner)
        {
            return new Sine(inner);
        }
    }
}