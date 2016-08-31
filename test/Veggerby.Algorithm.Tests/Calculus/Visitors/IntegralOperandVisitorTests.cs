using System;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors
{
    public class IntegralOperandVisitorTests
    {
        public class Visit_Addition
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Constant
        {
            [Fact]
            public void Should_return_correct_string()
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
        }

        public class Visit_Cosine
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Division
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Exponential
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Factorial
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_LogarithmBase
        {
            [Fact]
            public void Should_evaluate_base10()
            {
                // arrange
                var operation = LogarithmBase.Create(10, Variable.x);
                var visitor = new IntegralOperandVisitor(Variable.x);
                var expected = Division.Create(Constant.One, Multiplication.Create(Variable.x, Logarithm.Create(10)));

                // act
                Should.Throw<NotImplementedException>(() => operation.Accept(visitor));

                // assert
            }
        }

        public class Visit_Logarithm
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_Multiplication
        {
            [Fact]
            public void Should_evaluate()
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
        }

        public class Visit_NamedConstant
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = NamedConstant.Create("a", 3);
                var visitor = new IntegralOperandVisitor(Variable.x);
                Operand expected = "a*x+c";

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
                var visitor = new IntegralOperandVisitor(Variable.x);
                Operand expected = "x^3/3+c";

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
                var visitor = new IntegralOperandVisitor(Variable.x);
                Operand expected = "-cos(x)+c";

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
                var visitor = new IntegralOperandVisitor(Variable.x);
                Operand expected = "x^1.5/1.5+c";

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
                var visitor = new IntegralOperandVisitor(Variable.x);
                Operand expected = "(x^2/2+c)-(x+c)";

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
                var visitor = new IntegralOperandVisitor(Variable.x);

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
                var visitor = new IntegralOperandVisitor(Variable.x);
                Operand expected = "x^2/2+c";

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
                var visitor = new IntegralOperandVisitor(Variable.x);
                Operand expected = "(1/4)*x+c";

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBe(expected);
            }
        }

        public class Visit_Minimum
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var operation = Minimum.Create(Variable.x, 4);
                var visitor = new IntegralOperandVisitor(Variable.x);

                // act
                operation.Accept(visitor);

                // assert
                visitor.Result.ShouldBeNull();
            }
        }

        public class Visit_Maximum
        {
            [Fact]
            public void Should_evaluate()
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
}