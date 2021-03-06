using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class LogarithmBaseTests
    {
        [Fact]
        public void Should_initialize_from_constructor()
        {
            // arrange

            // act
            var actual = (LogarithmBase)LogarithmBase.Create(10, Variable.x);

            // assert
            actual.Base.ShouldBe(10);
            actual.Inner.ShouldBe(Variable.x);
        }

        [Fact]
        public void Should_equal_self()
        {
            // arrange
            var v = LogarithmBase.Create(10, Variable.x);

            // act
            var actual = v.Equals(v);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_null()
        {
            // arrange
            var v = LogarithmBase.Create(10, Variable.x);

            // act
            var actual = v.Equals(null);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_equal_same_operands()
        {
            // arrange
            var v1 = LogarithmBase.Create(10, Variable.x);
            var v2 = LogarithmBase.Create(10, Variable.x);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_different_operands()
        {
            // arrange
            var v1 = LogarithmBase.Create(10, Variable.x);
            var v2 = LogarithmBase.Create(10, 2);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_not_equal_different_base()
        {
            // arrange
            var v1 = LogarithmBase.Create(10, Variable.x);
            var v2 = LogarithmBase.Create(2, Variable.x);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_not_equal_different_operation_identical_operands()
        {
            // arrange
            var v1 = LogarithmBase.Create(10, Variable.x);
            var v2 = Sine.Create(Variable.x);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }
    }
}