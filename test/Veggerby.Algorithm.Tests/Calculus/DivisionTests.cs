using FluentAssertions;

using Veggerby.Algorithm.Calculus;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus;

public class DivisionTests
{
    [Fact]
    public void Should_initialize()
    {
        var actual = (Division)Division.Create(Constant.One, Variable.x);

        // assert
        actual.Left.Should().Be(Constant.One);
        actual.Right.Should().Be(Variable.x);
    }

    [Fact]
    public void Should_equal_self()
    {
        // arrange
        var v = Division.Create(Constant.One, ValueConstant.Create(3));

        // act
        var actual = v.Equals(v);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_null()
    {
        // arrange
        var v = Division.Create(Constant.One, ValueConstant.Create(3));

        // act
        var actual = v.Equals(null);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_equal_same_operands()
    {
        // arrange
        var v1 = Division.Create(Constant.One, ValueConstant.Create(3));
        var v2 = Division.Create(Constant.One, ValueConstant.Create(3));

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_different_operands()
    {
        // arrange
        var v1 = Division.Create(Constant.One, ValueConstant.Create(3));
        var v2 = Division.Create(ValueConstant.Create(2), ValueConstant.Create(2));

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_not_equal_mirrored_operands()
    {
        // arrange
        var v1 = Division.Create(Constant.One, ValueConstant.Create(3));
        var v2 = Division.Create(ValueConstant.Create(3), Constant.One);

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_not_equal_different_operation_identical_operands()
    {
        // arrange
        var v1 = Division.Create(Constant.One, ValueConstant.Create(3));
        var v2 = Subtraction.Create(Constant.One, ValueConstant.Create(3));

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }
}