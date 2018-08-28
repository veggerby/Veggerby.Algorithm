using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Veggerby.Algorithm.Trees.MerkleTree
{
    public class MerkleHash<T>
    {
        private byte[] _value;
        private bool _isValid = false;

        public byte[] Hash
        {
            get { return _value; }
            set { SetHash(value); }
        }

        protected virtual void SetHash(byte[] hash)
        {
            _value = hash;
            _isValid = hash != null && hash.Any();
        }

        public void SetInvalid()
        {
            _isValid = false;
        }

        public bool Equals(MerkleHash<T> other)
        {
            return _isValid && other._isValid && Hash != null && other.Hash != null && Hash.SequenceEqual(other.Hash);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MerkleHash<T>)obj);
        }

        public override int GetHashCode()
        {
            return _isValid.GetHashCode() ^ Hash?.GetHashCode() ?? 0;
        }

        public MerkleHash(byte[] hash)
        {
            SetHash(hash);
        }
    }
}