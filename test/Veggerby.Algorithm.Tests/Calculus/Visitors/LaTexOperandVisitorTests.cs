using FluentAssertions;

using Veggerby.Algorithm.Calculus.Parser;
using Veggerby.Algorithm.Calculus.Visitors;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors;

public class LaTeXOperandVisitorTests
{
    [Theory]
    [InlineData("2", "2")]
    [InlineData("x", "x")]
    [InlineData("pi", "π")]
    [InlineData("2+x", "{2}+{x}")]
    [InlineData("2-x", "{2}-{x}")]
    [InlineData("2*x", @"{2}\cdot{x}")]
    [InlineData("2/x", @"\frac{2}{x}")]
    [InlineData("2^x", "{2}^{x}")]
    [InlineData("sin(x)", @"\sin\left(x\right)")]
    [InlineData("cos(x)", @"\cos\left(x\right)")]
    [InlineData("tan(x)", @"\tan\left(x\right)")]
    [InlineData("exp(x)", @"e^{x}")]
    [InlineData("ln(x)", @"\ln\left(x\right)")]
    [InlineData("log(x)", @"\log\left(x\right)")]
    [InlineData("log2(x)", @"\log_2\left(x\right)")]
    [InlineData("sqrt(x)", @"\sqrt{x}")]
    [InlineData("root(3,x)", @"\sqrt[3]{x}")]
    [InlineData("min(3,x)", @"\min\left({3}, {x}\right)")]
    [InlineData("max(3,x)", @"\max\left({3}, {x}\right)")]
    public void Should_return_latex_formula(string input, string expected)
    {
        // arrange
        var operand = FunctionParser.Parse(input);
        var visitor = new LaTeXOperandVisitor();

        // act
        var actual = operand.Accept(visitor);

        // assert
        actual.Should().Be(expected);
    }
}