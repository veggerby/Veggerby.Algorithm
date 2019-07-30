using System;
using System.Linq;

namespace Veggerby.Algorithm.Trees
{
    public abstract class AbstractNode<T, TParent> : INode<T>, IChildNode<TParent> where TParent : IParentNode
    {
        private T _payload;
        private TParent _parent;

        public T Payload
        {
            get { return _payload; }
            set { SetPayload(value); }
        }

        public TParent Parent
        {
            get { return _parent; }
            set { SetParent(value); }
        }

        public virtual bool IsLeaf => true;

        protected virtual void SetPayload(T payload)
        {
            _payload = payload;
        }

        public virtual void SetParent(IParentNode parent)
        {
            if (parent != null && !(parent is TParent))
            {
                throw new ArgumentException(nameof(parent));
            }

            if (parent != null && !parent.Children.Contains(this as IChildNode<IParentNode>))
            {
                throw new IndexOutOfRangeException(nameof(parent));
            }

            _parent = (TParent)parent;
        }

        public bool Equals(AbstractNode<T, TParent> other) =>
            other != null &&
            ((Payload == null && other.Payload == null) || (Payload?.Equals(other.Payload) ?? false)) &&
            ((Parent == null && other.Parent == null) || (Parent?.Equals(other.Parent) ?? false));
        public override bool Equals(object obj) => Equals(obj as AbstractNode<T, TParent>);
        public override int GetHashCode() => Parent?.GetHashCode() ?? 0 ^ Payload?.GetHashCode() ?? 0;

        public AbstractNode(T payload = default(T))
        {
            SetPayload(payload);
            SetParent(default(TParent));
        }
    }
}