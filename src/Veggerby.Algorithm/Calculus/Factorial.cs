using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Factorial : UnaryOperation, IEquatable<Factorial>
    {
        private Factorial(Operand inner) : base(inner)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor) => visitor.Visit(this);

        public static Operand Create(Operand inner) => new Factorial(inner);

        public override bool Equals(object obj) => Equals(obj as Factorial);
        public override bool Equals(Operand other) => Equals(other as Factorial);
        public bool Equals(Factorial other) => other != null && Inner.Equals(other.Inner);
        public override int GetHashCode() => base.GetHashCode();
    }
}