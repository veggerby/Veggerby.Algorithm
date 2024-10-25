using System;
using System.Linq;

using FluentAssertions;

using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Parser;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Parser;

public class FunctionParserTests
{
    [Fact]
    public void Should_parse_simple_function()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("x+3");

        // assert
        actual.Should().BeOfType<Addition>();
        ((Addition)actual).Operands.Should().BeEquivalentTo(new Operand[] { Variable.x, ValueConstant.Create(3) }, options => options.ComparingByValue<Operand>());
    }

    [Fact]
    public void Should_parse_chained_subtraction_in_correct_order()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("x-y-1");  // should be (x-y)-1 not x-(y-1)

        // assert
        actual.Should().BeOfType<Subtraction>();
        ((Subtraction)actual).Left.Should().BeOfType<Subtraction>();

        var left = ((Subtraction)actual).Left as Subtraction;

        left.Left.Should().Be(Variable.x);
        left.Right.Should().Be(Variable.y);

        ((Subtraction)actual).Right.Should().Be(Constant.One);
    }


    [Fact]
    public void Should_parse_simple_function_with_parenthesis()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("x+(3)");

        // assert
        actual.Should().BeOfType<Addition>();
        ((Addition)actual).Operands.Should().BeEquivalentTo(new Operand[] { Variable.x, ValueConstant.Create(3) }, options => options.ComparingByValue<Operand>());
    }

    [Fact]
    public void Should_parse_simple_function_with_priority()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("x+3*x");

        // assert
        actual.Should().BeOfType<Addition>();
        ((Addition)actual).Operands.Count().Should().Be(2);
        ((Addition)actual).Operands.First().Should().Be(Variable.x);
        ((Addition)actual).Operands.Last().Should().BeOfType<Multiplication>();

        var right = ((Addition)actual).Operands.Last() as Multiplication;
        right.Operands.Should().BeEquivalentTo(new Operand[] { ValueConstant.Create(3), Variable.x }, options => options.ComparingByValue<Operand>());
    }

    [Fact]
    public void Should_parse_subtraction_correct_order()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("x-3+x"); // should be (x-3)+x not x-(3+x)

        // assert
        actual.Should().BeOfType<Addition>();

        ((Addition)actual).Operands.Count().Should().Be(2);
        ((Addition)actual).Operands.First().Should().BeOfType<Subtraction>();
        ((Addition)actual).Operands.Last().Should().Be(Variable.x);

        var left = ((Addition)actual).Operands.First() as Subtraction;
        left.Left.Should().Be(Variable.x);
        left.Right.Should().Be(ValueConstant.Create(3));
    }

    [Fact]
    public void Should_parse_parenthesis_transition()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("(x-3)*x");

        // assert
        actual.Should().BeOfType<Multiplication>();
        ((Multiplication)actual).Operands.Count().Should().Be(2);
        ((Multiplication)actual).Operands.First().Should().BeOfType<Subtraction>();
        ((Multiplication)actual).Operands.Last().Should().Be(Variable.x);

        var left = ((Multiplication)actual).Operands.First() as Subtraction;
        left.Left.Should().Be(Variable.x);
        left.Right.Should().Be(ValueConstant.Create(3));
    }

    [Fact]
    public void Should_parse_positive_constant()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("x-+3");

        // assert
        actual.Should().BeOfType<Subtraction>();
        ((Subtraction)actual).Left.Should().Be(Variable.x);
        ((Subtraction)actual).Right.Should().Be(ValueConstant.Create(3));
    }

    [Fact]
    public void Should_parse_fraction()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("1/2");

        // assert
        actual.Should().BeOfType<Fraction>();
        ((Fraction)actual).Numerator.Should().Be(Constant.One);
        ((Fraction)actual).Denominator.Should().Be(ValueConstant.Create(2));
    }

    [Fact]
    public void Should_parse_simple_factorial()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("x!");

        // assert
        actual.Should().BeOfType<Factorial>();
        ((Factorial)actual).Inner.Should().Be(Variable.x);
    }

    [Fact]
    public void Should_parse_named_constant()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("pi");

        // assert
        actual.Should().Be(Constant.Pi);
    }

    [Fact]
    public void Should_parse_negative()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("-x+3");

        // assert
        actual.Should().BeOfType<Subtraction>();
        ((Subtraction)actual).Left.Should().Be(ValueConstant.Create(3));
        ((Subtraction)actual).Right.Should().Be(Variable.x);
    }

    [Fact]
    public void Should_parse_negative_v2()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("+x-3");

        // assert
        actual.Should().BeOfType<Subtraction>();
        ((Subtraction)actual).Left.Should().Be(Variable.x);
        ((Subtraction)actual).Right.Should().Be(ValueConstant.Create(3));
    }

    [Fact]
    public void Should_parse_negative_in_parenthesis()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("x*(-3+x)");

        // assert
        actual.Should().BeOfType<Multiplication>();
        ((Multiplication)actual).Operands.Count().Should().Be(2);
        ((Multiplication)actual).Operands.First().Should().Be(Variable.x);
        ((Multiplication)actual).Operands.Last().Should().BeOfType<Subtraction>();
    }

    [Fact]
    public void Should_parse_function()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("sin(x)");

        // assert
        actual.Should().BeOfType<Sine>();
        ((Sine)actual).Inner.Should().Be(Variable.x);
    }

    [Fact]
    public void Should_parse_function_with_addition()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("3+sin(x)");

        // assert
        actual.Should().BeOfType<Addition>();
        ((Addition)actual).Operands.Count().Should().Be(2);
        ((Addition)actual).Operands.First().Should().Be(ValueConstant.Create(3));
        ((Addition)actual).Operands.Last().Should().BeOfType<Sine>();

        var right = ((Addition)actual).Operands.Last() as Sine;
        right.Inner.Should().Be(Variable.x);
    }

    [Fact]
    public void Should_binary_function()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("max(3,x)");

        // assert
        actual.Should().BeOfType<Maximum>();
        ((Maximum)actual).Operands.Count().Should().Be(2);
        ((Maximum)actual).Operands.First().Should().Be(ValueConstant.Create(3));
        ((Maximum)actual).Operands.Last().Should().Be(Variable.x);
    }

    [Fact]
    public void Should_root_function()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("root(3,x)");

        // assert
        actual.Should().BeOfType<Root>();
        ((Root)actual).Exponent.Should().Be(3);
        ((Root)actual).Inner.Should().Be(Variable.x);
    }

    [Fact]
    public void Should_complex_function_with_binary_function()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("x*4-root(3,sin(x))");

        // assert
        actual.Should().BeOfType<Subtraction>();
        ((Subtraction)actual).Left.Should().BeOfType<Multiplication>();
        ((Subtraction)actual).Right.Should().BeOfType<Root>();

        Multiplication left = ((Subtraction)actual).Left as Multiplication;
        Root right = ((Subtraction)actual).Right as Root;


        left.Operands.Count().Should().Be(2);
        left.Operands.First().Should().Be(Variable.x);
        left.Operands.Last().Should().Be(ValueConstant.Create(4)); ;

        right.Exponent.Should().Be(3);
        right.Inner.Should().BeOfType<Sine>();
    }

    [Fact]
    public void Should_parse_complex_function()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("(x+3)*(4-2)");

        // assert
        actual.Should().BeOfType<Multiplication>();

        ((Multiplication)actual).Operands.Should().BeEquivalentTo(new[] {
            Addition.Create(Variable.x, ValueConstant.Create(3)),
            Subtraction.Create(ValueConstant.Create(4), ValueConstant.Create(2)) }, options => options.ComparingByValue<Operand>());
    }

    [Fact]
    public void Should_parse_very_complex_function()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("(x+3)*(4-2)-sin(x*cos(tan(exp(3-ln(4/sin(x))))))");

        // assert
        actual.Should().BeOfType<Subtraction>();
        ((Subtraction)actual).Left.Should().BeOfType<Multiplication>();
        ((Subtraction)actual).Right.Should().BeOfType<Sine>();

        var left = ((Subtraction)actual).Left as Multiplication; // (x+3)*(4-2) will be reduced to 2*(x+3)
        var right = ((Subtraction)actual).Right as Sine;

        left.Operands.Should().BeEquivalentTo(new[] { Addition.Create(Variable.x, ValueConstant.Create(3)), Subtraction.Create(ValueConstant.Create(4), ValueConstant.Create(2)) }, options => options.ComparingByValue<Operand>());

        right.Inner.Should().BeOfType<Multiplication>();

        var inner = right.Inner as Multiplication;
        inner.Operands.Count().Should().Be(2);
        inner.Operands.First().Should().Be(Variable.x);
        inner.Operands.Last().Should().BeOfType<Cosine>();

        ((Cosine)inner.Operands.Last()).Inner.Should().BeOfType<Tangent>();
        ((Tangent)((Cosine)inner.Operands.Last()).Inner).Inner.Should().BeOfType<Exponential>();

        var exp = ((Tangent)((Cosine)inner.Operands.Last()).Inner).Inner as Exponential;
        exp.Inner.Should().BeOfType<Subtraction>();

        ((Subtraction)exp.Inner).Left.Should().Be(ValueConstant.Create(3));
        ((Subtraction)exp.Inner).Right.Should().BeOfType<Logarithm>();

        ((exp.Inner as Subtraction).Right as Logarithm).Inner.Should().BeOfType<Division>();

        var innerMostDivision = ((exp.Inner as Subtraction).Right as Logarithm).Inner as Division;
        innerMostDivision.Left.Should().Be(ValueConstant.Create(4));
        innerMostDivision.Right.Should().BeOfType<Sine>();
        ((Sine)innerMostDivision.Right).Inner.Should().Be(Variable.x);
    }

    [Fact]
    public void Should_fail_with_unmatching_parenthesis_open()
    {
        // arrange

        // act
        var actual = () => FunctionParser.Parse("((x+3)");

        // assert
        actual.Should().Throw<Exception>().WithMessage("*Parenthesis not properly closed*");
    }

    [Fact]
    public void Should_fail_with_unmatching_parenthesis_close()
    {
        // arrange

        // act
        var actual = () => FunctionParser.Parse("(x+3))");

        // assert
        actual.Should().Throw<Exception>().WithMessage("*Parenthesis not properly closed*");
    }

    [Fact]
    public void Should_fail_with_unmatching_parenthesis_close_nested()
    {
        // arrange

        // act
        var actual = () => FunctionParser.Parse("(3+(x+3)");

        // assert
        actual.Should().Throw<Exception>().WithMessage("*Parenthesis not properly closed*");
    }


    [Fact]
    public void Should_parse_unspecified_constant()
    {
        // arrange

        // act
        var actual = FunctionParser.Parse("(A-x)*(B-x*A)");

        // assert
        actual.Should().BeOfType<Multiplication>();
        ((Multiplication)actual).Operands.Count().Should().Be(2);

        var left = ((Multiplication)actual).Operands.First();
        var right = ((Multiplication)actual).Operands.Last();

        left.Should().BeOfType<Subtraction>();
        ((Subtraction)left).Left.Should().BeOfType<UnspecifiedConstant>();
        var unspec1 = ((Subtraction)left).Left as UnspecifiedConstant;
        ((Subtraction)left).Right.Should().Be(Variable.x);

        right.Should().BeOfType<Subtraction>();
        ((Subtraction)right).Left.Should().BeOfType<UnspecifiedConstant>();
        ((Subtraction)right).Right.Should().BeOfType<Multiplication>();
        var unspec2 = ((Subtraction)right).Left as UnspecifiedConstant;

        var mult = ((Subtraction)right).Right as Multiplication;
        mult.Operands.Count().Should().Be(2);
        mult.Operands.First().Should().Be(Variable.x);
        mult.Operands.Last().Should().BeOfType<UnspecifiedConstant>();
        var unspec3 = mult.Operands.Last() as UnspecifiedConstant;

        unspec1.Should().NotBe(unspec2);
        unspec1.Should().Be(unspec3);
        unspec2.Should().NotBe(unspec3);
    }
}