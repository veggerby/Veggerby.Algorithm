using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class DivisionTests
    {
        [Fact]
        public void Should_initialize()
        {
            var actual = (Division)Division.Create(Constant.One, Variable.x);

            // assert
            actual.Left.ShouldBe(Constant.One);
            actual.Right.ShouldBe(Variable.x);
        }

        [Fact]
        public void Should_equal_self()
        {
            // arrange
            var v = Division.Create(Constant.One, Constant.Create(3));

            // act
            var actual = v.Equals(v);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_null()
        {
            // arrange
            var v = Division.Create(Constant.One, Constant.Create(3));

            // act
            var actual = v.Equals(null);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_equal_same_operands()
        {
            // arrange
            var v1 = Division.Create(Constant.One, Constant.Create(3));
            var v2 = Division.Create(Constant.One, Constant.Create(3));

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_different_operands()
        {
            // arrange
            var v1 = Division.Create(Constant.One, Constant.Create(3));
            var v2 = Division.Create(Constant.Create(2), Constant.Create(2));

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_not_equal_mirrored_operands()
        {
            // arrange
            var v1 = Division.Create(Constant.One, Constant.Create(3));
            var v2 = Division.Create(Constant.Create(3), Constant.One);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_not_equal_different_operation_identical_operands()
        {
            // arrange
            var v1 = Division.Create(Constant.One, Constant.Create(3));
            var v2 = Subtraction.Create(Constant.One, Constant.Create(3));

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }
    }
}