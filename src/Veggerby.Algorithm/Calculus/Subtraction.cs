using System;

namespace Veggerby.Algorithm.Calculus
{
    public class Subtraction : BinaryOperation
    {
        private Subtraction(Operand left, Operand right) : base(left, right)
        { 
        }

        public override double Evaluate(OperationContext context)
        {
            return Left.Evaluate(context) - Right.Evaluate(context);
        }

        public override Operand GetDerivative(Variable variable)
        {
            var left = Left.GetDerivative(variable);
            var right = Right.GetDerivative(variable);
            
            return left != null && right != null 
                ? Subtraction.Create(left, right)
                : null;
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override string ToString(string left, string right)
        {
            return $"{left}-{right}";
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
                return 0;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return (Constant)left - (Constant)right;
            }

            if (left.Equals(Constant.Zero))
            {
                return Negative.Create(right);
            }

            return new Subtraction(left, right);
        }
    }
}