namespace Veggerby.Algorithm.Calculus.Parser;

public class BinaryNode(Node left, Token token, Node right) : Node(token)
{
    public Node Left { get; } = left;
    public Node Right { get; } = right;

    public override string ToString() => $"({Left}){base.ToString()}({Right})";
}