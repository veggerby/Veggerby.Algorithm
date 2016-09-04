using System;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors
{
    public class DerivativeOperandVisitorTests
    {
        [Fact]
        public void Should_derive_function()
        {
            // arrange
            var operation = Function.Create("f", Variable.x);
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Constant.One;

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBeOfType<Function>();
            Function result = (Function)visitor.Result;
            result.Identifier.ShouldBe("f'");
            result.Variables.ShouldBeEmpty();
            result.Operand.ShouldBe(expected);
        }

        [Fact]
        public void Should_return_null_derive_function_reference()
        {
            // arrange
            var operation = FunctionReference.Create("f", new[] { Variable.x });
            var visitor = new DerivativeOperandVisitor(Variable.x);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBeNull();
        }

        [Fact]
        public void Should_derive_addition()
        {
            // arrange
            var operation = Addition.Create(Variable.x, Constant.Create(3));
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Constant.One;

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_constant()
        {
            // arrange
            var operation = Constant.Create(3);
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Constant.Zero;

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_cosine()
        {
            // arrange
            var operation = Cosine.Create(Variable.x);
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Negative.Create(Sine.Create(Variable.x));

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_division()
        {
            // arrange
            var operation = Division.Create(Constant.One, Variable.x);
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Division.Create(-1, Power.Create(Variable.x, 2));

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_division_complex()
        {
            // arrange
            var operation = Division.Create(
                Sine.Create(Variable.x),
                Variable.x);

            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Division.Create(
                Subtraction.Create(
                    Multiplication.Create(Variable.x, Cosine.Create(Variable.x)),
                    Sine.Create(Variable.x)),
                Power.Create(Variable.x, 2));

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_exponential()
        {
            // arrange
            var operation = Exponential.Create(Variable.x);
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Exponential.Create(Variable.x);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_exponential_complex()
        {
            // arrange
            var operation = Exponential.Create(
                Division.Create(
                    Multiplication.Create(2, Variable.x),
                    Sine.Create(2 * Constant.Pi / Variable.x)));

            var visitor = new DerivativeOperandVisitor(Variable.x);

            Operand expected = "(2*sin((2*π)/x)+(2*π)/x^2*cos((2*π)/x)*2*x)/sin((2*π)/x)^2*exp((2*x)/sin((2*π)/x))";

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_return_null_derive_factorial()
        {
            // arrange
            var operation = Factorial.Create(Variable.x);
            var visitor = new DerivativeOperandVisitor(Variable.x);
            Operand expected = null;

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_log_base10()
        {
            // arrange
            var operation = LogarithmBase.Create(10, Variable.x);
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Division.Create(Constant.One, Multiplication.Create(Variable.x, Logarithm.Create(10)));

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_ln()
        {
            // arrange
            var operation = Logarithm.Create(Variable.x);
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Division.Create(Constant.One, Variable.x);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_multiplication()
        {
            // arrange
            var operation = Multiplication.Create(Variable.x, Constant.Create(2));
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Constant.Create(2);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_named_constant()
        {
            // arrange
            var operation = NamedConstant.Create("a", 3);
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Constant.Zero;

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_power()
        {
            // arrange
            var operation = Power.Create(Variable.x, Constant.Create(2));
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Multiplication.Create(2, Variable.x);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_since()
        {
            // arrange
            var operation = Sine.Create(Variable.x);
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Cosine.Create(Variable.x);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_root()
        {
            // arrange
            var operation = Root.Create(2, Variable.x);
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Division.Create(Power.Create(Variable.x, Fraction.Create(-1, 2)), 2);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_subtraction()
        {
            // arrange
            var operation = Subtraction.Create(Variable.x, Constant.One);
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Constant.One;

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_tangent()
        {
            // arrange
            var operation = Tangent.Create(Variable.x);
            var visitor = new DerivativeOperandVisitor(Variable.x);

            // act
            Should.Throw<NotImplementedException>(() => operation.Accept(visitor));

            // assert
        }

        [Fact]
        public void Should_derive_variable()
        {
            // arrange
            var operation = Variable.Create("x");
            var visitor = new DerivativeOperandVisitor(Variable.x);
            var expected = Constant.One;

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_derive_fraction()
        {
            // arrange
            var operation = Fraction.Create(1, 4);
            var visitor = new DerivativeOperandVisitor(Variable.x);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(0);
        }

        [Fact]
        public void Should_return_null_for_min_derivative()
        {
            // arrange
            var operation = Minimum.Create(Variable.x, 4);
            var visitor = new DerivativeOperandVisitor(Variable.x);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBeNull();
        }

        [Fact]
        public void Should_return_null_for_max_derivative()
        {
            // arrange
            var operation = Maximum.Create(Variable.x, 4);
            var visitor = new DerivativeOperandVisitor(Variable.x);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBeNull();
        }
    }
}