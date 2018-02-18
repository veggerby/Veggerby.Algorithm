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
            var operand = Function.Create("f", Sine.Create(Variable.x));
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("f(x)=sin(x)");
        }

        [Fact]
        public void Should_return_tostring_function_reference()
        {
            // arrange
            var operand = FunctionReference.Create("f", Sine.Create(Variable.x), Power.Create(Variable.y, 2));
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("f(sin(x), y^2)");
        }

        [Fact]
        public void Should_return_tostring_addition()
        {
            // arrange
            var operand = Addition.Create(Variable.x, Constant.Create(3));
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("x+3");
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(3.2, "3.2")]
        [InlineData(3.0000001, "3.0000001")]
        [InlineData(3.000000, "3")]
        public void Should_return_correct_constant_string(double value, string expected)
        {
            // arrange
            var operand = Constant.Create(value);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(expected);
        }

        [Fact]
        public void Should_return_tostring_cosine()
        {
            // arrange
            var operand = Cosine.Create(Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("cos(x)");
        }

        [Fact]
        public void Should_return_tostring_division()
        {
            // arrange
            var operand = Division.Create(Variable.x, Constant.Create(2));
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("x/2");
        }

        [Fact]
        public void Should_return_tostring_division_with_parenthesis()
        {
            // arrange
            var operand = Division.Create(Sine.Create(Variable.x), Division.Create(Variable.x, Constant.Create(2)));
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("sin(x)/(x/2)");
        }

        [Fact]
        public void Should_return_tostring_division_without_parenthesis()
        {
            // arrange
            var operand = Division.Create(Division.Create(Sine.Create(Variable.x), Variable.x), Constant.Create(2));
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("sin(x)/x/2");
        }
        [Fact]
        public void Should_return_tostring_exponential()
        {
            // arrange
            var operand = Exponential.Create(Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("exp(x)");
        }

        [Fact]
        public void Should_return_tostring_factorial()
        {
            // arrange
            var operand = Factorial.Create(Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("x!");
        }

        [Fact]
        public void Should_return_tostring_log_base10()
        {
            // arrange
            var operand = LogarithmBase.Create(10, Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("log(x)");
        }

        [Fact]
        public void Should_return_tostring_log_base2()
        {
            // arrange
            var operand = LogarithmBase.Create(2, Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("log2(x)");
        }

        [Fact]
        public void Should_return_tostring_ln()
        {
            // arrange
            var operand = Logarithm.Create(Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("ln(x)");
        }

        [Fact]
        public void Should_return_tostring_multiplication()
        {
            // arrange
            var operand = Multiplication.Create(Variable.x, Constant.Create(2));
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("x*2");
        }

        [Fact]
        public void Should_return_tostring_named_constant()
        {
            // arrange
            var operand = NamedConstant.Create("a", 3);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("a");
        }

        [Fact]
        public void Should_return_tostring_power()
        {
            // arrange
            var operand = Power.Create(Variable.x, Constant.Create(2));
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("x^2");
        }

        [Fact]
        public void Should_return_tostring_sqrt()
        {
            // arrange
            var operand = Root.Create(2, Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("sqrt(x)");
        }

        [Fact]
        public void Should_return_tostring_cubic_root()
        {
            // arrange
            var operand = Root.Create(3, Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("root(3, x)");
        }

        [Fact]
        public void Should_return_tostring_sine()
        {
            // arrange
            var operand = Sine.Create(Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("sin(x)");
        }

        [Fact]
        public void Should_return_tostring_subtraction()
        {
            // arrange
            var operand = Subtraction.Create(Variable.x, Constant.One);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("x-1");
        }

        [Fact]
        public void Should_return_tostring_subtraction_with_parenthesis()
        {
            // arrange
            var operand = Subtraction.Create(Sine.Create(Variable.x), Subtraction.Create(Variable.x, Constant.One));
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("sin(x)-(x-1)");
        }


        [Fact]
        public void Should_return_tostring_subtraction_without_parenthesis()
        {
            // arrange
            var operand = Subtraction.Create(Subtraction.Create(Sine.Create(Variable.x), Variable.x), Constant.One);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("sin(x)-x-1");
        }

        [Fact]
        public void Should_return_tostring_tangent()
        {
            // arrange
            var operand = Tangent.Create(Variable.x);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("tan(x)");
        }

        [Fact]
        public void Should_return_tostring_variable()
        {
            // arrange
            var operand = Variable.Create("x");
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("x");
        }

        [Fact]
        public void Should_return_tostring_fraction()
        {
            // arrange
            var operand = Fraction.Create(1, 4);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("1/4");
        }

        [Fact]
        public void Should_return_tostring_minimum()
        {
            // arrange
            var operand = Minimum.Create(Variable.x, 4);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("min(x, 4)");
        }

        [Fact]
        public void Should_return_tostring_maximum()
        {
            // arrange
            var operand = Maximum.Create(Variable.x, 4);
            var visitor = new ToStringOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe("max(x, 4)");
        }
    }
}