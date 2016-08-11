using System;

namespace Veggerby.Algorithm.Arithmetic
{
    public abstract class BinaryOperation : IBinaryOperation
    {
        public IOperand Left { get; }
        public IOperand Right { get; }

        public BinaryOperation(IOperand left, IOperand right)
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

        public abstract double Evaluate(OperationContext context);
        public abstract void Accept(IOperandVisitor visitor);

        protected bool Equals(BinaryOperation other)
        {
            return Left.Equals(other.Left) || Right.Equals(other.Right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Constant)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Left.GetHashCode();
                hashCode = (hashCode*397) ^ Right.GetHashCode();
                return hashCode;
            }
        }
    }
}