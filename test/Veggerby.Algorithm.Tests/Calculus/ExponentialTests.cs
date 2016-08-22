using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class ExponentialTests
    {
        public class ctor
        {
            [Fact]
            public void Should_initialize_from_constructor()
            {
                // arrange
                
                // act
                var actual = (Exponential)Exponential.Create(Variable.x);
                
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
                var v = Exponential.Create(2);
                
                // act
                var actual = v.Equals(v);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_null()
            {
                // arrange
                var v = Exponential.Create(2);
                
                // act
                var actual = v.Equals(null);

                // assert
                actual.ShouldBeFalse();
            }
            
            [Fact]
            public void Should_equal_same_operands()
            {
                // arrange
                var v1 = Exponential.Create(2);
                var v2 = Exponential.Create(2);
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_different_operands()
            {
                // arrange
                var v1 = Exponential.Create(2);
                var v2 = Exponential.Create(Variable.x);
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_not_equal_different_operation_identical_operands()
            {
                // arrange
                var v1 = Exponential.Create(Variable.x);
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
                var func = Exponential.Create(Variable.x);
                var expected = Exponential.Create(Variable.x);

                // act
                var actual = func.GetDerivative(Variable.x);

                // assert
                actual.ShouldBe(expected);
            }

            [Fact]
            public void Should_get_complex_derivative()
            {
                // arrange
                var func = Exponential.Create(
                    Division.Create(
                        Multiplication.Create(2, Variable.x),
                        Sine.Create(2 * Constant.Pi / Variable.x)));

                Operand expected = "(2*sin((2*π)/x)+(2*π)/x^2*cos((2*π)/x)*2*x)/sin((2*π)/x)^2*exp((2*x)/sin((2*π)/x))";

                // act
                var actual = func.GetDerivative(Variable.x);

                // assert
                actual.ShouldBe(expected);
            }
        }
    }
}