using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
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

        public class _Equals
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = new Addition(new Constant(1), new Constant(3));
                
                // act
                var actual = v.Equals(v);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_null()
            {
                // arrange
                var v = new Addition(new Constant(1), new Constant(3));
                
                // act
                var actual = v.Equals(null);

                // assert
                actual.ShouldBeFalse();
            }
            
            [Fact]
            public void Should_equal_same_operands()
            {
                // arrange
                var v1 = new Addition(new Constant(1), new Constant(3));
                var v2 = new Addition(new Constant(1), new Constant(3));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_different_operands()
            {
                // arrange
                var v1 = new Addition(new Constant(1), new Constant(3));
                var v2 = new Addition(new Constant(2), new Constant(2));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_not_equal_mirrored_operands()
            {
                // arrange
                var v1 = new Addition(new Constant(1), new Constant(3));
                var v2 = new Addition(new Constant(3), new Constant(1));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_not_equal_different_operation_identical_operands()
            {
                // arrange
                var v1 = new Addition(new Constant(1), new Constant(3));
                var v2 = new Subtraction(new Constant(1), new Constant(3));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }
        }
    }
}