using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Exponential : UnaryOperation, IEquatable<Exponential>
    {
        private Exponential(Operand inner) : base(inner)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

        public static Operand Create(Operand inner) => new Exponential(inner);

        public override bool Equals(object obj) => Equals(obj as Exponential);
        public override bool Equals(Operand other) => Equals(other as Exponential);
        public bool Equals(Exponential other) => other != null && Inner.Equals(other.Inner);
        public override int GetHashCode() => base.GetHashCode();
    }
}