using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class MultiplicationTests
    {
        public class Create
        {
            [Fact]
            public void Should_initialize()
            {
                var actual = (Multiplication)Multiplication.Create(new Constant(1), new Variable("x"));
                
                // assert
                actual.Left.ShouldBe(new Constant(1));
                actual.Right.ShouldBe(new Variable("x"));
            }

            [Fact]
            public void Should_collapse()
            {
                // arrange
                
                // act
                var actual = Multiplication.Create(new Constant(6), new Constant(2));
                
                // assert
                actual.ShouldBe(new Constant(12));
            }
        }

        public class Evaluate
        {
            [Fact]
            public void Should_evaluate()
            {
                // arrange
                var v = Multiplication.Create(new Constant(4), new Constant(2));
                
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
                var v = Multiplication.Create(new Constant(4), new Variable("x"));
                
                // act
                var actual = @v.ToString();

                // assert
                actual.ShouldBe("4*x");
            }
        }

        public class _Equals
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = Multiplication.Create(new Constant(1), new Variable("x"));
                
                // act
                var actual = v.Equals(v);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_null()
            {
                // arrange
                var v = Multiplication.Create(new Constant(1), new Variable("x"));
                
                // act
                var actual = v.Equals(null);

                // assert
                actual.ShouldBeFalse();
            }
            
            [Fact]
            public void Should_equal_same_operands()
            {
                // arrange
                var v1 = Multiplication.Create(new Constant(1), new Variable("x"));
                var v2 = Multiplication.Create(new Constant(1), new Variable("x"));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_different_operands()
            {
                // arrange
                var v1 = Multiplication.Create(new Constant(1), new Variable("x"));
                var v2 = Multiplication.Create(new Variable("y"), new Constant(2));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_not_equal_mirrored_operands()
            {
                // arrange
                var v1 = Multiplication.Create(new Constant(1), new Variable("x"));
                var v2 = Multiplication.Create(new Variable("x"), new Constant(1));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_not_equal_different_operation_identical_operands()
            {
                // arrange
                var v1 = Multiplication.Create(new Constant(1), new Variable("x"));
                var v2 = Subtraction.Create(new Constant(1), new Variable("x"));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }
        }
    }
}