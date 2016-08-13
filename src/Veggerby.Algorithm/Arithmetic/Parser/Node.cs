namespace Veggerby.Algorithm.Arithmetic.Parser
{
    public class Node
    {
        public Node Parent { get; set; }
        public string Value { get; }

        public Node(Node parent, string value)
        {
            Parent = parent;
            Value = value;
        }
    }
}