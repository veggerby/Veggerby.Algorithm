namespace Veggerby.Algorithm.Trees;

public interface INode<T> : IEquatable<INode<T>>, IEquatable<T>
{
    T Payload { get; set; }
}