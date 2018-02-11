using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class MultiplicationTests
    {
        [Fact]
        public void Should_initialize()
        {
            var actual = (Multiplication)Multiplication.Create(Constant.Create(3), Variable.x);

            // assert
            actual.Operands.ShouldBe(new Operand[] { Constant.Create(3), Variable.x });
        }

        [Fact]
        public void Should_equal_self()
        {
            // arrange
            var v = Multiplication.Create(Constant.Create(3), Variable.x);

            // act
            var actual = v.Equals(v);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_null()
        {
            // arrange
            var v = Multiplication.Create(Constant.Create(3), Variable.x);

            // act
            var actual = v.Equals(null);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_equal_same_operands()
        {
            // arrange
            var v1 = Multiplication.Create(Constant.Create(3), Variable.x);
            var v2 = Multiplication.Create(Constant.Create(3), Variable.x);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_different_operands()
        {
            // arrange
            var v1 = Multiplication.Create(Constant.Create(3), Variable.x);
            var v2 = Multiplication.Create(Variable.y, Constant.Create(2));

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_equal_mirrored_operands()
        {
            // arrange
            var v1 = Multiplication.Create(Constant.Create(3), Variable.x);
            var v2 = Multiplication.Create(Variable.x, Constant.Create(3));

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_different_operation_identical_operands()
        {
            // arrange
            var v1 = Multiplication.Create(Constant.Create(3), Variable.x);
            var v2 = Subtraction.Create(Constant.Create(3), Variable.x);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_equal_commutative()
        {
            // arrange
            var v1 = Multiplication.Create(
                    Variable.x,
                    Multiplication.Create(
                        Constant.Pi,
                        Sine.Create(Variable.x)
                    ));

            var v2 = Multiplication.Create(
                    Sine.Create(Variable.x),
                    Multiplication.Create(
                        Variable.x,
                        Constant.Pi
                    ));

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeTrue();
        }
    }
}