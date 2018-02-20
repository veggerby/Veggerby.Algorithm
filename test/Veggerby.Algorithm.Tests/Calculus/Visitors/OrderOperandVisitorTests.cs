using System;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors
{
    public class OrderOperandVisitorTests
    {
        [Fact]
        public void Should_return_constant()
        {
            // arrange
            var operand = Constant.One;
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(1);
        }

        [Fact]
        public void Should_return_named_constant()
        {
            // arrange
            var operand = Constant.Pi;
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(3);
        }

        [Fact]
        public void Should_return_variable()
        {
            // arrange
            var operand = Variable.x;
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(4);
        }

        [Fact]
        public void Should_return_fraction()
        {
            // arrange
            var operand = Fraction.Create(1, 2);
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_addition()
        {
            // arrange
            var operand = Addition.Create(Constant.One, Variable.x);
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(6);
        }

        [Fact]
        public void Should_return_multiplication()
        {
            // arrange
            var operand = Multiplication.Create(Constant.Create(2), Variable.x);
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(8);
        }

        [Fact]
        public void Should_return_subtraction()
        {
            // arrange
            var operand = Subtraction.Create(Constant.Create(2), Variable.x);
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(7);
        }

        [Fact]
        public void Should_return_negative()
        {
            // arrange
            var operand = Negative.Create(Variable.x);
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(5);
        }

        [Fact]
        public void Should_return_division()
        {
            // arrange
            var operand = Division.Create(Constant.Create(2), Variable.x);
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(9);
        }

        [Fact]
        public void Should_return_power()
        {
            // arrange
            var operand = Power.Create(Constant.Create(2), Variable.x);
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(10);
        }

        [Fact]
        public void Should_return_sine()
        {
            // arrange
            var operand = Sine.Create(Constant.One);
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(16);
        }

        [Fact]
        public void Should_return_cosine()
        {
            // arrange
            var operand = Cosine.Create(Constant.One);
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(17);
        }

        [Fact]
        public void Should_return_exponential()
        {
            // arrange
            var operand = Exponential.Create(Constant.One);
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(14);
        }

        [Fact]
        public void Should_return_logarithm()
        {
            // arrange
            var operand = Logarithm.Create(Constant.One);
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(15);
        }

        [Fact]
        public void Should_return_logarithm_base()
        {
            // arrange
            var operand = LogarithmBase.Create(10, Constant.One);
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(20);
        }

        [Fact]
        public void Should_return_tangent()
        {
            // arrange
            var operand = Tangent.Create(Constant.One);
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(18);
        }

        [Fact]
        public void Should_return_root()
        {
            // arrange
            var operand = Root.Create(3, Constant.Create(2));
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(19);
        }

        [Fact]
        public void Should_return_factorial()
        {
            // arrange
            var operand = Factorial.Create(Constant.Create(2));
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(13);
        }

        [Fact]
        public void Should_return_minimum()
        {
            // arrange
            var operand = Minimum.Create(Variable.x, Constant.Create(2));
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(21);
        }

        [Fact]
        public void Should_return_maximum()
        {
            // arrange
            var operand = Maximum.Create(Variable.x, Constant.Create(2));
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(22);
        }

        [Fact]
        public void Should_return_function()
        {
            // arrange
            var operand = Function.Create("f", Constant.Create(2));
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(11);
        }

        [Fact]
        public void Should_return_function_reference()
        {
            // arrange
            var operand = FunctionReference.Create("f", Constant.Create(2));
            var visitor = new OrderOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(12);
        }
    }
}