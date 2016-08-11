using Shouldly;
using Veggerby.Algorithm.Arithmetic;
using Xunit;

namespace Veggerby.Algorithm.Tests.Arithmetic
{
    public class NamedConstantTests
    {
        public class ctor
        {
            [Fact]
            public void Should_initialize_from_constructor()
            {
                // arrange
                
                // act
                var actual = new NamedConstant("p", 1);
                
                // assert
                actual.Symbol.ShouldBe("p");
                actual.Value.ShouldBe(1);
            }
        }

        public class Evaluate
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var constant = new NamedConstant("a", 3);
                
                // act
                var actual = constant.Evaluate(new OperationContext());

                // assert
                actual.ShouldBe(3);
            }
        }

        public class _ToString
        {
            [Fact]
            public void Should_return_correct_string()
            {
                // arrange
                var constant = new NamedConstant("a", 3);
                
                // act
                var actual = constant.ToString();

                // assert
                actual.ShouldBe("a");
            }
        }

        public class _Equals
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = new NamedConstant("a", 4);
                
                // act
                var actual = v.Equals(v);

                // assert
                actual.ShouldBeTrue();
            }
            
            [Fact]
            public void Should_not_equal_null()
            {
                // arrange
                var v = new NamedConstant("a", 4);
                
                // act
                var actual = v.Equals(null);

                // assert
                actual.ShouldBeFalse();
            }

            [Theory]
            [InlineData("a", 1, "a", 3, false)]
            [InlineData("a", 1, "a", 1, true)]
            [InlineData("a", 1, "b", 3, false)]
            [InlineData("a", 1, "b", 1, false)]
            public void Should_not_equal_different_operands(string symbol1, double value1, string symbol2, double value2, bool expected)
            {
                // arrange
                var v1 = new NamedConstant(symbol1, value1);
                var v2 = new NamedConstant(symbol2, value2);
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBe(expected);
            }
        }
    }
}