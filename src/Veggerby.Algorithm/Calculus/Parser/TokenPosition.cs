namespace Veggerby.Algorithm.Calculus.Parser
{
    public class TokenPosition
    {
        public TokenPosition(int index, int line, int column)
        {
            Index = index;
            Line = line;
            Column = column;
        }

        public int Index { get; }
        public int Line { get; }
        public int Column { get; }


        protected bool Equals(TokenPosition other)
        {
            return Index == other.Index && Line == other.Line && Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TokenPosition)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Index.GetHashCode();
                hashCode = (hashCode*397) ^ Line.GetHashCode();
                hashCode = (hashCode*397) ^ Column.GetHashCode();
                return hashCode;
            }
        }
    }
}