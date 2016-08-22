using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
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
                var actual = Variable.Create("x");
                
                // assert
                actual.Identifier.ShouldBe("x");
            }
        }

        public class _ToString
        {
            [Fact]
            public void Should_return_correct_string()
            {
                // arrange
                var v = Variable.Create("x");
                
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
                var v = Variable.Create("x");
                
                // act
                var actual = v.Equals(v);

                // assert
                actual.ShouldBeTrue();
            }
            
            [Fact]
            public void Should_not_equal_null()
            {
                // arrange
                var v = Variable.Create("x");
                
                // act
                var actual = v.Equals(null);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_equal_same_operands()
            {
                // arrange
                var v1 = Variable.Create("x");
                var v2 = Variable.Create("x");
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_different_operands()
            {
                // arrange
                var v1 = Variable.Create("x");
                var v2 = Variable.Create("y");
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }
        }
    }
}