using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Veggerby.Algorithm.Calculus.Parser
{
    public class BinaryNode : Node
    {
        public Node Left { get; }
        public Node Right { get; }

        public BinaryNode(Node left, Token token, Node right) : base(token)
        {
            Left = left;
            Right = right;
        }

        public override string ToString() => $"({Left}){base.ToString()}({Right})";
    }
}