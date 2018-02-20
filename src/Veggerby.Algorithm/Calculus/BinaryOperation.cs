using System;
using System.Linq;

namespace Veggerby.Algorithm.Calculus
{
    public abstract class BinaryOperation : Operand, IBinaryOperation
    {
        public Operand Left { get; }
        public Operand Right { get; }

        public override int MaxDepth => Math.Max(Left.MaxDepth, Right.MaxDepth) + 1;

        protected BinaryOperation(Operand left, Operand right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            Left = left;
            Right = right;
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() ^ Left.GetHashCode() ^ Right.GetHashCode();
        }
    }
}