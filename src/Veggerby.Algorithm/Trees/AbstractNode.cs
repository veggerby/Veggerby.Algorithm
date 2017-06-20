using System;
using System.Collections.Generic;
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AbstractNode<T, TParent>)obj);
        }

        public override int GetHashCode()
        {
            return Parent?.GetHashCode() ?? 0 ^ Payload?.GetHashCode() ?? 0;
        }

        public AbstractNode(T payload = default(T))
        {
            SetPayload(payload);
            SetParent(default(TParent));
        }
    }
}