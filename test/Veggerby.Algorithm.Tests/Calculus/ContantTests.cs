using System;

using FluentAssertions;

using Veggerby.Algorithm.Calculus;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus;

public class ConstantTests
{
    [Fact]
    public void Should_initialize_from_constructor()
    {
        // arrange

        // act
        var actual = ValueConstant.Create(123);

        // assert
        actual.Value.Should().Be(123);
    }

    [Fact]
    public void Should_have_zero_static()
    {
        // arrange
        // act
        // assert
        Constant.Zero.Value.Should().Be(0);
    }

    [Fact]
    public void Should_have_one_static()
    {
        // arrange
        // act
        // assert
        Constant.One.Value.Should().Be(1);
    }

    [Fact]
    public void Should_have_pi_static()
    {
        // arrange
        // act
        // assert
        Constant.Pi.Symbol.Should().Be("π");
        Constant.Pi.Value.Should().Be(Math.PI);
    }

    [Fact]
    public void Should_have_e_static()
    {
        // arrange
        // act
        // assert
        Constant.e.Symbol.Should().Be("e");
        Constant.e.Value.Should().Be(Math.E);
    }

    [Fact]
    public void Should_equal_self()
    {
        // arrange
        var v = ValueConstant.Create(4);

        // act
        var actual = v.Equals(v);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_null()
    {
        // arrange
        var v = ValueConstant.Create(3);

        // act
        var actual = v.Equals(null);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_equal_same_operands()
    {
        // arrange
        var v1 = ValueConstant.Create(3);
        var v2 = ValueConstant.Create(3);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_different_operands()
    {
        // arrange
        var v1 = Constant.One;
        var v2 = ValueConstant.Create(3);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }
}