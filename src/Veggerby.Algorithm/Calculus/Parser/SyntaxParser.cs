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
                throw new ArgumentException("Incomplete tree");
            }

            var node = ParseGroup(fsm.Root);

            //fsm.RootNode.Validate(); // check that structure is intact, e.g. idenfier has no children, etc.

            return node;
        }

        private Node ParseGroupChildrenForBinary(TokenType type, string value, IEnumerable<Group> children)
        {
            var childList = children.Where(x => x.Token.Type != TokenType.StartParenthesis).ToList();

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
                ParseGroupChildrenForBinary(TokenType.OperatorPriority1, "^", children);
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

            return null;
        }
    }
}