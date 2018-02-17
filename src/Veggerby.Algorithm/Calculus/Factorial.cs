using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Factorial : UnaryOperation, IEquatable<Factorial>
    {
        private Factorial(Operand inner) : base(inner)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static Operand Create(Operand inner)
        {
            return new Factorial(inner);
        }

        public override bool Equals(object obj) 
        {
            return Equals(obj as Factorial);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Factorial);
        }

        public bool Equals(Factorial other)
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