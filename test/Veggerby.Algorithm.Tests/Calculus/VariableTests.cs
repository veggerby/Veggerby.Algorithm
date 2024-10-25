using FluentAssertions;

using Veggerby.Algorithm.Calculus;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus;

public class VariableTests
{
    [Fact]
    public void Should_initialize_from_constructor()
    {
        // arrange

        // act
        var actual = Variable.Create("x");

        // assert
        actual.Identifier.Should().Be("x");
    }

    [Fact]
    public void Should_equal_self()
    {
        // arrange
        var v = Variable.Create("x");

        // act
        var actual = v.Equals(v);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_null()
    {
        // arrange
        var v = Variable.Create("x");

        // act
        var actual = v.Equals(null);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_equal_same_operands()
    {
        // arrange
        var v1 = Variable.Create("x");
        var v2 = Variable.Create("x");

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_different_operands()
    {
        // arrange
        var v1 = Variable.Create("x");
        var v2 = Variable.Create("y");

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }
}