using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Cosine : UnaryOperation, IEquatable<Cosine>
    {
        private Cosine(Operand inner) : base(inner)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

        public static Operand Create(Operand inner) => new Cosine(inner);

        public override bool Equals(object obj) => Equals(obj as Cosine);
        public override bool Equals(Operand other) => Equals(other as Cosine);
        public bool Equals(Cosine other) => other != null && Inner.Equals(other.Inner);
        public override int GetHashCode() => base.GetHashCode();
    }
}