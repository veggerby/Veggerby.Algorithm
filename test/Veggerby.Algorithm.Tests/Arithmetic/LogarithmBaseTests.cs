using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class LogarithmBaseTests
    {
        public class ctor
        {
            [Fact]
            public void Should_initialize_from_constructor()
            {
                // arrange
                
                // act
                var actual = new LogarithmBase(1, new Variable("x"));
                
                // assert
                actual.Inner.ShouldBe(new Variable("x"));
            }
        }

        public class Evaluate
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var v = new LogarithmBase(10, 1000);
                
                // act
                var actual = v.Evaluate(new OperationContext());

                // assert
                actual.ShouldBe(3, 1e-15); // if no tolerance 3d should be equal to 3d :/
            }
        }

        public class _ToString
        {
            [Fact]
            public void Should_return_correct_string_non_10_base()
            {
                // arrange
                var v = new LogarithmBase(2, new Variable("x"));
                
                // act
                var actual = @v.ToString();

                // assert
                actual.ShouldBe("log2(x)");
            }
            
            [Fact]
            public void Should_return_correct_string_base10()
            {
                // arrange
                var v = new LogarithmBase(10, new Variable("x"));
                
                // act
                var actual = @v.ToString();

                // assert
                actual.ShouldBe("log(x)");
            }
        }

        public class _Equals
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = new LogarithmBase(10, new Variable("x"));
                
                // act
                var actual = v.Equals(v);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_null()
            {
                // arrange
                var v = new LogarithmBase(10, new Variable("x"));
                
                // act
                var actual = v.Equals(null);

                // assert
                actual.ShouldBeFalse();
            }
            
            [Fact]
            public void Should_equal_same_operands()
            {
                // arrange
                var v1 = new LogarithmBase(10, new Variable("x"));
                var v2 = new LogarithmBase(10, new Variable("x"));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_different_operands()
            {
                // arrange
                var v1 = new LogarithmBase(10, new Variable("x"));
                var v2 = new LogarithmBase(10, 2);
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_not_equal_different_base()
            {
                // arrange
                var v1 = new LogarithmBase(10, new Variable("x"));
                var v2 = new LogarithmBase(2, new Variable("x"));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_not_equal_different_operation_identical_operands()
            {
                // arrange
                var v1 = new LogarithmBase(10, new Variable("x"));
                var v2 = new Sine(new Variable("x"));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }
        }
    }
}