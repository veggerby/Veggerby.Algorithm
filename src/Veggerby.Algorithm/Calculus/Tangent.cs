using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Tangent : UnaryOperation, IEquatable<Tangent>
    {
        private Tangent(Operand inner) : base(inner)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static Operand Create(Operand inner)
        {
            return new Tangent(inner);
        }

        public override bool Equals(object obj) 
        {
            return Equals(obj as Tangent);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Tangent);
        }

        public bool Equals(Tangent other)
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