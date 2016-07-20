using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Veggerby.Algorithm.LinearAlgebra;
using Xunit;

namespace Veggerby.Algorithm.Tests.LinearAlgebra
{
    public class MatrixExtensionsTests
    {
        public class IsSquare 
        {
            [Fact]
            public void Should_return_true_for_square_matrix()
            {
                // arrange
                var m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });

                // act
                var actual = m1.IsSquare();

                // assert
                Assert.True(actual);
            }

            [Fact]
            public void Should_return_false_for_non_square_matrix()
            {
                // arrange
                var m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

                // act
                var actual = m1.IsSquare();

                // assert
                Assert.False(actual);
            }
        }

        public class IsDiagonal 
        {
            [Fact]
            public void Should_return_true_for_diagonal()
            {
                // arrange
                var m1 = new Matrix(new double[,] { 
                    { 1, 0, 0 },
                    { 0, 5, 0 }, 
                    { 0, 0, 9 } });

                // act
                var actual = m1.IsDiagonal();

                // assert
                Assert.True(actual);
            }

            [Fact]
            public void Should_return_false_if_not_diagonal()
            {
                // arrange
                var m1 = new Matrix(new double[,] { 
                    { 1, 0, 1 },
                    { 1, 5, 0 }, 
                    { 1, 1, 9 } });

                // act
                var actual = m1.IsDiagonal();

                // assert
                Assert.False(actual);
            }

            [Fact]
            public void Should_return_false_for_non_square_matrix()
            {
                // arrange
                var m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

                // act
                var actual = m1.IsDiagonal();

                // assert
                Assert.False(actual);
            }
        }

        public class IsLowerTriangular 
        {
            [Fact]
            public void Should_return_true_for_lower_triangle()
            {
                // arrange
                var m1 = new Matrix(new double[,] { 
                    { 1, 0, 0 },
                    { 1, 5, 0 }, 
                    { 4, 2, 9 } });

                // act
                var actual = m1.IsLowerTriangular();

                // assert
                Assert.True(actual);
            }

            [Fact]
            public void Should_return_true_for_diagonal()
            {
                // arrange
                var m1 = new Matrix(new double[,] { 
                    { 1, 0, 0 },
                    { 0, 5, 0 }, 
                    { 0, 0, 9 } });

                // act
                var actual = m1.IsLowerTriangular();

                // assert
                Assert.True(actual);
            }

            [Fact]
            public void Should_return_false_if_not_lower_triangle()
            {
                // arrange
                var m1 = new Matrix(new double[,] { 
                    { 1, 0, 1 },
                    { 1, 5, 0 }, 
                    { 1, 1, 9 } });

                // act
                var actual = m1.IsLowerTriangular();

                // assert
                Assert.False(actual);
            }

            [Fact]
            public void Should_return_false_for_non_square_matrix()
            {
                // arrange
                var m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

                // act
                var actual = m1.IsLowerTriangular();

                // assert
                Assert.False(actual);
            }
        }

        public class IsUpperTriangular 
        {
            [Fact]
            public void Should_return_true_for_upper_triangle()
            {
                // arrange
                var m1 = new Matrix(new double[,] { 
                    { 1, 1, 1 },
                    { 0, 5, 3 }, 
                    { 0, 0, 9 } });

                // act
                var actual = m1.IsUpperTriangular();

                // assert
                Assert.True(actual);
            }

            [Fact]
            public void Should_return_true_for_diagonal()
            {
                // arrange
                var m1 = new Matrix(new double[,] { 
                    { 1, 0, 0 },
                    { 0, 5, 0 }, 
                    { 0, 0, 9 } });

                // act
                var actual = m1.IsUpperTriangular();

                // assert
                Assert.True(actual);
            }

            [Fact]
            public void Should_return_false_if_not_upper_triangle()
            {
                // arrange
                var m1 = new Matrix(new double[,] { 
                    { 1, 0, 1 },
                    { 1, 5, 0 }, 
                    { 1, 1, 9 } });

                // act
                var actual = m1.IsUpperTriangular();

                // assert
                Assert.False(actual);
            }

            [Fact]
            public void Should_return_false_for_non_square_matrix()
            {
                // arrange
                var m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });

                // act
                var actual = m1.IsUpperTriangular();

                // assert
                Assert.False(actual);
            }
        }

        public class Transpose 
        {
            [Fact]
            public void Should_return_transposed_matrix()
            {
                // arrange
                var m = new Matrix(new double[,] { 
                    { -2, 2, 3 }, 
                    { -1, 1, 3 } });

                var expected = new Matrix(new double[,] { 
                    { -2, -1 }, 
                    { 2, 1 }, 
                    { 3, 3 } });

                // act
                var actual = m.Transpose();
                
                // assert
                Assert.Equal(expected, actual);
            }
        }
    }
}