using System.Collections.Generic;

namespace Veggerby.Algorithm.Calculus.Parser
{
    public class MultiNode : Node
    {
        public IEnumerable<Node> Nodes { get; }

        public MultiNode(Node parent, string value, IEnumerable<Node> nodes) : base(parent, value)
        {
            Nodes = nodes;
        }
    }
}