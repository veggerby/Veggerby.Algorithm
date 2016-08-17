using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class OperandTests
    {
        public class OperatorAdd
        {
            [Fact]
            public void Should_return_addition()
            {
                // arrange
                var left = new Constant(3);
                var right = new Variable("x");

                // act
                var actual = left + right;
                
                // assert
                actual.ShouldBeOfType<Addition>();
                ((BinaryOperation)actual).Left.ShouldBe(left);
                ((BinaryOperation)actual).Right.ShouldBe(right);
            }

            [Fact]
            public void Should_return_constant_from_addition()
            {
                // arrange
                var left = new Constant(3);
                var right = new Constant(6);

                // act
                var actual = left + right;
                
                // assert
                actual.ShouldBe(new Constant(9));
            }

            [Fact]
            public void Should_return_self_times_2()
            {
                // arrange
                var left = new Variable("x");
                var right = new Variable("x");

                // act
                var actual = left + right;
                
                // assert
                actual.ShouldBeOfType<Multiplication>();
                ((BinaryOperation)actual).Left.ShouldBe(new Constant(2));
                ((BinaryOperation)actual).Right.ShouldBe(right);
            }
        }

        public class OperatorSubtract
        {
            [Fact]
            public void Should_return_subtraction()
            {
                // arrange
                var left = new Constant(3);
                var right = new Variable("x");

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
                var left = new Constant(3);
                var right = new Constant(6);

                // act
                var actual = left - right;
                
                // assert
                actual.ShouldBe(new Constant(-3));
            }

            [Fact]
            public void Should_return_zero()
            {
                // arrange
                var left = new Variable("x");
                var right = new Variable("x");

                // act
                var actual = left - right;
                
                // assert
                actual.ShouldBe(new Constant(0));
            }
        }

        public class OperatorMultiplication
        {
            [Fact]
            public void Should_return_multiplication()
            {
                // arrange
                var left = new Constant(3);
                var right = new Variable("x");

                // act
                var actual = left * right;
                
                // assert
                actual.ShouldBeOfType<Multiplication>();
                ((BinaryOperation)actual).Left.ShouldBe(left);
                ((BinaryOperation)actual).Right.ShouldBe(right);
            }

            [Fact]
            public void Should_return_constant_from_multiplication()
            {
                // arrange
                var left = new Constant(3);
                var right = new Constant(6);

                // act
                var actual = left * right;
                
                // assert
                actual.ShouldBe(new Constant(18));
            }

            [Fact]
            public void Should_return_self_squared()
            {
                // arrange
                var left = new Variable("x");
                var right = new Variable("x");

                // act
                var actual = left * right;
                
                // assert
                actual.ShouldBeOfType<Power>();
                ((BinaryOperation)actual).Left.ShouldBe(right);
                ((BinaryOperation)actual).Right.ShouldBe(new Constant(2));
            }
        }

        public class OperatorDivision
        {
            [Fact]
            public void Should_return_division()
            {
                // arrange
                var left = new Constant(3);
                var right = new Variable("x");

                // act
                var actual = left / right;
                
                // assert
                actual.ShouldBeOfType<Division>();
                ((BinaryOperation)actual).Left.ShouldBe(left);
                ((BinaryOperation)actual).Right.ShouldBe(right);
            }


            [Fact]
            public void Should_return_constant_from_division()
            {
                // arrange
                var left = new Constant(6);
                var right = new Constant(3);

                // act
                var actual = left / right;
                
                // assert
                actual.ShouldBe(new Constant(2));
            }

            [Fact]
            public void Should_return_zero()
            {
                // arrange
                var left = new Variable("x");
                var right = new Variable("x");

                // act
                var actual = left / right;
                
                // assert
                actual.ShouldBe(new Constant(1));
            }
        }

        public class OperatorPower
        {
            [Fact]
            public void Should_return_power()
            {
                // arrange
                var left = new Constant(3);
                var right = new Variable("x");

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
                var left = new Constant(2);
                var right = new Constant(3);

                // act
                var actual = left ^ right;
                
                // assert
                actual.ShouldBe(new Constant(8));
            }

            [Fact]
            public void Should_return_one_for_one_as_base()
            {
                // arrange
                var left = new Constant(1);
                var right = new Variable("x");

                // act
                var actual = left ^ right;
                
                // assert
                actual.ShouldBe(new Constant(1));
            }

            [Fact]
            public void Should_return_one_for_zero_as_exponent()
            {
                // arrange
                var left = new Variable("x");
                var right = new Constant(0);

                // act
                var actual = left ^ right;
                
                // assert
                actual.ShouldBe(new Constant(1));
            }


            [Fact]
            public void Should_return_baze_for_one_as_exponent()
            {
                // arrange
                var left = new Variable("x");
                var right = new Constant(1);

                // act
                var actual = left ^ right;
                
                // assert
                actual.ShouldBe(left);
            }
        }

        public class Implicit
        {
            [Fact]
            public void Should_create_constant_from_int()
            {
                // arrange
                
                // act
                var actual = (Constant)3;
                
                // assert
                actual.Value.ShouldBe(3);
            }

            [Fact]
            public void Should_create_constant_from_double()
            {
                // arrange
                
                // act
                var actual = (Constant)3.4;
                
                // assert
                actual.Value.ShouldBe(3.4);
            }
        }
    }
}