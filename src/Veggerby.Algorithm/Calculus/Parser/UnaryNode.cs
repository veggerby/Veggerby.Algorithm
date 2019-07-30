namespace Veggerby.Algorithm.Calculus.Parser
{
    public class UnaryNode : Node
    {
        public Node Inner { get; }

        public UnaryNode(Token token, Node inner) : base(token)
        {
            Inner = inner;
        }

        public override string ToString() => $"{base.ToString()}({Inner})";
    }
}