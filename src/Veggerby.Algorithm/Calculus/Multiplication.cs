using System;

namespace Veggerby.Algorithm.Calculus
{
    public class Multiplication : BinaryOperation, ICommutativeBinaryOperation, IAssociativeBinaryOperation
    {
        private Multiplication(Operand left, Operand right) : base(left, right)
        { 
        }

        public override double Evaluate(OperationContext context)
        {
            return Left.Evaluate(context) * Right.Evaluate(context);
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
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

            if (left.Equals(Constant.Zero) || right.Equals(Constant.Zero))
            {
                return Constant.Zero;
            }

            if (left.Equals(Constant.One))
            {
                return right;
            }

            if (right.Equals(Constant.One))
            {
                return left;
            }

            if (left.IsNegative() && right.IsNegative())
            {
                return Multiplication.Create(((Negative)left).Inner, ((Negative)right).Inner);
            }

            if (left.IsNegative())
            {
                return Negative.Create(Multiplication.Create(((Negative)left).Inner, right));
            }

            if (right.IsNegative())
            {
                return Negative.Create(Multiplication.Create(left, ((Negative)right).Inner));
            }

            if (left.Equals(Constant.MinusOne))
            {
                return Negative.Create(right);
            }

            if (right.Equals(Constant.MinusOne))
            {
                return Negative.Create(left);
            }

            if (!left.IsConstant() && right.IsConstant())
            {
                return Multiplication.Create(right, left);
            }

            return new Multiplication(left, right);
        }
    }
}