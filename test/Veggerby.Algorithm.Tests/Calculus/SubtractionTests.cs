using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class SubtractionTests
    {
        [Fact]
        public void Should_initialize()
        {
            var actual = (Subtraction)Subtraction.Create(Constant.One, Variable.x);

            // assert
            actual.Left.ShouldBe(Constant.One);
            actual.Right.ShouldBe(Variable.x);
        }

        [Fact]
        public void Should_equal_self()
        {
            // arrange
            var v = Subtraction.Create(Constant.One, Variable.x);

            // act
            var actual = v.Equals(v);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_null()
        {
            // arrange
            var v = Subtraction.Create(Constant.One, Variable.x);

            // act
            var actual = v.Equals(null);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_equal_same_operands()
        {
            // arrange
            var v1 = Subtraction.Create(Constant.One, Variable.x);
            var v2 = Subtraction.Create(Constant.One, Variable.x);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_different_operands()
        {
            // arrange
            var v1 = Subtraction.Create(Constant.One, Variable.x);
            var v2 = Subtraction.Create(Variable.y, Constant.Create(2));

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_not_equal_mirrored_operands()
        {
            // arrange
            var v1 = Subtraction.Create(Constant.One, Variable.x);
            var v2 = Subtraction.Create(Variable.x, Constant.One);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_not_equal_different_operation_identical_operands()
        {
            // arrange
            var v1 = Subtraction.Create(Constant.One, Variable.x);
            var v2 = Addition.Create(Constant.One, Variable.x);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }
    }
}