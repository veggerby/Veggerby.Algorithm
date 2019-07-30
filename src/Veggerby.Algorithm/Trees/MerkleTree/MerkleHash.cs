using System.Linq;

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

        public bool Equals(MerkleHash<T> other) => _isValid && other._isValid && Hash != null && other.Hash != null && Hash.SequenceEqual(other.Hash);
        public override bool Equals(object obj) => Equals(obj as MerkleHash<T>);
        public override int GetHashCode() => _isValid.GetHashCode() ^ Hash?.GetHashCode() ?? 0;

        public MerkleHash(byte[] hash)
        {
            SetHash(hash);
        }
    }
}