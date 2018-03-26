using System;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Parser;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Parser
{
    public class CompilerTests
    {
        [Theory]
        [InlineData("1", 1)]
        [InlineData("1.2", 1.2)]
        [InlineData("1.2e-1", 0.12)]
        [InlineData("2124.28761E123", 2.12428761E126)]
        public void Should_compile_token_number(string value, double expected)
        {
            // arrange
            var compiler = new Compiler();

            var node = new Node(new Token(TokenType.Number, value, null));

            // act
            var actual = compiler.Compile(node);

            // assert
            actual.ShouldBeOfType<ValueConstant>();
            ((ValueConstant)actual).Value.ShouldBe(expected);
        }

        [Fact]
        public void Should_compile_token_variable()
        {
            // arrange
            var compiler = new Compiler();

            var node = new Node(new Token(TokenType.Identifier, "x", null));

            // act
            var actual = compiler.Compile(node);

            // assert
            actual.ShouldBeOfType<Variable>();
            ((Variable)actual).Identifier.ShouldBe("x");
        }

        [Theory]
        [InlineData("pi", "π", Math.PI)]
        [InlineData("π", "π", Math.PI)]
        [InlineData("e", "e", Math.E)]
        public void Should_compile_token_named_constant(string value, string expectedSymbol, double expectedValue)
        {
            // arrange
            var compiler = new Compiler();

            var node = new Node(new Token(TokenType.Identifier, value, null));

            // act
            var actual = compiler.Compile(node);

            // assert
            actual.ShouldBeOfType<NamedConstant>();
            ((NamedConstant)actual).Symbol.ShouldBe(expectedSymbol);
            ((NamedConstant)actual).Value.ShouldBe(expectedValue);
        }

        [Theory]
        [InlineData("sin", typeof(Sine))]
        [InlineData("cos", typeof(Cosine))]
        [InlineData("tan", typeof(Tangent))]
        [InlineData("exp", typeof(Exponential))]
        [InlineData("ln", typeof(Logarithm))]
        [InlineData("log", typeof(LogarithmBase))]
        [InlineData("log2", typeof(LogarithmBase))]
        [InlineData("sqrt", typeof(Root))]
        public void Should_compile_token_function(string value, Type expectedOperandType)
        {
            // arrange
            var compiler = new Compiler();

            var node = new UnaryNode(
                new Token(TokenType.Function, value, null),
                new Node(new Token(TokenType.Identifier, "x", null)));

            // act
            var actual = compiler.Compile(node);

            // assert
            actual.ShouldBeOfType(expectedOperandType);
            ((UnaryOperation)actual).Inner.ShouldBe(Variable.x);
        }

        [Theory]
        [InlineData(TokenType.Sign, "-", typeof(Subtraction))]
        [InlineData(TokenType.OperatorPriority1, "/", typeof(Division))]
        [InlineData(TokenType.OperatorPriority1, "^", typeof(Power))]
        public void Should_compile_token_binary_function(TokenType type, string value, Type expectedOperandType)
        {
            // arrange
            var compiler = new Compiler();

            var node = new BinaryNode(
                new Node(new Token(TokenType.Number, "2", null)),
                new Token(type, value, null),
                new Node(new Token(TokenType.Identifier, "x", null)));

            // act
            var actual = compiler.Compile(node);

            // assert
            actual.ShouldBeOfType(expectedOperandType);
            ((BinaryOperation)actual).Left.ShouldBe(ValueConstant.Create(2));
            ((BinaryOperation)actual).Right.ShouldBe(Variable.x);
        }


        [Theory]
        [InlineData(TokenType.Function, "min", typeof(Minimum))]
        [InlineData(TokenType.Function, "max", typeof(Maximum))]
        [InlineData(TokenType.Sign, "+", typeof(Addition))]
        [InlineData(TokenType.OperatorPriority1, "*", typeof(Multiplication))]
        public void Should_compile_token_multi_function(TokenType type, string value, Type expectedOperandType)
        {
            // arrange
            var compiler = new Compiler();

            var node = new BinaryNode(
                new Node(new Token(TokenType.Number, "2", null)),
                new Token(type, value, null),
                new Node(new Token(TokenType.Identifier, "x", null)));

            // act
            var actual = compiler.Compile(node);

            // assert
            actual.ShouldBeOfType(expectedOperandType);
            ((MultiOperation)actual).Operands.ShouldBe(new Operand[] { ValueConstant.Create(2), Variable.x });
        }

        [Fact]
        public void Should_compile_token_root_function()
        {
            // arrange
            var compiler = new Compiler();

            var node = new BinaryNode(
                new Node(new Token(TokenType.Number, "2", null)),
                new Token(TokenType.Function, "root", null),
                new Node(new Token(TokenType.Identifier, "x", null)));

            // act
            var actual = compiler.Compile(node);

            // assert
            actual.ShouldBeOfType<Root>();
            ((Root)actual).Exponent.ShouldBe(2);
            ((Root)actual).Inner.ShouldBe(Variable.x);
        }

        [Fact]
        public void Should_compile_token_factorial()
        {
            // arrange
            var compiler = new Compiler();

            var node = new UnaryNode(
                new Token(TokenType.Factorial, "!", null),
                new Node(new Token(TokenType.Identifier, "x", null)));

            // act
            var actual = compiler.Compile(node);

            // assert
            actual.ShouldBeOfType<Factorial>();
            ((Factorial)actual).Inner.ShouldBe(Variable.x);
        }
    }
}