using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Sine : UnaryOperation, IEquatable<Sine>
    {
        private Sine(Operand inner) : base(inner)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

        public static Operand Create(Operand inner) => new Sine(inner);

        public override bool Equals(object obj) => Equals(obj as Sine);
        public override bool Equals(Operand other) => Equals(other as Sine);
        public bool Equals(Sine other) => other != null && Inner.Equals(other.Inner);
        public override int GetHashCode() => base.GetHashCode();
    }
}