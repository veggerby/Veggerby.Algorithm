using System;
using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class NamedConstant : Constant, IEquatable<NamedConstant>
    {
        public string Symbol { get; }

        protected NamedConstant(string symbol, double value) : base(value)
        {
            Symbol = symbol;
        }

        public override T Accept<T>(IOperandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public static NamedConstant Create(string symbol, double value)
        {
            return new NamedConstant(symbol, value);
        }
                
        public override bool Equals(object obj) 
        {
            return Equals(obj as NamedConstant);
        }

        public override bool Equals(Operand other)
        {
            return Equals(other as NamedConstant);
        }

        public bool Equals(NamedConstant other)
        {
            if (other == null)
            {
                return false;
            }

            return Symbol.Equals(other.Symbol) && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Symbol.GetHashCode();
                hashCode = (hashCode*397) ^ Value.GetHashCode();
                return hashCode;
            }
        }
    }
}