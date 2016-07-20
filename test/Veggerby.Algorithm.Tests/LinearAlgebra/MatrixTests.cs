using System;
using System.Linq;
using Veggerby.Algorithm.LinearAlgebra;
using Xunit;

// ignore "warning CS1718: Comparison made to same variable; did you mean to compare something else?"
#pragma warning disable CS1718


namespace Veggerby.Algorithm.Tests.LinearAlgebra
{
    public class MatrixTests
    {
        public class Identity
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
                Assert.Equal(expected, actual);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            public void Should_throw_with_negative_or_zero_dimension(int d)
            {
                // arrange
                
                // act + assert
                Assert.Throws<ArgumentOutOfRangeException>(() => Matrix.Identity(d));
            }
        }

        public class Contructor_and_ToArray
        {
            [Fact]
            public void Should_create_empty()
            {
                // arrange
                
                // act
                var actual = new Matrix(2, 3);
                
                // assert
                Assert.Equal(new double[2, 3], actual.ToArray());
            }

            [Theory]
            [InlineData(0, 0)]
            [InlineData(2, 0)]
            [InlineData(0, 2)]
            [InlineData(-1, 1)]
            [InlineData(1, -1)]
            [InlineData(-1, 1)]
            public void Should_throw_with_negative_or_zero_rows_or_column_count(int r, int c)
            {
                // arrange
                
                // act + assert
                Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix(r, c));
            }
            
