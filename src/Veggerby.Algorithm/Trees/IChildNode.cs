using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Algorithm.Trees
{
    public interface IChildNode<TParent> where TParent : IParentNode
    {
        TParent Parent { get; }
        void SetParent(IParentNode parent);
        bool IsLeaf { get; }
    }
}