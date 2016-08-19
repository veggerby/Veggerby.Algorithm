using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class MathJaxOperandVisitorTests
    {
        [Fact]
        public void Should_return_mathjax()
        {
            // arrange
            Operand func = "(2*sin((2*π)/x)+(2*π)/x^2*cos((2*π)/x)*2*x)/sin((2*π)/x)^2*exp((2*x)/sin((2*π)/x))";

            var visitor = new MathJaxOperandVisitor();

            // act
            func.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(@"{\frac{{2{\sin\left(\frac{2{π}}{x}\right)}}+{{2{{\frac{2{π}}{{x}^{2}}}\cdot{\cos\left(\frac{2{π}}{x}\right)}}}\cdot{x}}}{{\sin\left(\frac{2{π}}{x}\right)}^{2}}}\cdot{e^{\frac{2{x}}{\sin\left(\frac{2{π}}{x}\right)}}}");
        }
    }
}