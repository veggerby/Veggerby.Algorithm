using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class AdditionTests
    {
        public class Create
        {
            [Fact]
            public void Should_initialize()
            {
                var actual = (Addition)Addition.Create(Constant.One, Variable.x);

                // assert
                actual.Left.ShouldBe(Constant.One);
                actual.Right.ShouldBe(Variable.x);
            }

            [Fact]
            public void Should_collapse()
            {
                // arrange

                // act
                var actual = Addition.Create(Constant.One, Constant.Create(3));

                // assert
                actual.ShouldBe(Constant.Create(4));
            }

            [Fact]
            public void Should_collapse_flattened()
            {
                // arrange

                // act
                var actual = (Addition)Addition.Create(
                    Addition.Create(Constant.Create(7), Sine.Create(Variable.x)),
                    Addition.Create(Constant.Create(3), Sine.Create(Variable.x)));

                // assert
                actual.Left.ShouldBe(Constant.Create(10));
                actual.Right.ShouldBe(Multiplication.Create(2, Sine.Create(Variable.x)));
            }
        }

        public class _Equals
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = Addition.Create(Constant.One, Variable.x);

                // act
                var actual = v.Equals(v);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_null()
            {
                // arrange
                var v = Addition.Create(Constant.One, Variable.x);

                // act
                var actual = v.Equals(null);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_equal_same_operands()
            {
                // arrange
                var v1 = Addition.Create(Constant.One, Variable.x);
                var v2 = Addition.Create(Constant.One, Variable.x);

                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_different_operands()
            {
                // arrange
                var v1 = Addition.Create(Constant.One, Variable.x);
                var v2 = Addition.Create(Variable.y, Constant.Create(2));

                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_equal_mirrored_operands()
            {
                // arrange
                var v1 = Addition.Create(Constant.One, Variable.x);
                var v2 = Addition.Create(Variable.x, Constant.One);

                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_different_operation_identical_operands()
            {
                // arrange
                var v1 = Addition.Create(Constant.One, Variable.x);
                var v2 = Subtraction.Create(Constant.One, Variable.x);

                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }
        }
    }
}