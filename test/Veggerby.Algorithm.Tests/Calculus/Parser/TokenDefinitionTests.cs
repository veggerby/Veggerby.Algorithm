using FluentAssertions;

using Veggerby.Algorithm.Calculus.Parser;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Parser;

public class TokenDefinitionTests
{
    [Fact]
    public void Should_create_tokendefinition()
    {
        // arrange
        // act
        var actual = new TokenDefinition(TokenType.Number, "[0-9]+");

        // assert
        actual.Should().NotBeNull();
    }

    [Fact]
    public void Should_return_token_match_from_start()
    {
        // arrange
        var definition = new TokenDefinition(TokenType.Number, "[0-9]+");

        // act
        var actual = definition.FindMatch("1234abcd", new TokenPosition(0, 1, 0));

        // assert
        actual.Should().NotBeNull();
        actual.Type.Should().Be(TokenType.Number);
        actual.Value.Should().Be("1234");
        actual.Position.Index.Should().Be(0);
        actual.Position.Column.Should().Be(0);
        actual.Position.Line.Should().Be(1);
    }

    [Fact]
    public void Should_not_return_token_match_in_middle()
    {
        // arrange
        var definition = new TokenDefinition(TokenType.Number, "[0-9]+");

        // act
        var actual = definition.FindMatch("a+1234abcd", new TokenPosition(0, 1, 0));

        // assert
        actual.Should().BeNull();
    }

    [Fact]
    public void Should_return_token_match_in_middle()
    {
        // arrange
        var definition = new TokenDefinition(TokenType.Number, "[0-9]+");

        // act
        var actual = definition.FindMatch("a+1234abcd", new TokenPosition(2, 1, 2));

        // assert
        actual.Should().NotBeNull();
        actual.Type.Should().Be(TokenType.Number);
        actual.Value.Should().Be("1234");
        actual.Position.Index.Should().Be(2);
        actual.Position.Column.Should().Be(2);
        actual.Position.Line.Should().Be(1);
    }
}