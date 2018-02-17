using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Logarithm : UnaryOperation, IEquatable<Logarithm>
    {
        private Logarithm(Operand inner) : base(inner)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static Operand Create(Operand inner)
        {
            return new Logarithm(inner);
        }

        public override bool Equals(object obj) 
        {
            return Equals(obj as Logarithm);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Logarithm);
        }

        public bool Equals(Logarithm other)
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