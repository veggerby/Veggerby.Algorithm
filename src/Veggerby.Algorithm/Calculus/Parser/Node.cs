namespace Veggerby.Algorithm.Calculus.Parser
{
    public class Node
    {
        public Token Token { get; }

        public Node(Token token)
        {
            Token = token;
        }

        public override string ToString() => Token.ToString();
    }
}