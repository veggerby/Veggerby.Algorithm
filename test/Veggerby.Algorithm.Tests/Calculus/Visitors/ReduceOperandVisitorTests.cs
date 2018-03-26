using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Parser;
using Veggerby.Algorithm.Calculus.Visitors;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors
{
    public class ReduceOperandVisitorTests
    {
        [Fact]
        public void Should_reduce_addition()
        {
            // arrange
            var operand = Addition.Create(ValueConstant.One, ValueConstant.Create(3));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(ValueConstant.Create(4));
        }

        [Fact]
        public void Should_reduce_negative_constant()
        {
            // arrange
            var operand = Addition.Create(Variable.x, Negative.Create(ValueConstant.Create(3)));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBeOfType<Subtraction>();
            ((Subtraction)actual).Left.ShouldBe(Variable.x);
            ((Subtraction)actual).Right.ShouldBe(ValueConstant.Create(3));
        }

        [Fact]
        public void Should_reduce_addition_flattened()
        {
            // arrange
            var operand = Addition.Create(
                Addition.Create(ValueConstant.Create(7), Sine.Create(Variable.x)),
                Addition.Create(ValueConstant.Create(3), Sine.Create(Variable.x)));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = (Addition)operand.Accept(visitor);

            // assert
            actual.Operands.ShouldBe(new[] {
                ValueConstant.Create(10), Multiplication.Create(2, Sine.Create(Variable.x)) });
        }

        [Fact]
        public void Should_reduce_add_constants()
        {
            // arrange
            var operand = FunctionParser.Parse("1e2+3+(0-4-1.2e3)");
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(ValueConstant.Create(-1101));
        }

        [Fact]
        public void Should_reduce_addition_to_multiplication()
        {
            // arrange
            var operand = Addition.Create(Variable.x, Variable.x);
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBeOfType<Multiplication>();
            ((Multiplication)actual).Operands.ShouldBe(new Operand[] { ValueConstant.Create(2), Variable.x });
        }

        [Fact]
        public void Should_reduce_subtraction()
        {
            // arrange
            var operand = Subtraction.Create(ValueConstant.Create(6), ValueConstant.Create(2));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(ValueConstant.Create(4));
        }

        [Fact]
        public void Should_reduce_multiplication()
        {
            // arrange
            var operand = Multiplication.Create(ValueConstant.Create(6), ValueConstant.Create(2));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(ValueConstant.Create(12));
        }

        [Fact]
        public void Should_reduce_multiplication_self_squared()
        {
            // arrange
            var operand = Multiplication.Create(Variable.x, Variable.x);
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBeOfType<Power>();
            ((BinaryOperation)actual).Left.ShouldBe(Variable.x);
            ((BinaryOperation)actual).Right.ShouldBe(ValueConstant.Create(2));
        }

        [Fact]
        public void Should_reduce_multiplication_flattened()
        {
            // arrange
            var operand = Multiplication.Create(Sine.Create(Variable.x), Multiplication.Create(ValueConstant.Create(6), Sine.Create(Variable.x)));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = (Multiplication)operand.Accept(visitor);

            // assert
            actual.Operands.ShouldBe(new [] {
                ValueConstant.Create(6), Power.Create(Sine.Create(Variable.x), 2) });
        }

        [Fact]
        public void Should_reduce_subtraction_to_zero()
        {
            // arrange
            var operand = Subtraction.Create(Variable.x, Variable.x);
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(ValueConstant.Zero);
        }

        [Fact]
        public void Should_reduce_division()
        {
            // arrange
            var operand = Division.Create(ValueConstant.Create(6), ValueConstant.Create(2));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(ValueConstant.Create(3));
        }

        [Fact]
        public void Should_reduce_division_to_constant()
        {
            // arrange
            var operand = Division.Create(ValueConstant.Create(6), ValueConstant.Create(2));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(ValueConstant.Create(3));
        }

        [Fact]
        public void Should_reduce_division_to_one_division_numerator_equal_denominator()
        {
            // arrange
            var operand = Division.Create(Sine.Create(Variable.x), Sine.Create(Variable.x));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(ValueConstant.One);
        }

        [Fact]
        public void Should_reduce_power()
        {
            // arrange
            var operand = Power.Create(ValueConstant.Create(6), ValueConstant.Create(2));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(ValueConstant.Create(36));
        }

        [Fact]
        public void Should_reduce_minimum()
        {
            // arrange
            var operand = Minimum.Create(ValueConstant.Create(6), ValueConstant.Create(2));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(ValueConstant.Create(2));
        }

        [Fact]
        public void Should_reduce_maximum()
        {
            // arrange
            var operand = Maximum.Create(ValueConstant.Create(6), ValueConstant.Create(2));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(ValueConstant.Create(6));
        }

        [Fact]
        public void Should_reduce_fraction_with_gcd()
        {
            // arrange
            var operand = (Fraction)Fraction.Create(81, 36);
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = (Fraction)operand.Accept(visitor);

            // assert
            actual.Numerator.ShouldBe(9);
            actual.Denominator.ShouldBe(4);
        }
    }
}