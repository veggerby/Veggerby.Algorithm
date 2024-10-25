namespace Veggerby.Algorithm.Trees;

public abstract class AbstractNode<T, TParent> : INode<T>, IChildNode<TParent> where TParent : IParentNode
{
    private T _payload;

    public T Payload
    {
        get { return _payload; }
        set { SetPayload(value); }
    }

    public virtual bool IsLeaf => true;

    protected virtual void SetPayload(T payload)
    {
        _payload = payload;
    }


    public bool Equals(AbstractNode<T, TParent> other) =>
        other is not null &&
        Equals(other.Payload);

    public override bool Equals(object obj) => Equals(obj as AbstractNode<T, TParent>);
    public override int GetHashCode() => Payload?.GetHashCode() ?? 0;

    public bool Equals(INode<T> other) => Equals(other as AbstractNode<T, TParent>);

    public bool Equals(T other) => (Payload is null && other is null) || (Payload?.Equals(other) ?? false);

    public AbstractNode(T payload = default)
    {
        SetPayload(payload);
    }
}