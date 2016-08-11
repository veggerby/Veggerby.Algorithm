using Shouldly;
using Veggerby.Algorithm.Arithmetic;
using Xunit;

namespace Veggerby.Algorithm.Tests.Arithmetic
{
    public class MultiplicationTests
    {
        public class ctor
        {
            [Fact]
            public void Should_initialize_from_constructor()
            {
                // arrange
                
                // act
                var actual = new Multiplication(new Constant(4), new Constant(2));
                
                // assert
                (actual.Left as Constant).Value.ShouldBe(4);
                (actual.Right as Constant).Value.ShouldBe(2);
            }
        }

        public class Evaluate
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var v = new Multiplication(new Constant(4), new Constant(2));
                
                // act
                var actual = v.Evaluate(new OperationContext());

                // assert
                actual.ShouldBe(8);
            }
        }

        public class _ToString
        {
            [Fact]
            public void Should_return_correct_string()
            {
                // arrange
                var v = new Multiplication(new Constant(4), new Constant(2));
                
                // act
                var actual = @v.ToString();

                // assert
                actual.ShouldBe("4*2");
            }
        }
    }
}