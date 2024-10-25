using FluentAssertions;

using Veggerby.Algorithm.Calculus;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus;

public class OperandTests
{
    [Fact]
    public void Should_return_addition()
    {
        // arrange
        var left = ValueConstant.Create(3);
        var right = Variable.x;

        // act
        var actual = left + right;

        // assert
        actual.Should().BeOfType<Addition>();
        ((Addition)actual).Operands.Should().BeEquivalentTo(new Operand[] { left, right });
    }

    [Fact]
    public void Should_return_constant_from_addition()
    {
        // arrange
        var left = ValueConstant.Create(3);
        var right = ValueConstant.Create(6);

        // act
        var actual = left + right;

        // assert
        actual.Should().Be(ValueConstant.Create(9));
    }

    [Fact]
    public void Should_return_subtraction()
    {
        // arrange
        var left = ValueConstant.Create(3);
        var right = Variable.x;

        // act
        var actual = left - right;

        // assert
        actual.Should().BeOfType<Subtraction>();
        ((BinaryOperation)actual).Left.Should().Be(left);
        ((BinaryOperation)actual).Right.Should().Be(right);
    }

    [Fact]
    public void Should_return_constant_from_subtraction()
    {
        // arrange
        var left = ValueConstant.Create(3);
        var right = ValueConstant.Create(6);

        // act
        var actual = left - right;

        // assert
        actual.Should().Be(ValueConstant.Create(-3));
    }

    [Fact]
    public void Should_return_multiplication()
    {
        // arrange
        var left = ValueConstant.Create(3);
        var right = Variable.x;

        // act
        var actual = left * right;

        // assert
        actual.Should().BeOfType<Multiplication>();
        ((Multiplication)actual).Operands.Should().BeEquivalentTo(new Operand[] { left, right });
    }

    [Fact]
    public void Should_return_constant_from_multiplication()
    {
        // arrange
        var left = ValueConstant.Create(3);
        var right = ValueConstant.Create(6);

        // act
        var actual = left * right;

        // assert
        actual.Should().Be(ValueConstant.Create(18));
    }

    [Fact]
    public void Should_return_division()
    {
        // arrange
        var left = ValueConstant.Create(3);
        var right = Variable.x;

        // act
        var actual = left / right;

        // assert
        actual.Should().BeOfType<Division>();
        ((BinaryOperation)actual).Left.Should().Be(left);
        ((BinaryOperation)actual).Right.Should().Be(right);
    }

    [Fact]
    public void Should_return_fraction_from_constants_division()
    {
        // arrange
        var left = ValueConstant.Create(1);
        var right = ValueConstant.Create(3);

        // act
        var actual = Division.Create(left, right);

        // assert
        actual.Should().Be(Fraction.Create(1, 3));
    }

    [Fact]
    public void Should_return_power()
    {
        // arrange
        var left = ValueConstant.Create(3);
        var right = Variable.x;

        // act
        var actual = left ^ right;

        // assert
        actual.Should().BeOfType<Power>();
        ((BinaryOperation)actual).Left.Should().Be(left);
        ((BinaryOperation)actual).Right.Should().Be(right);
    }

    [Fact]
    public void Should_return_constant_from_power()
    {
        // arrange
        var left = ValueConstant.Create(2);
        var right = ValueConstant.Create(3);

        // act
        var actual = left ^ right;

        // assert
        actual.Should().Be(ValueConstant.Create(8));
    }

    [Fact]
    public void Should_return_one_for_one_as_base()
    {
        // arrange
        var left = Constant.One;
        var right = Variable.x;

        // act
        var actual = left ^ right;

        // assert
        actual.Should().Be(Constant.One);
    }

    [Fact]
    public void Should_return_one_for_zero_as_exponent()
    {
        // arrange
        var left = Variable.x;
        var right = Constant.Zero;

        // act
        var actual = left ^ right;

        // assert
        actual.Should().Be(Constant.One);
    }


    [Fact]
    public void Should_return_baze_for_one_as_exponent()
    {
        // arrange
        var left = Variable.x;
        var right = Constant.One;

        // act
        var actual = left ^ right;

        // assert
        actual.Should().Be(left);
    }

    [Fact]
    public void Should_create_constant_from_int()
    {
        // arrange

        // act
        var actual = (ValueConstant)3;

        // assert
        actual.Value.Should().Be(3);
    }

    [Fact]
    public void Should_create_constant_from_double()
    {
        // arrange

        // act
        var actual = (ValueConstant)3.4;

        // assert
        actual.Value.Should().Be(3.4);
    }
}