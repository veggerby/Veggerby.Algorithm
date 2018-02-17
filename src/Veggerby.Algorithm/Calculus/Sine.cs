using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Sine : UnaryOperation, IEquatable<Sine>
    {
        private Sine(Operand inner) : base(inner)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static Operand Create(Operand inner)
        {
            return new Sine(inner);
        }

        public override bool Equals(object obj) 
        {
            return Equals(obj as Sine);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Sine);
        }

        public bool Equals(Sine other)
        {
            if (other == null)
            {
                return false;
            }

            return Inner.Equals(other.Inner);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}