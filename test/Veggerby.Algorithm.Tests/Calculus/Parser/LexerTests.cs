using System;
using System.Linq;
using Shouldly;
using Veggerby.Algorithm.Calculus.Parser;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Parser
{
    public class LexerTests
    {
        [Fact]
        public void Should_get_only_end_no_definitions_no_content()
        {
            // arrange
            var lexer = new Lexer();

            // act
            var actual = lexer.Tokenize(string.Empty);

            // assert
            actual.Count().ShouldBe(1);
            actual.Single().Type.ShouldBe(TokenType.End);
        }

        [Fact]
        public void Should_get_word_and_whitespace_tokens()
        {
            // arrange
            var lexer = new Lexer();
            lexer.AddDefinition(new TokenDefinition(TokenType.Whitespace, " "));
            lexer.AddDefinition(new TokenDefinition(TokenType.Identifier, "[a-zA-Z]+"));

            var expected = new []
            {
                new Token(TokenType.Start, null, new TokenPosition(0, 1, 0)),
                new Token(TokenType.Identifier, "words", new TokenPosition(0, 1, 0)),
                new Token(TokenType.Whitespace, " ", new TokenPosition(5, 1, 5)),
                new Token(TokenType.Identifier, "are", new TokenPosition(6, 1, 6)),
                new Token(TokenType.Whitespace, " ", new TokenPosition(9, 1, 9)),
                new Token(TokenType.Identifier, "just", new TokenPosition(10, 1, 10)),
                new Token(TokenType.Whitespace, " ", new TokenPosition(14, 1, 14)),
                new Token(TokenType.Identifier, "letters", new TokenPosition(15, 1, 15)),
                new Token(TokenType.End, null, new TokenPosition(22, 1, 22))
            };

            // act
            var actual = lexer.Tokenize("words are just letters");

            // assert
            actual.ShouldBe(expected);
        }

        [Fact]
        public void Should_get_function_lexer()
        {
            // arrange

            // act
            var actual = FunctionParser.GetLexer();

            // assert
            actual.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("sin", TokenType.Function)]
        [InlineData("cos", TokenType.Function)]
        [InlineData("tan", TokenType.Function)]
        [InlineData("exp", TokenType.Function)]
        [InlineData("ln", TokenType.Function)]
        [InlineData("log", TokenType.Function)]
        [InlineData("log2", TokenType.Function)]
        [InlineData("sqrt", TokenType.Function)]
        [InlineData("root", TokenType.Function)]
        [InlineData("min", TokenType.Function)]
        [InlineData("max", TokenType.Function)]
        [InlineData("SIN", TokenType.Function)]
        [InlineData("Sin", TokenType.Function)]
        [InlineData("1", TokenType.Number)]
        [InlineData("12", TokenType.Number)]
        [InlineData("123456789", TokenType.Number)]
        [InlineData("1.2", TokenType.Number)]
        [InlineData("100.212", TokenType.Number)]
        [InlineData("100E2", TokenType.Number)]
        [InlineData("1e10", TokenType.Number)]
        [InlineData("10.1E-2", TokenType.Number)]
        [InlineData("10.1E+2", TokenType.Number)]
        [InlineData("10.19191E-20", TokenType.Number)]
        [InlineData("(", TokenType.StartParenthesis)]
        [InlineData(")", TokenType.EndParenthesis)]
        [InlineData(")", TokenType.EndParenthesis)]
        [InlineData(",", TokenType.Separator)]
        [InlineData("a", TokenType.Identifier)]
        [InlineData("abc", TokenType.Identifier)]
        [InlineData("ab12", TokenType.Identifier)]
        [InlineData("aBcD13E_", TokenType.Identifier)]
        [InlineData("π", TokenType.Identifier)]
        [InlineData("!", TokenType.Factorial)]
        [InlineData("*", TokenType.OperatorPriority1)]
        [InlineData("/", TokenType.OperatorPriority1)]
        [InlineData("^", TokenType.OperatorPriority1)]
        [InlineData("+", TokenType.Sign)]
        [InlineData("-", TokenType.Sign)]
        [InlineData(" ", TokenType.Whitespace)]
        [InlineData("\t", TokenType.Whitespace)]
        [InlineData("   ", TokenType.Whitespace)]
        [InlineData("\t  \t \t\t\t\t", TokenType.Whitespace)]
        public void Should_get_single_token(string value, TokenType expectedTokenType)
        {
            // arrange
            var lexer = FunctionParser.GetLexer();
            var expected = new []
            {
                new Token(TokenType.Start, null, new TokenPosition(0, 1, 0)),
                new Token(expectedTokenType, value, new TokenPosition(0, 1, 0)),
                new Token(TokenType.End, null, new TokenPosition(value.Length, 1, value.Length))
            };

            // act
            var actual = lexer.Tokenize(value);

            // assert
            actual.ShouldBe(expected);
        }

        [Fact]
        public void Should_work_multiple_tokens()
        {
            // arrange
            var lexer = FunctionParser.GetLexer();

            var expected = new []
            {
                new Token(TokenType.Start, null, new TokenPosition(0, 1, 0)),
                new Token(TokenType.Function, "sin", new TokenPosition(0, 1, 0)),
                new Token(TokenType.Function, "cos", new TokenPosition(3, 1, 3)),
                new Token(TokenType.Function, "tan", new TokenPosition(6, 1, 6)),
                new Token(TokenType.StartParenthesis, "(", new TokenPosition(9, 1, 9)),
                new Token(TokenType.EndParenthesis, ")", new TokenPosition(10, 1, 10)),
                new Token(TokenType.Separator, ",", new TokenPosition(11, 1, 11)),
                new Token(TokenType.Number, "1.2e10", new TokenPosition(12, 1, 12)),
                new Token(TokenType.Identifier, "x", new TokenPosition(18, 1, 18)),
                new Token(TokenType.Sign, "+", new TokenPosition(19, 1, 19)),
                new Token(TokenType.Identifier, "xyz", new TokenPosition(20, 1, 20)),
                new Token(TokenType.Sign, "-", new TokenPosition(23, 1, 23)),
                new Token(TokenType.Number, "1", new TokenPosition(24, 1, 24)),
                new Token(TokenType.OperatorPriority1, "*", new TokenPosition(25, 1, 25)),
                new Token(TokenType.Number, "44", new TokenPosition(26, 1, 26)),
                new Token(TokenType.End, null, new TokenPosition(28, 1, 28))
            };

            // act
            var actual = lexer.Tokenize("sincostan(),1.2e10x+xyz-1*44");

            // assert
            actual.ShouldBe(expected);
        }

        [Fact]
        public void Should_fail_invalid_token_dummy_lexer()
        {
            // arrange
            var lexer = new Lexer();
            lexer.AddDefinition(new TokenDefinition(TokenType.Whitespace, " "));
            lexer.AddDefinition(new TokenDefinition(TokenType.Identifier, "[a-zA-Z]+"));

            // act
            var actual = Should.Throw<Exception>(() => lexer.Tokenize("1234+123").ToList());

            // assert
            actual.Message.ShouldBe("Unrecognized symbol '1' at index 0 (line 1, column 0).");
        }

        [Fact]
        public void Should_fail_invalid_token()
        {
            // arrange
            var lexer = FunctionParser.GetLexer();

            // act
            var actual = Should.Throw<Exception>(() => lexer.Tokenize("[").ToList());

            // assert
            actual.Message.ShouldBe("Unrecognized symbol '[' at index 0 (line 1, column 0).");
        }

        [Fact]
        public void Should_fail_invalid_token_in_body()
        {
            // arrange
            var lexer = FunctionParser.GetLexer();

            // act
            var actual = Should.Throw<Exception>(() => lexer.Tokenize("sin{1+x}").ToList());

            // assert
            actual.Message.ShouldBe("Unrecognized symbol '{' at index 3 (line 1, column 3).");
        }
    }
}