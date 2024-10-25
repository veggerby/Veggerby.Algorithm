namespace Veggerby.Algorithm.Trees;

public interface IChildNode<TParent> where TParent : IParentNode
{
    bool IsLeaf { get; }
}