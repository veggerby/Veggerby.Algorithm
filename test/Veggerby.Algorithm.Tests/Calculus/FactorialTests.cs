using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class FactorialTests
    {
        [Fact]
        public void Should_initialize_from_constructor()
        {
            // arrange

            // act
            var actual = (Factorial)Factorial.Create(Variable.x);

            // assert
            actual.Inner.ShouldBe(Variable.x);
        }

        [Fact]
        public void Should_equal_self()
        {
            // arrange
            var v = Factorial.Create(ValueConstant.Create(4));

            // act
            var actual = v.Equals(v);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_null()
        {
            // arrange
            var v = Factorial.Create(ValueConstant.Create(4));

            // act
            var actual = v.Equals(null);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_equal_same_operands()
        {
            // arrange
            var v1 = Factorial.Create(ValueConstant.Create(4));
            var v2 = Factorial.Create(ValueConstant.Create(4));

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_different_operands()
        {
            // arrange
            var v1 = Factorial.Create(ValueConstant.Create(4));
            var v2 = Factorial.Create(Variable.x);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_not_equal_different_operation_identical_operands()
        {
            // arrange
            var v1 = Factorial.Create(Variable.x);
            var v2 = Sine.Create(Variable.x);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }
    }
}