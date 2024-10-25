using System.Linq;

using FluentAssertions;

using Veggerby.Algorithm.Calculus;
using Veggerby.Algorithm.Calculus.Visitors;

using Xunit;

namespace Veggerby.Algorithm.Tests.Calculus.Visitors;

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
        actual.Should().Be(0);
    }

    [Fact]
    public void Should_keep_order_constant_and_variable()
    {
        // arrange
        var comparer = new CommutativeOperationComparer();

        // act
        var actual = comparer.Compare(Constant.Zero, Variable.x);

        // assert
        actual.Should().Be(-1);
    }

    [Fact]
    public void Should_swap_variable_and_constant()
    {
        // arrange
        var comparer = new CommutativeOperationComparer();

        // act
        var actual = comparer.Compare(Variable.x, Constant.Zero);

        // assert
        actual.Should().Be(1);
    }

    [Fact]
    public void Should_swap_unary_and_variable()
    {
        // arrange
        var comparer = new CommutativeOperationComparer();

        // act
        var actual = comparer.Compare(Sine.Create(Variable.x), Variable.x);

        // assert
        actual.Should().Be(1);
    }

    [Fact]
    public void Should_keep_variable_and_unary()
    {
        // arrange
        var comparer = new CommutativeOperationComparer();

        // act
        var actual = comparer.Compare(Variable.x, Sine.Create(Variable.x));

        // assert
        actual.Should().Be(-1);
    }

    [Fact]
    public void Should_keep_variable_and_binary()
    {
        // arrange
        var comparer = new CommutativeOperationComparer();

        // act
        var actual = comparer.Compare(Variable.x, Multiplication.Create(Variable.x, Constant.Pi));

        // assert
        actual.Should().Be(-1);
    }

    [Fact]
    public void Should_swap_binary_and_variable()
    {
        // arrange
        var comparer = new CommutativeOperationComparer();

        // act
        var actual = comparer.Compare(Multiplication.Create(Variable.x, Constant.Pi), Variable.x);

        // assert
        actual.Should().Be(1);
    }

    [Fact]
    public void Should_order_based_on_priority()
    {
        // arrange
        var comparer = new CommutativeOperationComparer();
        var items = new[]
        {
            Factorial.Create(Variable.x),
            Division.Create(Constant.One, Variable.x),
            Subtraction.Create(Variable.x, Constant.One),
            Multiplication.Create(ValueConstant.Create(2), Variable.x),
            Power.Create(ValueConstant.Create(2), Variable.x),
            Addition.Create(ValueConstant.Create(10), Variable.x)
        };

        // act
        var actual = items.OrderBy(x => x, comparer).ToArray();

        // assert
        actual[0].Should().BeOfType<Addition>();
        actual[1].Should().BeOfType<Multiplication>(); // due to complexity is lower than subtraction
        actual[2].Should().BeOfType<Subtraction>();
        actual[3].Should().BeOfType<Division>();
        actual[4].Should().BeOfType<Power>();
        actual[5].Should().BeOfType<Factorial>();
    }


    [Fact]
    public void Should_order()
    {
        // arrange
        var comparer = new CommutativeOperationComparer();
        var items = new[]
        {
            Sine.Create(Variable.x),
            Constant.One,
            Factorial.Create(Variable.x),
            Division.Create(Constant.One, Variable.x),
            Subtraction.Create(Variable.x, Constant.One),
            Variable.x,
            Minimum.Create(Variable.x, Constant.Pi),
            Multiplication.Create(Constant.Pi, Variable.x),
            Power.Create(ValueConstant.Create(2), Variable.x),
            Addition.Create(ValueConstant.Create(10), Variable.x)
        };

        // act
        var actual = items.OrderBy(x => x, comparer).ToArray();

        // assert
        actual[0].Should().BeOfType<ValueConstant>();
        actual[1].Should().BeOfType<Variable>();
        actual[2].Should().BeOfType<Addition>();
        actual[3].Should().BeOfType<Multiplication>();
        actual[4].Should().BeOfType<Sine>();
        actual[5].Should().BeOfType<Subtraction>();
        actual[6].Should().BeOfType<Division>();
        actual[7].Should().BeOfType<Power>();
        actual[8].Should().BeOfType<Factorial>();
        actual[9].Should().BeOfType<Minimum>();
    }
    /*
            public int Compare(Operand x, Operand y)

                var xPrio = x.GetPriority();
                var yPrio = y.GetPriority();

                if (xPrio is not null && yPrio is not null)
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