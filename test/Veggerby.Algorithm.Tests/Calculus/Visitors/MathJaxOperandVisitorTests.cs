using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors
{
    public class MathJaxOperandVisitorTests
    {
        [Fact]
        public void Should_return_mathjax_simple()
        {
            // arrange
            Operand func = "(2*pi*cos(x)*x/(x^2))*2";

            var visitor = new MathJaxOperandVisitor();

            // act
            func.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(@"\frac{{{4{π}}\cdot{\cos\left(x\right)}}\cdot{x}}{{x}^{2}}");
        }

        [Fact]
        public void Should_return_mathjax()
        {
            // arrange
            Operand func = "(2*sin((2*π)/x)+(2*π)/x^2*cos((2*π)/x)*2*x)/sin((2*π)/x)^2*exp((2*x)/sqrt((2*π)/x))";

            var visitor = new MathJaxOperandVisitor();

            // act
            func.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(@"\frac{{{2{\sin\left(\frac{2{π}}{x}\right)}}+{\frac{{{4{π}}\cdot{\cos\left(\frac{2{π}}{x}\right)}}\cdot{x}}{{x}^{2}}}}\cdot{e^{\frac{2{x}}{\sqrt{\frac{2{π}}{x}}}}}}{{\sin\left(\frac{2{π}}{x}\right)}^{2}}");
        }
    }
}