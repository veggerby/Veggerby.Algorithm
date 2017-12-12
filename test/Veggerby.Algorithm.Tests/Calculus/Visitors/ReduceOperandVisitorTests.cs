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
            var operand = Addition.Create(Constant.One, Constant.Create(3));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(Constant.Create(4));
        }

        [Fact]
        public void Should_reduce_negative_constant()
        {
            // arrange
            var operand = Addition.Create(Variable.x, Negative.Create(Constant.Create(3)));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBeOfType<Subtraction>();
            ((Subtraction)actual).Left.ShouldBe(Variable.x);
            ((Subtraction)actual).Right.ShouldBe(Constant.Create(3));
        }

        [Fact]
        public void Should_reduce_addition_flattened()
        {
            // arrange
            var operand = Addition.Create(
                Addition.Create(Constant.Create(7), Sine.Create(Variable.x)),
                Addition.Create(Constant.Create(3), Sine.Create(Variable.x)));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = (Addition)operand.Accept(visitor);

            // assert
            actual.Left.ShouldBe(Constant.Create(10));
            actual.Right.ShouldBe(Multiplication.Create(2, Sine.Create(Variable.x)));
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
            actual.ShouldBe(Constant.Create(-1101));
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
            ((BinaryOperation)actual).Left.ShouldBe(Constant.Create(2));
            ((BinaryOperation)actual).Right.ShouldBe(Variable.x);
        }

        [Fact]
        public void Should_reduce_subtraction()
        {
            // arrange
            var operand = Subtraction.Create(Constant.Create(6), Constant.Create(2));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(Constant.Create(4));
        }

        [Fact]
        public void Should_reduce_multiplication()
        {
            // arrange
            var operand = Multiplication.Create(Constant.Create(6), Constant.Create(2));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(Constant.Create(12));
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
            ((BinaryOperation)actual).Right.ShouldBe(Constant.Create(2));
        }

        [Fact]
        public void Should_reduce_multiplication_flattened()
        {
            // arrange
            var operand = Multiplication.Create(Sine.Create(Variable.x), Multiplication.Create(Constant.Create(6), Sine.Create(Variable.x)));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = (Multiplication)operand.Accept(visitor);

            // assert
            actual.Left.ShouldBe(Constant.Create(6));
            actual.Right.ShouldBe(Power.Create(Sine.Create(Variable.x), 2));
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
            actual.ShouldBe(Constant.Zero);
        }

        [Fact]
        public void Should_reduce_division()
        {
            // arrange
            var operand = Division.Create(Constant.Create(6), Constant.Create(2));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(Constant.Create(3));
        }

        [Fact]
        public void Should_reduce_division_to_constant()
        {
            // arrange
            var operand = Division.Create(Constant.Create(6), Constant.Create(2));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(Constant.Create(3));
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
            actual.ShouldBe(Constant.One);
        }

        [Fact]
        public void Should_reduce_power()
        {
            // arrange
            var operand = Power.Create(Constant.Create(6), Constant.Create(2));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(Constant.Create(36));
        }

        [Fact]
        public void Should_reduce_minimum()
        {
            // arrange
            var operand = Minimum.Create(Constant.Create(6), Constant.Create(2));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(Constant.Create(2));
        }

        [Fact]
        public void Should_reduce_maximum()
        {
            // arrange
            var operand = Maximum.Create(Constant.Create(6), Constant.Create(2));
            var visitor = new ReduceOperandVisitor();

            // act
            var actual = operand.Accept(visitor);

            // assert
            actual.ShouldBe(Constant.Create(6));
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