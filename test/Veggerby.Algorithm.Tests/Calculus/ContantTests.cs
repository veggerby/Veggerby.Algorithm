using System;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class ConstantTests
    {
        public class ctor
        {
            [Fact]
            public void Should_initialize_from_constructor()
            {
                // arrange
                
                // act
                var actual = Constant.Create(123);
                
                // assert
                actual.Value.ShouldBe(123);
            }
        }

        public class StaticValues
        {
            [Fact]
            public void Should_have_zero_static()
            {
                // arrange
                // act
                // assert
                Constant.Zero.Value.ShouldBe(0);
            }

            [Fact]
            public void Should_have_one_static()
            {
                // arrange
                // act
                // assert
                Constant.One.Value.ShouldBe(1);
            }

            [Fact]
            public void Should_have_pi_static()
            {
                // arrange
                // act
                // assert
                Constant.Pi.Symbol.ShouldBe("π");
                Constant.Pi.Value.ShouldBe(Math.PI);
            }

            [Fact]
            public void Should_have_e_static()
            {
                // arrange
                // act
                // assert
                Constant.e.Symbol.ShouldBe("e");
                Constant.e.Value.ShouldBe(Math.E);
            }
        }

        public class _Equals
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = Constant.Create(4);
                
                // act
                var actual = v.Equals(v);

                // assert
                actual.ShouldBeTrue();
            }
            
            [Fact]
            public void Should_not_equal_null()
            {
                // arrange
                var v = Constant.Create(3);
                
                // act
                var actual = v.Equals(null);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_equal_same_operands()
            {
                // arrange
                var v1 = Constant.Create(3);
                var v2 = Constant.Create(3);
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_different_operands()
            {
                // arrange
                var v1 = Constant.One;
                var v2 = Constant.Create(3);
                
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
                var func = Constant.Create(12);
                var expected = Constant.Zero;

                // act
                var actual = func.GetDerivative(Variable.x);

                // assert
                actual.ShouldBe(expected);
            }
        }
    }
}