using System;
using System.Linq;
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
            ((Addition)actual).Operands.ShouldBe(new Operand[] { Variable.x, ValueConstant.Create(3) });
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

            ((Subtraction)actual).Right.ShouldBe(ValueConstant.One);
        }


        [Fact]
        public void Should_parse_simple_function_with_parenthesis()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("x+(3)");

            // assert
            actual.ShouldBeOfType<Addition>();
            ((Addition)actual).Operands.ShouldBe(new Operand[] { Variable.x, ValueConstant.Create(3) });
        }

        [Fact]
        public void Should_parse_simple_function_with_priority()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("x+3*x");

            // assert
            actual.ShouldBeOfType<Addition>();
            ((Addition)actual).Operands.Count().ShouldBe(2);
            ((Addition)actual).Operands.First().ShouldBe(Variable.x);
            ((Addition)actual).Operands.Last().ShouldBeOfType<Multiplication>();

            var right = ((Addition)actual).Operands.Last() as Multiplication;
            right.Operands.ShouldBe(new Operand[] { ValueConstant.Create(3), Variable.x });
        }

        [Fact]
        public void Should_parse_subtraction_correct_order()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("x-3+x"); // should be (x-3)+x not x-(3+x)

            // assert
            actual.ShouldBeOfType<Addition>();

            ((Addition)actual).Operands.Count().ShouldBe(2);
            ((Addition)actual).Operands.First().ShouldBeOfType<Subtraction>();
            ((Addition)actual).Operands.Last().ShouldBe(Variable.x);

            var left = ((Addition)actual).Operands.First() as Subtraction;
            left.Left.ShouldBe(Variable.x);
            left.Right.ShouldBe(ValueConstant.Create(3));
        }

        [Fact]
        public void Should_parse_parenthesis_transition()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("(x-3)*x");

            // assert
            actual.ShouldBeOfType<Multiplication>();
            ((Multiplication)actual).Operands.Count().ShouldBe(2);
            ((Multiplication)actual).Operands.First().ShouldBeOfType<Subtraction>();
            ((Multiplication)actual).Operands.Last().ShouldBe(Variable.x);

            var left = ((Multiplication)actual).Operands.First() as Subtraction;
            left.Left.ShouldBe(Variable.x);
            left.Right.ShouldBe(ValueConstant.Create(3));
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
            ((Subtraction)actual).Right.ShouldBe(ValueConstant.Create(3));
        }

        [Fact]
        public void Should_parse_fraction()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("1/2");

            // assert
            actual.ShouldBeOfType<Fraction>();
            ((Fraction)actual).Numerator.ShouldBe(ValueConstant.One);
            ((Fraction)actual).Denominator.ShouldBe(ValueConstant.Create(2));
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
            actual.ShouldBe(ValueConstant.Pi);
        }

        [Fact]
        public void Should_parse_negative()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("-x+3");

            // assert
            actual.ShouldBeOfType<Subtraction>();
            ((Subtraction)actual).Left.ShouldBe(ValueConstant.Create(3));
            ((Subtraction)actual).Right.ShouldBe(Variable.x);
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
            ((Subtraction)actual).Right.ShouldBe(ValueConstant.Create(3));
        }

        [Fact]
        public void Should_parse_negative_in_parenthesis()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("x*(-3+x)");

            // assert
            actual.ShouldBeOfType<Multiplication>();
            ((Multiplication)actual).Operands.Count().ShouldBe(2);
            ((Multiplication)actual).Operands.First().ShouldBe(Variable.x);
            ((Multiplication)actual).Operands.Last().ShouldBeOfType<Subtraction>();
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
            ((Addition)actual).Operands.Count().ShouldBe(2);
            ((Addition)actual).Operands.First().ShouldBe(ValueConstant.Create(3));
            ((Addition)actual).Operands.Last().ShouldBeOfType<Sine>();

            var right = ((Addition)actual).Operands.Last() as Sine;
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
            ((Maximum)actual).Operands.Count().ShouldBe(2);
            ((Maximum)actual).Operands.First().ShouldBe(ValueConstant.Create(3));
            ((Maximum)actual).Operands.Last().ShouldBe(Variable.x);
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


            left.Operands.Count().ShouldBe(2);
            left.Operands.First().ShouldBe(Variable.x);
            left.Operands.Last().ShouldBe(ValueConstant.Create(4));;

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

            ((Multiplication)actual).Operands.ShouldBe(new [] {
                Addition.Create(Variable.x, ValueConstant.Create(3)),
                Subtraction.Create(ValueConstant.Create(4), ValueConstant.Create(2)) });
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

            left.Operands.ShouldBe(new [] { Addition.Create(Variable.x, ValueConstant.Create(3)), Subtraction.Create(ValueConstant.Create(4), ValueConstant.Create(2)) });

            right.Inner.ShouldBeOfType<Multiplication>();

            var inner = right.Inner as Multiplication;
            inner.Operands.Count().ShouldBe(2);
            inner.Operands.First().ShouldBe(Variable.x);
            inner.Operands.Last().ShouldBeOfType<Cosine>();

            ((Cosine)inner.Operands.Last()).Inner.ShouldBeOfType<Tangent>();
            ((Tangent)((Cosine)inner.Operands.Last()).Inner).Inner.ShouldBeOfType<Exponential>();

            var exp = ((Tangent)((Cosine)inner.Operands.Last()).Inner).Inner as Exponential;
            exp.Inner.ShouldBeOfType<Subtraction>();

            ((Subtraction)exp.Inner).Left.ShouldBe(ValueConstant.Create(3));
            ((Subtraction)exp.Inner).Right.ShouldBeOfType<Logarithm>();

            ((exp.Inner as Subtraction).Right as Logarithm).Inner.ShouldBeOfType<Division>();

            var innerMostDivision = ((exp.Inner as Subtraction).Right as Logarithm).Inner as Division;
            innerMostDivision.Left.ShouldBe(ValueConstant.Create(4));
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


        [Fact]
        public void Should_parse_unspecified_constant()
        {
            // arrange

            // act
            var actual = FunctionParser.Parse("(A-x)*(B-x*A)");

            // assert
            actual.ShouldBeOfType<Multiplication>();
            ((Multiplication)actual).Operands.Count().ShouldBe(2);

            var left = ((Multiplication)actual).Operands.First();
            var right = ((Multiplication)actual).Operands.Last();

            left.ShouldBeOfType<Subtraction>();
            ((Subtraction)left).Left.ShouldBeOfType<UnspecifiedConstant>();
            var unspec1 = ((Subtraction)left).Left as UnspecifiedConstant;
            ((Subtraction)left).Right.ShouldBe(Variable.x);

            right.ShouldBeOfType<Subtraction>();
            ((Subtraction)right).Left.ShouldBeOfType<UnspecifiedConstant>();
            ((Subtraction)right).Right.ShouldBeOfType<Multiplication>();
            var unspec2 = ((Subtraction)right).Left as UnspecifiedConstant;

            var mult = ((Subtraction)right).Right as Multiplication;
            mult.Operands.Count().ShouldBe(2);
            mult.Operands.First().ShouldBe(Variable.x);
            mult.Operands.Last().ShouldBeOfType<UnspecifiedConstant>();
            var unspec3 = mult.Operands.Last() as UnspecifiedConstant;

            unspec1.ShouldNotBe(unspec2);
            unspec1.ShouldBe(unspec3);
            unspec2.ShouldNotBe(unspec3);
        }
    }
}