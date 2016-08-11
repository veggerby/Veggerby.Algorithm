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
    }
}