namespace Veggerby.Algorithm.Calculus.Parser;

public class TokenPosition(int index, int line, int column)
{
    public int Index { get; } = index;
    public int Line { get; } = line;
    public int Column { get; } = column;

    protected bool Equals(TokenPosition other) => other is not null && Index == other.Index && Line == other.Line && Column == other.Column;
    public override bool Equals(object obj) => Equals(obj as TokenPosition);

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Index.GetHashCode();
            hashCode = (hashCode * 397) ^ Line.GetHashCode();
            hashCode = (hashCode * 397) ^ Column.GetHashCode();
            return hashCode;
        }
    }
}