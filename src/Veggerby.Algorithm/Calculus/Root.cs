using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Root : UnaryOperation, IEquatable<Root>
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

        public static Operand Create(int exponent, Operand inner)
        {
            return new Root(exponent, inner);
        }

        public override bool Equals(object obj) 
        {
            return Equals(obj as Root);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Root);
        }

        public bool Equals(Root other)
        {
            if (other == null)
            {
                return false;
            }

            return Exponent.Equals(other.Exponent) && Inner.Equals(other.Inner);
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
    }
}