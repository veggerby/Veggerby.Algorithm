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
    }
}