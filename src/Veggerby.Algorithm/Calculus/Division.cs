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
                return 1;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return (Constant)left / (Constant)right;
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

            return new Division(left, right);
        }
    }
}