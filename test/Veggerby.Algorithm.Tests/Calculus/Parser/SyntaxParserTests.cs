using System;
using System.Linq;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Parser;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Parser
{
    public class SyntaxtParserTests
    {
        [Fact]
        public void Should_get_simple_parse_tree()
        {
            // arrange
            var parser = new SyntaxParser();

            var xtoken = new Token(TokenType.Identifier, "x", new TokenPosition(0, 1, 0));

            var tokens = new []
            {
                new Token(TokenType.Start, null, new TokenPosition(0, 1, 0)),
                xtoken,
                new Token(TokenType.End, null, new TokenPosition(1, 1, 1))
            };

            // act
            var actual = parser.ParseTree(tokens);

            // assert
            actual.Token.ShouldBe(xtoken);
        }

        [Fact]
        public void Should_get_parse_tree_binary()
        {
            // arrange
            var parser = new SyntaxParser();

            var xToken = new Token(TokenType.Identifier, "x", new TokenPosition(0, 1, 0));
            var signToken = new Token(TokenType.Sign, "+", new TokenPosition(1, 1, 1));
            var numberToken = new Token(TokenType.Number, "1.2", new TokenPosition(2, 1, 2));

            var tokens = new []
            {
                new Token(TokenType.Start, null, new TokenPosition(0, 1, 0)),
                xToken,
                signToken,
                numberToken,
                new Token(TokenType.End, null, new TokenPosition(5, 1, 5))
            };

            // act
            var actual = parser.ParseTree(tokens);

            // assert
            actual.ShouldBeOfType<BinaryNode>();
            actual.Token.ShouldBe(signToken);
            ((BinaryNode)actual).Left.Token.ShouldBe(xToken);
            ((BinaryNode)actual).Right.Token.ShouldBe(numberToken);
        }

        [Fact]
        public void Should_get_parse_tree_unary_function()
        {
            // arrange
            var parser = new SyntaxParser();

            var sinToken = new Token(TokenType.Function, "sin", new TokenPosition(0, 1, 0));
            var startParenthesisToken = new Token(TokenType.StartParenthesis, "(", new TokenPosition(4, 1, 4));
            var xToken = new Token(TokenType.Identifier, "x", new TokenPosition(5, 1, 5));
            var endParenthesisToken = new Token(TokenType.EndParenthesis, ")", new TokenPosition(6, 1, 6));

            var tokens = new []
            {
                new Token(TokenType.Start, null, new TokenPosition(0, 1, 0)),
                sinToken,
                startParenthesisToken,
                xToken,
                endParenthesisToken,
                new Token(TokenType.End, null, new TokenPosition(7, 1, 7))
            };

            // act
            var actual = parser.ParseTree(tokens);

            // assert
            actual.ShouldBeOfType<UnaryNode>();
            actual.Token.ShouldBe(sinToken);
            ((UnaryNode)actual).Inner.Token.ShouldBe(xToken);
        }

        [Fact]
        public void Should_get_parse_tree_unary_factorial()
        {
            // arrange
            var parser = new SyntaxParser();

            var xToken = new Token(TokenType.Identifier, "x", new TokenPosition(0, 1, 0));
            var factorialToken = new Token(TokenType.Factorial, "!", new TokenPosition(1, 1, 1));

            var tokens = new []
            {
                new Token(TokenType.Start, null, new TokenPosition(0, 1, 0)),
                xToken,
                factorialToken,
                new Token(TokenType.End, null, new TokenPosition(2, 1, 2))
            };

            // act
            var actual = parser.ParseTree(tokens);

            // assert
            actual.ShouldBeOfType<UnaryNode>();
            actual.Token.ShouldBe(factorialToken);
            ((UnaryNode)actual).Inner.Token.ShouldBe(xToken);
        }

        [Fact]
        public void Should_get_parse_tree_binary_function()
        {
            // arrange
            var parser = new SyntaxParser();

            var maxToken = new Token(TokenType.Function, "max", new TokenPosition(0, 1, 0));
            var startParenthesisToken = new Token(TokenType.StartParenthesis, "(", new TokenPosition(4, 1, 4));
            var xToken = new Token(TokenType.Identifier, "x", new TokenPosition(5, 1, 5));
            var separatorToken = new Token(TokenType.Separator, "m", new TokenPosition(6, 1, 6));
            var yToken = new Token(TokenType.Identifier, "y", new TokenPosition(7, 1, 7));
            var endParenthesisToken = new Token(TokenType.EndParenthesis, ")", new TokenPosition(8, 1, 8));

            var tokens = new []
            {
                new Token(TokenType.Start, null, new TokenPosition(0, 1, 0)),
                maxToken,
                startParenthesisToken,
                xToken,
                separatorToken,
                yToken,
                endParenthesisToken,
                new Token(TokenType.End, null, new TokenPosition(7, 1, 7))
            };

            // act
            var actual = parser.ParseTree(tokens);

            // assert
            actual.ShouldBeOfType<BinaryNode>();
            actual.Token.ShouldBe(maxToken);
            ((BinaryNode)actual).Left.Token.ShouldBe(xToken);
            ((BinaryNode)actual).Right.Token.ShouldBe(yToken);
        }
    }
}