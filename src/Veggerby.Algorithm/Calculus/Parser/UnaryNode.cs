namespace Veggerby.Algorithm.Calculus.Parser
{
    public class UnaryNode : Node
    {
        public Node Inner { get; }

        public UnaryNode(Node parent, string value, Node inner) : base(parent, value)
        {
            Inner = inner;
        }
    }
}