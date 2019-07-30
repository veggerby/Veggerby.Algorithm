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

        public override bool IsLeaf => !Children.Any();

        public int Height => IsLeaf ? 0 : Math.Max(Left.Height, Right.Height) + 1;

        public int TotalNodeCount => (Left?.TotalNodeCount ?? 0) + (Right?.TotalNodeCount ?? 0) + 1;

        public int LeafNodeCount => IsLeaf ? 1 : (Left?.LeafNodeCount ?? 0) + (Right?.LeafNodeCount ?? 0);

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

        public bool Equals(AbstractBinaryNode<T, TNode> other) => other != null && Left.Equals(other.Left) && Right.Equals(other.Right);
        public override bool Equals(object obj) => Equals(obj as AbstractBinaryNode<T, TNode>);
        public override int GetHashCode() => base.GetHashCode() ^ (Left?.GetHashCode() ?? 0) ^ (Right?.GetHashCode() ?? 0);

        public AbstractBinaryNode(T payload = default(T), TNode left = default(TNode), TNode right = default(TNode)) : base(payload)
        {
            SetLeft(left);
            SetRight(right);
        }
    }
}