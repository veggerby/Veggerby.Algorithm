using System;

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
            if (this is ICommutativeBinaryOperation)
            {
                var thisSet = ((ICommutativeBinaryOperation)this).FlattenCommutative();
                var otherSet = ((ICommutativeBinaryOperation)other).FlattenCommutative();

                return thisSet.SetEquals(otherSet);
            }

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
            return ToString().GetHashCode();
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