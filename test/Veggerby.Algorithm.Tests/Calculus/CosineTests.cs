using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class CosineTests
    {
        public class ctor
        {
            [Fact]
            public void Should_initialize_from_constructor()
            {
                // arrange
                
                // act
                var actual = (Cosine)Cosine.Create(Variable.x);
                
                // assert
                actual.Inner.ShouldBe(Variable.x);
            }
        }

        public class _Equals
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = Cosine.Create(Constant.Pi);
                
                // act
                var actual = v.Equals(v);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_null()
            {
                // arrange
                var v = Cosine.Create(Constant.Pi);
                
                // act
                var actual = v.Equals(null);

                // assert
                actual.ShouldBeFalse();
            }
            
            [Fact]
            public void Should_equal_same_operands()
            {
                // arrange
                var v1 = Cosine.Create(Constant.Pi);
                var v2 = Cosine.Create(Constant.Pi);
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_different_operands()
            {
                // arrange
                var v1 = Cosine.Create(Constant.Pi);
                var v2 = Cosine.Create(Variable.x);
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_not_equal_different_operation_identical_operands()
            {
                // arrange
                var v1 = Cosine.Create(Variable.x);
                var v2 = Sine.Create(Variable.x);
                
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
                var func = Sine.Create(Variable.x);
                var expected = Cosine.Create(Variable.x);

                // act
                var actual = func.GetDerivative(Variable.x);

                // assert
                actual.ShouldBe(expected);
            }
        }
    }
}