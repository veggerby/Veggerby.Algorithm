using System;
using System.Linq;

using FluentAssertions;

using Veggerby.Algorithm.LinearAlgebra;

using Xunit;

// ignore "warning CS1718: Comparison made to same variable; did you mean to compare something else?"
#pragma warning disable CS1718

namespace Veggerby.Algorithm.Tests.LinearAlgebra;

public class MatrixTests
{
    [Fact]
    public void Should_return_identity_matrix()
    {
        // arrange
        var expected = new Matrix(new double[,] {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 1 }
        });

        // act
        var actual = Matrix.Identity(3);

        // assert
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_throw_with_negative_or_zero_dimension(int d)
    {
        // arrange

        // act
        var actual = () => Matrix.Identity(d);

        // assert
        actual.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("n");
    }

    [Fact]
    public void Should_create_empty()
    {
        // arrange

        // act
        var actual = new Matrix(2, 3);

        // assert
        actual.ToArray().Should().BeEquivalentTo(new double[2, 3]);
    }

    [Theory]
    [InlineData(0, 0, "rows")]
    [InlineData(2, 0, "cols")]
    [InlineData(0, 2, "rows")]
    [InlineData(-1, -1, "rows")]
    [InlineData(1, -1, "cols")]
    [InlineData(-1, 1, "rows")]
    public void Should_throw_with_negative_or_zero_rows_or_column_count(int r, int c, string expected)
    {
        // arrange

        // act
        var actual = () => new Matrix(r, c);

        // assert
        actual.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(expected);
    }

