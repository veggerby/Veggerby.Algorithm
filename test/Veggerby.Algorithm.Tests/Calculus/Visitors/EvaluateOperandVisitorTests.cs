using System;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors
{
    public class EvaluateOperandVisitorTests
    {
        [Fact]
        public void Should_evaluate_function()
        {
            // arrange
            var operand =Function.Create("f", ValueConstant.Create(3));
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(3);
        }

        [Fact]
        public void Should_evaluate_function_reference()
        {
            // arrange
            var operand =FunctionReference.Create("f", ValueConstant.Create(3));
            var ctx = new OperationContext();
            ctx.Add(Function.Create("f", "z+1"));
            var visitor = new EvaluateOperandVisitor(ctx);

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(4);
        }

        [Fact]
        public void Should_evaluate_addition()
        {
            // arrange
            var operand =Addition.Create(ValueConstant.One, ValueConstant.Create(3));
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(4);
        }

        [Fact]
        public void Should_evaluate_constant()
        {
            // arrange
            var operand =ValueConstant.Create(3);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(3);
        }

        [Fact]
        public void Should_evaluate_cosine()
        {
            // arrange
            var operand =Cosine.Create(ValueConstant.Pi);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(-1);
        }

        [Fact]
        public void Should_evaluate_division()
        {
            // arrange
            var operand =Division.Create(ValueConstant.Create(4), ValueConstant.Create(2));
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_evaluate_exponential()
        {
            // arrange
            var operand =Exponential.Create(1);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(Math.E);
        }

        [Fact]
        public void Should_evaluate_factorial()
        {
            // arrange
            var operand =Factorial.Create(ValueConstant.Create(4));
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(24);
        }

        [Fact]
        public void Should_evaluate_log_base10()
        {
            // arrange
            var operand =LogarithmBase.Create(10, 1000);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(3, 1e-15); // if no tolerance 3d should be equal to 3d :/
        }

        [Fact]
        public void Should_evaluate_log_base2()
        {
            // arrange
            var operand =LogarithmBase.Create(2, 1024);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(10, 1e-15); // if no tolerance 3d should be equal to 3d :/
        }

        [Fact]
        public void Should_evaluate_ln()
        {
            // arrange
            var operand =Logarithm.Create(ValueConstant.e);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(1);
        }

        [Fact]
        public void Should_evaluate_multiplication()
        {
            // arrange
            var operand =Multiplication.Create(ValueConstant.Create(4), ValueConstant.Create(2));
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(8);
        }

        [Fact]
        public void Should_evaluate_named_constant()
        {
            // arrange
            var operand =NamedConstant.Create("a", 3);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(3);
        }

        [Fact]
        public void Should_evaluate_power()
        {
            // arrange
            var operand =Power.Create(ValueConstant.Create(3), ValueConstant.Create(2));
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(9);
        }

        [Fact]
        public void Should_evaluate_root()
        {
            // arrange
            var operand =Root.Create(2, ValueConstant.Create(16));
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(4);
        }

        [Fact]
        public void Should_evaluate_sine()
        {
            // arrange
            var operand =Sine.Create(ValueConstant.Pi / 2);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(1);
        }

        [Fact]
        public void Should_evaluate_subtraction()
        {
            // arrange
            var operand =Subtraction.Create(ValueConstant.Create(3), ValueConstant.One);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_evaluate_tangent()
        {
            // arrange
            var operand =Tangent.Create(ValueConstant.Pi);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(0, 1E-15); // -1.22464679914735E-16d
        }

        [Fact]
        public void Should_evaluate_variable()
        {
            // arrange
            var operand =Variable.Create("x");
            var ctx = new OperationContext();
            ctx.Add("x", 2);
            var visitor = new EvaluateOperandVisitor(ctx);

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_evaluate_fraction()
        {
            // arrange
            var operand =Fraction.Create(1, 4);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(0.25);
        }

        [Fact]
        public void Should_evaluate_minimum()
        {
            // arrange
            var operand =Minimum.Create(Variable.x, 4);
            var ctx = new OperationContext();
            ctx.Add("x", 2);
            var visitor = new EvaluateOperandVisitor(ctx);

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_evaluate_maximum()
        {
            // arrange
            var operand =Maximum.Create(Variable.x, 4);
            var ctx = new OperationContext();
            ctx.Add("x", 2);
            var visitor = new EvaluateOperandVisitor(ctx);

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(4);
        }
    }
}