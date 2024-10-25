using FluentAssertions;

using Veggerby.Algorithm.Calculus;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus;

public class FunctionTests
{
    [Fact]
    public void Should_initialize_from_constructor()
    {
        // arrange

        // act
        var actual = (Function)Function.Create("f", "sin(x)*cos(y)");

        // assert
        actual.Identifier.Should().Be("f");

        actual.Variables.Should().BeEquivalentTo(new[] { Variable.x, Variable.y });

        actual.Operand.Should().Be(Multiplication.Create(
            Sine.Create(Variable.x),
            Cosine.Create(Variable.y)
        ));
    }

    [Fact]
    public void Should_equal_self()
    {
        // arrange
        var v = Function.Create("f", Variable.x);

        // act
        var actual = v.Equals(v);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_null()
    {
        // arrange
        var v = Function.Create("f", Variable.x);

        // act
        var actual = v.Equals(null);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_equal_same_operands()
    {
        // arrange
        var v1 = Function.Create("f", Variable.x);
        var v2 = Function.Create("f", Variable.x);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_equal_same_operands_different_identifier()
    {
        // arrange
        var v1 = Function.Create("f", Variable.x);
        var v2 = Function.Create("g", Variable.x);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_different_operands()
    {
        // arrange
        var v1 = Function.Create("f", Variable.x);
        var v2 = Function.Create("f", Constant.Pi);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_not_equal_different_operation_identical_operands()
    {
        // arrange
        var v1 = Function.Create("f", Variable.x);
        var v2 = Sine.Create(Variable.x);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }
}