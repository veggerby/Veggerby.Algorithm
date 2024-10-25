using FluentAssertions;

using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors;

public class ComplexityOperandVisitorTests
{
    [Fact]
    public void Should_return_constant()
    {
        // arrange
        var operand = Constant.One;
        var visitor = new ComplexityOperandVisitor();

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
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(1);
    }

    [Fact]
    public void Should_return_variable()
    {
        // arrange
        var operand = Variable.x;
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(2);
    }

    [Fact]
    public void Should_return_fraction()
    {
        // arrange
        var operand = Fraction.Create(1, 2);
        var visitor = new ComplexityOperandVisitor();

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
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(4); // 1 + 2 + 1
    }

    [Fact]
    public void Should_return_multiplication()
    {
        // arrange
        var operand = Multiplication.Create(ValueConstant.Create(2), Variable.x);
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(4); // 1 + 2 + 1
    }

    [Fact]
    public void Should_return_subtraction()
    {
        // arrange
        var operand = Subtraction.Create(ValueConstant.Create(2), Variable.x);
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(5); // 1 + 2 + 2
    }

    [Fact]
    public void Should_return_negative()
    {
        // arrange
        var operand = Negative.Create(Variable.x);
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(4); // 2 + 2
    }

    [Fact]
    public void Should_return_division()
    {
        // arrange
        var operand = Division.Create(ValueConstant.Create(2), Variable.x);
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(6); // 1 + 2 + 3
    }

    [Fact]
    public void Should_return_power()
    {
        // arrange
        var operand = Power.Create(ValueConstant.Create(2), Variable.x);
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(6); // 1 + 2 + 3
    }

    [Fact]
    public void Should_return_sine()
    {
        // arrange
        var operand = Sine.Create(Constant.One);
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(3); // 1 + 2
    }

    [Fact]
    public void Should_return_cosine()
    {
        // arrange
        var operand = Cosine.Create(Constant.One);
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(3); // 1 + 2
    }

    [Fact]
    public void Should_return_exponential()
    {
        // arrange
        var operand = Exponential.Create(Constant.One);
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(3); // 1 + 2
    }

    [Fact]
    public void Should_return_logarithm()
    {
        // arrange
        var operand = Logarithm.Create(Constant.One);
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(3); // 1 + 2
    }

    [Fact]
    public void Should_return_logarithm_base()
    {
        // arrange
        var operand = LogarithmBase.Create(10, Constant.One);
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(4); // 1 + 3
    }

    [Fact]
    public void Should_return_tangent()
    {
        // arrange
        var operand = Tangent.Create(Constant.One);
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(4); // 1 + 3
    }

    [Fact]
    public void Should_return_root()
    {
        // arrange
        var operand = Root.Create(3, ValueConstant.Create(2));
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(4); // 1 + 3
    }

    [Fact]
    public void Should_return_factorial()
    {
        // arrange
        var operand = Factorial.Create(ValueConstant.Create(2));
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(5); // 1 + 4
    }

    [Fact]
    public void Should_return_minimum()
    {
        // arrange
        var operand = Minimum.Create(Variable.x, ValueConstant.Create(2));
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(7); // 1 + 2 + 4
    }

    [Fact]
    public void Should_return_maximum()
    {
        // arrange
        var operand = Maximum.Create(Variable.x, ValueConstant.Create(2));
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(7); // 1 + 2 + 4
    }

    [Fact]
    public void Should_return_function()
    {
        // arrange
        var operand = Function.Create("f", ValueConstant.Create(2));
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(3); // 1 + 2
    }

    [Fact]
    public void Should_return_function_reference()
    {
        // arrange
        var operand = FunctionReference.Create("f", ValueConstant.Create(2));
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(3); // 1 + 2
    }


    [Fact]
    public void Should_return_simple_function()
    {
        // arrange
        var operand = Division.Create(
            Addition.Create([
                Variable.x, // 2
                Sine.Create(Multiplication.Create(ValueConstant.Create(2), Variable.x)), // 2 + (1 + 2 + 1) = 6
                Power.Create(Constant.Pi, 2) // 1 + 1 + 3 = 5
            ]), // 2 + 6 + 5 + 2 = 16
            Cosine.Create(Variable.x) // 2 + 2 = 4
        ); // 15 + 4 + 3 = 22
        var visitor = new ComplexityOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(22);
    }
}