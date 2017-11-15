using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veggerby.Algorithm.Calculus.Parser
{
    public class UnaryNode : Node
    {
        public Node Inner { get; }

        public UnaryNode(Token token, Node inner) : base(token)
        {
            Inner = inner;
        }

        public override string ToString()
        {
            return $"{base.ToString()}({Inner})";
        }
    }
}