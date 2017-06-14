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
            var operation = Function.Create("f", Constant.Create(3));
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(3);
        }

        [Fact]
        public void Should_evaluate_function_reference()
        {
            // arrange
            var operation = FunctionReference.Create("f", Constant.Create(3));
            var ctx = new OperationContext();
            ctx.Add(Function.Create("f", "z+1"));
            var visitor = new EvaluateOperandVisitor(ctx);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(4);
        }

        [Fact]
        public void Should_evaluate_addition()
        {
            // arrange
            var operation = Addition.Create(Constant.One, Constant.Create(3));
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(4);
        }

        [Fact]
        public void Should_evaluate_constant()
        {
            // arrange
            var operation = Constant.Create(3);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(3);
        }

        [Fact]
        public void Should_evaluate_cosine()
        {
            // arrange
            var operation = Cosine.Create(Constant.Pi);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(-1);
        }

        [Fact]
        public void Should_evaluate_division()
        {
            // arrange
            var operation = Division.Create(Constant.Create(4), Constant.Create(2));
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(2);
        }

        [Fact]
        public void Should_evaluate_exponential()
        {
            // arrange
            var operation = Exponential.Create(1);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(Math.E);
        }

        [Fact]
        public void Should_evaluate_factorial()
        {
            // arrange
            var operation = Factorial.Create(Constant.Create(4));
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(24);
        }

        [Fact]
        public void Should_evaluate_log_base10()
        {
            // arrange
            var operation = LogarithmBase.Create(10, 1000);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(3, 1e-15); // if no tolerance 3d should be equal to 3d :/
        }

        [Fact]
        public void Should_evaluate_log_base2()
        {
            // arrange
            var operation = LogarithmBase.Create(2, 1024);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(10, 1e-15); // if no tolerance 3d should be equal to 3d :/
        }

        [Fact]
        public void Should_evaluate_ln()
        {
            // arrange
            var operation = Logarithm.Create(Constant.e);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(1);
        }

        [Fact]
        public void Should_evaluate_multiplication()
        {
            // arrange
            var operation = Multiplication.Create(Constant.Create(4), Constant.Create(2));
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(8);
        }

        [Fact]
        public void Should_evaluate_named_constant()
        {
            // arrange
            var operation = NamedConstant.Create("a", 3);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(3);
        }

        [Fact]
        public void Should_evaluate_power()
        {
            // arrange
            var operation = Power.Create(Constant.Create(3), Constant.Create(2));
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(9);
        }

        [Fact]
        public void Should_evaluate_root()
        {
            // arrange
            var operation = Root.Create(2, Constant.Create(16));
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(4);
        }

        [Fact]
        public void Should_evaluate_sine()
        {
            // arrange
            var operation = Sine.Create(Constant.Pi / 2);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(1);
        }

        [Fact]
        public void Should_evaluate_subtraction()
        {
            // arrange
            var operation = Subtraction.Create(Constant.Create(3), Constant.One);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(2);
        }

        [Fact]
        public void Should_evaluate_tangent()
        {
            // arrange
            var operation = Tangent.Create(Constant.Pi);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(0, 1E-15); // -1.22464679914735E-16d
        }

        [Fact]
        public void Should_evaluate_variable()
        {
            // arrange
            var operation = Variable.Create("x");
            var ctx = new OperationContext();
            ctx.Add("x", 2);
            var visitor = new EvaluateOperandVisitor(ctx);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(2);
        }

        [Fact]
        public void Should_evaluate_fraction()
        {
            // arrange
            var operation = Fraction.Create(1, 4);
            var visitor = new EvaluateOperandVisitor(new OperationContext());

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(0.25);
        }

        [Fact]
        public void Should_evaluate_minimum()
        {
            // arrange
            var operation = Minimum.Create(Variable.x, 4);
            var ctx = new OperationContext();
            ctx.Add("x", 2);
            var visitor = new EvaluateOperandVisitor(ctx);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(2);
        }

        [Fact]
        public void Should_evaluate_maximum()
        {
            // arrange
            var operation = Maximum.Create(Variable.x, 4);
            var ctx = new OperationContext();
            ctx.Add("x", 2);
            var visitor = new EvaluateOperandVisitor(ctx);

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(4);
        }
    }
}