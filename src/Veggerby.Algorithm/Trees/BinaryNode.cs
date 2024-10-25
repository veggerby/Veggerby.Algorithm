namespace Veggerby.Algorithm.Trees;

public class BinaryNode<T>(T payload = default, BinaryNode<T> left = null, BinaryNode<T> right = null) : AbstractBinaryNode<T, BinaryNode<T>>(payload, left, right)
{
}