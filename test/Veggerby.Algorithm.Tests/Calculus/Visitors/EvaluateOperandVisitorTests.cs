using System;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors
{
    public class EvaluateOperandVisitorTests
    {
        public class Visit_Addition
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Addition.Create(Constant.One, Constant.Create(3));
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(4);
            }
        }

        public class Visit_Constant
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Constant.Create(3);
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(3);
            }
        }


        public class Visit_Cosine
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Cosine.Create(Constant.Pi);
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(-1);
            }
        }


        public class Visit_Division
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Division.Create(Constant.Create(4), Constant.Create(2));
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(2);
            }
        }


        public class Visit_Exponential
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Exponential.Create(1);
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(Math.E);
            }
        }

        public class Visit_Factorial
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Factorial.Create(Constant.Create(4));
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(24);
            }
        }

        public class Visit_LogarithmBase
        {
            [Fact]
            public void Should_evaluate_base10()
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
            public void Should_evaluate_base2()
            {
                // arrange
                var operation = LogarithmBase.Create(2, 1024);
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(10, 1e-15); // if no tolerance 3d should be equal to 3d :/
            }
        }


        public class Visit_Logarithm
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Logarithm.Create(Constant.e);
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(1);
            }
        }

        public class Visit_Multiplication
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Multiplication.Create(Constant.Create(4), Constant.Create(2));
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(8);
            }
        }

        public class Visit_NamedConstant
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = NamedConstant.Create("a", 3);
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(3);
            }
        }

        public class Visit_Power
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Power.Create(Constant.Create(3), Constant.Create(2));
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(9);
            }
        }

        public class Visit_Root
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Root.Create(2, Constant.Create(16));
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(4);
            }
        }

        public class Visit_Sine
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Sine.Create(Constant.Pi / 2);
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(1);
            }
        }

        public class Visit_Subtraction
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Subtraction.Create(Constant.Create(3), Constant.One);
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(2);
            }
        }

        public class Visit_Tangent
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Tangent.Create(Constant.Pi);
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(0, 1E-15); // -1.22464679914735E-16d
            }
        }

        public class Visit_Variable
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Fraction
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Fraction.Create(1, 4);
                var visitor = new EvaluateOperandVisitor(new OperationContext());

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(0.25);
            }
        }

        public class Visit_Minimum
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Maximum
        {
            [Fact]
            public void Should_evaluate()
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
}