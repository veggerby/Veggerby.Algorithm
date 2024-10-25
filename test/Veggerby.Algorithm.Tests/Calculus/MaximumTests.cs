using FluentAssertions;

using Veggerby.Algorithm.Calculus;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus;

public class MaximumTests
{
    [Fact]
    public void Should_initialize()
    {
        var actual = (Maximum)Maximum.Create(Constant.One, Variable.x);

        // assert
        actual.Operands.Should().BeEquivalentTo(new Operand[] { Constant.One, Variable.x });
    }

    [Fact]
    public void Should_equal_self()
    {
        // arrange
        var v = Maximum.Create(Variable.x, ValueConstant.Create(3));

        // act
        var actual = v.Equals(v);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_null()
    {
        // arrange
        var v = Maximum.Create(Variable.x, ValueConstant.Create(3));

        // act
        var actual = v.Equals(null);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_equal_same_operands()
    {
        // arrange
        var v1 = Maximum.Create(Variable.x, ValueConstant.Create(3));
        var v2 = Maximum.Create(Variable.x, ValueConstant.Create(3));

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_different_operands()
    {
        // arrange
        var v1 = Maximum.Create(Variable.x, ValueConstant.Create(3));
        var v2 = Maximum.Create(ValueConstant.Create(2), Variable.y);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_equal_mirrored_operands()
    {
        // arrange
        var v1 = Maximum.Create(Variable.x, ValueConstant.Create(3));
        var v2 = Maximum.Create(ValueConstant.Create(3), Variable.x);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_different_operation_identical_operands()
    {
        // arrange
        var v1 = Maximum.Create(Variable.x, ValueConstant.Create(3));
        var v2 = Subtraction.Create(Variable.x, ValueConstant.Create(3));

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }
}