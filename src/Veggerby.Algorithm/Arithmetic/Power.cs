using System;

namespace Veggerby.Algorithm.Arithmetic
{
    public class Power : BinaryOperation
    {
        public Power(Operand left, Operand right) : base(left, right)
        { 
        }

        public override double Evaluate(OperationContext context)
        {
            return Math.Pow(Left.Evaluate(context), Right.Evaluate(context));
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override Operand GetDerivative(Variable variable)
        {
            if (Right.IsConstant())
            {
                return Right * Left ^ (Right - 1);
            }

            // exponential rule
            return new Exponential(Right * new Logarithm(Left));
        }

        protected override string ToString(string left, string right)
        {
            return $"{left}^{right}";
        }
    }
}