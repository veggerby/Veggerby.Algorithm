using FluentAssertions;

using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors;

public class OrderOperandVisitorTests
{
    [Fact]
    public void Should_return_constant()
    {
        // arrange
        var operand = Constant.One;
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(1);
    }

    [Fact]
    public void Should_return_named_constant()
    {
        // arrange
        var operand = Constant.Pi;
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(3);
    }


    [Fact]
    public void Should_return_unspecified_constant()
    {
        // arrange
        var operand = UnspecifiedConstant.Create();
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(4);
    }

    [Fact]
    public void Should_return_variable()
    {
        // arrange
        var operand = Variable.x;
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(5);
    }

    [Fact]
    public void Should_return_fraction()
    {
        // arrange
        var operand = Fraction.Create(1, 2);
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(2);
    }

    [Fact]
    public void Should_return_addition()
    {
        // arrange
        var operand = Addition.Create(Constant.One, Variable.x);
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(7);
    }

    [Fact]
    public void Should_return_multiplication()
    {
        // arrange
        var operand = Multiplication.Create(ValueConstant.Create(2), Variable.x);
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(9);
    }

    [Fact]
    public void Should_return_subtraction()
    {
        // arrange
        var operand = Subtraction.Create(ValueConstant.Create(2), Variable.x);
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(8);
    }

    [Fact]
    public void Should_return_negative()
    {
        // arrange
        var operand = Negative.Create(Variable.x);
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(6);
    }

    [Fact]
    public void Should_return_division()
    {
        // arrange
        var operand = Division.Create(ValueConstant.Create(2), Variable.x);
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(10);
    }

    [Fact]
    public void Should_return_power()
    {
        // arrange
        var operand = Power.Create(ValueConstant.Create(2), Variable.x);
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(11);
    }

    [Fact]
    public void Should_return_sine()
    {
        // arrange
        var operand = Sine.Create(Constant.One);
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(17);
    }

    [Fact]
    public void Should_return_cosine()
    {
        // arrange
        var operand = Cosine.Create(Constant.One);
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(18);
    }

    [Fact]
    public void Should_return_exponential()
    {
        // arrange
        var operand = Exponential.Create(Constant.One);
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(15);
    }

    [Fact]
    public void Should_return_logarithm()
    {
        // arrange
        var operand = Logarithm.Create(Constant.One);
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(16);
    }

    [Fact]
    public void Should_return_logarithm_base()
    {
        // arrange
        var operand = LogarithmBase.Create(10, Constant.One);
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(21);
    }

    [Fact]
    public void Should_return_tangent()
    {
        // arrange
        var operand = Tangent.Create(Constant.One);
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(19);
    }

    [Fact]
    public void Should_return_root()
    {
        // arrange
        var operand = Root.Create(3, ValueConstant.Create(2));
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(20);
    }

    [Fact]
    public void Should_return_factorial()
    {
        // arrange
        var operand = Factorial.Create(ValueConstant.Create(2));
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(14);
    }

    [Fact]
    public void Should_return_minimum()
    {
        // arrange
        var operand = Minimum.Create(Variable.x, ValueConstant.Create(2));
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(22);
    }

    [Fact]
    public void Should_return_maximum()
    {
        // arrange
        var operand = Maximum.Create(Variable.x, ValueConstant.Create(2));
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(23);
    }

    [Fact]
    public void Should_return_function()
    {
        // arrange
        var operand = Function.Create("f", ValueConstant.Create(2));
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(12);
    }

    [Fact]
    public void Should_return_function_reference()
    {
        // arrange
        var operand = FunctionReference.Create("f", ValueConstant.Create(2));
        var visitor = new OrderOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(13);
    }
}