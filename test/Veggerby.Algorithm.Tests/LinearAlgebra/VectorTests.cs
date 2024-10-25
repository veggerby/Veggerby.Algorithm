using System;
using System.Linq;

using FluentAssertions;

using Veggerby.Algorithm.LinearAlgebra;

using Xunit;

// ignore "warning CS1718: Comparison made to same variable; did you mean to compare something else?"
#pragma warning disable CS1718

namespace Veggerby.Algorithm.Tests.LinearAlgebra;

public class VectorTests
{
    [Fact]
    public void Should_initialize_empty_array()
    {
        // arrange

        // act
        var actual = new Vector(5);

        // assert
        actual.ToArray().Should().BeEquivalentTo(new double[5]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_throw_with_negative_or_zero_dimension(int d)
    {
        // arrange

        // act
        var actual = () => new Vector(d);

        // assert
        actual.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("d");
    }

    [Fact]
    public void Should_initialize_params_array()
    {
        // arrange

        // act
        var actual = new Vector(1, 2, 3, 4);

        // assert
        actual.ToArray().Should().BeEquivalentTo(new double[] { 1, 2, 3, 4 });
    }

    [Fact]
    public void Should_throw_with_null_array()
    {
        // arrange

        // act
        var actual = () => new Vector((double[])null);

        // assert
        actual.Should().Throw<ArgumentNullException>().WithParameterName("values");
    }

    [Fact]
    public void Should_initialize_actual_array()
    {
        // arrange

        // act
        var actual = new Vector([1, 2, 3, 4]);

        // assert
        actual.ToArray().Should().BeEquivalentTo(new double[] { 1, 2, 3, 4 });
    }

    [Fact]
    public void Should_throw_with_empty_array()
    {
        // arrange

        // act
        var actual = () => new Vector();

        // assert
        actual.Should().Throw<ArgumentException>().WithParameterName("values");
    }

    [Fact]
    public void Should_throw_with_enumerable_empty()
    {
        // arrange

        // act
        var actual = () => new Vector(Enumerable.Empty<double>());

        // assert
        actual.Should().Throw<ArgumentException>().WithParameterName("values");
    }

    [Fact]
    public void Should_return_correct_size_empty()
    {
        // arrange
        var v = new Vector(5);

        // act
        var actual = v.Size;

        // assert
        actual.Should().Be(5);
    }

    [Fact]
    public void Should_return_correct_size()
    {
        // arrange
        var v = new Vector([1, 2, 3]);

        // act
        var actual = v.Size;

        // assert
        actual.Should().Be(3);
    }

    [Fact]
    public void Should_return_value_at_index()
    {
        // arrange
        var v = new Vector(2, 4, 6, 7);

        // act
        var actual = v[1];

        // assert
        actual.Should().Be(4);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(4)]
    public void Should_throw_if_index_out_of_range(int i)
    {
        // arrange
        var v = new Vector(2, 4, 6, 7);

        // act
        var actual = () => { var x = v[i]; };

        // assert
        actual.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("i");
    }

    [Fact]
    public void Should_add_vectors_same_size()
    {
        // arrange
        var v1 = new Vector(1, 2, 3, 4);
        var v2 = new Vector(2, 4, 6, 8);
        var expected = new Vector(3, 6, 9, 12);

        // act
        var actual = v1 + v2;

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_add_vectors_negative_values()
    {
        // arrange
        var v1 = new Vector(1, -2, 3, -4);
        var v2 = new Vector(-2, -4, 6, 8);
        var expected = new Vector(-1, -6, 9, 4);

        // act
        var actual = v1 + v2;

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_throw_if_add_vectors_do_not_have_same_size()
    {
        // arrange
        var v1 = new Vector(1, 2, 3, 4);
        var v2 = new Vector(2, 4, 6);

        // act
        var actual = () => { var x = v1 + v2; };

        // assert
        actual.Should().Throw<ArgumentException>().WithParameterName("v2");
    }

    [Fact]
    public void Should_subtract_vectors_same_size()
    {
        // arrange
        var v1 = new Vector(3, 6, 9, 12);
        var v2 = new Vector(1, 2, 3, 4);
        var expected = new Vector(2, 4, 6, 8);

        // act
        var actual = v1 - v2;

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_subtract_vectors_negative_values()
    {
        // arrange
        var v1 = new Vector(1, -2, 3, -4);
        var v2 = new Vector(-2, -4, 6, 8);
        var expected = new Vector(3, 2, -3, -12);

        // act
        var actual = v1 - v2;

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_throw_if_subtract_vectors_do_not_have_same_size()
    {
        // arrange
        var v1 = new Vector(1, 2, 3, 4);
        var v2 = new Vector(2, 4, 6);

        // act
        var actual = () => { var x = v1 - v2; };

        // assert
        actual.Should().Throw<ArgumentException>().WithParameterName("v2");
    }

    [Fact]
    public void Should_return_true_equal_self()
    {
        // arrange
        var v = new Vector(1, 2, 3);

        // act
        var actual = (v == v);

        // assert
        actual.Should().BeTrue();
    }

    [Theory]
    [InlineData(new double[] { 1, 2, 3, 4 }, new double[] { 1, 2, 3, 4 }, true)]
    [InlineData(new double[] { 4, 3, 2, 1 }, new double[] { 1, 2, 3, 4 }, false)]
    [InlineData(new double[] { 4, 3, 2 }, new double[] { 1, 2, 3, 4 }, false)]
    [InlineData(new double[] { 1, 2, 3 }, new double[] { 5, 6, 7 }, false)]
    [InlineData(new double[] { 1, 2, 3 }, new double[] { 1, 2, 3, 4 }, false)]
    public void Should_return_expected_equal_other_vector(double[] vv1, double[] vv2, bool expected)
    {
        // arrange
        var v1 = new Vector(vv1);
        var v2 = new Vector(vv2);

        // act
        var actual = (v1 == v2);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_return_false_not_equal_self()
    {
        // arrange
        var v = new Vector(1, 2, 3);

        // act
        var actual = (v != v);

        // assert
        actual.Should().BeFalse();
    }

    [Theory]
    [InlineData(new double[] { 1, 2, 3, 4 }, new double[] { 1, 2, 3, 4 }, false)]
    [InlineData(new double[] { 4, 3, 2, 1 }, new double[] { 1, 2, 3, 4 }, true)]
    [InlineData(new double[] { 4, 3, 2 }, new double[] { 1, 2, 3, 4 }, true)]
    [InlineData(new double[] { 1, 2, 3 }, new double[] { 5, 6, 7 }, true)]
    [InlineData(new double[] { 1, 2, 3 }, new double[] { 1, 2, 3, 4 }, true)]
    public void Should_return_expected_not_equal_other_vector(double[] vv1, double[] vv2, bool expected)
    {
        // arrange
        var v1 = new Vector(vv1);
        var v2 = new Vector(vv2);

        // act
        var actual = (v1 != v2);

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_return_to_string()
    {
        // arrange
        var v = new Vector(1, 5, 2, 9, 2);
        var expected = "(1, 5, 2, 9, 2)";

        // act
        var actual = v.ToString();

        // assert
        actual.Should().Be(expected);
    }
}

#pragma warning restore CS1718