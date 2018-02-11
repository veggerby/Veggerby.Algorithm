using System;
using System.Linq;

namespace Veggerby.Algorithm.Calculus
{
    public abstract class BinaryOperation : Operand, IBinaryOperation
    {
        public Operand Left { get; }
        public Operand Right { get; }

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

        protected bool Equals(BinaryOperation other)
        {
            return (Left.Equals(other.Left) && Right.Equals(other.Right)) ||
                (this is ICommutativeOperation && Left.Equals(other.Right) && Right.Equals(other.Left));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BinaryOperation)obj);
        }

        public override int GetHashCode()
        {
            return GetType().Name.GetHashCode() ^ Left.GetHashCode() ^ Right.GetHashCode();
        }
    }
}