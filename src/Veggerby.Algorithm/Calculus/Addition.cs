using System;

namespace Veggerby.Algorithm.Calculus
{
    public class Addition : BinaryOperation, ICommutativeBinaryOperation, IAssociativeBinaryOperation
    {
        private Addition(Operand left, Operand right) : base(left, right)
        { 
        }

        public override double Evaluate(OperationContext context)
        {
            return Left.Evaluate(context) + Right.Evaluate(context);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override Operand GetDerivative(Variable variable)
        {
            var left = Left.GetDerivative(variable);
            var right = Right.GetDerivative(variable);

            return left != null && right != null 
                ? left + right
                : null;
        }

        protected override string ToString(string left, string right)
        {
            return $"{left}+{right}";
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
                return Multiplication.Create(2, left);
            }

            if (left.Equals(Constant.Zero))
            {
                return right;
            }

            if (right.Equals(Constant.Zero))
            {
                return left;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return (Constant)left + (Constant)right;
            }

            if (right.IsNegative())
            {
                return Subtraction.Create(left, ((Negative)right).Inner);
            }

            if (right.IsConstant() && ((Constant)right).Value < 0)
            {
                return Subtraction.Create(left, -((Constant)right).Value);
            }

            return new Addition(left, right);
        }
    }
}