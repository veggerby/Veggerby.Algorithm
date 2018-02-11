using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class MinimumTests
    {
        [Fact]
        public void Should_initialize()
        {
            var actual = (Minimum)Minimum.Create(Constant.One, Variable.x);

            // assert
            actual.Operands.ShouldBe(new Operand[] { Constant.One, Variable.x });
        }

        [Fact]
        public void Should_equal_self()
        {
            // arrange
            var v = Minimum.Create(Variable.x, Constant.Create(3));

            // act
            var actual = v.Equals(v);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_null()
        {
            // arrange
            var v = Minimum.Create(Variable.x, Constant.Create(3));

            // act
            var actual = v.Equals(null);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_equal_same_operands()
        {
            // arrange
            var v1 = Minimum.Create(Variable.x, Constant.Create(3));
            var v2 = Minimum.Create(Variable.x, Constant.Create(3));

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_different_operands()
        {
            // arrange
            var v1 = Minimum.Create(Variable.x, Constant.Create(3));
            var v2 = Minimum.Create(Constant.Create(2), Variable.y);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_equal_mirrored_operands()
        {
            // arrange
            var v1 = Minimum.Create(Variable.x, Constant.Create(3));
            var v2 = Minimum.Create(Constant.Create(3), Variable.x);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_different_operation_identical_operands()
        {
            // arrange
            var v1 = Minimum.Create(Variable.x, Constant.Create(3));
            var v2 = Subtraction.Create(Variable.x, Constant.Create(3));

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }
    }
}