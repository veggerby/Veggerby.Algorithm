using FluentAssertions;

using Veggerby.Algorithm.Calculus;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus;

public class RootTests
{
    [Fact]
    public void Should_initialize_from_constructor()
    {
        // arrange

        // act
        var actual = (Root)Root.Create(3, Variable.x);

        // assert
        actual.Exponent.Should().Be(3);
        actual.Inner.Should().Be(Variable.x);
    }

    [Fact]
    public void Should_equal_self()
    {
        // arrange
        var v = Root.Create(3, Variable.x);

        // act
        var actual = v.Equals(v);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_null()
    {
        // arrange
        var v = Root.Create(3, Variable.x);

        // act
        var actual = v.Equals(null);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_equal_same_operands()
    {
        // arrange
        var v1 = Root.Create(3, Variable.x);
        var v2 = Root.Create(3, Variable.x);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_different_operands()
    {
        // arrange
        var v1 = Root.Create(3, Variable.x);
        var v2 = Root.Create(3, 2);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_not_equal_different_base()
    {
        // arrange
        var v1 = Root.Create(3, Variable.x);
        var v2 = Root.Create(2, Variable.x);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_not_equal_different_operation_identical_operands()
    {
        // arrange
        var v1 = Root.Create(3, Variable.x);
        var v2 = Sine.Create(Variable.x);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }
}