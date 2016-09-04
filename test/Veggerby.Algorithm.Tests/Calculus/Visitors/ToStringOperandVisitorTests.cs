using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors
{
    public class ToStringOperandVisitorTests
    {
        [Fact]
        public void Should_return_tostring_function()
        {
            // arrange
            var operation = Function.Create("f", Sine.Create(Variable.x));
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("f(x)=sin(x)");
        }

        [Fact]
        public void Should_return_tostring_addition()
        {
            // arrange
            var operation = Addition.Create(Variable.x, Constant.Create(3));
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("x+3");
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(3.2, "3.2")]
        [InlineData(3.0000001, "3.0000001")]
        [InlineData(3.000000, "3")]
        public void Should_return_correct_constant_string(double value, string expected)
        {
            // arrange
            var operation = Constant.Create(value);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe(expected);
        }

        [Fact]
        public void Should_return_tostring_cosine()
        {
            // arrange
            var operation = Cosine.Create(Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("cos(x)");
        }

        [Fact]
        public void Should_return_tostring_division()
        {
            // arrange
            var operation = Division.Create(Variable.x, Constant.Create(2));
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("x/2");
        }

        [Fact]
        public void Should_return_tostring_exponential()
        {
            // arrange
            var operation = Exponential.Create(Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("exp(x)");
        }

        [Fact]
        public void Should_return_tostring_factorial()
        {
            // arrange
            var operation = Factorial.Create(Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("x!");
        }

        [Fact]
        public void Should_return_tostring_log_base10()
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
        public void Should_return_tostring_log_base2()
        {
            // arrange
            var operation = LogarithmBase.Create(2, Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("log2(x)");
        }

        [Fact]
        public void Should_return_tostring_ln()
        {
            // arrange
            var operation = Logarithm.Create(Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("ln(x)");
        }

        [Fact]
        public void Should_return_tostring_multiplication()
        {
            // arrange
            var operation = Multiplication.Create(Variable.x, Constant.Create(2));
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("2*x");
        }

        [Fact]
        public void Should_return_tostring_named_constant()
        {
            // arrange
            var operation = NamedConstant.Create("a", 3);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("a");
        }

        [Fact]
        public void Should_return_tostring_power()
        {
            // arrange
            var operation = Power.Create(Variable.x, Constant.Create(2));
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("x^2");
        }

        [Fact]
        public void Should_return_tostring_sqrt()
        {
            // arrange
            var operation = Root.Create(2, Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("sqrt(x)");
        }

        [Fact]
        public void Should_return_tostring_cubic_root()
        {
            // arrange
            var operation = Root.Create(3, Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("root(3, x)");
        }

        [Fact]
        public void Should_return_tostring_sine()
        {
            // arrange
            var operation = Sine.Create(Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("sin(x)");
        }

        [Fact]
        public void Should_return_tostring_subtraction()
        {
            // arrange
            var operation = Subtraction.Create(Variable.x, Constant.One);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("x-1");
        }

        [Fact]
        public void Should_return_tostring_tangent()
        {
            // arrange
            var operation = Tangent.Create(Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("tan(x)");
        }

        [Fact]
        public void Should_return_tostring_variable()
        {
            // arrange
            var operation = Variable.Create("x");
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("x");
        }

        [Fact]
        public void Should_return_tostring_fraction()
        {
            // arrange
            var operation = Fraction.Create(1, 4);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("1/4");
        }

        [Fact]
        public void Should_return_tostring_minimum()
        {
            // arrange
            var operation = Minimum.Create(Variable.x, 4);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("min(x, 4)");
        }

        [Fact]
        public void Should_return_tostring_maximum()
        {
            // arrange
            var operation = Maximum.Create(Variable.x, 4);
            var visitor = new ToStringOperandVisitor();

            // act
            operation.Accept(visitor);

            // assert
            visitor.Result.ShouldBe("max(x, 4)");
        }
    }
}