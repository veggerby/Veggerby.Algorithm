using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Subtraction : BinaryOperation
    {
        private Subtraction(Operand left, Operand right) : base(left, right)
        {
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

            if (right.IsNegative())
            {
                return Addition.Create(left, ((Negative)right).Inner);
            }

            if (right.IsConstant() && ((Constant)right).Value < 0)
            {
                return Addition.Create(left, -((Constant)right).Value);
            }

            return new Subtraction(left, right);
        }
    }
}