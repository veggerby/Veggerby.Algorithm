using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Veggerby.Algorithm.LinearAlgebra;
using Xunit;

// ignore "warning CS1718: Comparison made to same variable; did you mean to compare something else?"
#pragma warning disable CS1718

namespace Veggerby.Algorithm.Test
{
    public class VectorTests
    {
        public class Constructor_and_ToArray
        {
            [Fact]
            public void Should_initialize_empty_array()
            {
                // arrange
            
                // act
                var actual = new Vector(5);

                // assert
                Assert.Equal(new double[5], actual.ToArray());
            }

            [Fact]
            public void Should_initialize_params_array()
            {
                // arrange
            
                // act
                var actual = new Vector(1, 2, 3, 4);

                // assert
                Assert.Equal(new double[] { 1, 2, 3, 4 }, actual.ToArray());
            }

            [Fact]
            public void Should_throw_with_null_array()
            {
                // arrange
                
                // act + assert
                Assert.Throws<ArgumentNullException>(() => new Vector((double[]) null));
            }

            [Fact]
            public void Should_initialize_actual_array()
            {
                // arrange
            
                // act
                var actual = new Vector(new double[] { 1, 2, 3, 4 });

                // assert
                Assert.Equal(new double[] { 1, 2, 3, 4 }, actual.ToArray());
            }

            [Fact]
            public void Should_throw_with_empty_array()
            {
                // arrange
            
                // act + assert
                Assert.Throws<ArgumentException>(() => new Vector());
            }

            [Fact]
            public void Should_throw_with_enumerable_empty()
            {
                // arrange
            
                // act + assert
                Assert.Throws<ArgumentException>(() => new Vector(Enumerable.Empty<double>()));
            }
        }

        public class Size 
        {
            [Fact]
            public void Should_return_correct_size_empty()
            {
                // arrange
                var v = new Vector(5);

                // act
                var actual = v.Size;

                // assert
                Assert.Equal(5, actual);
            }

            [Fact]
            public void Should_return_correct_size()
            {
                // arrange
                var v = new Vector(new double[] { 1, 2, 3 });

                // act
                var actual = v.Size;

                // assert
                Assert.Equal(3, actual);
            }
        }

        public class Indexer 
        {
            [Fact]
            public void Should_return_value_at_index()
            {
                // arrange
                var v = new Vector(2, 4, 6, 7);
            
                // act
                var actual = v[1];
            
                // assert
                Assert.Equal(4, actual);
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(4)]
            public void Should_throw_if_index_out_of_range(int i)
            {
                // arrange
                var v = new Vector(2, 4, 6, 7);
            
                // act + assert
                Assert.Throws<ArgumentOutOfRangeException>(() => v[i]);
            }
        }

        public class Operator_Add
        {
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
                Assert.Equal(expected, actual);
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
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void Should_throw_if_vectors_do_not_have_same_size()
            {
                // arrange
                var v1 = new Vector(1, 2, 3, 4);
                var v2 = new Vector(2, 4, 6);
            
                // act + assert
                var e = Assert.Throws<ArgumentException>(() => v1 + v2);
            }
        }

        public class Operator_Subtract
        {
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
                Assert.Equal(expected, actual);
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
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void Should_throw_if_vectors_do_not_have_same_size()
            {
                // arrange
                var v1 = new Vector(1, 2, 3, 4);
                var v2 = new Vector(2, 4, 6);
            
                // act + assert
                var e = Assert.Throws<ArgumentException>(() => v1 - v2);
            }
        }  

        public class Operator_Equal
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = new Vector(1, 2, 3);
                
                // act
                var actual = (v == v);
                
                // assert
                Assert.True(actual);
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
                Assert.Equal(expected, actual);
            }
        } 

        public class Operator_NotEqual
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = new Vector(1, 2, 3);
                
                // act
                var actual = (v != v);
                
                // assert
                Assert.False(actual);
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
                Assert.Equal(expected, actual);
            }
        }

        public class ToString
        {
            [Fact]
            public void Should_return_to_string()
            {
                // arrange
                var v = new Vector(1, 5, 2, 9, 2);
                var expected = "(1, 5, 2, 9, 2)";

                // act
                var actual = v.ToString();
                
                // assert
                Assert.Equal(expected, actual);
            }
        }
    }
}

#pragma warning restore CS1718
