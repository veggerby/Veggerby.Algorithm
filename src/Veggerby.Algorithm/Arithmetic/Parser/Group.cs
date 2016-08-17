using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Calculus.Parser
{
    public class Group : Node
    {
        public IList<Node> ChildNodes { get; }

        public Group(Group parent, string value = null) : base(parent, value)
        {
            ChildNodes = new List<Node>();
        }

        public void Add(Node node)
        {
            ChildNodes.Add(node);
        }

        private static string[] _unaryOperations = new [] { "!" };
        private static string[] _binaryOperations = new [] { "^", "/", "*", "-", "+" };

        private static string[] _unaryFunctions = new [] { "sin", "cos", "tan", "exp", "log", "ln" };
        private static string[] _binaryFunctions = new [] { "max", "min" };

        private static string[] _operations = 
            _unaryOperations
            .Concat(_binaryFunctions)
            .Concat(_unaryFunctions)
            .Concat(_binaryOperations)  
            .ToArray();

        private void Replace(UnstructuredNode old, Node @new)
        {
            var ixc = ChildNodes.IndexOf(old);
            ChildNodes.Remove(old);
            ChildNodes.Insert(ixc, @new);
        }

        private void Replace(UnstructuredNode node)
        {
            if (_unaryOperations.Contains(node.Value))
            {
                var operand = ChildNodes.Previous(node);
                var unaryNode = new UnaryNode(node.Parent, node.Value, operand);
                operand.Parent = unaryNode;
                ChildNodes.Remove(operand);
                Replace(node, unaryNode);
            }
            else if (_unaryFunctions.Contains(node.Value))
            {
                var operand = ChildNodes.Next(node);
                var unaryNode = new UnaryNode(node.Parent, node.Value, operand);
                operand.Parent = unaryNode;
                ChildNodes.Remove(operand);
                Replace(node, unaryNode);
            }
            else if (_binaryFunctions.Contains(node.Value))
            {
                var operand1 = ChildNodes.Next(node);
                var operand2 = ChildNodes.Next(operand1);
                var unaryNode = new BinaryNode(node.Parent, node.Value, operand1, operand2);
                operand1.Parent = unaryNode;
                operand2.Parent = unaryNode;
                Replace(node, unaryNode);
                ChildNodes.Remove(operand1);
                ChildNodes.Remove(operand2);
            }
            else if (_binaryOperations.Contains(node.Value))
            {
                var left = ChildNodes.Previous(node);
                var right = ChildNodes.Next(node);
                var unaryNode = new BinaryNode(node.Parent, node.Value, left, right);
                left.Parent = unaryNode;
                right.Parent = unaryNode;
                Replace(node, unaryNode);
                ChildNodes.Remove(left);
                ChildNodes.Remove(right);
            }
            else
            {
                throw new Exception("Unknown operation");
            }
        }

        public void Restructure()
        {
            foreach (var group in ChildNodes.OfType<Group>())
            {
                group.Restructure();
            }

            foreach (var operation in _operations)
            {
                var nodes = ChildNodes
                    .OfType<UnstructuredNode>()
                    .Where(x => string.Equals(x.Value, operation))
                    .ToList();

                foreach (var node in nodes)
                {
                    Replace(node);
                }
            }
        }
    }
}