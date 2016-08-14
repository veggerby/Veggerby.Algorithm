using System;
using Shouldly;
using Veggerby.Algorithm.Arithmetic;
using Veggerby.Algorithm.Arithmetic.Parser;
using Xunit;

namespace Veggerby.Algorithm.Tests.Arithmetic.Parser
{
    public class FunctionParserTests
    {
        public class Parse
        {
            [Fact]
            public void Should_parse_simple_function()
            {
                // arrange

                // act
                var actual = FunctionParser.Parse("x+3");

                // assert
                actual.ShouldBeOfType<Addition>();
                ((Addition)actual).Left.ShouldBe(new Variable("x"));
                ((Addition)actual).Right.ShouldBe(new Constant(3));
            }

            [Fact]
            public void Should_parse_function()
            {
                // arrange

                // act
                var actual = FunctionParser.Parse("sin(3*4)");

                // assert
                actual.ShouldBeOfType<Sine>();
            }

            [Fact]
            public void Should_parse_complex_function()
            {
                // arrange

                // act
                var actual = FunctionParser.Parse("(x+3)*(4-3)");

                // assert
                actual.ShouldBeOfType<Multiplication>();
            }

            [Fact]
            public void Should_fail_with_unmatching_parenthesis_open()
            {
                // arrange

                // act
                var actual = Should.Throw<Exception>(() => FunctionParser.Parse("((x+3)"));

                // assert
                actual.Message.ShouldBe("Parenthesis not properly closed");
            }

            [Fact]
            public void Should_fail_with_unmatching_parenthesis_close()
            {
                // arrange

                // act
                var actual = Should.Throw<Exception>(() => FunctionParser.Parse("(x+3))"));

                // assert
                actual.Message.ShouldBe("Parenthesis not properly nested");
            }


            [Fact]
            public void Should_fail_with_unmatching_parenthesis_close_nested()
            {
                // arrange

                // act
                var actual = Should.Throw<Exception>(() => FunctionParser.Parse("(3+(x+3)"));

                // assert
                actual.Message.ShouldBe("Parenthesis not properly closed");
            }
        }
    }
}