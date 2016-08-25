using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Maximum : BinaryOperation, ICommutativeBinaryOperation, IAssociativeBinaryOperation
    {
        public Maximum(Operand left, Operand right) : base(left, right)
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
                return left;
            }

            if (left.IsConstant() && right.IsConstant())
            {
                return Math.Max((Constant)left, (Constant)right);
            }

            return new Maximum(left, right);
        }
    }
}