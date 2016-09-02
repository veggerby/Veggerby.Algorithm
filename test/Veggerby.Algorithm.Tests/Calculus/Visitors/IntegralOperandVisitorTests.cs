using System;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors
{
    public class IntegralOperandVisitorTests
    {
        [Fact]
        public void Should_integrate_addition()
        {
            // arrange
            var operation = Addition.Create(Variable.x, Constant.Create(3));
            var visitor = new IntegralOperandVisitor(Variable.x);
            Operand expected = "x^2/2+c+3*x+c";

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_integrate_constant()
        {
            // arrange
            var operation = Constant.Create(3);
            var visitor = new IntegralOperandVisitor(Variable.x);
            var expected = "3*x+c";

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_integrate_cosine()
        {
            // arrange
            var operation = Cosine.Create(Variable.x);
            var visitor = new IntegralOperandVisitor(Variable.x);
            Operand expected = "sin(x)+c";

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_integrate_division()
        {
            // arrange
            var operation = Division.Create(Constant.One, Variable.x);
            var visitor = new IntegralOperandVisitor(Variable.x);
            Operand expected = "ln(x)+c";

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_integrate_exponential()
        {
            // arrange
            var operation = Exponential.Create(Variable.x);
            var visitor = new IntegralOperandVisitor(Variable.x);
            Operand expected = "exp(x)+c";

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_return_null_for_integrate_factorial()
        {
            // arrange
            var operation = Factorial.Create(Variable.x);
            var visitor = new IntegralOperandVisitor(Variable.x);
            Operand expected = null;

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_integrate_log_base10()
        {
            // arrange
            var operation = LogarithmBase.Create(10, Variable.x);
            var visitor = new IntegralOperandVisitor(Variable.x);
            var expected = Division.Create(Constant.One, Multiplication.Create(Variable.x, Logarithm.Create(10)));

            // act
            Should.Throw<NotImplementedException>(() => operation.Accept(visitor));

            // assert
        }

        [Fact]
        public void Should_integrate_logarithm()
        {
            // arrange
            var operation = Logarithm.Create(Variable.x);
            var visitor = new IntegralOperandVisitor(Variable.x);
            var expected = "x*ln(x)-x+c";

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_integrate_multiplication()
        {
            // arrange
            var operation = Multiplication.Create(Variable.x, Cosine.Create(Variable.x));
            var visitor = new IntegralOperandVisitor(Variable.x);
            Operand expected = "x*(sin(x)+c)-(-cos(x)+2*c+c*x)";

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_integrate_named_constant()
        {
            // arrange
            var operation = NamedConstant.Create("a", 3);
            var visitor = new IntegralOperandVisitor(Variable.x);
            Operand expected = Addition.Create(
                Multiplication.Create(operation, Variable.x),
                Variable.Create("c"));

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_integrate_power()
        {
            // arrange
            var operation = Power.Create(Variable.x, Constant.Create(2));
            var visitor = new IntegralOperandVisitor(Variable.x);
            Operand expected = "x^3/3+c";

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_integrate_sine()
        {
            // arrange
            var operation = Sine.Create(Variable.x);
            var visitor = new IntegralOperandVisitor(Variable.x);
            Operand expected = "-cos(x)+c";

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_integrate_root()
        {
            // arrange
            var operation = Root.Create(2, Variable.x);
            var visitor = new IntegralOperandVisitor(Variable.x);
            Operand expected = "x^1.5/1.5+c";

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_integrate_subtraction()
        {
            // arrange
            var operation = Subtraction.Create(Variable.x, Constant.One);
            var visitor = new IntegralOperandVisitor(Variable.x);
            Operand expected = "(x^2/2+c)-(x+c)";

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_integrate_tangent()
        {
            // arrange
            var operation = Tangent.Create(Variable.x);
            var visitor = new IntegralOperandVisitor(Variable.x);

            // act
            Should.Throw<NotImplementedException>(() => operation.Accept(visitor));

            // assert
        }

        [Fact]
        public void Should_integrate_variable()
        {
            // arrange
            var operation = Variable.Create("x");
            var visitor = new IntegralOperandVisitor(Variable.x);
            Operand expected = "x^2/2+c";

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_integrate_fraction()
        {
            // arrange
            var operation = Fraction.Create(1, 4);
            var visitor = new IntegralOperandVisitor(Variable.x);
            Operand expected = "(1/4)*x+c";

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_return_null_for_integrate_minimum()
        {
            // arrange
            var operation = Minimum.Create(Variable.x, 4);
            var visitor = new IntegralOperandVisitor(Variable.x);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_for_integrate_maximum()
        {
            // arrange
            var operation = Maximum.Create(Variable.x, 4);
            var visitor = new IntegralOperandVisitor(Variable.x);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBeNull();
        }
    }
}