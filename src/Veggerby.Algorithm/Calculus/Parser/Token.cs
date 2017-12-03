namespace Veggerby.Algorithm.Calculus.Parser
{
    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }
        public TokenPosition Position { get; }

        public Token(TokenType tokenType, string value, TokenPosition position)
        {
            Type = tokenType;
            Value = value;
            Position = position;
        }

        public override string ToString() => $"Token: {Type}={Value}";

        protected bool Equals(Token other)
        {
            return Type.Equals(other.Type) && string.Equals(Value, other.Value) && Position.Equals(other.Position);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Token)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Type.GetHashCode();
                hashCode = (hashCode*397) ^ Value?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ Position?.GetHashCode() ?? 0;
                return hashCode;
            }
        }
    }
}