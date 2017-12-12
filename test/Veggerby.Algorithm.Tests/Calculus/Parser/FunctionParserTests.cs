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
        public void Should_parse_chained_subtraction_in_correct_order()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("x-y-1");  // should be (x-y)-1 not x-(y-1)

            // assert
            actual.ShouldBeOfType<Subtraction>();
            ((Subtraction)actual).Left.ShouldBeOfType<Subtraction>();

            var left = ((Subtraction)actual).Left as Subtraction;

            left.Left.ShouldBe(Variable.x);
            left.Right.ShouldBe(Variable.y);

            ((Subtraction)actual).Right.ShouldBe(Constant.One);
        }


        [Fact]
        public void Should_parse_simple_function_with_parenthesis()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("x+(3)");

            // assert
            actual.ShouldBeOfType<Addition>();
            ((Addition)actual).Left.ShouldBe(Variable.x);
            ((Addition)actual).Right.ShouldBe(Constant.Create(3));
        }

        [Fact]
        public void Should_parse_simple_function_with_priority()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("x+3*x");

            // assert
            actual.ShouldBeOfType<Addition>();
            ((Addition)actual).Left.ShouldBe(Variable.x);
            ((Addition)actual).Right.ShouldBeOfType<Multiplication>();

            var right = ((Addition)actual).Right as Multiplication;
            right.Left.ShouldBe(Constant.Create(3));
            right.Right.ShouldBe(Variable.x);
        }

        [Fact]
        public void Should_parse_subtraction_correct_order()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("x-3+x"); // should be (x-3)+x not x-(3+x)

            // assert
            actual.ShouldBeOfType<Addition>();
            ((Addition)actual).Left.ShouldBeOfType<Subtraction>();
            ((Addition)actual).Right.ShouldBe(Variable.x);

            var left = ((Addition)actual).Left as Subtraction;
            left.Left.ShouldBe(Variable.x);
            left.Right.ShouldBe(Constant.Create(3));
        }

        [Fact]
        public void Should_parse_parenthesis_transition()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("(x-3)*x");

            // assert
            actual.ShouldBeOfType<Multiplication>();
            ((Multiplication)actual).Left.ShouldBeOfType<Subtraction>();
            ((Multiplication)actual).Right.ShouldBe(Variable.x);

            var left = ((Multiplication)actual).Left as Subtraction;
            left.Left.ShouldBe(Variable.x);
            left.Right.ShouldBe(Constant.Create(3));
        }

        [Fact]
        public void Should_parse_positive_constant()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("x-+3");

            // assert
            actual.ShouldBeOfType<Subtraction>();
            ((Subtraction)actual).Left.ShouldBe(Variable.x);
            ((Subtraction)actual).Right.ShouldBe(Constant.Create(3));
        }

        [Fact]
        public void Should_parse_fraction()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("1/2");

            // assert
            actual.ShouldBeOfType<Fraction>();
            ((Fraction)actual).Numerator.ShouldBe(Constant.One);
            ((Fraction)actual).Denominator.ShouldBe(Constant.Create(2));
        }

        [Fact]
        public void Should_parse_simple_factorial()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("x!");

            // assert
            actual.ShouldBeOfType<Factorial>();
            ((Factorial)actual).Inner.ShouldBe(Variable.x);
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
        public void Should_parse_negative_v2()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("+x-3");

            // assert
            actual.ShouldBeOfType<Subtraction>();
            ((Subtraction)actual).Left.ShouldBe(Variable.x);
            ((Subtraction)actual).Right.ShouldBe(Constant.Create(3));
        }

        [Fact]
        public void Should_parse_negative_in_parenthesis()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("x*(-3+x)");

            // assert
            actual.ShouldBeOfType<Multiplication>();
            ((Multiplication)actual).Left.ShouldBe(Variable.x);
            ((Multiplication)actual).Right.ShouldBeOfType<Addition>();
        }

        [Fact]
        public void Should_parse_function()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("sin(x)");

            // assert
            actual.ShouldBeOfType<Sine>();
            ((Sine)actual).Inner.ShouldBe(Variable.x);
        }

        [Fact]
        public void Should_parse_function_with_addition()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("3+sin(x)");

            // assert
            actual.ShouldBeOfType<Addition>();
            ((Addition)actual).Left.ShouldBe(Constant.Create(3));
            ((Addition)actual).Right.ShouldBeOfType<Sine>();

            var right = ((Addition)actual).Right as Sine;
            right.Inner.ShouldBe(Variable.x);
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
        public void Should_complex_function_with_binary_function()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("x*4-root(3,sin(x))");

            // assert
            actual.ShouldBeOfType<Subtraction>();
            ((Subtraction)actual).Left.ShouldBeOfType<Multiplication>();
            ((Subtraction)actual).Right.ShouldBeOfType<Root>();

            Multiplication left = ((Subtraction)actual).Left as Multiplication;
            Root right = ((Subtraction)actual).Right as Root;

            left.Left.ShouldBe(Variable.x);
            left.Right.ShouldBe(Constant.Create(4));

            right.Exponent.ShouldBe(3);
            right.Inner.ShouldBeOfType<Sine>();
        }

        [Fact]
        public void Should_parse_complex_function()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("(x+3)*(4-2)");

            // assert
            actual.ShouldBeOfType<Multiplication>();
            ((Multiplication)actual).Left.ShouldBe(Addition.Create(Variable.x, Constant.Create(3)));
            ((Multiplication)actual).Right.ShouldBe(Subtraction.Create(Constant.Create(4), Constant.Create(2)));
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

            var left = ((Subtraction)actual).Left as Multiplication; // (x+3)*(4-2) will be reduced to 2*(x+3)
            var right = ((Subtraction)actual).Right as Sine;

            left.Left.ShouldBe(Addition.Create(Variable.x, Constant.Create(3)));
            left.Right.ShouldBe(Subtraction.Create(Constant.Create(4), Constant.Create(2)));

            right.Inner.ShouldBeOfType<Multiplication>();

            var inner = right.Inner as Multiplication;

            inner.Left.ShouldBe(Variable.x);
            inner.Right.ShouldBeOfType<Cosine>();

            ((Cosine)inner.Right).Inner.ShouldBeOfType<Tangent>();
            ((Tangent)((Cosine)inner.Right).Inner).Inner.ShouldBeOfType<Exponential>();

            var exp = ((Tangent)((Cosine)inner.Right).Inner).Inner as Exponential;
            exp.Inner.ShouldBeOfType<Subtraction>();

            ((Subtraction)exp.Inner).Left.ShouldBe(Constant.Create(3));
            ((Subtraction)exp.Inner).Right.ShouldBeOfType<Logarithm>();

            ((exp.Inner as Subtraction).Right as Logarithm).Inner.ShouldBeOfType<Division>();

            var innerMostDivision = ((exp.Inner as Subtraction).Right as Logarithm).Inner as Division;
            innerMostDivision.Left.ShouldBe(Constant.Create(4));
            innerMostDivision.Right.ShouldBeOfType<Sine>();
            ((Sine)innerMostDivision.Right).Inner.ShouldBe(Variable.x);
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
            actual.Message.ShouldBe("Parenthesis not properly closed");
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