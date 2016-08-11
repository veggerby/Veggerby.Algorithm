using Shouldly;
using Veggerby.Algorithm.Arithmetic;
using Xunit;

namespace Veggerby.Algorithm.Tests.Arithmetic
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
    }
}