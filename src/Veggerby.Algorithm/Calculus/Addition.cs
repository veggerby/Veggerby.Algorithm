using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Addition : BinaryOperation, ICommutativeBinaryOperation, IAssociativeBinaryOperation
    {
        private Addition(Operand left, Operand right) : base(left, right)
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

            if (left.Equals(Constant.Zero))
            {
                return right;
            }

            if (right.Equals(Constant.Zero))
            {
                return left;
            }

            return new Addition(left, right);
        }
    }
}