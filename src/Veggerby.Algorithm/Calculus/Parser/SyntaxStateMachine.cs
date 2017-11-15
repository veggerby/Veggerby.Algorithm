using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Veggerby.Algorithm.Calculus.Parser
{
    public class SyntaxStateMachine
    {
        public Group Root { get; private set; }
        public Group Current { get; private set; }

        public bool IsClosed { get; private set; }

        // whitespace and EOL is explicitly ignored
        private static readonly Dictionary<TokenType, TokenType[]> Transitions = new Dictionary<TokenType, TokenType[]>
        {
            {TokenType.EndParenthesis, new[] { TokenType.End, TokenType.EndOfLine, TokenType.EndParenthesis, TokenType.Function, TokenType.Identifier, TokenType.Number, TokenType.Sign, TokenType.OperatorPriority1, TokenType.Factorial, TokenType.Separator }},  // !TokenType.StartParenthesis
            {TokenType.Function, new[] { TokenType.StartParenthesis }},
            {TokenType.Identifier, new[] { TokenType.OperatorPriority1, TokenType.Sign, TokenType.Factorial, TokenType.Separator, TokenType.EndParenthesis, TokenType.End }},
            {TokenType.Number, new[] { TokenType.OperatorPriority1, TokenType.Sign, TokenType.Factorial, TokenType.Separator, TokenType.EndParenthesis, TokenType.End }},
            {TokenType.Sign, new[] { TokenType.Number, TokenType.Identifier, TokenType.Sign, TokenType.Function, TokenType.StartParenthesis }},
            {TokenType.OperatorPriority1, new[] { TokenType.Number, TokenType.Identifier, TokenType.Sign, TokenType.Function, TokenType.StartParenthesis }},
            {TokenType.Factorial, new[] { TokenType.OperatorPriority1, TokenType.Sign, TokenType.Factorial, TokenType.EndParenthesis }},
            {TokenType.Separator, new[] { TokenType.Number, TokenType.Sign, TokenType.Identifier, TokenType.Function, TokenType.StartParenthesis }},
            {TokenType.Start, new[] { TokenType.Number, TokenType.Sign, TokenType.Identifier, TokenType.Function, TokenType.StartParenthesis }},
            {TokenType.StartParenthesis, new[] { TokenType.Number, TokenType.Sign, TokenType.Identifier, TokenType.Function, TokenType.StartParenthesis }}
        };

        public SyntaxStateMachine()
        {
            Root = null;
            Current = null;
            IsClosed = false;
        }

        public Group GetNext(Token token)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (IsClosed)
            {
                throw new Exception("Parsing is closed");
            }

            if (token.Type == TokenType.Start)
            {
                if (Current != null)
                {
                    throw new Exception("Start node only allowed at the start");
                }

                Current = Root = new Group(null, token);
                return Current;
            }

            if (token.Type == TokenType.End)
            {
                if (Current.Parent != Root)
                {
                    throw new Exception("Incomplete tree");
                }

                IsClosed = true;
                return Current = Root;
            }

            if (Current == null && token.Type != TokenType.Start)
            {
                throw new Exception("Tree has to start with a start node");
            }

            if (token.Type == TokenType.Whitespace || token.Type == TokenType.EndOfLine)
            {
                // ignore whitespace and EOL
                return Current;
            }

            TokenType[] allowedTypes;

            if (!Transitions.TryGetValue(Current.Token.Type, out allowedTypes))
            {
                throw new Exception($"No allowed transition from {Current}");
            }

            if (!allowedTypes.Contains(token.Type))
            {
                throw new Exception($"Invalid transition from {Current} to {token}");
            }

            if (token.Type == TokenType.EndParenthesis)
            {
                // do not add node, but step up tree
                return Current = (Group)Current.Parent;
            }

            if (token.Type == TokenType.StartParenthesis)
            {
                return Current = Current.AddChildToken(token);
            }

            if (Current == Root)
            {
                return Current = Root.AddChildToken(token);
            }

            return Current = Current.AddSiblingToken(token);
        }
    }
}