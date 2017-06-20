using System.Text;
using Shouldly;
using Veggerby.Algorithm.Trees;
using Veggerby.Algorithm.Trees.MerkleTree;
using Veggerby.Algorithm.Trees.MerkleTree.Visitors;
using Xunit;

namespace Veggerby.Algorithm.Tests.Trees.MerkleTree
{
    public class MerkleNodeTests
    {
        [Fact]
        public void Should_initialize_merkle_node_simple()
        {
            // arrange
            var payload = "test";
            var hash = new byte[] { 0, 1, 2 };
            var chunk = new MerkleChunk<string>(payload, hash);

            // act 
            var actual = new MerkleNode<string>(chunk);

            // assert
            actual.ShouldNotBeNull();
            actual.Left.ShouldBeNull();
            actual.Right.ShouldBeNull();
            actual.Payload.ShouldBe(chunk);
            actual.Parent.ShouldBeNull();
        }
    }
}