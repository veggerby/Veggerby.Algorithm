namespace Veggerby.Algorithm.Trees;

public interface IBinaryNode<T, TNode> : INode<T>, IParentNode, IChildNode<TNode> where TNode : IBinaryNode<T, TNode>
{
    TNode Left { get; set; }
    TNode Right { get; set; }
    int Height { get; }
    int TotalNodeCount { get; }
    int LeafNodeCount { get; }
}