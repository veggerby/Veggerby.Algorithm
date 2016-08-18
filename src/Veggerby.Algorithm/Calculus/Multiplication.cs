using System;

namespace Veggerby.Algorithm.Calculus
{
    public class Multiplication : BinaryOperation
    {
        private Multiplication(Operand left, Operand right) : base(left, right)
        { 
        }

        public override double Evaluate(OperationContext context)
        {
            return Left.Evaluate(context) * Right.Evaluate(context);
        }

        public override Operand GetDerivative(Variable variable)
        {
            var left = Left.GetDerivative(variable);
            var right = Right.GetDerivative(variable);

            if (left == null || right == null)
            {
                return null;
            }

            // product rule
            return (left * Right + right * Left);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override string ToString(string left, string right)
        {
            return $"{left}*{right}";
        }

        public static Operand Create(Operand left, Operand right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            if (left.Equals(right))
            {
                return Power.Create(left, 2);
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return (Constant)left * (Constant)right;
            }

            return new Multiplication(left, right);
        }
    }
}