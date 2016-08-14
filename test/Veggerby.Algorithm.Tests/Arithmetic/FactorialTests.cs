using Shouldly;
using Veggerby.Algorithm.Arithmetic;
using Xunit;

namespace Veggerby.Algorithm.Tests.Arithmetic
{
    public class FactorialTests
    {
        public class ctor
        {
            [Fact]
            public void Should_initialize_from_constructor()
            {
                // arrange
                
                // act
                var actual = new Factorial(new Constant(4));
                
                // assert
                (actual.Inner as Constant).Value.ShouldBe(4);
            }
        }

        public class Evaluate
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var v = new Factorial(new Constant(4));
                
                // act
                var actual = v.Evaluate(new OperationContext());

                // assert
                actual.ShouldBe(24);
            }
        }

        public class _ToString
        {
            [Fact]
            public void Should_return_correct_string()
            {
                // arrange
                var v = new Factorial(new Constant(4));
                
                // act
                var actual = @v.ToString();

                // assert
                actual.ShouldBe("4!");
            }
        }

        public class _Equals
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = new Factorial(new Constant(4));
                
                // act
                var actual = v.Equals(v);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_null()
            {
                // arrange
                var v = new Factorial(new Constant(4));
                
                // act
                var actual = v.Equals(null);

                // assert
                actual.ShouldBeFalse();
            }
            
            [Fact]
            public void Should_equal_same_operands()
            {
                // arrange
                var v1 = new Factorial(new Constant(4));
                var v2 = new Factorial(new Constant(4));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_different_operands()
            {
                // arrange
                var v1 = new Factorial(new Constant(4));
                var v2 = new Factorial(new Variable("x"));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_not_equal_different_operation_identical_operands()
            {
                // arrange
                var v1 = new Factorial(new Variable("x"));
                var v2 = new Sine(new Variable("x"));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }
        }
    }
}