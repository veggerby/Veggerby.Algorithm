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
        }

        public class _ToString
        {
            [Fact]
            public void Should_return_correct_string()
            {
                // arrange
                var v = Addition.Create(Constant.Create(4), Variable.x);
                
                // act
                var actual = @v.ToString();

                // assert
                actual.ShouldBe("4+x");
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

        public class GetDerivative
        {
            [Fact]
            public void Should_get_derivative()
            {
                // arrange
                var func = Addition.Create(Power.Create(Variable.x, 2), Variable.x);
                var expected = Addition.Create(Multiplication.Create(2, Variable.x), 1);

                // act
                var actual = func.GetDerivative(Variable.x);

                // assert
                actual.ShouldBe(expected);
            }
        }
    }
}