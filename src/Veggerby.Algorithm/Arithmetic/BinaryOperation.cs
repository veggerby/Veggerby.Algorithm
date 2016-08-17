using System;

namespace Veggerby.Algorithm.Calculus
{
    public abstract class BinaryOperation : Operand, IBinaryOperation
    {
        public Operand Left { get; }
        public Operand Right { get; }

        public BinaryOperation(Operand left, Operand right)
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
            return Left.Equals(other.Left) && Right.Equals(other.Right);
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
            unchecked
            {
                var hashCode = Left.GetHashCode();
                hashCode = (hashCode*397) ^ Right.GetHashCode();
                return hashCode;
            }
        }

        protected abstract string ToString(string left, string right);

        public override string ToString()
        {
            var leftPriority = Left.GetPriority();
            var rightPriority = Right.GetPriority();
            var thisPriority = this.GetPriority();

            var left = Left.CouldUseParenthesis() && (leftPriority != null && leftPriority > thisPriority) ? $"({Left})" : Left.ToString();
            var right = Right.CouldUseParenthesis() && (rightPriority != null && rightPriority > thisPriority) ? $"({Right})" : Right.ToString();
            return ToString(left, right);
        }
    }
}