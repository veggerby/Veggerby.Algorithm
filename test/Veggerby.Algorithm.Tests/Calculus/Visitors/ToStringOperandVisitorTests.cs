using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors
{
    public class ToStringOperandVisitorTests
    {
        public class Visit_Addition
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Addition.Create(Variable.x, Constant.Create(3));
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("x+3");
            }
        }

        public class Visit_Constant
        {
            [Theory]
            [InlineData(1, "1")]
            [InlineData(3.2, "3.2")]
            [InlineData(3.0000001, "3.0000001")]
            [InlineData(3.000000, "3")]
            public void Should_return_correct_string(double value, string expected)
            {
                // arrange
                var operation = Constant.Create(value);
                var visitor = new ToStringOperandVisitor();
                
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
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("cos(x)");
            }
        }


        public class Visit_Division
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Division.Create(Variable.x, Constant.Create(2));
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("x/2");
            }
        }


        public class Visit_Exponential
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Exponential.Create(Variable.x);
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("exp(x)");
            }
        }

        public class Visit_Factorial
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Factorial.Create(Variable.x);
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("x!");
            }
        }

        public class Visit_LogarithmBase
        {
            [Fact]
            public void Should_evaluate_base10()
            {
                // arrange
                var operation = LogarithmBase.Create(10, Variable.x);
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("log(x)");
            }

            [Fact]
            public void Should_evaluate_base2()
            {
                // arrange
                var operation = LogarithmBase.Create(2, Variable.x);
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("log2(x)");
            }
        }


        public class Visit_Logarithm
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Logarithm.Create(Variable.x);
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("ln(x)"); 
            }
        }

        public class Visit_Multiplication
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Multiplication.Create(Variable.x, Constant.Create(2));
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("2*x"); 
            }
        }

        public class Visit_NamedConstant
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = NamedConstant.Create("a", 3);
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("a"); 
            }
        }

        public class Visit_Power
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Power.Create(Variable.x, Constant.Create(2));
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("x^2"); 
            }
        }

        public class Visit_Sine
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Sine.Create(Variable.x);
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("sin(x)"); 
            }
        }

        public class Visit_Subtraction
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Subtraction.Create(Variable.x, Constant.One);
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("x-1"); 
            }
        }

        public class Visit_Tangent
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Tangent.Create(Variable.x);
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("tan(x)");
            }
        }

        public class Visit_Evaluate
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Variable.Create("x");
                var visitor = new ToStringOperandVisitor();
                
                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe("x");
            }
        }

    }
}