using System;

using FluentAssertions;

using Veggerby.Algorithm.Calculus;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus;

public class UnspecifiedConstantTests
{
    [Fact]
    public void Should_initialize_from_constructor()
    {
        // arrange

        // act
        var actual = UnspecifiedConstant.Create();

        // assert
        actual.InstanceId.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Should_equal_self()
    {
        // arrange
        var v = UnspecifiedConstant.Create();

        // act
        var actual = v.Equals(v);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_not_equal_null()
    {
        // arrange
        var v = UnspecifiedConstant.Create();

        // act
        var actual = v.Equals(null);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_not_equal_other()
    {
        // arrange
        var v1 = UnspecifiedConstant.Create();
        var v2 = UnspecifiedConstant.Create();

        // act
        var actual = v1.Equals(v2);

        // assert
        actual.Should().BeFalse();
    }
}