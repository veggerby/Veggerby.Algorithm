using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class BinaryOperationTests
    {
        [Fact]
        public void Should_equal_hashcode_non_commutative()
        {
            // arrange
            var p1 = Power.Create(Variable.x, 2);
            var p2 = Power.Create(Variable.x, 2);

            // act
            var h1 = p1.GetHashCode();
            var h2 = p2.GetHashCode();

            // assert
            h1.ShouldBe(h2);
        }

        [Fact]
        public void Should_equal_hashcode_commutative_simple_same_order()
        {
            // arrange
            var p1 = Addition.Create(Variable.x, 2);
            var p2 = Addition.Create(Variable.x, 2);

            // act
            var h1 = p1.GetHashCode();
            var h2 = p2.GetHashCode();

            // assert
            h1.ShouldBe(h2);
        }

        [Fact]
        public void Should_equal_hashcode_commutative_simple_swapped_order()
        {
            // arrange
            var p1 = Addition.Create(Variable.x, 2);
            var p2 = Addition.Create(2, Variable.x);

            // act
            var h1 = p1.GetHashCode();
            var h2 = p2.GetHashCode();

            // assert
            h1.ShouldBe(h2);
        }

        [Fact]
        public void Should_equal_hashcode_commutative_complex_same_order()
        {
            // arrange
            var p1 = Multiplication.Create(Variable.x, Multiplication.Create(Sine.Create(Variable.x),2));
            var p2 = Multiplication.Create(Variable.x, Multiplication.Create(Sine.Create(Variable.x),2));

            // act
            var h1 = p1.GetHashCode();
            var h2 = p2.GetHashCode();

            // assert
            h1.ShouldBe(h2);
        }

        [Fact]
        public void Should_equal_hashcode_commutative_complex_other_parenthesis()
        {
            // arrange
            var p1 = Multiplication.Create(Multiplication.Create(Variable.x, Sine.Create(Variable.x)), 2);
            var p2 = Multiplication.Create(Variable.x, Multiplication.Create(Sine.Create(Variable.x),2));

            // act
            var h1 = p1.GetHashCode();
            var h2 = p2.GetHashCode();

            // assert
            h1.ShouldBe(h2);
        }


        [Fact]
        public void Should_equal_hashcode_commutative_complex_random_order()
        {
            // arrange
            var p1 = Multiplication.Create(Multiplication.Create(2, Variable.x), Sine.Create(Variable.x));
            var p2 = Multiplication.Create(Variable.x, Multiplication.Create(Sine.Create(Variable.x),2));

            // act
            var h1 = p1.GetHashCode();
            var h2 = p2.GetHashCode();

            // assert
            h1.ShouldBe(h2);
        }
    }
}