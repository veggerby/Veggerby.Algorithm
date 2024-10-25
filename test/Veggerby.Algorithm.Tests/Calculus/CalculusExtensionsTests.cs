using System;

using FluentAssertions;

using Veggerby.Algorithm.Calculus;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus;

public class CalculusExtensionsTests
{
    [Theory]
    [InlineData("x!", typeof(Factorial), 0)]
    [InlineData("x^2", typeof(Power), 1)]
    [InlineData("x/2", typeof(Division), 2)]
    [InlineData("x*2", typeof(Multiplication), 3)]
    [InlineData("x-2", typeof(Subtraction), 4)]
    [InlineData("x+2", typeof(Addition), 5)]
    [InlineData("sin(x)", typeof(Sine), null)]
    [InlineData("cos(x)", typeof(Cosine), null)]
    [InlineData("exp(x)", typeof(Exponential), null)]
    [InlineData("ln(x)", typeof(Logarithm), null)]
    [InlineData("log(x)", typeof(LogarithmBase), null)]
    [InlineData("2", typeof(ValueConstant), null)]
    [InlineData("x", typeof(Variable), null)]
    [InlineData("pi", typeof(NamedConstant), null)]
    [InlineData("-x", typeof(Negative), null)]
    public void Should_return_correct_priority(string f, Type type, int? expected)
    {
        // arrange
        Operand func = f;

        // act
        var actual = func.GetPriority();

        // assert
        func.Should().BeOfType(type);
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("x!", typeof(Factorial), true)]
    [InlineData("x^2", typeof(Power), true)]
    [InlineData("x/2", typeof(Division), true)]
    [InlineData("x*2", typeof(Multiplication), true)]
    [InlineData("x-2", typeof(Subtraction), true)]
    [InlineData("x+2", typeof(Addition), true)]
    [InlineData("sin(x)", typeof(Sine), false)]
    [InlineData("cos(x)", typeof(Cosine), false)]
    [InlineData("exp(x)", typeof(Exponential), false)]
    [InlineData("ln(x)", typeof(Logarithm), false)]
    [InlineData("log(x)", typeof(LogarithmBase), false)]
    [InlineData("2", typeof(ValueConstant), false)]
    [InlineData("x", typeof(Variable), false)]
    [InlineData("pi", typeof(NamedConstant), false)]
    [InlineData("-x", typeof(Negative), false)]
    public void Should_return_could_use_parenthesis(string f, Type type, bool expected)
    {
        // arrange
        Operand func = f;

        // act
        var actual = func.CouldUseParenthesis();

        // assert
        func.Should().BeOfType(type);
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("x!", typeof(Factorial), false)]
    [InlineData("x^2", typeof(Power), false)]
    [InlineData("x/2", typeof(Division), false)]
    [InlineData("x*2", typeof(Multiplication), false)]
    [InlineData("x-2", typeof(Subtraction), false)]
    [InlineData("x+2", typeof(Addition), false)]
    [InlineData("sin(x)", typeof(Sine), false)]
    [InlineData("cos(x)", typeof(Cosine), false)]
    [InlineData("exp(x)", typeof(Exponential), false)]
    [InlineData("ln(x)", typeof(Logarithm), false)]
    [InlineData("log(x)", typeof(LogarithmBase), false)]
    [InlineData("2", typeof(ValueConstant), true)]
    [InlineData("x", typeof(Variable), false)]
    [InlineData("pi", typeof(NamedConstant), false)]
    [InlineData("-x", typeof(Negative), false)]
    public void Should_return_is_constant(string f, Type type, bool expected)
    {
        // arrange
        Operand func = f;

        // act
        var actual = func.IsConstant();

        // assert
        func.Should().BeOfType(type);
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("x!", typeof(Factorial), false)]
    [InlineData("x^2", typeof(Power), false)]
    [InlineData("x/2", typeof(Division), false)]
    [InlineData("x*2", typeof(Multiplication), false)]
    [InlineData("x-2", typeof(Subtraction), false)]
    [InlineData("x+2", typeof(Addition), false)]
    [InlineData("sin(x)", typeof(Sine), false)]
    [InlineData("cos(x)", typeof(Cosine), false)]
    [InlineData("exp(x)", typeof(Exponential), false)]
    [InlineData("ln(x)", typeof(Logarithm), false)]
    [InlineData("log(x)", typeof(LogarithmBase), false)]
    [InlineData("2", typeof(ValueConstant), false)]
    [InlineData("x", typeof(Variable), true)]
    [InlineData("pi", typeof(NamedConstant), false)]
    [InlineData("-x", typeof(Negative), false)]
    public void Should_return_is_variable(string f, Type type, bool expected)
    {
        // arrange
        Operand func = f;

        // act
        var actual = func.IsVariable();

        // assert
        func.Should().BeOfType(type);
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("x!", typeof(Factorial), false)]
    [InlineData("x^2", typeof(Power), false)]
    [InlineData("x/2", typeof(Division), false)]
    [InlineData("x*2", typeof(Multiplication), false)]
    [InlineData("x-2", typeof(Subtraction), false)]
    [InlineData("x+2", typeof(Addition), false)]
    [InlineData("sin(x)", typeof(Sine), false)]
    [InlineData("cos(x)", typeof(Cosine), false)]
    [InlineData("exp(x)", typeof(Exponential), false)]
    [InlineData("ln(x)", typeof(Logarithm), false)]
    [InlineData("log(x)", typeof(LogarithmBase), false)]
    [InlineData("2", typeof(ValueConstant), false)]
    [InlineData("x", typeof(Variable), false)]
    [InlineData("pi", typeof(NamedConstant), false)]
    [InlineData("-x", typeof(Negative), true)]
    public void Should_return_is_negative(string f, Type type, bool expected)
    {
        // arrange
        Operand func = f;

        // act
        var actual = func.IsNegative();

        // assert
        func.Should().BeOfType(type);
        actual.Should().Be(expected);
    }

