using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class LogarithmTests
    {
        public class ctor
        {
            [Fact]
            public void Should_initialize_from_constructor()
            {
                // arrange
                
                // act
                var actual = (Logarithm)Logarithm.Create(Variable.x);
                
                // assert
                actual.Inner.ShouldBe(Variable.x);
            }
        }

        public class Evaluate
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var v = Logarithm.Create(Constant.e);
                
                // act
                var actual = v.Evaluate(new OperationContext());

                // assert
                actual.ShouldBe(1);
            }
        }

        public class _ToString
        {
            [Fact]
            public void Should_return_correct_string()
            {
                // arrange
                var v = Logarithm.Create(2);
                
                // act
                var actual = @v.ToString();

                // assert
                actual.ShouldBe("ln(2)");
            }
        }

        public class _Equals
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = Logarithm.Create(2);
                
                // act
                var actual = v.Equals(v);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_null()
            {
                // arrange
                var v = Logarithm.Create(2);
                
                // act
                var actual = v.Equals(null);

                // assert
                actual.ShouldBeFalse();
            }
            
            [Fact]
            public void Should_equal_same_operands()
            {
                // arrange
                var v1 = Logarithm.Create(2);
                var v2 = Logarithm.Create(2);
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_different_operands()
            {
                // arrange
                var v1 = Logarithm.Create(2);
                var v2 = Logarithm.Create(Variable.x);
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_not_equal_different_operation_identical_operands()
            {
                // arrange
                var v1 = Logarithm.Create(Variable.x);
                var v2 = Sine.Create(Variable.x);
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }
        }
    }
}