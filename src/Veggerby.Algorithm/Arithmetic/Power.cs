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

        protected override string ToString(string left, string right)
        {
            return $"{left}^{right}";
        }
    }
}