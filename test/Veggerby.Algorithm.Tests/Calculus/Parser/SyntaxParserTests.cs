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

        [Fact]
        public void Should_get_parse_tree_complex_function()
        {
            // arrange
            var parser = new SyntaxParser();

            // x^2+sin(x*cos(3*pi-x))
            var xToken1 = new Token(TokenType.Identifier, "x", new TokenPosition(0, 1, 0));
            var powerToken = new Token(TokenType.OperatorPriority1, "^", new TokenPosition(1, 1, 1));
            var constant2Token = new Token(TokenType.Number, "2", new TokenPosition(2, 1, 2));
            var signPlusToken = new Token(TokenType.Sign, "+", new TokenPosition(3, 1, 3));
            var sinToken = new Token(TokenType.Function, "sin", new TokenPosition(4, 1, 4));
            var parenthesisToken1 = new Token(TokenType.StartParenthesis, "(", new TokenPosition(7, 1, 7));
            var xToken2 = new Token(TokenType.Identifier, "x", new TokenPosition(8, 1, 8));
            var multiplicationToken1 = new Token(TokenType.OperatorPriority1, "*", new TokenPosition(9, 1, 9));
            var cosToken = new Token(TokenType.Function, "cos", new TokenPosition(10, 1, 10));
            var parenthesisToken2 = new Token(TokenType.StartParenthesis, "(", new TokenPosition(11, 1, 11));
            var constant3Token = new Token(TokenType.Number, "3", new TokenPosition(12, 1, 12));
            var multiplicationToken2 = new Token(TokenType.OperatorPriority1, "*", new TokenPosition(13, 1, 13));
            var piToken = new Token(TokenType.Identifier, "pi", new TokenPosition(15, 1, 15));
            var signMinusToken = new Token(TokenType.Sign, "-", new TokenPosition(17, 1, 17));
            var xToken3 = new Token(TokenType.Identifier, "x", new TokenPosition(18, 1, 18));
            var parenthesisToken3 = new Token(TokenType.EndParenthesis, ")", new TokenPosition(19, 1, 19));
            var parenthesisToken4 = new Token(TokenType.EndParenthesis, ")", new TokenPosition(20, 1, 20));

            var tokens = new []
            {
                new Token(TokenType.Start, null, new TokenPosition(0, 1, 0)),
                xToken1,
                powerToken,
                constant2Token,
                signPlusToken,
                sinToken,
                parenthesisToken1,
                xToken2,
                multiplicationToken1,
                cosToken,
                parenthesisToken2,
                constant3Token,
                multiplicationToken2,
                piToken,
                signMinusToken,
                xToken3,
                parenthesisToken3,
                parenthesisToken4,
                new Token(TokenType.End, null, new TokenPosition(21, 1, 21))
            };

            // act
            var actual = parser.ParseTree(tokens);

            // assert
            actual.ShouldBeOfType<BinaryNode>();
            actual.Token.ShouldBe(signPlusToken);
            var left = ((BinaryNode)actual).Left as BinaryNode;
            var right = ((BinaryNode)actual).Right as UnaryNode;
            left.Token.ShouldBe(powerToken);
            right.Token.ShouldBe(sinToken);

            left.Left.Token.ShouldBe(xToken1);
            left.Right.Token.ShouldBe(constant2Token);

            right.Inner.Token.ShouldBe(multiplicationToken1);

            var rightInner = (BinaryNode)right.Inner;
            rightInner.Left.Token.ShouldBe(xToken2);
            rightInner.Right.Token.ShouldBe(cosToken);

            var cosInner = ((UnaryNode)rightInner.Right).Inner as BinaryNode;
            cosInner.Token.ShouldBe(signMinusToken);
            cosInner.Left.Token.ShouldBe(multiplicationToken2);
            cosInner.Right.Token.ShouldBe(xToken3);

            var cosInnerLeft = (BinaryNode)cosInner.Left;
            cosInnerLeft.Left.Token.ShouldBe(constant3Token);
            cosInnerLeft.Right.Token.ShouldBe(piToken);
        }
    }
}