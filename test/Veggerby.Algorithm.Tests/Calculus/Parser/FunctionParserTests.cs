using System;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Parser;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Parser
{
    public class FunctionParserTests
    {
        [Fact]
        public void Should_parse_simple_function()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("x+3");

            // assert
            actual.ShouldBeOfType<Addition>();
            ((Addition)actual).Left.ShouldBe(Variable.x);
            ((Addition)actual).Right.ShouldBe(Constant.Create(3));
        }

        [Fact]
        public void Should_parse_negative_constant()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("x+-3");

            // assert
            actual.ShouldBeOfType<Subtraction>();
            ((Subtraction)actual).Left.ShouldBe(Variable.x);
            ((Subtraction)actual).Right.ShouldBe(Constant.Create(3));
        }

        [Fact]
        public void Should_parse_named_constant()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("pi");

            // assert
            actual.ShouldBe(Constant.Pi);
        }

        [Fact]
        public void Should_parse_simple_add_constant()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("1e2+3+(0-4-1.2e3)");

            // assert
            actual.ShouldBe(Constant.Create(-1101));
        }

        [Fact]
        public void Should_parse_negative()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("-x+3");

            // assert
            actual.ShouldBeOfType<Addition>();
            ((Addition)actual).Left.ShouldBe(Negative.Create(Variable.x));
            ((Addition)actual).Right.ShouldBe(Constant.Create(3));
        }

        [Fact]
        public void Should_parse_function()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("sin(3*4)");

            // assert
            actual.ShouldBeOfType<Sine>();
            ((Sine)actual).Inner.ShouldBe(Constant.Create(12));
        }

        [Fact]
        public void Should_binary_function()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("max(3,x)");

            // assert
            actual.ShouldBeOfType<Maximum>();
            ((Maximum)actual).Left.ShouldBe(Constant.Create(3));
            ((Maximum)actual).Right.ShouldBe(Variable.x);
        }

        [Fact]
        public void Should_parse_function_reference()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("f(3,x,x+2)");

            // assert
            actual.ShouldBeOfType<FunctionReference>();
            ((FunctionReference)actual).Identifier.ShouldBe("f");
            ((FunctionReference)actual).Parameters.ShouldBe(new[] { Constant.Create(3), Variable.x, Addition.Create(Variable.x, 2) });
        }


        [Fact]
        public void Should_throw_exception_trying_to_parse_function_reference_no_parameters()
        {
            // arrange

            // act
            var actual = Should.Throw<Exception>(() => FunctionParser.Parse("f()"));

            // assert
            actual.Message.ShouldBe("Empty group: n/a");
        }

        [Fact]
        public void Should_root_function()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("root(3,x)");

            // assert
            actual.ShouldBeOfType<Root>();
            ((Root)actual).Exponent.ShouldBe(3);
            ((Root)actual).Inner.ShouldBe(Variable.x);
        }

        [Fact]
        public void Should_parse_complex_function()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("(x+3)*(4-2)");

            // assert
            actual.ShouldBeOfType<Multiplication>();
            ((Multiplication)actual).Left.ShouldBe(Constant.Create(2));
            ((Multiplication)actual).Right.ShouldBeOfType<Addition>();
        }

        [Fact]
        public void Should_parse_very_complex_function()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("(x+3)*(4-2)-sin(x*cos(tan(exp(3-ln(4/sin(x))))))");

            // assert
            actual.ShouldBeOfType<Subtraction>();
            ((Subtraction)actual).Left.ShouldBeOfType<Multiplication>();
            ((Subtraction)actual).Right.ShouldBeOfType<Sine>();
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