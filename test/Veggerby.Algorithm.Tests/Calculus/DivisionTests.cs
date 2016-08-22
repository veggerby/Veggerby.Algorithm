using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class DivisionTests
    {
        public class Create
        {
            [Fact]
            public void Should_initialize()
            {
                var actual = (Division)Division.Create(Constant.One, Variable.x);
                
                // assert
                actual.Left.ShouldBe(Constant.One);
                actual.Right.ShouldBe(Variable.x);
            }

            [Fact]
            public void Should_collapse()
            {
                // arrange
                
                // act
                var actual = Division.Create(Constant.Create(6), Constant.Create(2));
                
                // assert
                actual.ShouldBe(Constant.Create(3));
            }
        }

        public class _ToString
        {
            [Fact]
            public void Should_return_correct_string()
            {
                // arrange
                var v = Division.Create(Constant.Create(4), Variable.x);
                
                // act
                var actual = @v.ToString();

                // assert
                actual.ShouldBe("4/x");
            }
        }

        public class _Equals
        {
            [Fact]
            public void Should_equal_self()
            {
                // arrange
                var v = Division.Create(Constant.One, Constant.Create(3));
                
                // act
                var actual = v.Equals(v);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_null()
            {
                // arrange
                var v = Division.Create(Constant.One, Constant.Create(3));
                
                // act
                var actual = v.Equals(null);

                // assert
                actual.ShouldBeFalse();
            }
            
            [Fact]
            public void Should_equal_same_operands()
            {
                // arrange
                var v1 = Division.Create(Constant.One, Constant.Create(3));
                var v2 = Division.Create(Constant.One, Constant.Create(3));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeTrue();
            }

            [Fact]
            public void Should_not_equal_different_operands()
            {
                // arrange
                var v1 = Division.Create(Constant.One, Constant.Create(3));
                var v2 = Division.Create(Constant.Create(2), Constant.Create(2));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_not_equal_mirrored_operands()
            {
                // arrange
                var v1 = Division.Create(Constant.One, Constant.Create(3));
                var v2 = Division.Create(Constant.Create(3), Constant.One);
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }

            [Fact]
            public void Should_not_equal_different_operation_identical_operands()
            {
                // arrange
                var v1 = Division.Create(Constant.One, Constant.Create(3));
                var v2 = Subtraction.Create(Constant.One, Constant.Create(3));
                
                // act
                var actual = v1.Equals(v2);

                // assert
                actual.ShouldBeFalse();
            }
        }

        public class GetDerivative
        {
            [Fact]
            public void Should_get_derivative()
            {
                // arrange
                var func = Division.Create(Constant.One, Variable.x);
                var expected = Division.Create(Constant.MinusOne, Power.Create(Variable.x, 2));

                // act
                var actual = func.GetDerivative(Variable.x);

                // assert
                actual.ShouldBe(expected);
            }

            [Fact]
            public void Should_get_complex_derivative()
            {
                // arrange
                var func = Division.Create(
                    Sine.Create(Variable.x), 
                    Variable.x);

                var expected = Division.Create(
                    Subtraction.Create(
                        Multiplication.Create(Variable.x, Cosine.Create(Variable.x)),
                        Sine.Create(Variable.x)), 
                    Power.Create(Variable.x, 2));

                // act
                var actual = func.GetDerivative(Variable.x);

                // assert
                actual.ShouldBe(expected);
            }
        }
    }
}