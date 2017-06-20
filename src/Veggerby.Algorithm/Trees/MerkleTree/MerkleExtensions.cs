using System.Text;
using Veggerby.Algorithm.Trees.MerkleTree.Visitors;

namespace Veggerby.Algorithm.Trees.MerkleTree
{
    public static class MerkleExtensions
    {
        public static void Recalculate<T>(this IMerkleNode<T> node, CalculateHashVisitor<T> hashCalculator)
        {
            node.Accept(hashCalculator);
            var hash = hashCalculator.Result;
            node.Payload.Hash = hash;
            node.Parent.Recalculate(hashCalculator);
        }

        public static void Recalculate(this IMerkleNode<string> node, Encoding encoding = null)
        {
            var visitor = new CalculateSha256HashVisitor<string>(x => (encoding ?? Encoding.UTF8).GetBytes(x));
            node.Recalculate(visitor);        
        }
    }
}