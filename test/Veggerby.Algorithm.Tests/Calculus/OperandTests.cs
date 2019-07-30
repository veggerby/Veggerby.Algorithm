using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class OperandTests
    {
        [Fact]
        public void Should_return_addition()
        {
            // arrange
            var left = ValueConstant.Create(3);
            var right = Variable.x;

            // act
            var actual = left + right;

            // assert
            actual.ShouldBeOfType<Addition>();
            ((Addition)actual).Operands.ShouldBe(new Operand[] { left, right });
        }

        [Fact]
        public void Should_return_constant_from_addition()
        {
            // arrange
            var left = ValueConstant.Create(3);
            var right = ValueConstant.Create(6);

            // act
            var actual = left + right;

            // assert
            actual.ShouldBe(ValueConstant.Create(9));
        }

        [Fact]
        public void Should_return_subtraction()
        {
            // arrange
            var left = ValueConstant.Create(3);
            var right = Variable.x;

            // act
            var actual = left - right;

            // assert
            actual.ShouldBeOfType<Subtraction>();
            ((BinaryOperation)actual).Left.ShouldBe(left);
            ((BinaryOperation)actual).Right.ShouldBe(right);
        }

        [Fact]
        public void Should_return_constant_from_subtraction()
        {
            // arrange
            var left = ValueConstant.Create(3);
            var right = ValueConstant.Create(6);

            // act
            var actual = left - right;

            // assert
            actual.ShouldBe(ValueConstant.Create(-3));
        }

        [Fact]
        public void Should_return_multiplication()
        {
            // arrange
            var left = ValueConstant.Create(3);
            var right = Variable.x;

            // act
            var actual = left * right;

            // assert
            actual.ShouldBeOfType<Multiplication>();
            ((Multiplication)actual).Operands.ShouldBe(new Operand[] { left, right });
        }

        [Fact]
        public void Should_return_constant_from_multiplication()
        {
            // arrange
            var left = ValueConstant.Create(3);
            var right = ValueConstant.Create(6);

            // act
            var actual = left * right;

            // assert
            actual.ShouldBe(ValueConstant.Create(18));
        }

        [Fact]
        public void Should_return_division()
        {
            // arrange
            var left = ValueConstant.Create(3);
            var right = Variable.x;

            // act
            var actual = left / right;

            // assert
            actual.ShouldBeOfType<Division>();
            ((BinaryOperation)actual).Left.ShouldBe(left);
            ((BinaryOperation)actual).Right.ShouldBe(right);
        }

        [Fact]
        public void Should_return_fraction_from_constants_division()
        {
            // arrange
            var left = ValueConstant.Create(1);
            var right = ValueConstant.Create(3);

            // act
            var actual = Division.Create(left, right);

            // assert
            actual.ShouldBe(Fraction.Create(1, 3));
        }

        [Fact]
        public void Should_return_power()
        {
            // arrange
            var left = ValueConstant.Create(3);
            var right = Variable.x;

            // act
            var actual = left ^ right;

            // assert
            actual.ShouldBeOfType<Power>();
            ((BinaryOperation)actual).Left.ShouldBe(left);
            ((BinaryOperation)actual).Right.ShouldBe(right);
        }

        [Fact]
        public void Should_return_constant_from_power()
        {
            // arrange
            var left = ValueConstant.Create(2);
            var right = ValueConstant.Create(3);

            // act
            var actual = left ^ right;

            // assert
            actual.ShouldBe(ValueConstant.Create(8));
        }

        [Fact]
        public void Should_return_one_for_one_as_base()
        {
            // arrange
            var left = ValueConstant.One;
            var right = Variable.x;

            // act
            var actual = left ^ right;

            // assert
            actual.ShouldBe(ValueConstant.One);
        }

        [Fact]
        public void Should_return_one_for_zero_as_exponent()
        {
            // arrange
            var left = Variable.x;
            var right = ValueConstant.Zero;

            // act
            var actual = left ^ right;

            // assert
            actual.ShouldBe(ValueConstant.One);
        }


        [Fact]
        public void Should_return_baze_for_one_as_exponent()
        {
            // arrange
            var left = Variable.x;
            var right = ValueConstant.One;

            // act
            var actual = left ^ right;

            // assert
            actual.ShouldBe(left);
        }

        [Fact]
        public void Should_create_constant_from_int()
        {
            // arrange

            // act
            var actual = (ValueConstant)3;

            // assert
            actual.Value.ShouldBe(3);
        }

        [Fact]
        public void Should_create_constant_from_double()
        {
            // arrange

            // act
            var actual = (ValueConstant)3.4;

            // assert
            actual.Value.ShouldBe(3.4);
        }
    }
}