    [Fact]
    public void Should_create_with_value_array()
    {
        // arrange

        // act
        var actual = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

        // assert
        actual.ToArray().Should().BeEquivalentTo(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
    }

    [Fact]
    public void Should_throw_with_null_array()
    {
        // arrange

        // act
        var actual = () => new Matrix((double[,])null);

        // assert
        actual.Should().Throw<ArgumentNullException>().WithParameterName("values");
    }

    [Fact]
    public void Should_throw_with_empty_outer_array()
    {
        // arrange
        var v = new double[,] { };

        // act
        var actual = () => new Matrix(v);

        // assert
        actual.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("values");
    }

    [Fact]
    public void Should_throw_with_empty_inner_arrays()
    {
        // arrange
        var v = new double[,] { { }, { }, { } };

        // act
        var actual = () => new Matrix(v);

        // assert
        actual.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("values");
    }

    [Fact]
    public void Should_create_matrix_from_vector_list()
    {
        // arrange
        var v1 = new Vector(1, 2, 3, 4);
        var v2 = new Vector(5, 6, 7, 8);
        var v3 = new Vector(4, 3, 2, 1);
        var expected = new double[,]
        {
            { 1, 2, 3, 4 },
            { 5, 6, 7, 8 },
            { 4, 3, 2, 1 }
        };

        // act
        var actual = new Matrix([v1, v2, v3]);

        // assert
        actual.ToArray().Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Should_throw_with_null_vector_list()
    {
        // arrange

        // act
        var actual = () => new Matrix((Vector[])null);

        // assert
        actual.Should().Throw<ArgumentNullException>().WithParameterName("rows");
    }

    [Fact]
    public void Should_throw_with_empty_vector_list()
    {
        // arrange

        // act
        var actual = () => new Matrix(Enumerable.Empty<Vector>());

        // assert
        actual.Should().Throw<ArgumentException>().WithParameterName("rows");
    }

    [Fact]
    public void Should_throw_with_different_dimensioned_vectors()
    {
        // arrange
        var v1 = new Vector(1, 2, 3);
        var v2 = new Vector(4, 5);

        // act
        var actual = () => new Matrix([v1, v2]);

        // assert
        actual.Should().Throw<ArgumentException>().WithParameterName("rows");
    }

    [Fact]
    public void Should_return_correct_row_count()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 1, 2, 3 },
            { 4, 5, 6 } });

        // act
        var actual = m1.RowCount;

        // assert
        actual.Should().Be(2);
    }

    [Fact]
    public void Should_return_correct_col_count()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 1, 2, 3 },
            { 4, 5, 6 } });

        // act
        var actual = m1.ColCount;

        // assert
        actual.Should().Be(3);
    }

    [Fact]
    public void Should_return_correct_rows()
    {
        // arrange
        var v1 = new Vector(1, 2, 3);
        var v2 = new Vector(4, 5, 6);

        var m1 = new Matrix(new double[,] {
            { 1, 2, 3 },
            { 4, 5, 6 } });

        // act
        var actual = m1.Rows;

        // assert
        actual.Should().BeEquivalentTo(new[] { v1, v2 });
    }

    [Fact]
    public void Should_return_correct_cols()
    {
        // arrange
        var v1 = new Vector(1, 4);
        var v2 = new Vector(2, 5);
        var v3 = new Vector(3, 6);

        var m1 = new Matrix(new double[,] {
            { 1, 2, 3 },
            { 4, 5, 6 } });

        // act
        var actual = m1.Cols;

        // assert
        actual.Should().BeEquivalentTo(new[] { v1, v2, v3 });
    }

    [Fact]
    public void Should_return_value_at_index()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 1, 2, 3 },
            { 4, 5, 6 } });

        // act
        var actual = m1[1, 2];

        // assert
        actual.Should().Be(6);
    }

    [Theory]
    [InlineData(2, 0, "r")]
    [InlineData(0, 3, "c")]
    [InlineData(-1, 1, "r")]
    [InlineData(1, -1, "c")]
    [InlineData(2, 1, "r")]
    [InlineData(1, 3, "c")]
    public void Should_throw_if_row_index_out_of_range(int r, int c, string expected)
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 1, 2, 3 },
            { 4, 5, 6 } });

        // act
        var actual = () => { var x = m1[r, c]; };

        // assert
        actual.Should().Throw<ArgumentOutOfRangeException>().WithParameterName(expected);
    }

    [Fact]
    public void Should_return_correct_row()
    {
        // arrange
        var v1 = new Vector(1, 2, 3);
        var v2 = new Vector(4, 5, 6);

        var m1 = new Matrix(new double[,] {
            { 1, 2, 3 },
            { 4, 5, 6 } });

        // act
        var actual = m1.GetRow(1);

        // assert
        actual.Should().Be(v2);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(4)]
    public void Should_throw_if_index_out_of_range(int i)
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 1, 2, 3 },
            { 4, 5, 6 } });

        // act
        var actual = () => m1.GetRow(i);

        // assert
        actual.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("r");
    }

    [Fact]
    public void Should_return_correct_col()
    {
        // arrange
        var v1 = new Vector(1, 4);
        var v2 = new Vector(2, 5);
        var v3 = new Vector(3, 6);

        var m1 = new Matrix(new double[,] {
            { 1, 2, 3 },
            { 4, 5, 6 } });

        // act
        var actual = m1.GetCol(1);

        // assert
        actual.Should().Be(v2);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(4)]
    public void Should_throw_if_column_index_out_of_range(int i)
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 1, 2, 3 },
            { 4, 5, 6 } });

        // act
        var actual = () => m1.GetCol(i);

        // assert
        actual.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("c");
    }

    [Fact]
    public void Should_return_correct_determinant()
    {
        // arrange
        var m = new Matrix(new double[,] {
            { -2, 2, 3 },
            { -1, 1, 3 },
            { 2, 0, -1 } });

        // act
        var actual = m.Determinant;

        // assert
        actual.Should().Be(6);
    }

    [Fact]
    public void Should_throw_when_non_square()
    {
        // arrange
        var m = new Matrix(new double[,] {
            { -2, 2, 3 },
            { -1, 1, 3 }, });

        // act
        var actual = () => { var x = m.Determinant; };

        // assert
        actual.Should().Throw<IndexOutOfRangeException>();
    }

    [Fact]
    public void Should_return_addition()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 1, 0, 2 },
            { -1, 3, 1 } ,
            { 2, 1, 3 } });

        var m2 = new Matrix(new double[,] {
            { 3, 1, 2 },
            { 2, 1, 4 },
            { 1, 0, -3 } });

        var expected = new Matrix(new double[,] {
            { 4, 1, 4 },
            { 1, 4, 5 } ,
            { 3, 1, 0 } });

        // act
        var actual = m1 + m2;

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_throw_when_adding_incorrect_dimensons()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 1, 0 },
            { -3, 1 } });

        var m2 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        // act
        var actual = () => { var x = m1 + m2; };

        // assert
        actual.Should().Throw<ArgumentException>().WithParameterName("m2");
    }

    [Fact]
    public void Should_return_subtraction()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 1, 0, 2 },
            { -1, 3, 1 } ,
            { 2, 1, 3 } });

        var m2 = new Matrix(new double[,] {
            { 3, 1, 2 },
            { 2, 1, 4 },
            { 1, 0, -3 } });

        var expected = new Matrix(new double[,] {
            { -2, -1, 0 },
            { -3, 2, -3 } ,
            { 1, 1, 6 } });

        // act
        var actual = m1 - m2;

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_throw_when_subtracting_incorrect_dimensons()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 1, 0 },
            { -3, 1 } });

        var m2 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        // act
        var actual = () => { var x = m1 - m2; };

        // assert
        actual.Should().Throw<ArgumentException>().WithParameterName("m2");
    }

    [Fact]
    public void Should_return_scale()
    {
        // arrange
        var m = new Matrix(new double[,] {
            { 1, 0, 2 },
            { -1, 3, 1 } });

        var expected = new Matrix(new double[,] {
            { 3, 0, 6 },
            { -3, 9, 3 } });

        // act
        var actual = 3 * m;

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_return_multiplication()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 1, 0, 2 },
            { -1, 3, 1 } });

        var m2 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        var expected = new Matrix(new double[,] {
            { 5, 1 },
            { 4, 2 } });

        // act
        var actual = m1 * m2;

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_throw_when_multiplying_incorrect_dimensons()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 1, 0 },
            { -3, 1 } });

        var m2 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        // act
        var actual = () => { var x = m1 * m2; };

        // assert
        actual.Should().Throw<ArgumentException>().WithParameterName("m2");
    }

    [Fact]
    public void Should_return_vector_multiplication()
    {
        // arrange
        var m = new Matrix(new double[,] {
            { 1, 0, 2 },
            { -1, 3, 1 } });

        var v = new Vector(1, 2, 3);

        var expected = new Vector(7, 8);

        // act
        var actual = m * v;

        // assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_throw_when_multiplying_incorrect_dimensons_vector_and_matrix()
    {
        // arrange
        var m = new Matrix(new double[,] {
            { 1, 0 },
            { -3, 1 } });

        var v = new Vector(1, 2, 3);

        // act
        var actual = () => { var x = m * v; };

        // assert
        actual.Should().Throw<ArgumentException>().WithParameterName("v");
    }

    [Fact]
    public void Should_return_true_equal_self()
    {
        // arrange
        var m = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        // act
        var actual = (m == m);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_return_true_equal()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        var m2 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        // act
        var actual = (m1 == m2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_return_false_equal_single_value_different()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        var m2 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 3 },
            { 1, 0 } });

        // act
        var actual = (m1 == m2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_return_false_equal_wrong_row_count()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        var m2 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 } });

        // act
        var actual = (m1 == m2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_return_false_equal_wrong_col_count()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        var m2 = new Matrix(new double[,] {
            { 3, 1, 5 },
            { 2, 1, 6 },
            { 1, 0, 7 } });

        // act
        var actual = (m1 == m2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_return_false_not_equal_self()
    {
        // arrange
        var m = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        // act
        var actual = (m != m);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_return_false_not_equal()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        var m2 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        // act
        var actual = (m1 != m2);

        // assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Should_return_true_not_equal_single_value_different()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        var m2 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 3 },
            { 1, 0 } });

        // act
        var actual = (m1 != m2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_return_true_not_equal_wrong_row_count()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        var m2 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 } });

        // act
        var actual = (m1 != m2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_return_true_not_equal_wrong_col_count()
    {
        // arrange
        var m1 = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        var m2 = new Matrix(new double[,] {
            { 3, 1, 5 },
            { 2, 1, 6 },
            { 1, 0, 7 } });

        // act
        var actual = (m1 != m2);

        // assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Should_return_to_string()
    {
        // arrange
        var m = new Matrix(new double[,] {
            { 3, 1 },
            { 2, 1 },
            { 1, 0 } });

        var expected = "((3, 1)" + Environment.NewLine +
                        "(2, 1)" + Environment.NewLine +
                        "(1, 0))";
        // act
        var actual = m.ToString();

        // assert
        actual.Should().Be(expected);
    }
}

#pragma warning restore CS1718