using System;

using FluentAssertions;

using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors;

public class IntegralOperandVisitorTests
{
    [Fact]
    public void Should_integrate_function()
    {
        // arrange
        var operand = Function.Create("f", ValueConstant.Create(3));
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = "3*x+c";

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().BeOfType<Function>();
        Function result = (Function)actual;
        result.Identifier.Should().Be("F");
        result.Variables.Should().BeEquivalentTo(new[] { Variable.Create("c"), Variable.x }, options => options.ComparingByValue<Operand>());
        result.Operand.Should().Be(expected);
    }

    [Fact]
    public void Should_return_null_integrate_function_reference()
    {
        // arrange
        var operand = FunctionReference.Create("f", Variable.x);
        var visitor = new IntegralOperandVisitor(Variable.x);

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().BeNull();
    }

    [Fact]
    public void Should_integrate_addition()
    {
        // arrange
        var operand = Addition.Create(Variable.x, ValueConstant.Create(3));
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = "x^2/2+c+3*x+c";

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_integrate_constant()
    {
        // arrange
        var operand = ValueConstant.Create(3);
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = "3*x+c";

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_integrate_cosine()
    {
        // arrange
        var operand = Cosine.Create(Variable.x);
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = "sin(x)+c";

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_integrate_division()
    {
        // arrange
        var operand = Division.Create(Constant.One, Variable.x);
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = "ln(x)+c";

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_integrate_exponential()
    {
        // arrange
        var operand = Exponential.Create(Variable.x);
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = "exp(x)+c";

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_return_null_for_integrate_factorial()
    {
        // arrange
        var operand = Factorial.Create(Variable.x);
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = null;

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_integrate_log_base10()
    {
        // arrange
        var operand = LogarithmBase.Create(10, Variable.x);
        var visitor = new IntegralOperandVisitor(Variable.x);
        var expected = Division.Create(Constant.One, Multiplication.Create(Variable.x, Logarithm.Create(10)));

        // act
        var actual = () => operand.Accept(visitor);

        // assert
        actual.Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void Should_integrate_logarithm()
    {
        // arrange
        var operand = Logarithm.Create(Variable.x);
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = "x*ln(x)-x+c";

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_integrate_multiplication()
    {
        // arrange
        var operand = Multiplication.Create(Variable.x, Cosine.Create(Variable.x));
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = "(c+x*(c-sin(x)))-(c+sin(x)+c*x)";

        // act
        var actual = operand.Accept(visitor)?.Reduce();

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_integrate_multiplication_complex()
    {
        // arrange
        var operand = Multiplication.Create([Variable.x, Exponential.Create(Variable.x), Addition.Create(Constant.One, Variable.x)]);
        var visitor = new IntegralOperandVisitor(Variable.x);

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().NotBeNull();
    }

    [Fact]
    public void Should_integrate_named_constant()
    {
        // arrange
        var operand = NamedConstant.Create("a", 3);
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = Addition.Create(
            Multiplication.Create(operand, Variable.x),
            Variable.Create("c"));

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_integrate_power()
    {
        // arrange
        var operand = Power.Create(Variable.x, ValueConstant.Create(2));
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = "x^3/3+c";

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_integrate_sine()
    {
        // arrange
        var operand = Sine.Create(Variable.x);
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = "-cos(x)+c";

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_integrate_root()
    {
        // arrange
        var operand = Root.Create(2, Variable.x);
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = "x^1.5/1.5+c";

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_integrate_subtraction()
    {
        // arrange
        var operand = Subtraction.Create(Variable.x, Constant.One);
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = "(x^2/2+c)-(x+c)";

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_integrate_tangent()
    {
        // arrange
        var operand = Tangent.Create(Variable.x);
        var visitor = new IntegralOperandVisitor(Variable.x);

        // act
        var actual = () => operand.Accept(visitor);

        // assert
        actual.Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void Should_integrate_variable()
    {
        // arrange
        var operand = Variable.Create("x");
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = "x^2/2+c";

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_integrate_fraction()
    {
        // arrange
        var operand = Fraction.Create(1, 4);
        var visitor = new IntegralOperandVisitor(Variable.x);
        Operand expected = "(1/4)*x+c";

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_return_null_for_integrate_minimum()
    {
        // arrange
        var operand = Minimum.Create(Variable.x, 4);
        var visitor = new IntegralOperandVisitor(Variable.x);

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().BeNull();
    }

    [Fact]
    public void Should_return_null_for_integrate_maximum()
    {
        // arrange
        var operand = Maximum.Create(Variable.x, 4);
        var visitor = new IntegralOperandVisitor(Variable.x);

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().BeNull();
    }
}