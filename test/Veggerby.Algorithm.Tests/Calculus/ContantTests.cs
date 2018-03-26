using System;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class ConstantTests
    {
        [Fact]
        public void Should_initialize_from_constructor()
        {
            // arrange

            // act
            var actual = ValueConstant.Create(123);

            // assert
            actual.Value.ShouldBe(123);
        }

        [Fact]
        public void Should_have_zero_static()
        {
            // arrange
            // act
            // assert
            ValueConstant.Zero.Value.ShouldBe(0);
        }

        [Fact]
        public void Should_have_one_static()
        {
            // arrange
            // act
            // assert
            ValueConstant.One.Value.ShouldBe(1);
        }

        [Fact]
        public void Should_have_pi_static()
        {
            // arrange
            // act
            // assert
            ValueConstant.Pi.Symbol.ShouldBe("π");
            ValueConstant.Pi.Value.ShouldBe(Math.PI);
        }

        [Fact]
        public void Should_have_e_static()
        {
            // arrange
            // act
            // assert
            ValueConstant.e.Symbol.ShouldBe("e");
            ValueConstant.e.Value.ShouldBe(Math.E);
        }

        [Fact]
        public void Should_equal_self()
        {
            // arrange
            var v = ValueConstant.Create(4);

            // act
            var actual = v.Equals(v);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_null()
        {
            // arrange
            var v = ValueConstant.Create(3);

            // act
            var actual = v.Equals(null);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_equal_same_operands()
        {
            // arrange
            var v1 = ValueConstant.Create(3);
            var v2 = ValueConstant.Create(3);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_different_operands()
        {
            // arrange
            var v1 = ValueConstant.One;
            var v2 = ValueConstant.Create(3);

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }
    }
}