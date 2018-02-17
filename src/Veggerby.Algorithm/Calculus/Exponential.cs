using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Exponential : UnaryOperation, IEquatable<Exponential>
    {
        private Exponential(Operand inner) : base(inner)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static Operand Create(Operand inner)
        {
            return new Exponential(inner);
        }
        
        public override bool Equals(object obj) 
        {
            return Equals(obj as Exponential);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Exponential);
        }

        public bool Equals(Exponential other)
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