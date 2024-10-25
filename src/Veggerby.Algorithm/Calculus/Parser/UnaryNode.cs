namespace Veggerby.Algorithm.Calculus.Parser;

public class UnaryNode(Token token, Node inner) : Node(token)
{
    public Node Inner { get; } = inner;

    public override string ToString() => $"{base.ToString()}({Inner})";
}