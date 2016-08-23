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
            if (this is ICommutativeBinaryOperation)
            {
                var thisSet = ((ICommutativeBinaryOperation)this).FlattenCommutative().ToList();
                var otherSet = ((ICommutativeBinaryOperation)other).FlattenCommutative().ToList();

                // simple case -> different items in list
                if (thisSet.Count() != otherSet.Count())
                {
                    return false;
                }

                // if either set contains items not in the other
                if (thisSet.Except(otherSet).Any() || otherSet.Except(thisSet).Any())
                {
                    return false;
                }

                // compare the number of item count to ensure 2*x*2 != x*2*x
                return thisSet.GroupBy(x => x)
                    .Join(otherSet.GroupBy(x => x), x => x.Key, x => x.Key, (a, b) => a.Count() - b.Count())
                    .All(x => x == 0);
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
            if (this is ICommutativeBinaryOperation)
            {
                var thisSet = ((ICommutativeBinaryOperation)this).FlattenCommutative();

                return thisSet.Aggregate(GetType().Name.GetHashCode(), (seed, x) => seed ^ x.GetHashCode());
            }

            return GetType().Name.GetHashCode() ^ Left.GetHashCode() ^ Right.GetHashCode();
        }
    }
}