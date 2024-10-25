namespace Veggerby.Algorithm.Calculus.Parser;

public class Token(TokenType tokenType, string value, TokenPosition position)
{
    public TokenType Type { get; } = tokenType;
    public string Value { get; } = value;
    public TokenPosition Position { get; } = position;

    public override string ToString() => $"Token: {Type}={Value}";
    protected bool Equals(Token other) => other is not null && Type.Equals(other.Type) && string.Equals(Value, other.Value) && Position.Equals(other.Position);
    public override bool Equals(object obj) => Equals(obj as Token);

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Type.GetHashCode();
            hashCode = (hashCode * 397) ^ Value?.GetHashCode() ?? 0;
            hashCode = (hashCode * 397) ^ Position?.GetHashCode() ?? 0;
            return hashCode;
        }
    }
}