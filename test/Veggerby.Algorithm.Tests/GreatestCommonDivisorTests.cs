using FluentAssertions;

using Xunit;

namespace Veggerby.Algorithm.Tests;

public class GreatestCommonDivisorTests
{
    [Theory]
    [InlineData(9, 6, 3)]
    [InlineData(71, 4, 1)]
    [InlineData(1000, 2, 2)]
    [InlineData(81, 36, 9)]
    [InlineData(1071, 462, 21)]
    public void Should_return_correct_euclid_gcd(int a, int b, int expected)
    {
        // arrange

        // act
        var actual = GreatestCommonDivisor.Euclid(a, b);

        // assert
        actual.Should().Be(expected);
    }
}