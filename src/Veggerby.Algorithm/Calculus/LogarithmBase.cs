using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class LogarithmBase : UnaryOperation, IEquatable<LogarithmBase>
    {
        public int Base { get; }

        private LogarithmBase(int @base, Operand inner) : base(inner)
        {
            Base = @base;
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static Operand Create(int @base, Operand inner)
        {
            return new LogarithmBase(@base, inner);
        }

        public override bool Equals(object obj) 
        {
            return Equals(obj as LogarithmBase);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as LogarithmBase);
        }

        public bool Equals(LogarithmBase other)
        {
            if (other == null)
            {
                return false;
            }

            return Base == other.Base && Inner.Equals(other.Inner);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Base.GetHashCode();
                hashCode = (hashCode*397) ^ Inner.GetHashCode();
                return hashCode;
            }
        }
    }
}