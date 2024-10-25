namespace Veggerby.Algorithm.Calculus.Parser;

public class Node(Token token)
{
    public Token Token { get; } = token;

    public override string ToString() => Token.ToString();
}