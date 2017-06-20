using System;
using System.Linq;
using System.Security.Cryptography;

namespace Veggerby.Algorithm.Trees.MerkleTree.Visitors
{
    public class CalculateSha256HashVisitor<T> : CalculateHashVisitor<T>
    {
        protected override HashAlgorithm CreateAlgorithm()
        {
            return SHA256.Create();
        }

        public CalculateSha256HashVisitor(Func<T, byte[]> bufferFunc) : base(bufferFunc)
        {            
        }
    }
}