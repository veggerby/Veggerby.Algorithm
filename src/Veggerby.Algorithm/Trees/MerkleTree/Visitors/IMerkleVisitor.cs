namespace Veggerby.Algorithm.Trees.MerkleTree.Visitors
{
    public interface IMerkleVisitor<T>
    {
        void Visit(MerkleNode<T> node);
    }
}