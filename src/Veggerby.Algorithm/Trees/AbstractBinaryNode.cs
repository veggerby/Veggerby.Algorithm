namespace Veggerby.Algorithm.Trees;

public abstract class AbstractBinaryNode<T, TNode> : AbstractNode<T, TNode>, IBinaryNode<T, TNode> where TNode : IBinaryNode<T, TNode>
{
    private TNode _left;
    private TNode _right;

    public TNode Left
    {
        get { return _left; }
        set { SetLeft(value); }
    }

    public TNode Right
    {
        get { return _right; }
        set { SetRight(value); }
    }

    public IEnumerable<IChildNode<IParentNode>> Children
    {
        get
        {
            if (Left is not null)
            {
                yield return Left as IChildNode<IParentNode>;
            }

            if (Right is not null)
            {
                yield return Right as IChildNode<IParentNode>;
            }
        }
    }

    public override bool IsLeaf => !Children.Any();

    public int Height => IsLeaf ? 0 : Math.Max(Left.Height, Right.Height) + 1;

    public int TotalNodeCount => (Left?.TotalNodeCount ?? 0) + (Right?.TotalNodeCount ?? 0) + 1;

    public int LeafNodeCount => IsLeaf ? 1 : (Left?.LeafNodeCount ?? 0) + (Right?.LeafNodeCount ?? 0);

    protected void SetLeft(TNode left) => _left = left;

    protected void SetRight(TNode right) => _right = right;

    public bool Equals(AbstractBinaryNode<T, TNode> other) => Left?.Equals(other.Left) == true && Right?.Equals(other.Right) == true;
    public override bool Equals(object obj) => Equals(obj as AbstractBinaryNode<T, TNode>);
    public override int GetHashCode() => base.GetHashCode() ^ (Left?.GetHashCode() ?? 0) ^ (Right?.GetHashCode() ?? 0);

    public AbstractBinaryNode(T payload = default, TNode left = default, TNode right = default) : base(payload)
    {
        SetLeft(left);
        SetRight(right);
    }
}