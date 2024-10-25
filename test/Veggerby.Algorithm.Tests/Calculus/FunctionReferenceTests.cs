using FluentAssertions;

using Veggerby.Algorithm.Calculus;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus;

public class FunctionReferenceTests
{
    [Fact]
    public void Should_initialize_from_constructor()
    {
        // arrange

        // act
        var actual = (FunctionReference)FunctionReference.Create("f", Power.Create(Variable.x, 2));

        // assert
        actual.Identifier.Should().Be("f");

        actual.Parameters.Should().BeEquivalentTo(new[] { Power.Create(Variable.x, 2) }, options => options.ComparingByValue<Operand>());
    }

    [Fact]
    public void Should_equal_self()
    {
        // arrange
        var v = FunctionReference.Create("f", Power.Create(Variable.x, 2));

        // act
        var actual = v.Equals(v);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_null()
    {
        // arrange
        var v = FunctionReference.Create("f", Power.Create(Variable.x, 2));

        // act
        var actual = v.Equals(null);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_equal_same_operands()
    {
        // arrange
        var v1 = FunctionReference.Create("f", Power.Create(Variable.x, 2));
        var v2 = FunctionReference.Create("f", Power.Create(Variable.x, 2));

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_same_operands_different_identifier()
    {
        // arrange
        var v1 = FunctionReference.Create("f", Power.Create(Variable.x, 2));
        var v2 = FunctionReference.Create("g", Power.Create(Variable.x, 2));

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_not_equal_different_operands()
    {
        // arrange
        var v1 = FunctionReference.Create("f", Power.Create(Variable.x, 2), Variable.x);
        var v2 = FunctionReference.Create("g", Variable.y, Division.Create(Variable.x, 2));

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_not_equal_different_operation_identical_operands()
    {
        // arrange
        var v1 = FunctionReference.Create("f", Power.Create(Variable.x, 2));
        var v2 = Sine.Create(Power.Create(Variable.x, 2));

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }
}