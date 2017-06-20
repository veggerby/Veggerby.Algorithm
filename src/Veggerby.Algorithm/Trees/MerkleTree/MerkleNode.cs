using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Veggerby.Algorithm.Trees.MerkleTree.Visitors;

namespace Veggerby.Algorithm.Trees.MerkleTree
{
    public class MerkleNode<T> : AbstractBinaryNode<MerkleHash<T>, IMerkleNode<T>>, IMerkleNode<T>
    {
        public void Accept(IMerkleVisitor<T> visitor)
        {
            visitor.Visit(this);
        }

        public MerkleNode(MerkleHash<T> hash, IMerkleNode<T> left, IMerkleNode<T> right = null) : base(hash, left, right)
        {
            if (hash is MerkleChunk<T>)
            {
                throw new ArgumentException("Cannot add chunck on a branch node", nameof(hash));
            }

            if (left == null) 
            {
                throw new ArgumentNullException(nameof(left), "Branch node must have child node(s)");
            }
        }

        public MerkleNode(MerkleChunk<T> chunk) : base(chunk, null, null)
        {
        }
    }
}