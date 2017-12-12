using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Subtraction : BinaryOperation
    {
        private Subtraction(Operand left, Operand right) : base(left, right)
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

            if (right.Equals(Constant.Zero))
            {
                return left;
            }

            if (left.Equals(Constant.Zero))
            {
                return Negative.Create(right);
            }

            return new Subtraction(left, right);
        }
    }
}