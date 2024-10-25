using FluentAssertions;

using Veggerby.Algorithm.Calculus;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus;

public class TangentTests
{
    [Fact]
    public void Should_initialize_from_constructor()
    {
        // arrange

        // act
        var actual = (Tangent)Tangent.Create(Variable.x);

        // assert
        actual.Inner.Should().Be(Variable.x);
    }

    [Fact]
    public void Should_equal_self()
    {
        // arrange
        var v = Tangent.Create(Constant.Pi);

        // act
        var actual = v.Equals(v);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_null()
    {
        // arrange
        var v = Tangent.Create(Constant.Pi);

        // act
        var actual = v.Equals(null);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_equal_same_operands()
    {
        // arrange
        var v1 = Tangent.Create(Constant.Pi);
        var v2 = Tangent.Create(Constant.Pi);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_different_operands()
    {
        // arrange
        var v1 = Tangent.Create(Constant.Pi);
        var v2 = Tangent.Create(Variable.x);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_not_equal_different_operation_identical_operands()
    {
        // arrange
        var v1 = Tangent.Create(Variable.x);
        var v2 = Sine.Create(Variable.x);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }
}