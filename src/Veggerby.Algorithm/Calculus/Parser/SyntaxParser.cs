using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Veggerby.Algorithm.Calculus.Parser
{
    public class SyntaxParser
    {
        public Node ParseTree(IEnumerable<Token> tokens)
        {
            if (tokens == null)
            {
                throw new ArgumentNullException(nameof(tokens));
            }

            var fsm = new SyntaxStateMachine();

            foreach (var token in tokens)
            {
               fsm.GetNext(token);
            }

            if (!fsm.IsClosed)
            {
                throw new ArgumentException("Parenthesis not properly closed");
            }

            return ParseGroup(fsm.Root);
        }

        private Node ParseGroupChildrenForBinary(TokenType type, string value, IEnumerable<Group> children)
        {
            var childList = children.ToList();

            var leftGroups = new List<Group>();
            var rightGroups = new List<Group>();
            Group previous = null;
            Group tokenGroup = null;

            foreach (var group in childList)
            {
                if (tokenGroup != null)
                {
                    rightGroups.Add(group);
                }
                else if (group.Token.Type == type && string.Equals(group.Token.Value, value, StringComparison.OrdinalIgnoreCase))
                {
                    if (previous == null || (previous.Token.Type != TokenType.Sign && previous.Token.Type != TokenType.OperatorPriority1))
                    {
                        tokenGroup = group;
                    }
                }

                if (tokenGroup == null)
                {
                    leftGroups.Add(group);
                }

                previous = group;
            }

            if (tokenGroup == null)
            {
                return null;
            }

            var left = ParseGroupChildren(leftGroups);
            var right = ParseGroupChildren(rightGroups);

            return new BinaryNode(left, tokenGroup.Token, right);
        }

        private Node ParseGroupChildrenForBinaryFunction(TokenType type, string[] values, IEnumerable<Group> children)
        {
            if (children.Count() != 3)
            {
                return null;
            }

            var function = children.First();

            if (!values.Any(x => string.Equals(x, function.Token.Value, StringComparison.OrdinalIgnoreCase)))
            {
                return null;
            }

            var leftGroup = children.Skip(1).First();
            var rightGroup = children.Last();


            var left = ParseGroup(leftGroup);
            var right = ParseGroup(rightGroup);

            return new BinaryNode(left, function.Token, right);
        }

        private Node ParseGroupChildrenForUnary(TokenType type, IEnumerable<Group> children)
        {
            if (children.Count() != 2)
            {
                return null;
            }

            var first = children.First();

            if (first.Token.Type != type)
            {
                return null;
            }

            var second = children.Last();

            var inner = ParseGroup(second);
            return new UnaryNode(first.Token, inner);
        }

        private Node ParseGroupChildren(IEnumerable<Group> children)
        {
            if (children == null || !children.Any())
            {
                return null;
            }

            if (children.Count() == 1)
            {
                return ParseGroup(children.Single());
            }

            return
                ParseGroupChildrenForBinary(TokenType.Sign, "+", children) ??
                ParseGroupChildrenForBinary(TokenType.Sign, "-", children) ??
                ParseGroupChildrenForBinary(TokenType.OperatorPriority1, "*", children) ??
                ParseGroupChildrenForBinary(TokenType.OperatorPriority1, "/", children) ??
                ParseGroupChildrenForBinary(TokenType.OperatorPriority1, "^", children) ??
                ParseGroupChildrenForBinaryFunction(TokenType.Function, new [] { "root", "min", "max" }, children) ??
                ParseGroupChildrenForUnary(TokenType.Factorial, children.Reverse()) ??
                ParseGroupChildrenForUnary(TokenType.Function, children);
        }

        private Node ParseGroup(Group group)
        {
            if (!group.Children.Any())
            {
                return new Node(group.Token);
            }

            var childNode = ParseGroupChildren(group.Children);

            switch (group.Token.Type)
            {
                case TokenType.Start:
                    return childNode;
                case TokenType.Function:
                    return new UnaryNode(group.Token, childNode);
            }

            return childNode;
        }
    }
}