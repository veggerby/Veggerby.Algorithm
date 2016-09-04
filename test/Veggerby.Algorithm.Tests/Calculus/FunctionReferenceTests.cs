using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class FunctionReferenceTests
    {
        [Fact]
        public void Should_initialize_from_constructor()
        {
            // arrange

            // act
            var actual = (FunctionReference)FunctionReference.Create("f", new [] { Power.Create(Variable.x, 2) });

            // assert
            actual.Identifier.ShouldBe("f");

            actual.Parameters.ShouldBe(new[] { Power.Create(Variable.x, 2) });
        }

        [Fact]
        public void Should_equal_self()
        {
            // arrange
            var v = FunctionReference.Create("f", new [] { Power.Create(Variable.x, 2) });

            // act
            var actual = v.Equals(v);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_null()
        {
            // arrange
            var v = FunctionReference.Create("f", new [] { Power.Create(Variable.x, 2) });

            // act
            var actual = v.Equals(null);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_equal_same_operands()
        {
            // arrange
            var v1 = FunctionReference.Create("f", new [] { Power.Create(Variable.x, 2) });
            var v2 = FunctionReference.Create("f", new [] { Power.Create(Variable.x, 2) });

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeTrue();
        }

        [Fact]
        public void Should_not_equal_same_operands_different_identifier()
        {
            // arrange
            var v1 = FunctionReference.Create("f", new [] { Power.Create(Variable.x, 2) });
            var v2 = FunctionReference.Create("g", new [] { Power.Create(Variable.x, 2) });

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_not_equal_different_operands()
        {
            // arrange
            var v1 = FunctionReference.Create("f", new [] { Power.Create(Variable.x, 2), Variable.x });
            var v2 = FunctionReference.Create("g", new [] { Variable.y, Division.Create(Variable.x, 2) });

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }

        [Fact]
        public void Should_not_equal_different_operation_identical_operands()
        {
            // arrange
            var v1 = FunctionReference.Create("f", new [] { Power.Create(Variable.x, 2) });
            var v2 = Sine.Create(Power.Create(Variable.x, 2));

            // act
            var actual = v1.Equals(v2);

            // assert
            actual.ShouldBeFalse();
        }
    }
}