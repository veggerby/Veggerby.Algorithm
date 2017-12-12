using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Root : UnaryOperation
    {
        public int Exponent { get; }
        private Root(int exponent, Operand inner) : base(inner)
        {
            if (exponent < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(exponent));
            }

            Exponent = exponent;
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        protected bool Equals(Root other)
        {
            return Exponent.Equals(other.Exponent) && base.Equals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Root)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Exponent.GetHashCode();
                hashCode = (hashCode*397) ^ Inner.GetHashCode();
                return hashCode;
            }
        }

        public static Operand Create(int exponent, Operand inner)
        {
            return new Root(exponent, inner);
        }
    }
}