using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Veggerby.Algorithm.Trees.MerkleTree.Visitors;

namespace Veggerby.Algorithm.Trees.MerkleTree
{
    public interface IMerkleNode<T> : IBinaryNode<MerkleHash<T>, IMerkleNode<T>>, INode<MerkleHash<T>>, IChildNode<IMerkleNode<T>>, IParentNode
    {
        void Accept(IMerkleVisitor<T> visitor); 
    }
}