using Shouldly;
using Veggerby.Algorithm.Arithmetic;
using Xunit;

namespace Veggerby.Algorithm.Tests.Arithmetic
{
    public class AdditionTests
    {
        public class ctor
        {
            [Fact]
            public void Should_initialize_from_constructor()
            {
                // arrange
                
                // act
                var actual = new Addition(new Constant(1), new Constant(3));
                
                // assert
                (actual.Left as Constant).Value.ShouldBe(1);
                (actual.Right as Constant).Value.ShouldBe(3);
            }
        }

        public class Evaluate
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var @add = new Addition(new Constant(1), new Constant(3));
                
                // act
                var actual = @add.Evaluate(new OperationContext());

                // assert
                actual.ShouldBe(4);
            }
        }

        public class _ToString
        {
            [Fact]
            public void Should_return_correct_string()
            {
                // arrange
                var @add = new Addition(new Constant(1), new Constant(3));
                
                // act
                var actual = @add.ToString();

                // assert
                actual.ShouldBe("1+3");
            }
        }
    }
}