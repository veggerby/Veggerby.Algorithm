using FluentAssertions;

using Veggerby.Algorithm.Calculus;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus;

public class PowerTests
{
    [Fact]
    public void Should_initialize()
    {
        var actual = (Power)Power.Create(ValueConstant.Create(2), Variable.x);

        // assert
        actual.Left.Should().Be(ValueConstant.Create(2));
        actual.Right.Should().Be(Variable.x);
    }

    [Fact]
    public void Should_equal_self()
    {
        // arrange
        var v = Power.Create(Constant.One, Variable.x);

        // act
        var actual = v.Equals(v);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_null()
    {
        // arrange
        var v = Power.Create(Constant.One, Variable.x);

        // act
        var actual = v.Equals(null);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_equal_same_operands()
    {
        // arrange
        var v1 = Power.Create(Constant.One, Variable.x);
        var v2 = Power.Create(Constant.One, Variable.x);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_different_operands()
    {
        // arrange
        var v1 = Power.Create(Constant.One, Variable.x);
        var v2 = Power.Create(Variable.y, ValueConstant.Create(2));

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_not_equal_mirrored_operands()
    {
        // arrange
        var v1 = Power.Create(Constant.One, Variable.x);
        var v2 = Power.Create(Variable.x, Constant.One);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_not_equal_different_operation_identical_operands()
    {
        // arrange
        var v1 = Power.Create(Constant.One, Variable.x);
        var v2 = Subtraction.Create(Constant.One, Variable.x);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_equal_complex()
    {
        // arrange
        var func = Power.Create(
            Sine.Create(
                Division.Create(
                    Multiplication.Create(
                        2,
                        Constant.Pi
                    ),
                    Variable.x
                )
            ),
            2
        );

        Operand expected = "sin((2*π)/x)^2";

        // act
        var actual = func.Equals(expected);

        // assert
        actual.Should().BeTrue();
    }
}