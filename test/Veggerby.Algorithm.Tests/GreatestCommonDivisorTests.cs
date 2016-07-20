using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Veggerby.Algorithm;
using Xunit;

namespace Veggerby.Algorithm.Tests
{
    public class GreatestCommonDivisorTests
    {
        public class Euclid
        {
            [Theory]
            [InlineData(9, 6, 3)]
            [InlineData(71, 4, 1)]
            [InlineData(1000, 2, 2)]
            [InlineData(81, 36, 9)]
            [InlineData(1071, 462, 21)]
            public void Should_return_correct_gcd(int a, int b, int expected)
            {
                // arrange

                // act
                var actual = GreatestCommonDivisor.Euclid(a, b);

                // assert
                Assert.Equal(expected, actual);
            }        
        }
    }
}