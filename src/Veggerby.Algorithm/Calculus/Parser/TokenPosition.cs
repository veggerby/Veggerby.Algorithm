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

        protected bool Equals(TokenPosition other) => other != null && Index == other.Index && Line == other.Line && Column == other.Column;
        public override bool Equals(object obj) => Equals(obj as TokenPosition);

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