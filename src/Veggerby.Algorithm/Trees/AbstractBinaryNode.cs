using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Trees
{
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
                if (Left != null)
                {
                    yield return Left as IChildNode<IParentNode>;
                }

                if (Right != null) 
                {
                    yield return Right as IChildNode<IParentNode>;
                }
            }
        }        

        protected void SetLeft(TNode left)
        {
            _left = left;
            _left?.SetParent(this);
        }

        protected void SetRight(TNode right)
        {
            _right = right;
            _right?.SetParent(this);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AbstractBinaryNode<T, TNode>)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ (Left?.GetHashCode() ?? 0) ^ (Right?.GetHashCode() ?? 0);
        }

        public AbstractBinaryNode(T payload = default(T), TNode left = default(TNode), TNode right = default(TNode)) : base(payload)
        {
            SetLeft(left);
            SetRight(right);
        }
    }
}