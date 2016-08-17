namespace Veggerby.Algorithm.Calculus.Parser
{
    public class BinaryNode : Node
    {
        public Node Left { get; }
        public Node Right { get; }

        public BinaryNode(Node parent, string value, Node left, Node right) : base(parent, value)
        {
            Left = left;
            Right = right;
        }
    }
}