using System;
using System.Linq;
using System.Security.Cryptography;

namespace Veggerby.Algorithm.Trees.MerkleTree.Visitors
{
    public abstract class CalculateHashVisitor<T> : IMerkleVisitor<T>
    {
        private readonly Func<T, byte[]> _bufferFunc = null;
        private readonly HashAlgorithm _hashAlgorithm = null;

        public byte[] Result { get; private set; }
    
        public void Visit(MerkleNode<T> node) 
        {
            if (node.Payload is MerkleChunk<T>)
            {
                var buffer = _bufferFunc(((MerkleChunk<T>)node.Payload).Chunk);
                Result = _hashAlgorithm.ComputeHash(buffer);
                return;
            }

            var leftHash = node.Left?.Payload?.Hash ?? Enumerable.Empty<byte>();
            var rightHash = node.Right?.Payload?.Hash ?? Enumerable.Empty<byte>();
            var combinedHash = leftHash.Concat(rightHash).ToArray();
            Result = _hashAlgorithm.ComputeHash(combinedHash);
        }

        protected abstract HashAlgorithm CreateAlgorithm();

        public CalculateHashVisitor(Func<T, byte[]> bufferFunc) 
        {
            _hashAlgorithm = CreateAlgorithm();
            _bufferFunc = bufferFunc;
        }
    }
}