using Shouldly;
using Veggerby.Algorithm.Arithmetic;
using Xunit;

namespace Veggerby.Algorithm.Tests.Arithmetic
{
    public class VariableTests
    {
        public class ctor
        {
            [Fact]
            public void Should_initialize_from_constructor()
            {
                // arrange
                
                // act
                var actual = new Variable("x");
                
                // assert
                actual.Identifier.ShouldBe("x");
            }
        }

        public class Evaluate
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var v = new Variable("x");
                var ctx = new OperationContext();
                ctx.Add("x", 2);

                // act
                var actual = v.Evaluate(ctx);

                // assert
                actual.ShouldBe(2);
            }
        }

        public class _ToString
        {
            [Fact]
            public void Should_return_correct_string()
            {
                // arrange
                var v = new Variable("x");
                
                // act
                var actual = @v.ToString();

                // assert
                actual.ShouldBe("x");
            }
        }

        public class _Equals
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = new Variable("x");
                
                // act
                var actual = v.Equals(v);

                // assert
                actual.ShouldBeTrue();
            }
            
            [Fact]
            public void Should_not_equal_null()
            {
                // arrange
                var v = new Variable("x");
                
                // act
                var actual = v.Equals(null);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_equal_same_operands()
            {
                // arrange
                var v1 = new Variable("x");
                var v2 = new Variable("x");
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_different_operands()
            {
                // arrange
                var v1 = new Variable("x");
                var v2 = new Variable("y");
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }
        }
    }
}