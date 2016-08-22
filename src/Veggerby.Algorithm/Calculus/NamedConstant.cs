using Veggerby.Algorithm.Calculus.Visitors;

namespace Veggerby.Algorithm.Calculus
{
    public class NamedConstant : Constant
    {
        public string Symbol { get; }

        protected NamedConstant(string symbol, double value) : base(value)
        {
            Symbol = symbol;
        }

        public override void Accept(IOperandVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected bool Equals(NamedConstant other)
        {
            return Symbol.Equals(other.Symbol) && Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NamedConstant)obj);
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

        public static NamedConstant Create(string symbol, double value)
        {
            return new NamedConstant(symbol, value);
        }
    }
}