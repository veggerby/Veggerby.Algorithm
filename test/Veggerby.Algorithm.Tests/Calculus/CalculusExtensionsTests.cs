using System;
using System.Collections.Generic;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus
{
    public class CalculusExtensionsTests
    {
        public class GetPriority
        {
            [Theory]
            [InlineData("x!", typeof(Factorial), 0)]
            [InlineData("x^2", typeof(Power), 1)]
            [InlineData("x/2", typeof(Division), 2)]
            [InlineData("x*2", typeof(Multiplication), 3)]
            [InlineData("x-2", typeof(Subtraction), 4)]
            [InlineData("x+2", typeof(Addition), 5)]
            [InlineData("sin(x)", typeof(Sine), null)]
            [InlineData("cos(x)", typeof(Cosine), null)]
            [InlineData("exp(x)", typeof(Exponential), null)]
            [InlineData("ln(x)", typeof(Logarithm), null)]
            [InlineData("log(x)", typeof(LogarithmBase), null)]
            [InlineData("2", typeof(Constant), null)]
            [InlineData("x", typeof(Variable), null)]
            [InlineData("pi", typeof(NamedConstant), null)]
            [InlineData("-x", typeof(Negative), null)]
            public void Should_return_correct_priority(string f, Type type, int? expected)
            {
                // arrange
                Operand func = f;

                // act
                var actual = func.GetPriority();

                // assert
                func.ShouldBeOfType(type);
                actual.ShouldBe(expected);
            }
        }

        public class CouldUseParenthesis
        {
            [Theory]
            [InlineData("x!", typeof(Factorial), true)]
            [InlineData("x^2", typeof(Power), true)]
            [InlineData("x/2", typeof(Division), true)]
            [InlineData("x*2", typeof(Multiplication), true)]
            [InlineData("x-2", typeof(Subtraction), true)]
            [InlineData("x+2", typeof(Addition), true)]
            [InlineData("sin(x)", typeof(Sine), false)]
            [InlineData("cos(x)", typeof(Cosine), false)]
            [InlineData("exp(x)", typeof(Exponential), false)]
            [InlineData("ln(x)", typeof(Logarithm), false)]
            [InlineData("log(x)", typeof(LogarithmBase), false)]
            [InlineData("2", typeof(Constant), false)]
            [InlineData("x", typeof(Variable), false)]
            [InlineData("pi", typeof(NamedConstant), false)]
            [InlineData("-x", typeof(Negative), false)]
            public void Should_return_could_use_parenthesis(string f, Type type, bool expected)
            {
                // arrange
                Operand func = f;

                // act
                var actual = func.CouldUseParenthesis();

                // assert
                func.ShouldBeOfType(type);
                actual.ShouldBe(expected);
            }
        }

        public class IsConstant
        {
            [Theory]
            [InlineData("x!", typeof(Factorial), false)]
            [InlineData("x^2", typeof(Power), false)]
            [InlineData("x/2", typeof(Division), false)]
            [InlineData("x*2", typeof(Multiplication), false)]
            [InlineData("x-2", typeof(Subtraction), false)]
            [InlineData("x+2", typeof(Addition), false)]
            [InlineData("sin(x)", typeof(Sine), false)]
            [InlineData("cos(x)", typeof(Cosine), false)]
            [InlineData("exp(x)", typeof(Exponential), false)]
            [InlineData("ln(x)", typeof(Logarithm), false)]
            [InlineData("log(x)", typeof(LogarithmBase), false)]
            [InlineData("2", typeof(Constant), true)]
            [InlineData("x", typeof(Variable), false)]
            [InlineData("pi", typeof(NamedConstant), false)]
            [InlineData("-x", typeof(Negative), false)]
            public void Should_return_is_constant(string f, Type type, bool expected)
            {
                // arrange
                Operand func = f;

                // act
                var actual = func.IsConstant();

                // assert
                func.ShouldBeOfType(type);
                actual.ShouldBe(expected);
            }
        }

        public class IsVariable
        {
            [Theory]
            [InlineData("x!", typeof(Factorial), false)]
            [InlineData("x^2", typeof(Power), false)]
            [InlineData("x/2", typeof(Division), false)]
            [InlineData("x*2", typeof(Multiplication), false)]
            [InlineData("x-2", typeof(Subtraction), false)]
            [InlineData("x+2", typeof(Addition), false)]
            [InlineData("sin(x)", typeof(Sine), false)]
            [InlineData("cos(x)", typeof(Cosine), false)]
            [InlineData("exp(x)", typeof(Exponential), false)]
            [InlineData("ln(x)", typeof(Logarithm), false)]
            [InlineData("log(x)", typeof(LogarithmBase), false)]
            [InlineData("2", typeof(Constant), false)]
            [InlineData("x", typeof(Variable), true)]
            [InlineData("pi", typeof(NamedConstant), false)]
            [InlineData("-x", typeof(Negative), false)]
            public void Should_return_is_variable(string f, Type type, bool expected)
            {
                // arrange
                Operand func = f;

                // act
                var actual = func.IsVariable();

                // assert
                func.ShouldBeOfType(type);
                actual.ShouldBe(expected);
            }
        }

        public class IsNegative
        {
            [Theory]
            [InlineData("x!", typeof(Factorial), false)]
            [InlineData("x^2", typeof(Power), false)]
            [InlineData("x/2", typeof(Division), false)]
            [InlineData("x*2", typeof(Multiplication), false)]
            [InlineData("x-2", typeof(Subtraction), false)]
            [InlineData("x+2", typeof(Addition), false)]
            [InlineData("sin(x)", typeof(Sine), false)]
            [InlineData("cos(x)", typeof(Cosine), false)]
            [InlineData("exp(x)", typeof(Exponential), false)]
            [InlineData("ln(x)", typeof(Logarithm), false)]
            [InlineData("log(x)", typeof(LogarithmBase), false)]
            [InlineData("2", typeof(Constant), false)]
            [InlineData("x", typeof(Variable), false)]
            [InlineData("pi", typeof(NamedConstant), false)]
            [InlineData("-x", typeof(Negative), true)]
            public void Should_return_is_negative(string f, Type type, bool expected)
            {
                // arrange
                Operand func = f;

                // act
                var actual = func.IsNegative();

                // assert
                func.ShouldBeOfType(type);
                actual.ShouldBe(expected);
            }
        }

        public class FlattenCommutative
        {
            [Fact]
            public void Should_return_simple_flatten()
            {
                // arrange
                var f = (ICommutativeBinaryOperation)Multiplication.Create(Variable.x, Constant.Pi);
                var expected = new HashSet<Operand> { f.Left, f.Right };

                // act
                var actual = f.FlattenCommutative();

                // assert
                actual.ShouldBe(expected);
            }

            [Fact]
            public void Should_return_flatten_nested()
            {
                // arrange
                var f = (ICommutativeBinaryOperation)Multiplication.Create(
                    Multiplication.Create(
                        Variable.x, 
                        Sine.Create(Variable.x)), 
                    Multiplication.Create(
                        Multiplication.Create(
                            Cosine.Create(Variable.x),
                            Logarithm.Create(Variable.x)
                        ),
                        Division.Create(
                            Constant.One,
                            Variable.x
                )));

                var expected = new HashSet<Operand> 
                { 
                    Variable.x, 
                    Sine.Create(Variable.x),
                    Cosine.Create(Variable.x),
                    Logarithm.Create(Variable.x),
                    Division.Create(Constant.One, Variable.x) 
                };

                // act
                var actual = f.FlattenCommutative();

                // assert
                actual.ShouldBe(expected);
            }
        }

        public class ToMathJaxString
        {
            [Fact]
            public void Should_return_simple_mj()
            {
                // arrange
                var operand = Sine.Create(Variable.x);

                // act
                var actual = operand.ToMathJaxString();

                // assert
                actual.ShouldBe(@"\sin\left(x\right)");
            }
        }
    }
}