            [Fact]
            public void Should_create_with_value_array()
            {
                // arrange
                
                // act
                var actual = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }});
                
                // assert
                Assert.Equal(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }}, actual.ToArray());
            }
            
            [Fact]
            public void Should_throw_with_null_array()
            {
                // arrange
                
                // act + assert
                Assert.Throws<ArgumentNullException>(() => new Matrix((double[,]) null));
            }

            [Fact]
            public void Should_throw_with_empty_outer_array()
            {
                // arrange
                var v = new double[,] { };
                
                // act + assert
                Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix(v));
            }

            [Fact]
            public void Should_throw_with_empty_inner_arrays()
            {
                // arrange
                var v = new double[,] { {}, {}, {} };
                
                // act + assert
                Assert.Throws<ArgumentOutOfRangeException>(() => new Matrix(v));
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
                var actual = new Matrix(new [] { v1, v2, v3 });
                
                // assert
                Assert.Equal(expected, actual.ToArray());
            }

            [Fact]
            public void Should_throw_with_null_vector_list()
            {
                // arrange
                
                // act + assert
                Assert.Throws<ArgumentNullException>(() => new Matrix((Vector[]) null));
            }

            [Fact]
            public void Should_throw_with_empty_vector_list()
            {
                // arrange
                
                // act + assert
                Assert.Throws<ArgumentException>(() => new Matrix(Enumerable.Empty<Vector>()));
            }

            [Fact]
            public void Should_throw_with_different_dimensioned_vectors()
            {
                // arrange
                var v1 = new Vector(1, 2, 3);
                var v2 = new Vector(4, 5);
                
                // act + assert
                Assert.Throws<ArgumentException>(() => new Matrix(new[] { v1, v2}));
            }
        }

        public class RowCount 
        {
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
                Assert.Equal(2, actual);
            }
        }

        public class ColCount 
        {
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
                Assert.Equal(3, actual);
            }
        }

        public class Rows 
        {
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
                Assert.Equal(new[] { v1, v2 }, actual);
            }
        }

        public class Cols 
        {
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
                Assert.Equal(new[] { v1, v2, v3 }, actual);
            }
        }

        public class Indexer 
        {
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
                Assert.Equal(6, actual);
            }

            [Theory]
            [InlineData(2, 0)]
            [InlineData(0, 3)]
            [InlineData(-1, 1)]
            [InlineData(1, -1)]
            [InlineData(-1, 1)]
            [InlineData(2, 1)]
            [InlineData(1, 3)]
            public void Should_throw_if_index_out_of_range(int r, int c)
            {
                // arrange
                var m1 = new Matrix(new double[,] { 
                    { 1, 2, 3 }, 
                    { 4, 5, 6 } });
            
                // act + assert
                Assert.Throws<ArgumentOutOfRangeException>(() => m1[r, c]);
            }
        }

        public class GetRow 
        {
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
                Assert.Equal(v2, actual);
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
            
                // act + assert
                Assert.Throws<ArgumentOutOfRangeException>(() => m1.GetRow(i));
            }
        }

        public class GetCol 
        {
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
                Assert.Equal(v2, actual);
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
            
                // act + assert
                Assert.Throws<ArgumentOutOfRangeException>(() => m1.GetCol(i));
            }
        }

        public class Determinant 
        {
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
                Assert.Equal(6, actual);
            }

            [Fact]
            public void Should_throw_when_non_square()
            {
                // arrange
                var m = new Matrix(new double[,] { 
                    { -2, 2, 3 }, 
                    { -1, 1, 3 }, });

                // act + assert
                Assert.Throws<IndexOutOfRangeException>(() => m.Determinant);
            }
        }

        public class Operator_Add
        {
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
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void Should_throw_when_incorrect_dimensons()
            {
                // arrange
                var m1 = new Matrix(new double[,] { 
                    { 1, 0 }, 
                    { -3, 1 } });

                var m2 = new Matrix(new double[,] { 
                    { 3, 1 }, 
                    { 2, 1 }, 
                    { 1, 0 } });

                // act + assert
                Assert.Throws<ArgumentException>(() => m1 + m2);
            }
        }

        public class Operator_Subtract
        {
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
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void Should_throw_when_incorrect_dimensons()
            {
                // arrange
                var m1 = new Matrix(new double[,] { 
                    { 1, 0 }, 
                    { -3, 1 } });

                var m2 = new Matrix(new double[,] { 
                    { 3, 1 }, 
                    { 2, 1 }, 
                    { 1, 0 } });

                // act + assert
                Assert.Throws<ArgumentException>(() => m1 - m2);
            }
        }
        
        public class Operator_Mult_Scale
        {
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
                Assert.Equal(expected, actual);
            }
        }

        public class Operator_Mult 
        {
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
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void Should_throw_when_incorrect_dimensons()
            {
                // arrange
                var m1 = new Matrix(new double[,] { 
                    { 1, 0 }, 
                    { -3, 1 } });

                var m2 = new Matrix(new double[,] { 
                    { 3, 1 }, 
                    { 2, 1 }, 
                    { 1, 0 } });

                // act + assert
                Assert.Throws<ArgumentException>(() => m1 * m2);
            }
        }

        public class Operator_Equal
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var m = new Matrix(new double[,] { 
                    { 3, 1 }, 
                    { 2, 1 }, 
                    { 1, 0 } });
                
                // act
                var actual = (m == m);
                
                // assert
                Assert.True(actual);
            }

            [Fact]
            public void Should_equal()
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
                Assert.True(actual);
            }

            [Fact]
            public void Should_not_equal_single_value_different()
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
                Assert.False(actual);
            }

            [Fact]
            public void Should_not_equal_wrong_row_count()
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
                Assert.False(actual);
            }

            [Fact]
            public void Should_not_equal_wrong_col_count()
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
                Assert.False(actual);
            }            
        } 

        public class Operator_NotEqual
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var m = new Matrix(new double[,] { 
                    { 3, 1 }, 
                    { 2, 1 }, 
                    { 1, 0 } });
                
                // act
                var actual = (m != m);
                
                // assert
                Assert.False(actual);
            }

            [Fact]
            public void Should_equal()
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
                Assert.False(actual);
            }

            [Fact]
            public void Should_not_equal_single_value_different()
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
                Assert.True(actual);
            }

            [Fact]
            public void Should_not_equal_wrong_row_count()
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
                Assert.True(actual);
            }

            [Fact]
            public void Should_not_equal_wrong_col_count()
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
                Assert.True(actual);
            }            
        }

        public class _ToString
        {
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
                Assert.Equal(expected, actual);
            }
        }

        public class _Equals
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var m = new Matrix(new double[,] { 
                    { 3, 1 }, 
                    { 2, 1 }, 
                    { 1, 0 } });
                
                // act
                var actual = m.Equals(m);
                
                // assert
                Assert.True(actual);
            }

            [Fact]
            public void Should_equal()
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
                var actual = m1.Equals(m2);

                // assert
                Assert.True(actual);
            }

            [Fact]
            public void Should_not_equal_single_value_different()
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
                var actual = m1.Equals(m2);

                // assert
                Assert.False(actual);
            }

            [Fact]
            public void Should_not_equal_wrong_row_count()
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
                var actual = m1.Equals(m2);

                // assert
                Assert.False(actual);
            }

            [Fact]
            public void Should_not_equal_wrong_col_count()
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
                var actual = m1.Equals(m2);

                // assert
                Assert.False(actual);
            }            
        } 
    }
}

#pragma warning restore CS1718