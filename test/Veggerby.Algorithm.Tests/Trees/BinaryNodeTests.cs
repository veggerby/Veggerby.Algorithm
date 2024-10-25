using FluentAssertions;

using Veggerby.Algorithm.Trees;

using Xunit;

namespace Veggerby.Algorithm.Tests.Trees;

public class BinaryNodeTests
{
    [Fact]
    public void Should_initialize_binary_node_simple()
    {
        // arrange
        // act
        var actual = new BinaryNode<string>("this");

        // assert
        actual.Should().NotBeNull();
        actual.Left.Should().BeNull();
        actual.Right.Should().BeNull();
        actual.Payload.Should().Be("this");
        actual.IsLeaf.Should().BeTrue();
        actual.Height.Should().Be(0);
    }

    [Fact]
    public void Should_initialize_binary_node_with_children()
    {
        // arrange
        var left = new BinaryNode<string>("left");
        var right = new BinaryNode<string>("right");

        // act
        var actual = new BinaryNode<string>("this", left, right);

        // assert
        actual.Should().NotBeNull();
        actual.Left.Should().BeEquivalentTo(left);
        actual.Right.Should().BeEquivalentTo(right);
        actual.Children.Should().Contain(left as IChildNode<IParentNode>);
        actual.Children.Should().Contain(right as IChildNode<IParentNode>);
        actual.Payload.Should().Be("this");
        actual.IsLeaf.Should().BeFalse();
        actual.Left.IsLeaf.Should().BeTrue();
        actual.Right.IsLeaf.Should().BeTrue();
        actual.Height.Should().Be(1);
        actual.Left.Height.Should().Be(0);
        actual.Right.Height.Should().Be(0);
    }
}