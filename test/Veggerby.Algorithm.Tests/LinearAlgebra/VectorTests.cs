using System;
using System.Linq;
using Shouldly;
using Veggerby.Algorithm.LinearAlgebra;
using Xunit;

// ignore "warning CS1718: Comparison made to same variable; did you mean to compare something else?"
#pragma warning disable CS1718

namespace Veggerby.Algorithm.Tests.LinearAlgebra
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
                actual.ToArray().ShouldBe(new double[5]);
            }

            [Theory]
            [InlineData(0)]
            [InlineData(-1)]
            public void Should_throw_with_negative_or_zero_dimension(int d)
            {
                // arrange
                
                // act + assert
                var actual = Should.Throw<ArgumentOutOfRangeException>(() => new Vector(d));

                // assert
                actual.ParamName.ShouldBe("d");
            }

            [Fact]
            public void Should_initialize_params_array()
            {
                // arrange
            
                // act
                var actual = new Vector(1, 2, 3, 4);

                // assert
                actual.ToArray().ShouldBe(new double[] { 1, 2, 3, 4 });
            }

            [Fact]
            public void Should_throw_with_null_array()
            {
                // arrange
                
                // act + assert
                var actual = Should.Throw<ArgumentNullException>(() => new Vector((double[]) null));

                // assert
                actual.ParamName.ShouldBe("values");
            }

            [Fact]
            public void Should_initialize_actual_array()
            {
                // arrange
            
                // act
                var actual = new Vector(new double[] { 1, 2, 3, 4 });

                // assert
                actual.ToArray().ShouldBe(new double[] { 1, 2, 3, 4 });
            }

            [Fact]
            public void Should_throw_with_empty_array()
            {
                // arrange
            
                // act + assert
                var actual = Should.Throw<ArgumentException>(() => new Vector());
                
                // assert
                actual.ParamName.ShouldBe("values");
            }

            [Fact]
            public void Should_throw_with_enumerable_empty()
            {
                // arrange
            
                // act + assert
                var actual = Should.Throw<ArgumentException>(() => new Vector(Enumerable.Empty<double>()));
                
                // assert
                actual.ParamName.ShouldBe("values");
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
                actual.ShouldBe(5);
            }

            [Fact]
            public void Should_return_correct_size()
            {
                // arrange
                var v = new Vector(new double[] { 1, 2, 3 });

                // act
                var actual = v.Size;

                // assert
                actual.ShouldBe(3);
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
                actual.ShouldBe(4);
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(4)]
            public void Should_throw_if_index_out_of_range(int i)
            {
                // arrange
                var v = new Vector(2, 4, 6, 7);
            
                // act + assert
                var actual = Should.Throw<ArgumentOutOfRangeException>(() => { var x = v[i]; });

                // assert
                actual.ParamName.ShouldBe("i");
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
                actual.ShouldBe(expected);
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
                actual.ShouldBe(expected);
            }

            [Fact]
            public void Should_throw_if_vectors_do_not_have_same_size()
            {
                // arrange
                var v1 = new Vector(1, 2, 3, 4);
                var v2 = new Vector(2, 4, 6);
            
                // act + assert
                var actual = Should.Throw<ArgumentException>(() => { var x = v1 + v2; });

                // assert
                actual.ParamName.ShouldBe("v2");
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
                actual.ShouldBe(expected);
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
                actual.ShouldBe(expected);
            }

            [Fact]
            public void Should_throw_if_vectors_do_not_have_same_size()
            {
                // arrange
                var v1 = new Vector(1, 2, 3, 4);
                var v2 = new Vector(2, 4, 6);
            
                // act + assert
                var actual = Should.Throw<ArgumentException>(() => { var x = v1 - v2; } );

                // assert
                actual.ParamName.ShouldBe("v2");
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
                actual.ShouldBeTrue();
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
                actual.ShouldBe(expected);
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
                actual.ShouldBeFalse();
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
                actual.ShouldBe(expected);
            }
        }

        public class _ToString
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
                actual.ShouldBe(expected);
            }
        }

        public class _Equals // same tests as == operator
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = new Vector(1, 2, 3);
                
                // act
                var actual = v.Equals(v);
                
                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_other_type()
            {
                // arrange
                var v = new Vector(1, 2, 3);
                
                // act
                var actual = v.Equals("test");
                
                // assert
                actual.ShouldBeFalse();
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
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBe(expected);
            }
        }
    }
}

#pragma warning restore CS1718
