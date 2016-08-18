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
                var actual = new Constant(1);
                
                // assert
                actual.Value.ShouldBe(1);
            }
        }

        public class StaticValues
        {
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

        public class Evaluate
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var constant = new Constant(3);
                
                // act
                var actual = constant.Evaluate(new OperationContext());

                // assert
                actual.ShouldBe(3);
            }
        }

        public class _ToString
        {
            [Theory]
            [InlineData(1, "1")]
            [InlineData(3.2, "3.2")]
            [InlineData(3.0000001, "3.0000001")]
            [InlineData(3.000000, "3")]
            public void Should_return_correct_string(double value, string expected)
            {
                // arrange
                var constant = new Constant(value);
                
                // act
                var actual = constant.ToString();

                // assert
                actual.ShouldBe(expected);
            }
        }

        public class _Equals
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = new Constant(4);
                
                // act
                var actual = v.Equals(v);

                // assert
                actual.ShouldBeTrue();
            }
            
            [Fact]
            public void Should_not_equal_null()
            {
                // arrange
                var v = new Constant(3);
                
                // act
                var actual = v.Equals(null);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_equal_same_operands()
            {
                // arrange
                var v1 = new Constant(3);
                var v2 = new Constant(3);
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_different_operands()
            {
                // arrange
                var v1 = new Constant(1);
                var v2 = new Constant(3);
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }
        }
    }
}