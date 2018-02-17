using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class Negative : UnaryOperation, IEquatable<Negative>
    {
        private Negative(Operand inner) : base(inner)
        {
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static Operand Create(Operand inner)
        {
            if (inner.IsConstant() && !(inner is NamedConstant))
            {
                return Constant.Create(-((Constant)inner).Value);
            }

            return new Negative(inner);
        }

        public override bool Equals(object obj) 
        {
            return Equals(obj as Negative);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as Negative);
        }

        public bool Equals(Negative other)
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