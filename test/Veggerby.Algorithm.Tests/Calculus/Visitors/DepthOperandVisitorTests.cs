using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors
{
    public class DepthOperandVisitorTests
    {
        [Fact]
        public void Should_return_constant()
        {
            // arrange
            var operand = ValueConstant.One;
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(1);
        }

        [Fact]
        public void Should_return_named_constant()
        {
            // arrange
            var operand = ValueConstant.Pi;
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(1);
        }

        [Fact]
        public void Should_return_variable()
        {
            // arrange
            var operand = Variable.x;
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(1);
        }

        [Fact]
        public void Should_return_fraction()
        {
            // arrange
            var operand = Fraction.Create(1, 2);
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(1);
        }

        [Fact]
        public void Should_return_addition()
        {
            // arrange
            var operand = Addition.Create(ValueConstant.One, Variable.x);
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_multiplication()
        {
            // arrange
            var operand = Multiplication.Create(ValueConstant.Create(2), Variable.x);
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_subtraction()
        {
            // arrange
            var operand = Subtraction.Create(ValueConstant.Create(2), Variable.x);
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_negative()
        {
            // arrange
            var operand = Negative.Create(Variable.x);
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_division()
        {
            // arrange
            var operand = Division.Create(ValueConstant.Create(2), Variable.x);
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_power()
        {
            // arrange
            var operand = Power.Create(ValueConstant.Create(2), Variable.x);
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_sine()
        {
            // arrange
            var operand = Sine.Create(ValueConstant.One);
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_cosine()
        {
            // arrange
            var operand = Cosine.Create(ValueConstant.One);
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_exponential()
        {
            // arrange
            var operand = Exponential.Create(ValueConstant.One);
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_logarithm()
        {
            // arrange
            var operand = Logarithm.Create(ValueConstant.One);
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_logarithm_base()
        {
            // arrange
            var operand = LogarithmBase.Create(10, ValueConstant.One);
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_tangent()
        {
            // arrange
            var operand = Tangent.Create(ValueConstant.One);
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_root()
        {
            // arrange
            var operand = Root.Create(3, ValueConstant.Create(2));
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_factorial()
        {
            // arrange
            var operand = Factorial.Create(ValueConstant.Create(2));
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_minimum()
        {
            // arrange
            var operand = Minimum.Create(Variable.x, ValueConstant.Create(2));
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_maximum()
        {
            // arrange
            var operand = Maximum.Create(Variable.x, ValueConstant.Create(2));
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_function()
        {
            // arrange
            var operand = Function.Create("f", ValueConstant.Create(2));
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(2);
        }

        [Fact]
        public void Should_return_function_reference()
        {
            // arrange
            var operand = FunctionReference.Create("f", ValueConstant.Create(2));
            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(1);
        }

        [Fact]
        public void Should_return_complex_function()
        {
            // arrange
            Operand operand = "sin(x+2/cos(x^2))";
            /*
                Sine
                    Addition
                        Variable (x)
                        Division
                            Constant (2)
                            Cosine
                                Power
                                    Variable (x)
                                    Constant (2)
             */

            var visitor = new DepthOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(6);
        }
    }
}