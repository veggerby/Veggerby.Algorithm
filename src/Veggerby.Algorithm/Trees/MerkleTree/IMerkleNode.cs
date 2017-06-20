using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Veggerby.Algorithm.Trees.MerkleTree
{
    public interface IMerkleNode<T> : IBinaryNode<MerkleHash<T>, IMerkleNode<T>>, INode<MerkleHash<T>>, IChildNode<IMerkleNode<T>>, IParentNode
    {
        byte[] Hash { get; }
        T Chunk { get; }
    }
}