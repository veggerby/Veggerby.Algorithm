using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Division : BinaryOperation
    {
        private Division(Operand left, Operand right) : base(left, right)
        {
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        public static Operand Create(Operand left, Operand right, bool reduce = true)
        {
            if (!reduce)
            {
                return new Division(left, right);
            }

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
                return 1;
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

            if (left.IsNegative() && right.IsNegative())
            {
                return Division.Create(((Negative)left).Inner, ((Negative)right).Inner);
            }

            if (left.IsNegative())
            {
                return Negative.Create(Division.Create(((Negative)left).Inner, right));
            }

            if (right.IsNegative())
            {
                return Negative.Create(Division.Create(left, ((Negative)right).Inner));
            }

            if (left is Division && right is Division)
            {
                return Division.Create(
                    Multiplication.Create(((Division)left).Left, ((Division)right).Right),
                    Multiplication.Create(((Division)left).Right, ((Division)right).Left));
            }

            if (left is Division)
            {
                return Division.Create(
                    ((Division)left).Left,
                    Multiplication.Create(((Division)left).Right, right));
            }

            if (right is Division)
            {
                return Division.Create(
                    Multiplication.Create(left, ((Division)right).Right),
                    ((Division)right).Left);
            }

            return new Division(left, right);
        }
    }
}