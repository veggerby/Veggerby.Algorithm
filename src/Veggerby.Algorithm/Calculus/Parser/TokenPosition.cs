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
    }
}