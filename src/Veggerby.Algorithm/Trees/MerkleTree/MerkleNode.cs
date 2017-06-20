using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Veggerby.Algorithm.Trees.MerkleTree
{
    public class MerkleNode<T> : AbstractBinaryNode<MerkleHash<T>, IMerkleNode<T>>, IMerkleNode<T>
    {
        public static HashAlgorithm DefaultHashAlgorithm = SHA256.Create();

        public byte[] Hash => Payload?.Hash;

        public T Chunk => HasChunk ? (Payload as MerkleChunk<T>).Chunk : default(T);

        public bool HasChunk => Payload is MerkleChunk<T>;

        private MerkleNode(MerkleHash<T> hash, IMerkleNode<T> left, IMerkleNode<T> right) : base(hash, left, right)
        {
        }

        public static IMerkleNode<T> Create(IMerkleNode<T> left, IMerkleNode<T> right)
        {
            if (left == null && right == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (left == null)
            {
                left = right;
                right = null;
            }

            var leftHash = left?.Payload?.Hash ?? Enumerable.Empty<byte>();
            var rightHash = right?.Payload?.Hash ?? Enumerable.Empty<byte>();
            var combinedHash = leftHash.Concat(rightHash).ToArray();
            var merkleHash = new MerkleHash<T>(combinedHash);
            return new MerkleNode<T>(merkleHash, left, right);
        }

        public static IMerkleNode<T> Create(T chunk, Func<T, byte[]> bufferFunc, HashAlgorithm hashAlgorithm = null)
        {            
            var buffer = bufferFunc(chunk);
            var hash = (hashAlgorithm ?? DefaultHashAlgorithm).ComputeHash(buffer);
            var merkleChunk = new MerkleChunk<T>(chunk, hash);
            return new MerkleNode<T>(merkleChunk, null, null);
        }

        public static IMerkleNode<string> Create(string chunk, Encoding encoding = null, HashAlgorithm hashAlgorithm = null)
        {            
            return MerkleNode<string>.Create(chunk, (encoding ?? Encoding.UTF8).GetBytes, hashAlgorithm);
        }
    }
}