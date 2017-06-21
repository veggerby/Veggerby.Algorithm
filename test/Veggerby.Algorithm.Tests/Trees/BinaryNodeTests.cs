using Shouldly;
using Veggerby.Algorithm.Trees;
using Xunit;

namespace Veggerby.Algorithm.Tests.Trees
{
    public class BinaryNodeTests
    {
        [Fact]
        public void Should_initialize_binary_node_simple()
        {
            // arrange
            // act
            var actual = new BinaryNode<string>("this");

            // assert
            actual.ShouldNotBeNull();
            actual.Left.ShouldBeNull();
            actual.Right.ShouldBeNull();
            actual.Payload.ShouldBe("this");
            actual.Parent.ShouldBeNull();
            actual.IsLeaf.ShouldBeTrue();
            actual.Height.ShouldBe(0);
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
            actual.ShouldNotBeNull();
            actual.Left.ShouldBe(left);
            actual.Right.ShouldBe(right);
            actual.Left.Parent.ShouldBe(actual);
            actual.Right.Parent.ShouldBe(actual);
            actual.Children.ShouldContain(left as IChildNode<IParentNode>);
            actual.Children.ShouldContain(right as IChildNode<IParentNode>);
            actual.Payload.ShouldBe("this");
            actual.IsLeaf.ShouldBeFalse();
            actual.Left.IsLeaf.ShouldBeTrue();
            actual.Right.IsLeaf.ShouldBeTrue();
            actual.Height.ShouldBe(1);
            actual.Left.Height.ShouldBe(0);
            actual.Right.Height.ShouldBe(0);
        }
    }
}