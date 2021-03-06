using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class AdditionTests
    {
        [Fact]
        public void Should_create()
        {
            var actual = (Addition)Addition.Create(ValueConstant.One, Variable.x);

            // assert
            actual.Operands.ShouldBe(new Operand[] { ValueConstant.One, Variable.x });
        }

        [Fact]
        public void Should_equal_self()
        {
            // arrange
            var v = Addition.Create(ValueConstant.One, Variable.x);

            // act
            var actual = v.Equals(v);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_null()
        {
            // arrange
            var v = Addition.Create(ValueConstant.One, Variable.x);

            // act
            var actual = v.Equals(null);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_equal_same_operands()
        {
            // arrange
            var v1 = Addition.Create(ValueConstant.One, Variable.x);
            var v2 = Addition.Create(ValueConstant.One, Variable.x);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_different_operands()
        {
            // arrange
            var v1 = Addition.Create(ValueConstant.One, Variable.x);
            var v2 = Addition.Create(Variable.y, ValueConstant.Create(2));

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_equal_mirrored_operands()
        {
            // arrange
            var v1 = Addition.Create(ValueConstant.One, Variable.x);
            var v2 = Addition.Create(Variable.x, ValueConstant.One);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_different_operation_identical_operands()
        {
            // arrange
            var v1 = Addition.Create(ValueConstant.One, Variable.x);
            var v2 = Subtraction.Create(ValueConstant.One, Variable.x);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }
    }
}