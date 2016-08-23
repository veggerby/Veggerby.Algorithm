using System;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors
{
    public class DerivativeOperandVisitorTests
    {
        public class Visit_Addition
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Constant
        {
            [Fact]
            public void Should_return_correct_string()
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
        }

        public class Visit_Cosine
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Division
        {
            [Fact]
            public void Should_evaluate()
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
            public void Should_get_complex_derivative()
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
        }

        public class Visit_Exponential
        {
            [Fact]
            public void Should_evaluate()
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
            public void Should_get_complex_derivative()
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
        }

        public class Visit_Factorial
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_LogarithmBase
        {
            [Fact]
            public void Should_evaluate_base10()
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
        }

        public class Visit_Logarithm
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Multiplication
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_NamedConstant
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Power
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Sine
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Root
        {
            [Fact]
            public void Should_evaluate()
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
        }
        
        public class Visit_Subtraction
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Tangent
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Tangent.Create(Variable.x);
                var visitor = new DerivativeOperandVisitor(Variable.x);
                
                // act
                Should.Throw<NotImplementedException>(() => operation.Accept(visitor));

                // assert
            }
        }

        public class Visit_Variable
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Fraction
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Fraction.Create(1, 4);
                var visitor = new DerivativeOperandVisitor(Variable.x);
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(0);
            }
        }
    }
}