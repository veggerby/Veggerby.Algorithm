using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Multiplication : BinaryOperation, ICommutativeBinaryOperation, IAssociativeBinaryOperation
    {
        private Multiplication(Operand left, Operand right) : base(left, right)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
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

            if (left.Equals(Constant.Zero) || right.Equals(Constant.Zero))
            {
                return Constant.Zero;
            }

            if (right.Equals(Constant.One))
            {
                return left;
            }

            if (left.Equals(Constant.One))
            {
                return right;
            }

            if (right.Equals(Constant.MinusOne))
            {
                return Negative.Create(left);
            }

            if (left.Equals(Constant.MinusOne))
            {
                return Negative.Create(right);
            }

            return new Multiplication(left, right);
        }
    }
}