    /*        [Fact]
            public void Should_return_simple_flatten()
            {
                // arrange
                var f = (IAssociativeOperation)Multiplication.Create(Variable.x, Constant.Pi);
                var expected = new HashSet<Operand> { f.Left, f.Right };

                // act
                var actual = f.FlattenAssociative();

                // assert
                actual.Should().Be(expected);
            }

            [Fact]
            public void Should_return_flatten_from_string()
            {
                // arrange
                Operand f = "(2*pi*cos(x)*x)*2";
                var expected = new Operand[] { Constant.Create(2), Constant.Pi, Cosine.Create(Variable.x), Variable.x, Constant.Create(2) };

                // act
                var actual = ((IAssociativeOperation)f).FlattenAssociative();

                // assert
                actual.Should().Be(expected);
            }

            [Fact]
            public void Should_return_flatten_nested()
            {
                // arrange
                var f = (IAssociativeBinaryOperation)Multiplication.Create(
                    Multiplication.Create(
                        Variable.x,
                        Sine.Create(Variable.x)),
                    Multiplication.Create(
                        Multiplication.Create(
                            Cosine.Create(Variable.x),
                            Logarithm.Create(Variable.x)
                        ),
                        Addition.Create(
                            Constant.One,
                            Variable.x
                )));

                var expected = new HashSet<Operand>
                {
                    Variable.x,
                    Sine.Create(Variable.x),
                    Cosine.Create(Variable.x),
                    Logarithm.Create(Variable.x),
                    Addition.Create(Constant.One, Variable.x)
                };

                // act
                var actual = f.FlattenAssociative();

                // assert
                actual.Should().Be(expected);
            }*/

    [Fact]
    public void Should_return_simple_latex()
    {
        // arrange
        var operand = Sine.Create(Variable.x);

        // act
        var actual = operand.ToLaTeXString();

        // assert
        actual.Should().Be(@"\sin\left(x\right)");
    }

    [Theory]
    [InlineData(0.1, false)]
    [InlineData(1.000000000, true)]
    [InlineData(5d, true)]
    [InlineData(4 / 2, true)]
    public void Should_return_expected(double value, bool expected)
    {
        // arrange
        var constant = ValueConstant.Create(value);

        // act
        var actual = constant.IsInteger();

        // assert
        actual.Should().Be(expected);
    }
}