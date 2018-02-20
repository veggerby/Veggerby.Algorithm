using System;
using System.Linq;
using Shouldly;
using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;
using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors
{
    public class CommutativeOperationComparerTests
    {
        [Fact]
        public void Should_equal_two_constants()
        {
            // arrange
            var comparer = new CommutativeOperationComparer();

            // act
            var actual = comparer.Compare(Constant.One, Constant.Zero);

            // assert
            actual.ShouldBe(0);
        }

        [Fact]
        public void Should_keep_order_constant_and_variable()
        {
            // arrange
            var comparer = new CommutativeOperationComparer();

            // act
            var actual = comparer.Compare(Constant.Zero, Variable.x);

            // assert
            actual.ShouldBe(-1);
        }

        [Fact]
        public void Should_swap_variable_and_constant()
        {
            // arrange
            var comparer = new CommutativeOperationComparer();

            // act
            var actual = comparer.Compare(Variable.x, Constant.Zero);

            // assert
            actual.ShouldBe(1);
        }

        [Fact]
        public void Should_swap_unary_and_variable()
        {
            // arrange
            var comparer = new CommutativeOperationComparer();

            // act
            var actual = comparer.Compare(Sine.Create(Variable.x), Variable.x);

            // assert
            actual.ShouldBe(1);
        }

        [Fact]
        public void Should_keep_variable_and_unary()
        {
            // arrange
            var comparer = new CommutativeOperationComparer();

            // act
            var actual = comparer.Compare(Variable.x, Sine.Create(Variable.x));

            // assert
            actual.ShouldBe(-1);
        }

        [Fact]
        public void Should_keep_variable_and_binary()
        {
            // arrange
            var comparer = new CommutativeOperationComparer();

            // act
            var actual = comparer.Compare(Variable.x, Multiplication.Create(Variable.x, Constant.Pi));

            // assert
            actual.ShouldBe(-1);
        }

        [Fact]
        public void Should_swap_binary_and_variable()
        {
            // arrange
            var comparer = new CommutativeOperationComparer();

            // act
            var actual = comparer.Compare(Multiplication.Create(Variable.x, Constant.Pi), Variable.x);

            // assert
            actual.ShouldBe(1);
        }

        [Fact]
        public void Should_order_based_on_priority()
        {
            // arrange
            var comparer = new CommutativeOperationComparer();
            var items = new []
            {
                Factorial.Create(Variable.x),
                Division.Create(Constant.One, Variable.x),
                Subtraction.Create(Variable.x, Constant.One),
                Multiplication.Create(Constant.Create(2), Variable.x),
                Power.Create(Constant.Create(2), Variable.x),
                Addition.Create(Constant.Create(10), Variable.x)
            };

            // act
            var actual = items.OrderBy(x => x, comparer).ToArray();

            // assert
            actual[0].ShouldBeOfType<Addition>();
            actual[1].ShouldBeOfType<Multiplication>(); // due to complexity is lower than subtraction
            actual[2].ShouldBeOfType<Subtraction>();
            actual[3].ShouldBeOfType<Division>();
            actual[4].ShouldBeOfType<Power>();
            actual[5].ShouldBeOfType<Factorial>();
        }


        [Fact]
        public void Should_order()
        {
            // arrange
            var comparer = new CommutativeOperationComparer();
            var items = new []
            {
                Sine.Create(Variable.x),
                Constant.One,
                Factorial.Create(Variable.x),
                Division.Create(Constant.One, Variable.x),
                Subtraction.Create(Variable.x, Constant.One),
                Variable.x,
                Minimum.Create(Variable.x, Constant.Pi),
                Multiplication.Create(Constant.Pi, Variable.x),
                Power.Create(Constant.Create(2), Variable.x),
                Addition.Create(Constant.Create(10), Variable.x)
            };

            // act
            var actual = items.OrderBy(x => x, comparer).ToArray();

            // assert
            actual[0].ShouldBeOfType<Constant>();
            actual[1].ShouldBeOfType<Variable>();
            actual[2].ShouldBeOfType<Addition>();
            actual[3].ShouldBeOfType<Multiplication>();
            actual[4].ShouldBeOfType<Sine>();
            actual[5].ShouldBeOfType<Subtraction>();
            actual[6].ShouldBeOfType<Division>();
            actual[7].ShouldBeOfType<Power>();
            actual[8].ShouldBeOfType<Factorial>();
            actual[9].ShouldBeOfType<Minimum>();
        }
/*
        public int Compare(Operand x, Operand y)

            var xPrio = x.GetPriority();
            var yPrio = y.GetPriority();

            if (xPrio != null && yPrio != null)
            {
                return -xPrio.Value.CompareTo(yPrio.Value); // opposite priority order, i.e. add first, factorial last
            }

            if (x is BinaryOperation && y is UnaryOperation)
            {
                return -1;
            }

            if (x is UnaryOperation && y is BinaryOperation)
            {
                return 1;
            }

            return 0;
        } */
    }
}