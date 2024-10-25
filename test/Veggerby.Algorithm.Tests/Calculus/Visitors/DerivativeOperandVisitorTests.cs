using System;

using FluentAssertions;

using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors;

public class DerivativeOperandVisitorTests
{
    [Fact]
    public void Should_derive_function()
    {
        // arrange
        var operand = Function.Create("f", Variable.x);
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Constant.One;

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().BeOfType<Function>();
        Function result = (Function)actual;
        result.Identifier.Should().Be("f'");
        result.Variables.Should().BeEmpty();
        result.Operand.Should().Be(expected);
    }

    [Fact]
    public void Should_return_null_derive_function_reference()
    {
        // arrange
        var operand = FunctionReference.Create("f", Variable.x);
        var visitor = new DerivativeOperandVisitor(Variable.x);

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().BeNull();
    }

    [Fact]
    public void Should_derive_addition()
    {
        // arrange
        var operand = Addition.Create(Variable.x, ValueConstant.Create(3));
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Constant.One;

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_constant()
    {
        // arrange
        var operand = ValueConstant.Create(3);
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Constant.Zero;

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_cosine()
    {
        // arrange
        var operand = Cosine.Create(Variable.x);
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Negative.Create(Sine.Create(Variable.x));

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_division()
    {
        // arrange
        var operand = Division.Create(Constant.One, Variable.x);
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Division.Create(-1, Power.Create(Variable.x, 2));

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_division_complex()
    {
        // arrange
        var operand = Division.Create(
            Sine.Create(Variable.x),
            Variable.x);

        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Division.Create(
            Subtraction.Create(
                Multiplication.Create(Variable.x, Cosine.Create(Variable.x)),
                Sine.Create(Variable.x)),
            Power.Create(Variable.x, 2));

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_exponential()
    {
        // arrange
        var operand = Exponential.Create(Variable.x);
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Exponential.Create(Variable.x);

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_exponential_complex()
    {
        // arrange
        var operand = Exponential.Create(  // exp(2x/sin(2*pi/x))
            Division.Create(
                Multiplication.Create(2, Variable.x),
                Sine.Create(2 * Constant.Pi / Variable.x)));

        var visitor = new DerivativeOperandVisitor(Variable.x);

        Operand expected = "(exp((2*x)/sin((2*π)/x))*(2*sin((2*π)/x)+(4*x*cos((2*π)/x)*π)/x^2))/sin((2*π)/x)^2";

        // act
        var actual = operand.Accept(visitor).Reduce();

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_return_null_derive_factorial()
    {
        // arrange
        var operand = Factorial.Create(Variable.x);
        var visitor = new DerivativeOperandVisitor(Variable.x);
        Operand expected = null;

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_log_base10()
    {
        // arrange
        var operand = LogarithmBase.Create(10, Variable.x);
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Division.Create(Constant.One, Multiplication.Create(Variable.x, Logarithm.Create(10)));

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_ln()
    {
        // arrange
        var operand = Logarithm.Create(Variable.x);
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Division.Create(Constant.One, Variable.x);

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_multiplication()
    {
        // arrange
        var operand = Multiplication.Create(Variable.x, ValueConstant.Create(2));
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = ValueConstant.Create(2);

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_named_constant()
    {
        // arrange
        var operand = NamedConstant.Create("a", 3);
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Constant.Zero;

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_power()
    {
        // arrange
        var operand = Power.Create(Variable.x, ValueConstant.Create(2));
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Multiplication.Create(2, Power.Create(Variable.x, Subtraction.Create(2, 1)));

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_since()
    {
        // arrange
        var operand = Sine.Create(Variable.x);
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Cosine.Create(Variable.x);

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_root()
    {
        // arrange
        var operand = Root.Create(2, Variable.x);
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Division.Create(Power.Create(Variable.x, Division.Create(Subtraction.Create(1, 2), 2)), 2); // no implicit reduction

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_subtraction()
    {
        // arrange
        var operand = Subtraction.Create(Variable.x, Constant.One);
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Constant.One;

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_tangent()
    {
        // arrange
        var operand = Tangent.Create(Variable.x);
        var visitor = new DerivativeOperandVisitor(Variable.x);

        // act
        var actual = () => operand.Accept(visitor);

        // assert
        actual.Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void Should_derive_variable()
    {
        // arrange
        var operand = Variable.Create("x");
        var visitor = new DerivativeOperandVisitor(Variable.x);
        var expected = Constant.One;

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_derive_fraction()
    {
        // arrange
        var operand = Fraction.Create(1, 4);
        var visitor = new DerivativeOperandVisitor(Variable.x);

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(Constant.Zero);
    }

    [Fact]
    public void Should_return_null_for_min_derivative()
    {
        // arrange
        var operand = Minimum.Create(Variable.x, 4);
        var visitor = new DerivativeOperandVisitor(Variable.x);

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().BeNull();
    }

    [Fact]
    public void Should_return_null_for_max_derivative()
    {
        // arrange
        var operand = Maximum.Create(Variable.x, 4);
        var visitor = new DerivativeOperandVisitor(Variable.x);

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().BeNull();
    }
}