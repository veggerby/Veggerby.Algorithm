using System;
using System.Linq;
using System.Text;
using Shouldly;
using Veggerby.Algorithm.Trees.MerkleTree;
using Xunit;

namespace Veggerby.Algorithm.Tests.Trees.MerkleTree
{
    public class MerkleNodeTests
    {
        [Fact]
        public void Should_initialize_merkle_node_simple()
        {
            // arrange
            // act
            var actual = MerkleNode<string>.Create("test");
            var expectedHash = "9F-86-D0-81-88-4C-7D-65-9A-2F-EA-A0-C5-5A-D0-15-A3-BF-4F-1B-2B-0B-82-2C-D1-5D-6C-15-B0-F0-0A-08";

            // assert
            actual.ShouldNotBeNull();
            actual.Left.ShouldBeNull();
            actual.Right.ShouldBeNull();
            actual.Chunk.ShouldBe("test");

            var actualHash = BitConverter.ToString(actual.Hash);
            actualHash.ShouldBe(expectedHash);
            actual.Parent.ShouldBeNull();
            actual.IsLeaf.ShouldBeTrue();
            actual.Height.ShouldBe(0);
        }

        [Fact]
        public void Should_initialize_merkle_node_branch()
        {
            // arrange
            var left = MerkleNode<string>.Create("left");
            var right = MerkleNode<string>.Create("right");
            var expectedLeftHash = "36-0F-84-03-59-42-24-3C-6A-36-53-7A-E2-F8-67-34-85-E6-C0-44-55-A0-A8-5A-0D-B1-96-90-F2-54-14-80";
            var expectedRightHash = "27-04-2F-4E-6E-CA-7D-0B-2A-7E-E4-02-6D-F2-EC-FA-51-D3-33-9E-6D-12-2A-A0-99-11-8E-CD-85-63-BA-D9";

            // act
            var actual = MerkleNode<string>.Create(left, right);
            var expectedHash = expectedLeftHash + "-" + expectedRightHash;

            // assert
            actual.ShouldNotBeNull();
            actual.Left.ShouldBe(left);
            actual.Right.ShouldBe(right);
            actual.Chunk.ShouldBeNull();

            var actualHash = BitConverter.ToString(actual.Hash);
            actualHash.ShouldBe(expectedHash);
            actual.Parent.ShouldBeNull();
            actual.IsLeaf.ShouldBeFalse();
            actual.Left.IsLeaf.ShouldBeTrue();
            actual.Right.IsLeaf.ShouldBeTrue();
            actual.Height.ShouldBe(1);
            actual.Left.Height.ShouldBe(0);
            actual.Right.Height.ShouldBe(0);
        }


        [Fact]
        public void Should_initialize_merkle_tree()
        {
            // arrange
            var list = Enumerable.Range(0, 7).Select(x => MerkleNode<string>.Create($"node-{x}")).ToArray();
            var n0 = list[0];
            var n1 = list[1];
            var n2 = list[2];
            var n3 = list[3];
            var n4 = list[4];
            var n5 = list[5];
            var n6 = list[6];

            var n01 = MerkleNode<string>.Create(n0, n1);
            var n23 = MerkleNode<string>.Create(n2, n3);
            var n45 = MerkleNode<string>.Create(n4, n5);
            var n0123 = MerkleNode<string>.Create(n01, n23);
            var n012345 = MerkleNode<string>.Create(n0123, n45);

            // act
            var actual = MerkleNode<string>.Create(n012345, n6);

            // assert
            actual.Height.ShouldBe(4);
            actual.LeafNodeCount.ShouldBe(7);
            actual.TotalNodeCount.ShouldBe(13);
            n01.Hash.ShouldBe(n0.Hash.Concat(n1.Hash));
            n23.Hash.ShouldBe(n2.Hash.Concat(n3.Hash));
            n45.Hash.ShouldBe(n4.Hash.Concat(n5.Hash));
            n0123.Hash.ShouldBe(n0.Hash.Concat(n1.Hash).Concat(n2.Hash).Concat(n3.Hash));
            n012345.Hash.ShouldBe(n0.Hash.Concat(n1.Hash).Concat(n2.Hash).Concat(n3.Hash).Concat(n4.Hash).Concat(n5.Hash));
            actual.Hash.ShouldBe(n0.Hash.Concat(n1.Hash).Concat(n2.Hash).Concat(n3.Hash).Concat(n4.Hash).Concat(n5.Hash).Concat(n6.Hash));
        }

        [Fact]
        public void Should_initialize_merkle_node_from_list()
        {
            // arrange
            var list = Enumerable.Range(0, 13).Select(x => $"node-{x}").ToList();

            // act
            var actual = MerkleNode<string>.Create(list, Encoding.UTF8.GetBytes);

            // assert
            actual.Height.ShouldBe(4);
            actual.LeafNodeCount.ShouldBe(13);
        }
    }
}