using System;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Parser;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Parser
{
    public class TokenDefinitionTests
    {
        [Fact]
        public void Should_create_tokendefinition()
        {
            // arrange
            // act
            var actual = new TokenDefinition(TokenType.Number, "[0-9]+");

            // assert
            actual.ShouldNotBeNull();
        }

        [Fact]
        public void Should_return_token_match_from_start()
        {
            // arrange
            var definition = new TokenDefinition(TokenType.Number, "[0-9]+");

            // act
            var actual = definition.FindMatch("1234abcd", new TokenPosition(0, 1, 0));

            // assert
            actual.ShouldNotBeNull();
            actual.Type.ShouldBe(TokenType.Number);
            actual.Value.ShouldBe("1234");
            actual.Position.Index.ShouldBe(0);
            actual.Position.Column.ShouldBe(0);
            actual.Position.Line.ShouldBe(1);
        }

        [Fact]
        public void Should_not_return_token_match_in_middle()
        {
            // arrange
            var definition = new TokenDefinition(TokenType.Number, "[0-9]+");

            // act
            var actual = definition.FindMatch("a+1234abcd", new TokenPosition(0, 1, 0));

            // assert
            actual.ShouldBeNull();
        }

        [Fact]
        public void Should_return_token_match_in_middle()
        {
            // arrange
            var definition = new TokenDefinition(TokenType.Number, "[0-9]+");

            // act
            var actual = definition.FindMatch("a+1234abcd", new TokenPosition(2, 1, 2));

            // assert
            actual.ShouldNotBeNull();
            actual.Type.ShouldBe(TokenType.Number);
            actual.Value.ShouldBe("1234");
            actual.Position.Index.ShouldBe(2);
            actual.Position.Column.ShouldBe(2);
            actual.Position.Line.ShouldBe(1);
        }
    }
}