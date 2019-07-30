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

        public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

        public static Operand Create(int exponent, Operand inner) => new Root(exponent, inner);

        public override bool Equals(object obj) => Equals(obj as Root);
        public override bool Equals(Operand other) => Equals(other as Root);
        public bool Equals(Root other) => other != null && Exponent.Equals(other.Exponent) && Inner.Equals(other.Inner);

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