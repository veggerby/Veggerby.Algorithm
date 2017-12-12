using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Division : BinaryOperation
    {
        private Division(Operand left, Operand right) : base(left, right)
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

            if (left is Fraction && right is Fraction)
            {
                return ((Fraction)left) / ((Fraction)right);
            }

            if (left.IsConstant() && right.IsConstant())
            {
                var l = (Constant)left;
                var r = (Constant)right;

                if (l.IsInteger() && r.IsInteger())
                {
                    return Fraction.Create(l, r);
                }

                return l.Value / r.Value;
            }

            if (left.IsConstant() && right is Fraction)
            {
                return ((Constant)left).Value / (Fraction)right;
            }

            if (left is Fraction && right.IsConstant())
            {
                return ((Fraction)left) / ((Constant)right).Value;
            }

            if (right.Equals(Constant.One))
            {
                return left;
            }

            return new Division(left, right);
        }
    }
}