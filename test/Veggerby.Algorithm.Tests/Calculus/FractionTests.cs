using FluentAssertions;

using Veggerby.Algorithm.Calculus;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus;

public class FractionTests
{
    [Fact]
    public void Should_initialize_from_constructor()
    {
        // arrange

        // act
        var actual = (Fraction)Fraction.Create(1, 2);

        // assert
        actual.Numerator.Should().Be(1);
        actual.Denominator.Should().Be(2);
    }

    [Fact]
    public void Should_equal_self()
    {
        // arrange
        var v = Fraction.Create(1, 2);

        // act
        var actual = v.Equals(v);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_null()
    {
        // arrange
        var v = Fraction.Create(1, 2);

        // act
        var actual = v.Equals(null);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_equal_same_operands()
    {
        // arrange
        var v1 = Fraction.Create(1, 2);
        var v2 = Fraction.Create(1, 2);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_different_operands()
    {
        // arrange
        var v1 = Fraction.Create(1, 2);
        var v2 = Fraction.Create(2, 3);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_not_equal_different_operation_identical_operands()
    {
        // arrange
        var v1 = Fraction.Create(1, 2);
        var v2 = Sine.Create(Variable.x);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }
}