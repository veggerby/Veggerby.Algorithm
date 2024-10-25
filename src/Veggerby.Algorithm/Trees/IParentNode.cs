namespace Veggerby.Algorithm.Trees;

public interface IParentNode
{
    IEnumerable<IChildNode<IParentNode>> Children { get; }
}